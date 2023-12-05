using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    public class World
    {
        private readonly Person?[,] _world;
        //private Person[] _people;

        public World(SimulationConfig config)
        {
            _world = new Person[config.Width, config.Height];
            _people = new Person[config.PopulationSize];
            //Init(config);
        }

        //private void Init(SimulationConfig config)
        //{
        //    Random rnd = new Random();
        //    for (int i = 0; i < config.PopulationSize; i++)
        //    {
        //        Point pos;
        //        do
        //        {
        //            pos = new Point(rnd.Next(0, config.Width), rnd.Next(0, config.Height));
        //        } while (GetPersonAt(pos) == null);

        //        _people[i] = new Person(config.PersonHealth, rnd.Next(0, 100) / 100f, pos);
        //    }
        //}

        public Person? GetPersonAt(Point position)
        {
            return _world[position.X, position.Y];
        }

        public void SimulateDay()
        {

        }
    }
}
