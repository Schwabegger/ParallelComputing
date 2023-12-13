using System.Drawing;

namespace Simulator
{
    public class World
    {
        private readonly Person?[,] _worldMap;
        //private Person[] _people;

        public World(SimulationConfig config)
        {
            _worldMap = new Person[config.Width, config.Height];
            //_people = new Person[config.PopulationSize];
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
            return _worldMap[position.X, position.Y];
        }

        public void SimulateDay()
        {

        }
    }
}