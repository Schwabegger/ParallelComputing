using System.Drawing;

namespace Simulator
{
    public class SimulationUpdateNewEventArgs : EventArgs
    {
        public int PeopleAlive { get; set; }
        public int PeopleInfected { get; set; }
        public int PeopleContagious { get; set; }
        public MovedPersonNew[] MovedPeople { get; set; }
        public Point[] PeopleDied { get; set; }
    }

    public sealed class SimulationNew(SimulationConfig config, CancellationToken cancellationToken) : ISimulation
    {
        public event EventHandler<SimulationEndEventArgs>? OnSimulationFinished;
        public event EventHandler<SimulationUpdateNewEventArgs>? OnSimulationUpdated;

        private readonly WorldArrayNew _world = new WorldArrayNew(config);

        private WorldUpdateNew _worldUpdateNewRef;

        /// <summary>
        /// Runs the simulation
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task Run()
        {
            if (!config.IsValid())
                throw new InvalidOperationException("Invalid simulation configuration");
            _worldUpdateNewRef = _world.GetWorldRef();
            do
            {
                await _world.Update();
                OnSimulationUpdated?.Invoke(this, new SimulationUpdateNewEventArgs
                {
                    PeopleAlive = _worldUpdateNewRef.PplAlive,
                    PeopleInfected = _worldUpdateNewRef.PplInfected,
                    PeopleContagious = _worldUpdateNewRef.PplContagious,
                    MovedPeople = _worldUpdateNewRef.MovedPeople.ToArray(),
                    PeopleDied = _worldUpdateNewRef.DiedPeople.ToArray()
                });
            } while (RunCondition());

            OnSimulationFinished?.Invoke(this, new SimulationEndEventArgs
            { 
                PeopleAlive = _worldUpdateNewRef.PplAlive,
                PeopleInfected = _worldUpdateNewRef.PplInfected,
                PeopleContagious = _worldUpdateNewRef.PplContagious
            });
        }

        private bool RunCondition()
        {
            return _worldUpdateNewRef.PplAlive > 0 && _worldUpdateNewRef.PplInfected > 0 && !cancellationToken.IsCancellationRequested;
        }
    }
}