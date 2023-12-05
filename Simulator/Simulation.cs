using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    public class Simulation
    {
        private readonly SimulationConfig _config;
        private readonly World _world;
        Person[] people;
        int pplAlive;
        int pplInfected;

        public Simulation(SimulationConfig config)
        {
            _config = config;
            _world = new World(config);
            people = new Person[config.PopulationSize];
            pplAlive = config.PopulationSize;
            pplInfected = config.InitialInfectionRate;
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
            }
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

            }
        }

        private bool RunCondition()
        {
            return pplAlive > 0 && pplInfected > 0;
        }
    }
}
