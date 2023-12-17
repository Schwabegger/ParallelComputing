using Simulator;
using System.Reflection;
using System.Text.Json;

namespace PandemicSimulator
{
    public partial class ConfigurationForm : Form
    {
        static HashSet<Virus> Viruses = new();
        public SimulationConfig Configuration { get; } = new();

        public ConfigurationForm()
        {
            InitializeComponent();
            InitialiseNumericUpDownControls();
            CenterToScreen();
            LoadVirusesFromDisk();
        }

        private void InitialiseNumericUpDownControls()
        {
            // Set the minimum values and increments for the numeric up down controls for the world
            nudWidth.Increment = 1;
            nudWidth.Minimum = 10;
            nudHeight.Increment = 1;
            nudHeight.Minimum = 10;

            nudPopulation.Increment = 1;
            nudPopulation.Minimum = 10;

            nudInitialInfected.Increment = 1;
            nudInitialInfected.Minimum = 1;

            // Set the minimum values and increments for the numeric up down controls for the person
            nudInitialResistanceMin.Increment = 0.1M;
            nudInitialResistanceMin.Minimum = 3.0M;
            nudInitialResistanceMax.Increment = 0.1M;
            nudInitialResistanceMax.Minimum = nudInitialResistanceMin.Minimum;

            nudIncreaseResistanceAfterCuringMin.Increment = 0.1M;
            nudIncreaseResistanceAfterCuringMin.Minimum = 0.1M;
            nudIncreaseResistanceAfterCuringMax.Increment = 0.1M;
            nudIncreaseResistanceAfterCuringMax.Minimum = nudIncreaseResistanceAfterCuringMin.Minimum;

            nudAdditionalResistancePerDayWhenInfectedMin.Increment = 0.1M;
            nudAdditionalResistancePerDayWhenInfectedMin.Minimum = 0.1M;
            nudAdditionalResistancePerDayWhenInfectedMax.Increment = 0.1M;
            nudAdditionalResistancePerDayWhenInfectedMax.Minimum = nudAdditionalResistancePerDayWhenInfectedMin.Minimum;

            // Set the minimum values and increments for the numeric up down controls for the durations
            nudIncubationTimeMin.Increment = 1;
            nudIncubationTimeMin.Minimum = 1;
            nudIncubationTimeMax.Increment = 1;
            nudIncubationTimeMax.Minimum = nudIncubationTimeMin.Minimum;

            nudContagiousTimeMin.Increment = 1;
            nudContagiousTimeMin.Minimum = 1;
            nudContagiousTimeMax.Increment = 1;
            nudContagiousTimeMax.Minimum = nudContagiousTimeMin.Minimum;

            nudDmgDelayMin.Increment = 1;
            nudDmgDelayMin.Minimum = 1;
            nudDmgDelayMax.Increment = 1;
            nudDmgDelayMax.Minimum = nudDmgDelayMin.Minimum;

            nudImmunityMin.Increment = 1;
            nudImmunityMin.Minimum = 1;
            nudImmunityMax.Increment = 1;
            nudImmunityMax.Minimum = nudImmunityMin.Minimum;

            // Set the minimum values and increments for the numeric up down controls for the health
            nudHealAmountMin.Increment = 0.1M;
            nudHealAmountMin.Minimum = 5.0M;
            nudHealAmountMax.Increment = 0.1M;
            nudHealAmountMax.Minimum = nudHealAmountMin.Minimum;
        }

        private void LoadVirusesFromDisk()
        {
            if (!File.Exists("Viruses.json"))
                return;

            var viruses = JsonSerializer.Deserialize<HashSet<Virus>>(File.ReadAllText("Viruses.json"));
            if (viruses is not null)
            {
                foreach (var virus in viruses)
                {
                    Viruses.Add(virus);
                    cbViruses.Items.Add(virus);
                }
            }
        }

        private void btnCreateVirus_Click(object sender, EventArgs e)
        {
            using (var createVirusForm = new CreateVirusForm())
            {
                if (createVirusForm.ShowDialog() == DialogResult.OK)
                {
                    var virus = createVirusForm.CreatedVirus;
                    if (virus != null)
                    {
                        Viruses.Add(virus);
                        cbViruses.Items.Add(virus);
                        Viruses.Add(virus);
                        cbViruses.SelectedItem = virus;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Nud_ValueChanged(object sender, EventArgs e)
        {
            var nud = (NumericUpDown)sender;
            var name = nud.Name;
            var value = nud.Value;

            AssignNumericUpDownToConfig(name[3..], (float)value);

            if (name.EndsWith("Min"))
            {
                var maxNud = (NumericUpDown)Controls.Find(name[..^3] + "Max", true).FirstOrDefault();
                if (maxNud is not null && maxNud.Value < value)
                {
                    maxNud.Value = value;
                }
            }
            else if (name.EndsWith("Max"))
            {
                var minNud = (NumericUpDown)Controls.Find(name[..^3] + "Min", true).FirstOrDefault();
                if (minNud is not null && minNud.Value > value)
                {
                    minNud.Value = value;
                }
            }
        }

        private void AssignNumericUpDownToConfig(string propertyName, float value)
        {
            // Use reflection to get the property from Config
            PropertyInfo? propertyInfo = Configuration.GetType().GetProperty(propertyName);

            if (propertyInfo is not null)
            {
                // Convert the value to the appropriate type
                object typedValue = Convert.ChangeType(value, propertyInfo.PropertyType);

                // Set the property value
                propertyInfo.SetValue(Configuration, typedValue);
            }
            else
            {
                // Handle the case where the property is not found or has a different type
                Console.WriteLine($"Property '{propertyName}' not found or has an incompatible type.");
            }
        }

        private void cbViruses_SelectedIndexChanged(object sender, EventArgs e)
        {
            Configuration.Virus = (Virus)cbViruses.SelectedItem;
        }

        private void ConfigurationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Viruses.Count == 0)
                return;

            // Save the viruses to disk
            var json = JsonSerializer.Serialize(Viruses);
            File.WriteAllText("Viruses.json", json);
        }
    }
}