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

    public sealed class SimulationEndEventArgs : EventArgs
    {
        public required int PeopleAlive { get; set; }
        public required int PeopleInfected { get; set; }
        public required int PeopleContagious { get; set; }
        public required Person[] People { get; set; }
    }

    public sealed class Simulation(SimulationConfig config, CancellationToken cancellationToken)
    {
        public event EventHandler<SimulationEndEventArgs>? OnSimulationFinished;
        public event EventHandler<SimulationUpdateEventArgs>? OnSimulationUpdated;

        private readonly World _world = new(config);

        private WorldUpdate _worldUpdate;

        /// <summary>
        /// Runs the simulation
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task Run()
        {
            if (!config.IsValid())
                throw new InvalidOperationException("Invalid simulation configuration");

            do
            {
                _worldUpdate = _world.Update();
                OnSimulationUpdated?.Invoke(this, new SimulationUpdateEventArgs
                {
                    PeopleAlive = _worldUpdate.PplAlive,
                    PeopleInfected = _worldUpdate.PplInfected,
                    PeopleContagious = _worldUpdate.PplContagious,
                    MovedPeople = _worldUpdate.MovedPeople.ToArray(),
                    PeopleDied = _worldUpdate.DiedPeople.ToArray()
                });
            } while (RunCondition());

            OnSimulationFinished?.Invoke(this, new SimulationEndEventArgs
            { 
                People = _world.People.ToArray(),
                PeopleAlive = _worldUpdate.PplAlive,
                PeopleInfected = _worldUpdate.PplInfected,
                PeopleContagious = _worldUpdate.PplContagious
            });
        }

        private bool RunCondition()
        {
            return _worldUpdate.PplAlive > 0 && _worldUpdate.PplInfected > 0 && !cancellationToken.IsCancellationRequested;
        }
    }
}