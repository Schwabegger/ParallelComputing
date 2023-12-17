namespace Simulator
{
    public sealed record Virus
    {
        public string Name { get; set; }
        public float InfectionRate { get; set; }
        public float MortalityRate { get; set; }
        public float DamageMin { get; set; }
        public float DamageMax { get; set; }

        public Virus(string name, float infectionRate, float mortalityRate, float damageMin, float damageMax)
        {
            Name = name;
            InfectionRate = infectionRate;
            MortalityRate = mortalityRate;
            DamageMin = damageMin;
            DamageMax = damageMax;
        }

        public bool IsValid() {
            return !string.IsNullOrWhiteSpace(Name)
                   && InfectionRate > 0
                   && MortalityRate > 0
                   && DamageMin > 0
                   && DamageMax >= DamageMin;
        }
    }
}
