using System.Collections.Concurrent;
using System.Drawing;

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

    public class World : IWorld
    {
        const int MOVEDIRECTIONSLENGTH = 8;
        private readonly bool[] _infectionRate = new bool[100];
        private volatile int _infectionRateIndex = 0;
        private static readonly MoveDirection[] moveDirections = (MoveDirection[])Enum.GetValues(typeof(MoveDirection));

        static readonly ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random(int.MaxValue));
        Dictionary<Point, Person> _people = new();
        private readonly SimulationConfig _config;
        private List<MovedPerson> _movedPeople = new();
        private ConcurrentBag<Point> _diedPeople = new();
        private ConcurrentBag<Person> _survivedPeople = new();
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

            // randomly assign infection rate
            for (var i = 0; i < _config.Virus.InfectionRate; i++)
                _infectionRate[i] = true;
            rnd.Shuffle(_infectionRate);
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
            _movedPeople.Clear();
            _diedPeople.Clear();
            _retryMove.Clear();
            _survivedPeople.Clear();

            random.Value.Shuffle(People);

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

            People = _survivedPeople.ToArray();

            return new(_movedPeople, _diedPeople, _pplAlive, _pplInfected, _pplContagious);
        }

        private void MakePersonInfectedContagiousTakeDamageAndDie()
        {
            #region Sync
            //foreach (var person in _people)
            //{
            //    if (person.IsInfected && !person.IsContagious)
            //    {
            //        person.IncubationTime--;
            //        if (person.IncubationTime <= 0)
            //        {
            //            person.MakeContagious();
            //            person.ContagiousTime =
            //                (byte)(random.Value.Next(_config.ContagiousTimeMin, _config.ContagiousTimeMax));
            //        }
            //    }
            //}

            //foreach (var person in _people)
            //{
            //    if (person.IsContagious)
            //    {
            //        InfectNeighborsParallelSafe(person, random.Value);
            //        person.ContagiousTime--;
            //        if (person.ContagiousTime <= 0)
            //        {
            //            person.Cure();
            //            person.Resistance += random.Value.Next((int)(_config.IncreaseResistanceAfterCuringMin * 10),
            //                (int)(_config.IncreaseResistanceAfterCuringMax * 10)) / 1000f;
            //            person.DaysOfImmunity = (byte)(random.Value.Next(_config.ImmunityMin, _config.ImmunityMax) + 1);
            //        }
            //    }
            //}

            //foreach (var person in _people)
            //{
            //    if (person.IsInfected || person.IsContagious)
            //    {
            //        person.DmgDelay--;
            //        if (person.DmgDelay <= 0)
            //            DamagePerson(person);
            //        person.AdditionalInfectionResistance += random.Value.Next(
            //            (int)(_config.AdditionalResistancePerDayWhenInfectedMin * 10),
            //            (int)(_config.AdditionalResistancePerDayWhenInfectedMax * 10)) / 10f;
            //    }
            //}

            //foreach (var person in _people)
            //{
            //    if (person.DaysOfImmunity > 0)
            //        person.DaysOfImmunity--;

            //    if (person.Health <= 0)
            //    {
            //        person.Die();
            //        _diedPeople.Add(person.Position);
            //        _occupiedPositions.Remove(person.Position);
            //    }
            //    else
            //        _survivedPeople.Add(person);
            //}
            #endregion

            #region Parallel
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

            Parallel.ForEach(People, person =>
            {
                if (person.IsContagious)
                {
                    InfectNeighborsParallelSafe(person, random.Value);
                    person.ContagiousTime--;
                    if (person.ContagiousTime <= 0)
                    {
                        person.Cure();
                        if (_config.IncreaseResistanceAfterCuringMax > 0)
                            person.Resistance += random.Value.Next((int)(_config.IncreaseResistanceAfterCuringMin * 10), (int)(_config.IncreaseResistanceAfterCuringMax * 10)) / 1000f;
                        if (_config.ImmunityMax > 0)
                            person.DaysOfImmunity = (byte)(random.Value.Next(_config.ImmunityMin, _config.ImmunityMax) + 1);
                    }
                }
            });

            Parallel.ForEach(People, person =>
            {
                if (person.IsInfected)
                {
                    if (person.DmgDelay > 0)
                        person.DmgDelay--;
                    if (person.DmgDelay <= 0)
                        DamagePerson(person);
                    if (_config.AdditionalResistancePerDayWhenInfectedMax > 0)
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
                else
                    _survivedPeople.Add(person);
            });
            #endregion
        }

        private void RetryMovingPerson()
        {
            foreach (var person in _retryMove)
            {
                if (person.CanMove)
                    if (!IsSurrounded(person))
                        MovePerson(person);
                _people[person.Position] = person;
            }
        }

        private void HealAndTryToMovePerson()
        {
            if (_config.HealAmountMax > 0)
                Parallel.ForEach(People, HealPerson);

            foreach (var person in People)
            {
                if (person.CanMove)
                    if (!IsSurrounded(person))
                        MovePerson(person);
                    else
                        _retryMove.Add(person);
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
            if (_people.TryGetValue(pos, out var p) && p.IsSusceptibleToInfection())
            {
                var i = Interlocked.Increment(ref _infectionRateIndex);
                if (_infectionRate[i % 100])
                {
                    p.Infect();
                    p.IncubationTime = (byte)rnd.Next(_config.IncubationTimeMin, _config.IncubationTimeMax);
                    p.DmgDelay = (byte)rnd.Next(_config.DmgDelayMin, _config.DmgDelayMax);
                }
            }
        }

        private void DamagePerson(Person person)
        {
            var baseDamage = random.Value.Next((int)(_config.Virus.DamageMin * 10), (int)(_config.Virus.DamageMax * 10)) / 10f;
            float damage;
            if (person.Resistance > 0 || person.AdditionalInfectionResistance > 0)
                damage = baseDamage * (1 - person.Resistance / 100f - person.AdditionalInfectionResistance / 100f);
            else
                damage = baseDamage;
            person.Health -= damage;
        }

        private void HealPerson(Person person)
        {
            if (person.Health == 100)
                return;
            if (Math.Abs(person.Health - 100) < 5)
            {
                person.Health = 100;
                return;
            }

            int randomValue = random.Value.Next((int)(_config.HealAmountMin * 10), (int)(_config.HealAmountMax * 10));
            float healAmount = randomValue * 0.001f; // Convert the random number to a percentage

            person.Health += (person.IsInfected || person.IsContagious) ? person.Health * healAmount : 100f * healAmount;

            person.Health = Math.Min(person.Health, 100);
        }

        private void MovePerson(Person person)
        {
            random.Value.Shuffle(moveDirections);
            Point newPosition = default;
            foreach (var moveDirection in moveDirections)
            {
                newPosition = GetNewPosition(moveDirection, person);
                if (!IsPersonAt(newPosition))
                    break;
            }
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

        private Point GetNewPosition(MoveDirection direction, Person person)
        {
            var (dx, dy) = direction switch
            {
                MoveDirection.Left => (-1, 0),
                MoveDirection.Right => (1, 0),
                MoveDirection.Up => (0, -1),
                MoveDirection.Down => (0, 1),
                MoveDirection.UpLeft => (-1, -1),
                MoveDirection.UpRight => (1, -1),
                MoveDirection.DownLeft => (-1, 1),
                MoveDirection.DownRight => (1, 1),
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