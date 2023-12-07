using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    public sealed record Virus(string Name, float InfectionRate, float MortalityRate);
    //{
    //    public string Name { get; set; }
    //    public float InfectionRate { get; set; }
    //    public float MortalityRate { get; set; }
    //}
}
