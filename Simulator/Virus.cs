namespace Simulator
{
    public sealed class Virus(string name, float infectionRate, float mortalityRate, float damageMin, float damageMax)
    {
        public string Name { get; set; } = name;
        public float InfectionRate { get; set; } = infectionRate;
        public float MortalityRate { get; set; } = mortalityRate;
        public float DamageMin { get; set; } = damageMin;
        public float DamageMax { get; set; } = damageMax;

        public bool IsValid() {
            return !string.IsNullOrWhiteSpace(Name)
                   && InfectionRate > 0
                   && MortalityRate > 0
                   && DamageMin > 0
                   && DamageMax >= DamageMin;
        }
    }
}
