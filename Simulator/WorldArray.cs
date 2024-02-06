using System.Collections.Concurrent;
using System.Drawing;

namespace Simulator
{
    public class WorldArray : IWorld
    {
        const int MOVEDIRECTIONSLENGTH = 8;
        // Global infection rate to ensure set infection rate
        // Random could infect all or none
        private readonly bool[] _infectionRate = new bool[100];
        private volatile int _infectionRateIndex = 0;
        private static readonly MoveDirection[] moveDirections = (MoveDirection[])Enum.GetValues(typeof(MoveDirection));

        static readonly ThreadLocal<Random> random = new(() => new Random(int.MaxValue));
        private ConcurrentBag<Point> _diedPeople = new();
        private ConcurrentBag<Person> _survivedPeople = new();
        private List<MovedPerson> _movedPeople = new();
        private List<Person> _retryMove = new();
        private Person[] _people;
        private int _pplAlive;
        private int _pplInfected;
        private int _pplContagious;

        #region config
        private static int _width;
        private static int _height;
        private static int _population;
        private static int _initialInfected;
        private static float _maxResistance;
        private static float _increaseResistanceAfterCuringMin;
        private static float _increaseResistanceAfterCuringMax;
        private static float _additionalResistancePerDayWhenInfectedMin;
        private static float _additionalResistancePerDayWhenInfectedMax;
        private static float _initialResistanceMin;
        private static float _initialResistanceMax;
        private static byte _incubationTimeMin;
        private static byte _incubationTimeMax;
        private static byte _immunityMin;
        private static byte _immunityMax;
        private static byte _dmgDelayMin;
        private static byte _dmgDelayMax;
        private static byte _contagiousTimeMin;
        private static byte _contagiousTimeMax;
        private static float _healAmountMin;
        private static float _healAmountMax;
        private static int _infectionRatePercentage;
        private static float _damageMin;
        private static float _damageMax;
        #endregion

        private Person?[,] _world;

        public WorldArray(SimulationConfig config)
        {
            _people = new Person[config.Population];
            _world = new Person[config.Height, config.Width];
            _pplAlive = config.Population;
            _pplInfected = config.InitialInfected;
            SetLocalVariables(config);
            Initialize();
        }

        /// <summary>
        /// Initializes the world by placing all the people in random positions
        /// and infecting a certain number of them
        /// </summary>
        public void Initialize()
        {
            var rnd = random.Value!;
            int x; int y;
            for (var i = 0; i < _population; i++)
            {
                x = rnd.Next(0, _width);
                y = rnd.Next(0, _height);
                while (_world[y, x] is not null)
                {
                    // Move right if the position is already occupied to enhance performance
                    x++;
                    if (x >= _width)
                    {
                        x = 0;
                        y++;
                        if (y >= _height)
                            y = 0;
                    }
                }

                //do
                //{
                //    x = rnd.Next(0, _width);
                //    y = rnd.Next(0, _height);
                //} while (_world[y, x] is not null);

                var person = new Person(new Point(x, y), (float)rnd.Next((int)(_initialResistanceMin * 10), (int)(_initialResistanceMax * 10)) / 10);

                // Subscribe to events
                person.OnInfection += Person_OnInfection;
                person.OnContagious += Person_OnContagious;
                person.OnDeath += Person_OnDeath;
                person.OnCured += Person_OnCured;
                person.OnMoved += Person_OnMoved;

                _people[i] = person;
                _world[y, x] = _people[i];
            }

            // Infect a certain number of people
            for (var i = 0; i < _initialInfected; i++)
            {
                _people[i].IsInfected = true;
                _people[i].IncubationTime = (byte)rnd.Next(_incubationTimeMin, _incubationTimeMax);
                _people[i].DmgDelay = (byte)rnd.Next(_dmgDelayMin, _dmgDelayMax);
            }

            // Randomly assign infection rate
            for (var i = 0; i < _infectionRatePercentage; i++)
                _infectionRate[i] = true;
            rnd.Shuffle(_infectionRate);
        }

