using System;
using System.Collections.Concurrent;
using System.Drawing;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Simulator
{
    //#region struct
    //public readonly struct MovedPerson(Point previousPosition, Point currentPosition, bool isInfected, bool isContagious, int health)
    //{
    //    public Point PreviousPosition => previousPosition;
    //    public Point CurrentPosition => currentPosition;
    //    public bool IsInfected => isInfected;
    //    public bool IsContagious => isContagious;
    //    public int Health => health;
    //}

    //public readonly struct WorldUpdate(IEnumerable<MovedPerson> movedPeople, IEnumerable<Point> diedPeople, int pplAlive, int pplInfected, int pplContagious)
    //{
    //    public IEnumerable<MovedPerson> MovedPeople => movedPeople;
    //    public IEnumerable<Point> DiedPeople => diedPeople;
    //    public int PplAlive => pplAlive;
    //    public int PplInfected => pplInfected;
    //    public int PplContagious => pplContagious;
    //}
    //#endregion

    public sealed record MovedPerson(Point PreviousPosition, Point CurrentPosition, bool IsInfected, bool IsContagious, float Health);

    public sealed record WorldUpdate(IEnumerable<MovedPerson> MovedPeople, IEnumerable<Point> DiedPeople, int PplAlive, int PplInfected, int PplContagious);

    public class World
    {
        const int MOVEDIRECTIONSLENGTH = 8;

        static readonly ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random());
        Dictionary<Point, Person> _people = new();
        private readonly SimulationConfig _config;
        private List<MovedPerson> _movedPeople = new();
        private ConcurrentBag<Point> _diedPeople = new();
        private List<Person> _retryMove = new();
        public Person[] People;
        private int _pplAlive;
        private int _pplInfected;
        private int _pplContagious;
        private HashSet<Point> _occupiedPositions = new();


        public World(SimulationConfig config)
        {
            _config = config;
            People = new Person[config.Population];
            _pplAlive = config.Population;
            _pplInfected = config.InitialInfected;
            Initialize();
        }

        public void Initialize()
        {
            var rnd = random.Value;

            for (var i = 0; i < _config.Population; i++)
            {
                Point pos;
                do
                {
                    pos = new Point(rnd.Next(0, _config.Width), rnd.Next(0, _config.Height));
                } while (!_occupiedPositions.Add(pos));

                People[i] = new Person(pos, (float)rnd.Next((int)(_config.InitialResistanceMin * 10), (int)(_config.InitialResistanceMax * 10)) / 10);
                People[i].OnInfection += Person_OnInfection;
                People[i].OnContagious += Person_OnContagious;
                People[i].OnDeath += Person_OnDeath;
                People[i].OnCured += Person_OnCured;
                People[i].OnMoved += Person_OnMoved;
            }

            for (var i = 0; i < _config.InitialInfected; i++)
            {
                People[i].IsInfected = true;
                People[i].IncubationTime = (byte)rnd.Next(_config.IncubationTimeMin, _config.IncubationTimeMax);
                People[i].DmgDelay = (byte)rnd.Next(_config.DmgDelayMin, _config.DmgDelayMax);
            }
        }

        #region Handle Events
        private void Person_OnInfection(object? sender, EventArgs e) => Interlocked.Increment(ref _pplInfected);
        private void Person_OnContagious(object? sender, EventArgs e) => Interlocked.Increment(ref _pplContagious);
        private void Person_OnCured(object? sender, EventArgs e)
        {
            Interlocked.Decrement(ref _pplInfected);
            Interlocked.Decrement(ref _pplContagious);
        }
        private void Person_OnDeath(object? sender, EventArgs e)
        {
            var person = (Person)sender!;
            Interlocked.Decrement(ref _pplInfected);
            Interlocked.Decrement(ref _pplAlive);
            if (person.IsContagious) Interlocked.Decrement(ref _pplContagious);

            person.OnInfection -= Person_OnInfection;
            person.OnContagious -= Person_OnContagious;
            person.OnDeath -= Person_OnDeath;
            person.OnCured -= Person_OnCured;
            person.OnMoved -= Person_OnMoved;
            // Does change the _people array, which would break the foreach loop
            //_people = _people.Where(p => p != person).ToArray();
        }
        private void Person_OnMoved(object? sender, PersonMoveEventArgs e)
        {
            var person = (Person)sender!;
            _movedPeople.Add(new MovedPerson(e.PreviousPosition, e.CurrentPosition, person.IsContagious, person.IsInfected, person.Health));
        }
        #endregion

        private bool IsPersonAt(Point position) => _occupiedPositions.Contains(position);

        public WorldUpdate Update()
        {
            _movedPeople = new();
            _diedPeople = new();
            _retryMove = new();

            // Shuffle people
            People = People.OrderBy(p => Guid.NewGuid()).ToArray();

            try
            {
                _people = new(People.Length);
                HealAndTryToMovePerson();
                RetryMovingPerson();
                MakePersonInfectedContagiousTakeDamageAndDie();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            // Remove dead people
            People = People.Where(p => p.Health > 0).ToArray();

            return new(_movedPeople, _diedPeople, _pplAlive, _pplInfected, _pplContagious);
        }

        private void MakePersonInfectedContagiousTakeDamageAndDie()
        {
            Parallel.ForEach(People, person =>
            {
                if (person.IsInfected && !person.IsContagious)
                {
                    person.IncubationTime--;
                    if (person.IncubationTime <= 0)
                    {
                        person.MakeContagious();
                        person.ContagiousTime = (byte)(random.Value.Next(_config.ContagiousTimeMin, _config.ContagiousTimeMax));
                    }
                }
            });

            Parallel.ForEach(People, async person =>
            {
                if (person.IsContagious)
                {
                    InfectNeighborsParallelSafe(person, random.Value);
                    person.ContagiousTime--;
                    if (person.ContagiousTime <= 0)
                    {
                        person.Cure();
                        person.Resistance += random.Value.Next((int)(_config.IncreaseResistanceAfterCuringMin * 10), (int)(_config.IncreaseResistanceAfterCuringMax * 10)) / 1000f;
                        person.DaysOfImmunity = (byte)(random.Value.Next(_config.ImmunityMin, _config.ImmunityMax) + 1);
                    }
                }
            });

            Parallel.ForEach(People, person =>
            {
                if (person.IsInfected || person.IsContagious)
                {
                    person.DmgDelay--;
                    if (person.DmgDelay <= 0)
                        DamagePerson(person);
                    person.AdditionalInfectionResistance += random.Value.Next((int)(_config.AdditionalResistancePerDayWhenInfectedMin * 10), (int)(_config.AdditionalResistancePerDayWhenInfectedMax * 10)) / 10f;
                }
            });

            Parallel.ForEach(People, person =>
            {
                if (person.DaysOfImmunity > 0)
                    person.DaysOfImmunity--;

                if (person.Health <= 0)
                {
                    person.Die();
                    _diedPeople.Add(person.Position);
                    _occupiedPositions.Remove(person.Position);
                }
            });
        }

        private void RetryMovingPerson()
        {
            Parallel.ForEach(People, person =>
            {
                if (!IsSurrounded(person))
                    person.IsSurrounded = false;
                else
                    _people[person.Position] = person;
            });

            foreach (var person in _retryMove)
            {
                if (person.CanMove)
                    MovePerson(person);
            }
        }

        private void HealAndTryToMovePerson()
        {
            Parallel.ForEach(People, person =>
            {
                if (person.Health < 100.0)
                    HealPerson(person);
            });

            Parallel.ForEach(People, person =>
            {
                if (IsSurrounded(person))
                {
                    _retryMove.Add(person);
                    person.IsSurrounded = true;
                }
            });

            foreach (var person in People)
            {
                if (person.CanMove)
                    MovePerson(person);
            }
        }

        private void InfectNeighborsParallelSafe(Person person, Random rnd)
        {
            int width = _config.Width;
            int height = _config.Height;

            Parallel.For(person.Position.X - 1, person.Position.X + 2, x =>
            {
                for (var y = person.Position.Y - 1; y <= person.Position.Y + 1; y++)
                {
                    var wrappedX = (x + width) % width;
                    var wrappedY = (y + height) % height;

                    CheckAndInfectSafe(new Point(wrappedX, wrappedY), rnd);
                }
            });
        }

        private void CheckAndInfectSafe(Point pos, Random rnd)
        {
            if (IsPersonAt(pos))
            {
                var p = _people[pos];

                if (!p.IsInfected && !p.IsContagious && p.DaysOfImmunity <= 0)
                {
                    lock (p)
                    {
                        if (!p.IsInfected && !p.IsContagious && p.DaysOfImmunity <= 0)
                        {
                            p.Infect();
                            p.IncubationTime = (byte)rnd.Next(_config.IncubationTimeMin, _config.IncubationTimeMax);
                            p.DmgDelay = (byte)rnd.Next(_config.DmgDelayMin, _config.DmgDelayMax);
                        }
                    }
                }
            }
        }

        private void DamagePerson(Person person)
        {
            var baseDamage = random.Value.Next((int)(_config.Virus.DamageMin * 10), (int)(_config.Virus.DamageMax * 10)) / 10f;
            var damage = baseDamage * (1 - person.Resistance / 100f - person.AdditionalInfectionResistance / 100f);
            person.Health -= damage;
        }

        private void HealPerson(Person person)
        {
            if (person.IsInfected || person.IsContagious)
            {
                person.Health += person.Health * random.Value.Next((int)(_config.HealAmountMin * 10), (int)(_config.HealAmountMax * 10)) / 1000f;
            }
            else
            {
                person.Health = 100f * random.Value.Next((int)(_config.HealAmountMin * 10), (int)(_config.HealAmountMax * 10)) / 1000f;
            }

            if (person.Health > 100)
                person.Health = 100;
        }
        
        private void MovePerson(Person person)
        {
            Point newPosition;
            do
            {
                newPosition = GetNewPosition((MoveDirections)random.Value.Next(0, MOVEDIRECTIONSLENGTH), person);
            } while (IsPersonAt(newPosition));
            _occupiedPositions.Add(newPosition);
            _occupiedPositions.Remove(person.Position);
            person.Move(newPosition);
            _people[newPosition] = person;
        }

        private bool IsSurrounded(Person person)
        {
            for (var xOffset = -1; xOffset <= 1; xOffset++)
            {
                for (var yOffset = -1; yOffset <= 1; yOffset++)
                {
                    var x = (person.Position.X + xOffset + _config.Width) % _config.Width;
                    var y = (person.Position.Y + yOffset + _config.Height) % _config.Height;

                    var pos = new Point(x, y);
                    if (!IsPersonAt(pos))
                        return false;
                }
            }

            return true;
        }

        private Point GetNewPosition(MoveDirections direction, Person person)
        {
            var (dx, dy) = direction switch
            {
                MoveDirections.Left => (-1, 0),
                MoveDirections.Right => (1, 0),
                MoveDirections.Up => (0, -1),
                MoveDirections.Down => (0, 1),
                MoveDirections.UpLeft => (-1, -1),
                MoveDirections.UpRight => (1, -1),
                MoveDirections.DownLeft => (-1, 1),
                MoveDirections.DownRight => (1, 1),
                _ => throw new ArgumentOutOfRangeException()
            };

            var newPosition = new Point
            {
                X = (person.Position.X + dx + _config.Width) % _config.Width,
                Y = (person.Position.Y + dy + _config.Height) % _config.Height
            };

            return newPosition;
        }
    }
}