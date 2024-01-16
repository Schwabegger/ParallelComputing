using System.Collections.Concurrent;
using System.Drawing;
using System.Security.Cryptography;

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


    public class WorldUpdateNew
    {
        public List<MovedPersonNew> MovedPeople { get; set; }
        public ConcurrentBag<Point> DiedPeople { get; set; }
        public int PplAlive { get; set; }
        public int PplInfected { get; set; }
        public int PplContagious { get; set; }
    }

    public record MovedPersonNew(int PreviousX, int PreviousY, int CurrentX, int CurrentY, bool IsInfected, bool IsContagious, float Health);

    public class WorldArrayNew
    {
        const int MOVEDIRECTIONSLENGTH = 8;
        private readonly bool[] _infectionRate = new bool[100];
        private volatile int _infectionRateIndex = 0;
        private static readonly MoveDirection[] moveDirections = (MoveDirection[])Enum.GetValues(typeof(MoveDirection));

        static readonly ThreadLocal<Random> random = new(() => new Random(int.MaxValue));
        private ConcurrentBag<Point> _diedPeople = new();
        private ConcurrentBag<PersonNew> _survivedPeople = new();
        private List<MovedPersonNew> _movedPeople = new();
        private List<PersonNew> _retryMove = new();
        private PersonNew[] _people;
        private int _pplAlive;
        private int _pplInfected;
        private int _pplContagious;

        private WorldUpdateNew _worldUpdate;

        #region Config
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

        private PersonNew?[,] _world;

        public WorldArrayNew(SimulationConfig config)
        {
            _people = new PersonNew[config.Population];
            _world = new PersonNew[config.Height, config.Width];
            _pplAlive = config.Population;
            _pplInfected = config.InitialInfected;
            _worldUpdate = new();
            SetLocalVariables(config);
            Initialize();
        }

        public ref WorldUpdateNew GetWorldRef() => ref _worldUpdate;

        public void Initialize()
        {
            var rnd = random.Value!;
            int x; int y;
            for (var i = 0; i < _population; i++)
            {
                // move right if the position is already occupied to enhance
                x = rnd.Next(0, _width);
                y = rnd.Next(0, _height);
                while (_world[y, x] is not null)
                {
                    x++;
                    if (x >= _width)
                    {
                        x = 0;
                        y++;
                        if (y >= _height)
                            y = 0;
                    }
                }

                var person = new PersonNew(y, x, (float)rnd.Next((int)(_initialResistanceMin * 10), (int)(_initialResistanceMax * 10)) / 10);

                _people[i] = person;
                _world[y, x] = _people[i];
            }

            for (var i = 0; i < _initialInfected; i++)
            {
                _people[i].IsInfected = true;
                _people[i].IncubationTime = (byte)rnd.Next(_incubationTimeMin, _incubationTimeMax);
                _people[i].DmgDelay = (byte)rnd.Next(_dmgDelayMin, _dmgDelayMax);
            }

            // randomly assign infection rate
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
        private void PersonOnInfection() => Interlocked.Increment(ref _pplInfected);
        private void PersonOnContagious() => Interlocked.Increment(ref _pplContagious);
        private void PersonCured()
        {
            Interlocked.Decrement(ref _pplInfected);
            Interlocked.Decrement(ref _pplContagious);
        }
        private void PersonDied(PersonNew person)
        {
            Interlocked.Decrement(ref _pplInfected);
            Interlocked.Decrement(ref _pplAlive);
            if (person.IsContagious) Interlocked.Decrement(ref _pplContagious);
        }
        private void Person_OnMoved(int previousX, int previousY, int newX, int newY, bool isContaigous, bool isInfected, float health)
        {
            _movedPeople.Add(new MovedPersonNew(previousX, previousY, newX, newY, isContaigous, isInfected, health));
        }
        #endregion

        public async Task Update()
        {
            _movedPeople.Clear();
            _diedPeople.Clear();
            _retryMove.Clear();
            _survivedPeople.Clear();

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

            _people = _survivedPeople.ToArray();

            _worldUpdate.MovedPeople = _movedPeople;
            _worldUpdate.DiedPeople = _diedPeople;
            _worldUpdate.PplAlive = _pplAlive;
            _worldUpdate.PplInfected = _pplInfected;
            _worldUpdate.PplContagious = _pplContagious;
        }

        private void MakePersonInfectedContagiousTakeDamageAndDie()
        {
            int peopleLength = _people.Length;
            Parallel.For(0, peopleLength, i =>
            {
                var person = _people[i];
                if (person.IsInfected && !person.IsContagious)
                {
                    person.IncubationTime--;
                    if (person.IncubationTime <= 0)
                    {
                        person.IsContagious = true;
                        PersonOnContagious();
                        person.ContagiousTime = (byte)(random.Value.Next(_contagiousTimeMin, _contagiousTimeMax));
                    }
                }
            });


            Parallel.For(0, peopleLength, i =>
            {
                var person = _people[i];
                if (person.IsContagious)
                {
                    InfectNeighborsParallelSafe(person, random.Value);
                    person.ContagiousTime--;
                    if (person.ContagiousTime <= 0)
                    {
                        PersonCured();
                        person.IsInfected = false;
                        person.IsContagious = false;
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
            });

            Parallel.For(0, peopleLength, i =>
            {
                var person = _people[i];
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

            Parallel.For(0, peopleLength, i =>
            {
                var person = _people[i];
                if (person.DaysOfImmunity > 0)
                    person.DaysOfImmunity--;

                if (person.Health <= 0)
                {
                    PersonDied(person);
                    person.Health = 0;
                    _diedPeople.Add(new Point(person.PositionX, person.PositionY));
                    _world[person.PositionY, person.PositionX] = null;
                }
                else
                    _survivedPeople.Add(person);
            });
        }

        private void RetryMovingPerson()
        {
            foreach (var person in _retryMove)
            {
                if (person.CanMove && !IsSurrounded(person))
                    MovePerson(person);
            }
        }

        private void HealAndTryToMovePerson()
        {
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

        private void InfectNeighborsParallelSafe(PersonNew person, Random rnd)
        {
            int width = _width;
            int height = _height;

            Parallel.For(person.PositionX - 1, person.PositionX + 2, x =>
            {
                for (var y = person.PositionY - 1; y <= person.PositionY + 1; y++)
                {
                    var wrappedX = (x + width) % width;
                    var wrappedY = (y + height) % height;

                    CheckAndInfect(new Point(wrappedX, wrappedY), rnd);
                }
            });
        }

        private void CheckAndInfect(Point pos, Random rnd)
        {
            var person = _world[pos.Y, pos.X];
            if (person is not null && person.IsSusceptibleToInfection())
            {
                var i = Interlocked.Increment(ref _infectionRateIndex);
                if (_infectionRate[i % 100])
                {
                    PersonOnInfection();
                    person.IsInfected = true;
                    person.IncubationTime = (byte)rnd.Next(_incubationTimeMin, _incubationTimeMax);
                    person.DmgDelay = (byte)rnd.Next(_dmgDelayMin, _dmgDelayMax);
                }
            }
        }

        private void DamagePerson(PersonNew person)
        {
            var baseDamage = random.Value.Next((int)(_damageMin * 10), (int)(_damageMax * 10)) / 10f;
            float damage;
            if (person.Resistance > 0 || person.AdditionalInfectionResistance > 0)
                damage = baseDamage * (1 - person.Resistance / 100f - person.AdditionalInfectionResistance / 100f);
            else
                damage = baseDamage;
            person.Health -= damage;
        }

        private void HealPerson(PersonNew person)
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

        private void MovePerson(PersonNew person)
        {
            random.Value!.Shuffle(moveDirections);
            Point newPosition = default;

            foreach (var moveDirection in moveDirections)
            {
                newPosition = GetNewPosition(moveDirection, person);
                if (_world[newPosition.Y, newPosition.X] is null)
                    break;
            }
            Person_OnMoved(person.PositionX, person.PositionY, newPosition.X, newPosition.Y, person.IsContagious, person.IsInfected, person.Health);
            _world[newPosition.Y, newPosition.X] = person;
            _world[person.PositionY, person.PositionX] = null;
        }

        private bool IsSurrounded(PersonNew person)
        {
            for (var xOffset = -1; xOffset <= 1; xOffset++)
            {
                for (var yOffset = -1; yOffset <= 1; yOffset++)
                {
                    var x = (person.PositionX + xOffset + _width) % _width;
                    var y = (person.PositionY + yOffset + _height) % _height;

                    if (_world[y, x] is null)
                        return false;
                }
            }

            return true;
        }

        private Point GetNewPosition(MoveDirection direction, PersonNew person)
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
                X = (person.PositionX + dx + _width) % _width,
                Y = (person.PositionY + dy + _height) % _height
            };

            return newPosition;
        }
    }
}