        private void SetLocalVariables(SimulationConfig config)
        {
            _width = config.Width;
            _height = config.Height;
            _population = config.Population;
            _initialInfected = config.InitialInfected;
            _maxResistance = config.MaxResistance;
            _increaseResistanceAfterCuringMin = config.IncreaseResistanceAfterCuringMin;
            _increaseResistanceAfterCuringMax = config.IncreaseResistanceAfterCuringMax;
            _additionalResistancePerDayWhenInfectedMin = config.AdditionalResistancePerDayWhenInfectedMin;
            _additionalResistancePerDayWhenInfectedMax = config.AdditionalResistancePerDayWhenInfectedMax;
            _initialResistanceMin = config.InitialResistanceMin;
            _initialResistanceMax = config.InitialResistanceMax;
            _incubationTimeMin = config.IncubationTimeMin;
            _incubationTimeMax = config.IncubationTimeMax;
            _immunityMin = config.ImmunityMin;
            _immunityMax = config.ImmunityMax;
            _dmgDelayMin = config.DmgDelayMin;
            _dmgDelayMax = config.DmgDelayMax;
            _contagiousTimeMin = config.ContagiousTimeMin;
            _contagiousTimeMax = config.ContagiousTimeMax;
            _healAmountMin = config.HealAmountMin;
            _healAmountMax = config.HealAmountMax;
            _infectionRatePercentage = config.Virus!.InfectionRate;
            _damageMin = config.Virus!.DamageMin;
            _damageMax = config.Virus!.DamageMax;
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
            // Does change the _peopleByPointDic array, which would break the foreach loop
            //_peopleByPointDic = _peopleByPointDic.Where(p => p != person).ToArray();
        }
        private void Person_OnMoved(object? sender, PersonMoveEventArgs e)
        {
            var person = (Person)sender!;
            _movedPeople.Add(new MovedPerson(e.PreviousPosition, e.CurrentPosition, person.IsContagious, person.IsInfected, person.Health));
        }
        #endregion

        /// <summary>
        /// Updates the world by one day
        /// </summary>
        /// <returns>Returns <see cref="WorldUpdate"/> so the caller knows important information about the current world state</returns>
        public WorldUpdate Update()
        {
            // Clear the lists
            _movedPeople.Clear();
            _diedPeople.Clear();
            _retryMove.Clear();
            _survivedPeople.Clear();

            // Shuffle the people array to make the order of the people random so that the same person doesn't always move first
            random.Value.Shuffle(_people);

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

            // Update the people array
            _people = _survivedPeople.ToArray();

            return new(_movedPeople, _diedPeople, _pplAlive, _pplInfected, _pplContagious);
        }

        /// <summary>
        /// Executes the functions that make a person infected, contagious, take damage and die in parallel
        /// </summary>
        private void MakePersonInfectedContagiousTakeDamageAndDie()
        {
            // Parallelizing may not be the best option here ?
            // Makes a person contagious if they are infected and their incubation time is over
            Parallel.ForEach(_people, (person) =>
            {
                if (person.IsInfected && !person.IsContagious)
                {
                    person.IncubationTime--;
                    if (person.IncubationTime <= 0)
                    {
                        person.MakeContagious();
                        person.ContagiousTime = (byte)(random.Value.Next(_contagiousTimeMin, _contagiousTimeMax));
                    }
                }
            });

            // Infects the neighbors of contagious people and reduces the time of immunity
            Parallel.ForEach(_people, (person) =>
            {
                if (person.IsContagious)
                {
                    InfectNeighborsParallelSafe(person, random.Value);
                    person.ContagiousTime--;
                    if (person.ContagiousTime <= 0)
                    {
                        person.Cure();
                        if (_increaseResistanceAfterCuringMax > 0 && person.Resistance < _maxResistance)
                        {
                            person.Resistance += random.Value.Next((int)(_increaseResistanceAfterCuringMin * 10), (int)(_increaseResistanceAfterCuringMax * 10)) / 1000f;
                            if (person.Resistance > _maxResistance)
                                person.Resistance = _maxResistance;
                        }
                        if (_immunityMax > 0)
                            person.DaysOfImmunity = (byte)(random.Value.Next(_immunityMin, _immunityMax) + 1);
                    }
                }
                else if (person.DaysOfImmunity > 0)
                    person.DaysOfImmunity--;
            });

            // Parallelizing may not be the best option here ?
            // Takes damage and dies if health is 0
            Parallel.ForEach(_people, (person) =>
            {
                if (person.IsInfected)
                {
                    if (person.DmgDelay > 0)
                        person.DmgDelay--;
                    if (person.DmgDelay <= 0)
                        DamagePerson(person);
                    if (_additionalResistancePerDayWhenInfectedMax > 0)
                        person.AdditionalInfectionResistance += random.Value.Next((int)(_additionalResistancePerDayWhenInfectedMin * 10), (int)(_additionalResistancePerDayWhenInfectedMax * 10)) / 10f;
                }
            });
        }

        /// <summary>
        /// Retries to move the people that couldn't move in the first try
        /// </summary>
        private void RetryMovingPerson()
        {
            foreach (var person in _retryMove)
            {
                if (person.CanMove && !IsSurrounded(person))
                    MovePerson(person);
            }
        }

        /// <summary>
        /// Heals the people and tries to move them
        /// </summary>
        private void HealAndTryToMovePerson()
        {
            // Parallelizing may not be the best option here ?
            var peopleLength = _people.Length;
            if (_healAmountMax > 0)
                Parallel.For(0, peopleLength, i => HealPerson(_people[i]));

            foreach (var person in _people)
            {
                if (person.CanMove)
                    if (!IsSurrounded(person))
                        MovePerson(person);
                    else
                        _retryMove.Add(person);
            }
        }

