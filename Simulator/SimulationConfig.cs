namespace Simulator
{
    public sealed class SimulationConfig
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Population { get; set; }
        public int InitialInfected { get; set; }
        public float InitialResistanceMin { get; set; }
        public float InitialResistanceMax { get; set; }
        public float MaxResistance { get; set; }
        public float IncreaseResistanceAfterCuringMin { get; set; }
        public float IncreaseResistanceAfterCuringMax { get; set; }
        public float AdditionalResistancePerDayWhenInfectedMin { get; set; }
        public float AdditionalResistancePerDayWhenInfectedMax { get; set; }
        public byte IncubationTimeMin { get; set; }
        public byte IncubationTimeMax { get; set; }
        public byte ImmunityMin { get; set; }
        public byte ImmunityMax { get; set; }
        public byte DmgDelayMin { get; set; }
        public byte DmgDelayMax { get; set; }
        public byte ContagiousTimeMin { get; set; }
        public byte ContagiousTimeMax { get; set; }
        public float HealAmountMin { get; set; }
        public float HealAmountMax { get; set; }

        public Virus? Virus { get; set; }

        /// <summary>
        /// Checks if the configuration is valid
        /// </summary>
        /// <returns>True if the configuration is valid, false if not</returns>
        public bool IsValid()
        {
            return CheckMaxValues() 
                && CheckMinValues() 
                && CheckMinSmallerMax()
                && Virus is not null;
        }

        private bool CheckMinValues()
        {
            return Width > 0
                && Height > 0
                && Population > 0
                && InitialInfected > 0
                && IncreaseResistanceAfterCuringMin >= 0
                && AdditionalResistancePerDayWhenInfectedMin >= 0
                && InitialResistanceMin >= 0
                && IncubationTimeMin > 0
                && ImmunityMin >= 0
                && DmgDelayMin >= 0
                && ContagiousTimeMin > 0;
        }

        private bool CheckMaxValues()
        {
            return Width < int.MaxValue
                && Height < int.MaxValue
                && Population < Width * Height
                && InitialInfected < int.MaxValue
                && InitialInfected <= Population
                && IncreaseResistanceAfterCuringMax < float.MaxValue
                && AdditionalResistancePerDayWhenInfectedMax < float.MaxValue
                && InitialResistanceMax < float.MaxValue
                && IncubationTimeMax < byte.MaxValue - 1
                && ImmunityMax < byte.MaxValue - 1
                && DmgDelayMax < byte.MaxValue - 1
                && ContagiousTimeMax < byte.MaxValue - 1;
        }

        private bool CheckMinSmallerMax()
        {
            return IncreaseResistanceAfterCuringMin <= IncreaseResistanceAfterCuringMax
                && AdditionalResistancePerDayWhenInfectedMin <= AdditionalResistancePerDayWhenInfectedMax
                && InitialResistanceMin <= InitialResistanceMax
                && IncubationTimeMin <= IncubationTimeMax
                && ImmunityMin <= ImmunityMax
                && DmgDelayMin <= DmgDelayMax
                && ContagiousTimeMin <= ContagiousTimeMax;
        }
    }
}