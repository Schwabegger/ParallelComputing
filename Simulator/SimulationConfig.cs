﻿namespace Simulator
{
    public sealed class SimulationConfig
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Population { get; set; }
        public int InitialInfected { get; set; }
        public float InitialResistanceMin { get; set; }
        public float InitialResistanceMax { get; set; }
        public float MacResistance { get; set; }
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
            return Width > 0
                && Height > 0
                && Population > 0
                && InitialInfected > 0
                && InitialInfected <= Population
                && IncreaseResistanceAfterCuringMin > 0
                && IncreaseResistanceAfterCuringMax >= IncreaseResistanceAfterCuringMin
                && AdditionalResistancePerDayWhenInfectedMin > 0
                && AdditionalResistancePerDayWhenInfectedMax >= AdditionalResistancePerDayWhenInfectedMin
                && InitialResistanceMin > 0
                && InitialResistanceMax >= InitialResistanceMin
                && IncubationTimeMin > 0
                && IncubationTimeMax >= IncubationTimeMin
                && ImmunityMin > 0
                && ImmunityMax >= ImmunityMin
                && DmgDelayMin > 0
                && DmgDelayMax >= DmgDelayMin
                && ContagiousTimeMin > 0
                && ContagiousTimeMax >= ContagiousTimeMin
                && Virus is not null;
        }
    }
}