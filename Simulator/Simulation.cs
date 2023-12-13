using System.Drawing;

namespace Simulator
{
    public sealed class SimulationUpdateEventArgs : EventArgs
    {
        public required int PeopleAlive { get; set; }
        public required int PeopleInfected { get; set; }
        public required int PeopleContagious { get; set; }
        public required MovedPerson[] MovedPeople { get; set; }

        public required Point[] PeopleDied { get; set; }
    }

    public sealed class MovedPerson
    {
        public Point PreviousPosition { get; set; }
        public Point CurrentPosition { get; set; }
        public int Health { get; set; }
        public bool IsInfected { get; set; }
        public bool IsContagious { get; set; }
    }

    public sealed class Simulation
    {
        public CancellationToken CancellationToken { get; set; }
        public event EventHandler? OnSimulationFinished;
        public event EventHandler<SimulationUpdateEventArgs>? OnSimulationUpdated;

        private readonly SimulationConfig _config;
        private readonly World _world;
        List<MovedPerson> movedPeople = new();
        List<Point> diedPeople = new();
        Person[] people;
        int pplAlive;
        int pplInfected;
        int pplContagious;

        public Simulation(SimulationConfig config, CancellationToken cancellationToken)
        {
            _config = config;
            _world = new World(config);
            people = new Person[config.PopulationSize];
            pplAlive = config.PopulationSize;
            pplInfected = config.InitialInfectionRate;
            CancellationToken = cancellationToken;
        }

        public void Initialize()
        {
            Random rnd = new Random();
            for (int i = 0; i < _config.PopulationSize; i++)
            {
                Point pos;
                do
                {
                    pos = new Point(rnd.Next(0, _config.Width), rnd.Next(0, _config.Height));
                } while (_world.GetPersonAt(pos) == null);

                people[i] = new Person(_config.PersonHealth, rnd.Next(0, 100) / 100f, pos);
                people[i].OnInfection += Simulation_OnInfection;
                people[i].OnContagious += Simulation_OnContagious;
                people[i].OnDeath += Simulation_OnDeath;
                people[i].OnHealed += Simulation_OnHealed;
                people[i].OnMoved += Simulation_OnMoved;
            }
        }

        private void Simulation_OnInfection(object? sender, EventArgs e) => pplInfected++;
        private void Simulation_OnContagious(object? sender, EventArgs e) => pplContagious++;
        private void Simulation_OnHealed(object? sender, EventArgs e)
        {
            pplInfected--;
            pplContagious--;
        }
        private void Simulation_OnDeath(object? sender, EventArgs e)
        {
            pplInfected--;
            pplContagious--;
            pplAlive--;

            var person = (Person)sender!;
            person.OnInfection -= Simulation_OnInfection;
            person.OnContagious -= Simulation_OnContagious;
            person.OnDeath -= Simulation_OnDeath;
            person.OnHealed -= Simulation_OnHealed;
            person.OnMoved -= Simulation_OnMoved;
            people = people.Where(p => p != person).ToArray();
        }
        private void Simulation_OnMoved(object? sender, PersonMoveEventArgs e)
        {
            var person = (Person)sender!;
            movedPeople.Append(new MovedPerson
            {
                PreviousPosition = e.PreviousPosition,
                CurrentPosition = e.CurrentPosition,
                Health = person.Health
            });
        }

        /// <summary>
        /// Starts the simulation
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void Run()
        {
            if (!_config.IsValid())
                throw new InvalidOperationException("Invalid simulation configuration");

            while (RunCondition())
            {

                OnSimulationUpdated?.Invoke(this, new SimulationUpdateEventArgs
                {
                    PeopleAlive = pplAlive,
                    PeopleInfected = pplInfected,
                    PeopleContagious = pplContagious,
                    MovedPeople = movedPeople.ToArray(),
                    PeopleDied = diedPeople.ToArray()
                });
                movedPeople.Clear();
                diedPeople.Clear();
            }
            OnSimulationFinished?.Invoke(this, EventArgs.Empty);
        }

        private bool RunCondition()
        {
            return pplAlive > 0 && pplInfected > 0 && !CancellationToken.IsCancellationRequested;
        }
    }
}