        /// <summary>
        /// Infects all the neighbors of a given person
        /// </summary>
        /// <param name="person">The <see cref="Person"/> which neighbors should be infected</param>
        /// <param name="rnd">A <see cref="ThreadLocal{T}"/> random to ensure thread safety</param>
        private void InfectNeighborsParallelSafe(Person person, Random rnd)
        {
            int width = _width;
            int height = _height;

            Parallel.For(person.Position.X - 1, person.Position.X + 2, x =>
            {
                for (var y = person.Position.Y - 1; y <= person.Position.Y + 1; y++)
                {
                    var wrappedX = (x + width) % width;
                    var wrappedY = (y + height) % height;

                    CheckAndInfect(new Point(wrappedX, wrappedY), rnd);
                }
            });
        }

        /// <summary>
        /// Checks if a person is susceptible to infection and if so, infects them with a certain probability
        /// </summary>
        /// <param name="pos"><see cref="Point"/> where the <see cref="Person"/> should be infected if there is one</param>
        /// <param name="rnd">A <see cref="ThreadLocal{T}"/> random to ensure thread safety</param>
        private void CheckAndInfect(Point pos, Random rnd)
        {
            var person = _world[pos.Y, pos.X];
            if (person is not null && person.IsSusceptibleToInfection())
            {
                var i = Interlocked.Increment(ref _infectionRateIndex);
                if (_infectionRate[i % 100])
                {
                    person.Infect();
                    person.IncubationTime = (byte)rnd.Next(_incubationTimeMin, _incubationTimeMax);
                    person.DmgDelay = (byte)rnd.Next(_dmgDelayMin, _dmgDelayMax);
                }
            }
        }

        /// <summary>
        /// Damages the given person and kills them if their health is 0
        /// </summary>
        /// <param name="person"><see cref="Person"/> which should take damage</param>
        private void DamagePerson(Person person)
        {
            var baseDamage = random.Value.Next((int)(_damageMin * 10), (int)(_damageMax * 10)) / 10f;
            float damage;
            if (person.Resistance > 0 || person.AdditionalInfectionResistance > 0)
                damage = baseDamage * (1 - person.Resistance / 100f - person.AdditionalInfectionResistance / 100f);
            else
                damage = baseDamage;
            person.Health -= damage;

            if (person.Health <= 0)
            {
                person.Die();
                _diedPeople.Add(person.Position);
                _world[person.Position.Y, person.Position.X] = null;
            }
            else
                _survivedPeople.Add(person);
        }

        /// <summary>
        /// Heals the given person
        /// </summary>
        /// <param name="person"><see cref="Person"/> which should be healed</param>
        private void HealPerson(Person person)
        {
            if (person.Health == 100)
                return;
            if (Math.Abs(person.Health - 100) < 5)
            {
                person.Health = 100;
                return;
            }

            int randomValue = random.Value.Next((int)(_healAmountMin * 10), (int)(_healAmountMax * 10));
            float healAmount = randomValue * 0.001f; // Convert the random number to a percentage

            person.Health += (person.IsInfected || person.IsContagious) ? person.Health * healAmount : 100f * healAmount;

            person.Health = Math.Min(person.Health, 100);
        }

        /// <summary>
        /// Moves the given person to a new position
        /// </summary>
        /// <param name="person"></param>
        private void MovePerson(Person person)
        {
            // Shuffle the moveDirections array to make the order of the directions random so that the same person doesn't always move in the same direction and to enhance performance
            // because random would try to use in the same direction multiple times
            random.Value!.Shuffle(moveDirections);
            Point newPosition = default;

            foreach (var moveDirection in moveDirections)
            {
                newPosition = GetNewPosition(moveDirection, person);
                if (_world[newPosition.Y, newPosition.X] is null)
                    break;
            }
            _world[newPosition.Y, newPosition.X] = person;
            _world[person.Position.Y, person.Position.X] = null;
            person.Move(newPosition);
        }

        /// <summary>
        /// Checks if a person is surrounded by other people
        /// </summary>
        /// <param name="person"><see cref="Person"/> which surrundings should be checked</param>
        /// <returns></returns>
        private bool IsSurrounded(Person person)
        {
            for (var xOffset = -1; xOffset <= 1; xOffset++)
            {
                for (var yOffset = -1; yOffset <= 1; yOffset++)
                {
                    var x = (person.Position.X + xOffset + _width) % _width;
                    var y = (person.Position.Y + yOffset + _height) % _height;

                    if (_world[y, x] is null)
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Gets the new position of a person after moving in a certain direction
        /// </summary>
        /// <param name="direction"><see cref="MovedPerson"/> the direction the person should move to</param>
        /// <param name="person"><see cref="Person"/> person to move</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
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
                X = (person.Position.X + dx + _width) % _width,
                Y = (person.Position.Y + dy + _height) % _height
            };

            return newPosition;
        }
    }
}