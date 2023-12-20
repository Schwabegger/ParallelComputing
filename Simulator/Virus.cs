namespace Simulator
{
    public sealed record Virus
    {
        public string Name { get; set; }
        public byte InfectionRate { get; set; }
        public float MortalityRate { get; set; }
        public float DamageMin { get; set; }
        public float DamageMax { get; set; }

        public Virus(string name, byte infectionRate, float mortalityRate, float damageMin, float damageMax)
        {
            Name = name;
            InfectionRate = infectionRate;
            MortalityRate = mortalityRate;
            DamageMin = damageMin;
            DamageMax = damageMax;
        }

        public bool IsValid() {
            return !string.IsNullOrWhiteSpace(Name)
                && CheckMaxValue()
                && CheckMinValue();
        }

        private bool CheckMinValue()
        {
            return InfectionRate > 0
                   && MortalityRate > 0
                   && DamageMin > 0
                   && DamageMax > DamageMax;
        }

        private bool CheckMaxValue()
        {
            return InfectionRate <= 100
                   && MortalityRate <= 100
                   && DamageMin <= DamageMax
                   && DamageMax <= 100;
        }
    }
}
