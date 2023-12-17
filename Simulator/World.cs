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

    public class World
    {
        private readonly SimulationConfig _config;
        List<MovedPerson> movedPeople = new();
        List<Point> diedPeople = new();
        List<Person> retryMove = new();
        Person[] people;
        public int PplAlive { get; private set; }
        public int PplInfected { get; private set; }
        public int PplContagious { get; private set; }
        private HashSet<Point> _occupiedPositions = new HashSet<Point>();
        private ConcurrentDictionary<Person, Point> _personPositions = new ConcurrentDictionary<Person, Point>();


        public World(SimulationConfig config)
        {
            _config = config;
            people = new Person[config.PopulationSize];
            PplAlive = config.PopulationSize;
            PplInfected = config.InitialInfectionRate;
            Initialize();
        }

        public void Initialize()
        {
            var rnd = new Random();

            for (var i = 0; i < _config.PopulationSize; i++)
            {
                Point pos;
                do
                {
                    pos = new Point(rnd.Next(0, _config.Width), rnd.Next(0, _config.Height));
                } while (!_occupiedPositions.Add(pos));

                people[i] = new Person(pos, (float)rnd.Next((int)(_config.InitialResistanceMin * 10), (int)(_config.InitialResistanceMax * 10)) / 10);
                people[i].OnInfection += Simulation_OnInfection;
                people[i].OnContagious += Simulation_OnContagious;
                people[i].OnDeath += Simulation_OnDeath;
                people[i].OnCured += Simulation_OnCured;
                people[i].OnMoved += Simulation_OnMoved;
            }

            for (var i = 0; i < _config.InitialInfectionRate; i++)
            {
                people[i].IsInfected = true;
                people[i].IncubationTime = (byte)rnd.Next(_config.IncubationTimeMin, _config.IncubationTimeMax);
                people[i].DmgDelay = (byte)rnd.Next(_config.DmgDelayMin, _config.DmgDelayMax);
            }
        }

        #region Handle Events
        private void Simulation_OnInfection(object? sender, EventArgs e) => PplInfected++;
        private void Simulation_OnContagious(object? sender, EventArgs e) => PplContagious++;
        private void Simulation_OnCured(object? sender, EventArgs e)
        {
            PplInfected--;
            PplContagious--;
        }
        private void Simulation_OnDeath(object? sender, EventArgs e)
        {
            PplInfected--;
            PplContagious--;
            PplAlive--;

            var person = (Person)sender!;
            person.OnInfection -= Simulation_OnInfection;
            person.OnContagious -= Simulation_OnContagious;
            person.OnDeath -= Simulation_OnDeath;
            person.OnCured -= Simulation_OnCured;
            person.OnMoved -= Simulation_OnMoved;
            people = people.Where(p => p != person).ToArray();
        }
        private void Simulation_OnMoved(object? sender, PersonMoveEventArgs e)
        {
            var person = (Person)sender!;
            movedPeople.Append(new MovedPerson(e.PreviousPosition, e.CurrentPosition, person.IsContagious, person.IsInfected, person.Health));
        }
        #endregion

        private bool IsPersonAt(Point position) => _occupiedPositions.Contains(position);

        public WorldUpdate Update()
        {
            movedPeople.Clear();
            diedPeople.Clear();
            retryMove.Clear();

            HealAndTryToMovePerson();
            RetryMovingPerson();
            MakePersonInfectedContagiousTakeDamageAndDie();

            return new(movedPeople, diedPeople, PplAlive, PplInfected, PplContagious);
        }

        private void MakePersonInfectedContagiousTakeDamageAndDie()
        {
            Random rnd = new();
            foreach (var person in people)
            {
                if (person.IsInfected)
                {
                    person.IncubationTime--;
                    if (person.IncubationTime <= 0)
                    {
                        person.MakeContagious();
                        person.ContagiousTime = (byte)rnd.Next(_config.ContagiousTimeMin, _config.ContagiousTimeMax);
                    }
                }

                if (person.IsContagious)
                {
                    InfectNeighbors(person, rnd);
                    person.ContagiousTime--;
                    if (person.ContagiousTime <= 0)
                    {
                        person.Cure();
                        person.DaysOfImmunity = (byte)rnd.Next(_config.DaysOfImmunityMin, _config.DaysOfImmunityMax);
                    }
                }

                if (person.IsInfected || person.IsContagious)
                {
                    person.DmgDelay--;
                    if (person.DmgDelay <= 0)
                        DamagePerson(person);
                }

                if (person.DaysOfImmunity > 0)
                    person.DaysOfImmunity--;

                if (person.Health <= 0)
                {
                    person.Die();
                    diedPeople.Add(person.Position);
                    _occupiedPositions.Remove(person.Position);
                }
            }
        }

        private void RetryMovingPerson()
        {
            foreach (var person in retryMove.Where(person => !IsSurrounded(person)).Where(person => person.CanMove))
            {
                MovePerson(person);
            }
        }

        private void HealAndTryToMovePerson()
        {
            foreach (var person in people)
            {
                if (person.Health < 100.0)
                    HealPerson(person);
                if (IsSurrounded(person))
                {
                    retryMove.Add(person);
                    continue;
                }

                if (person.CanMove)
                    MovePerson(person);
            }
        }

        private void InfectNeighbors(Person person, Random rnd)
        {
            for (var x = person.Position.X - 1; x <= person.Position.X + 1; x++)
            {
                for (var y = person.Position.Y - 1; y <= person.Position.Y + 1; y++)
                {
                    if (x < 0)
                        x = _config.Width - 1;
                    else if (x >= _config.Width)
                        x = 0;
                    if (y < 0)
                        y = _config.Height - 1;
                    else if (y >= _config.Height)
                        y = 0;

                    var pos = new Point(x, y);
                    if (IsPersonAt(pos))
                    {
                        var p = people.First(p => p.Position == pos);
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
            person.Health -= person.Health * new Random().Next((int)(_config.Virus!.DamageMin * 10), (int)(_config.Virus!.DamageMax * 10)) / 10;
        }

        private void HealPerson(Person person)
        {
            if (person.IsInfected || person.IsContagious)
            {
                person.Health += person.Health * new Random().Next((int)(_config.HealAmountMin * 10), (int)(_config.HealAmountMax * 10)) / 10;
            }
            else
            {
                person.Health = 100f * new Random().Next((int)(_config.HealAmountMin * 10), (int)(_config.HealAmountMax * 10)) / 10;
            }

            if (person.Health > 100)
                person.Health = 100;
        }

        private void MovePerson(Person person)
        {
            Point newPosition;
            do
            {
                newPosition = GetNewPosition((MoveDirections)new Random().Next(0, Enum.GetValues(typeof(MoveDirections)).Length), person);
            } while (!_occupiedPositions.Add(newPosition));
            _occupiedPositions.Remove(person.Position);
            person.Move(newPosition);
        }

        private bool IsSurrounded(Person person)
        {
            for (var x = person.Position.X - 1; x <= person.Position.X + 1; x++)
            {
                for (var y = person.Position.Y - 1; y <= person.Position.Y + 1; y++)
                {
                    if (x < 0)
                        x = _config.Width - 1;
                    else if (x >= _config.Width)
                        x = 0;
                    if (y < 0)
                        y = _config.Height - 1;
                    else if (y >= _config.Height)
                        y = 0;

                    var pos = new Point(x, y);
                    if (!IsPersonAt(pos))
                        return false;
                }
            }

            return true;
        }

        private static Point GetNewPosition(MoveDirections direction, Person person)
        {
            return direction switch
            {
                MoveDirections.Left => person.Position with { X = person.Position.X - 1 },
                MoveDirections.Right => person.Position with { X = person.Position.X + 1 },
                MoveDirections.Up => person.Position with { Y = person.Position.Y - 1 },
                MoveDirections.Down => person.Position with { Y = person.Position.Y + 1 },
                MoveDirections.UpLeft => person.Position with { X = person.Position.X - 1, Y = person.Position.Y - 1 },
                MoveDirections.UpRight => person.Position with { X = person.Position.X + 1, Y = person.Position.Y - 1 },
                MoveDirections.DownLeft => person.Position with { X = person.Position.X - 1, Y = person.Position.Y + 1 },
                MoveDirections.DownRight => person.Position with { X = person.Position.X + 1, Y = person.Position.Y + 1 },
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}