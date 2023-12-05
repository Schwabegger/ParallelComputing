using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    public class SimulationConfig
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int PersonHealth { get; set; }
        public int PopulationSize { get; set; }
        public float InitialInfectionRate { get; set; }
        public Virus Virus { get; set; }

        /// <summary>
        /// Checks if the configuration is valid
        /// </summary>
        /// <returns>True if the configuration is valid, false if not</returns>
        public bool IsValid()
        {
            return Width > 0 && Height > 0 && PersonHealth > 0 && PopulationSize > 0 && InitialInfectionRate > 0 && Virus != null && PopulationSize < Width * Height;
        }
    }
}