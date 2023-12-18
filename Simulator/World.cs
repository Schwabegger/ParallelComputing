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
        private List<MovedPerson> _movedPeople = new();
        private List<Point> _diedPeople = new();
        private List<Person> _retryMove = new();
        private Person[] _people;
        private int _pplAlive;
        private int _pplInfected;
        private int _pplContagious;
        private HashSet<Point> _occupiedPositions = new();


        public World(SimulationConfig config)
        {
            _config = config;
            _people = new Person[config.Population];
            _pplAlive = config.Population;
            _pplInfected = config.InitialInfected;
            Initialize();
        }

        public void Initialize()
        {
            var rnd = new Random();

            for (var i = 0; i < _config.Population; i++)
            {
                Point pos;
                do
                {
                    pos = new Point(rnd.Next(0, _config.Width), rnd.Next(0, _config.Height));
                } while (!_occupiedPositions.Add(pos));

                _people[i] = new Person(pos, (float)rnd.Next((int)(_config.InitialResistanceMin * 10), (int)(_config.InitialResistanceMax * 10)) / 10);
                _people[i].OnInfection += Person_OnInfection;
                _people[i].OnContagious += Person_OnContagious;
                _people[i].OnDeath += Person_OnDeath;
                _people[i].OnCured += Person_OnCured;
                _people[i].OnMoved += Person_OnMoved;
            }

            for (var i = 0; i < _config.InitialInfected; i++)
            {
                _people[i].IsInfected = true;
                _people[i].IncubationTime = (byte)rnd.Next(_config.IncubationTimeMin, _config.IncubationTimeMax);
                _people[i].DmgDelay = (byte)rnd.Next(_config.DmgDelayMin, _config.DmgDelayMax);
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
            _people = _people.OrderBy(p => Guid.NewGuid()).ToArray();

            try
            {
                HealAndTryToMovePerson();
                RetryMovingPerson();
                MakePersonInfectedContagiousTakeDamageAndDie();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            // Remove dead people
            _people = _people.Where(p => p.Health > 0).ToArray();

            return new(_movedPeople, _diedPeople, _pplAlive, _pplInfected, _pplContagious);
        }

        private void MakePersonInfectedContagiousTakeDamageAndDie()
        {
            Random rnd = new();
            foreach (var person in _people)
            {
                if (person.IsInfected && !person.IsContagious)
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
                        person.Resistance += new Random().Next((int)(_config.IncreaseResistanceAfterCuringMin * 10), (int)(_config.IncreaseResistanceAfterCuringMax * 10)) / 10f;
                        person.DaysOfImmunity = (byte)(rnd.Next(_config.ImmunityMin, _config.ImmunityMax) + 1);
                    }
                }

                if (person.IsInfected || person.IsContagious)
                {
                    person.DmgDelay--;
                    if (person.DmgDelay <= 0)
                        DamagePerson(person);
                    person.AdditionalInfectionResistance += new Random().Next((int)(_config.AdditionalResistancePerDayWhenInfectedMin * 10), (int)(_config.AdditionalResistancePerDayWhenInfectedMax * 10)) / 10f;
                }

                if (person.DaysOfImmunity > 0)
                    person.DaysOfImmunity--;

                if (person.Health <= 0)
                {
                    person.Die();
                    _diedPeople.Add(person.Position);
                    _occupiedPositions.Remove(person.Position);
                }
            }
        }

        private void RetryMovingPerson()
        {
            foreach (var person in _retryMove.Where(person => !IsSurrounded(person)).Where(person => person.CanMove))
            {
                MovePerson(person);
            }
        }

        private void HealAndTryToMovePerson()
        {
            foreach (var person in _people)
            {
                if (person.Health < 100.0)
                    HealPerson(person);
                if (IsSurrounded(person))
                {
                    _retryMove.Add(person);
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
                    // Wrap around
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
                        var p = _people.First(p => p.Position == pos);
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
            var baseDamage = new Random().Next((int)(_config.Virus.DamageMin * 10), (int)(_config.Virus.DamageMax * 10)) / 10f;
            var damage = baseDamage * (1 - person.Resistance - person.AdditionalInfectionResistance);
            person.Health -= damage;
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
            } while (IsPersonAt(newPosition));
            _occupiedPositions.Add(newPosition);
            _occupiedPositions.Remove(person.Position);
            person.Move(newPosition);
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