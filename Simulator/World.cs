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

        public Person? GetPersonAt(Point position)
        {
            return _worldMap[position.X, position.Y];
        }

        public void SimulateDay()
        {

        }
    }
}