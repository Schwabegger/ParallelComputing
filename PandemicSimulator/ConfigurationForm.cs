﻿using Simulator;
using System.Reflection;
using System.Text.Json;

namespace PandemicSimulator
{
    public partial class ConfigurationForm : Form
    {
        static HashSet<Virus> Viruses = new();
        public SimulationConfig Configuration { get; private set; } = new();

        public ConfigurationForm()
        {
            InitializeComponent();
            InitializeNumericUpDownControls();
            LoadVirusesFromDisk();
            LoadConfigurationFromDisk();
            CenterToParent();
            // Disable resizing
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void InitializeNumericUpDownControls()
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
            nudIncubationTimeMin.Maximum = byte.MaxValue - 1;
            nudIncubationTimeMax.Increment = 1;
            nudIncubationTimeMax.Minimum = nudIncubationTimeMin.Minimum;
            nudIncubationTimeMax.Maximum = nudIncubationTimeMin.Maximum;

            nudContagiousTimeMin.Increment = 1;
            nudContagiousTimeMin.Minimum = 1;
            nudContagiousTimeMin.Maximum = byte.MaxValue - 1;
            nudContagiousTimeMax.Increment = 1;
            nudContagiousTimeMax.Minimum = nudContagiousTimeMin.Minimum;
            nudContagiousTimeMax.Maximum = nudContagiousTimeMin.Maximum;

            nudDmgDelayMin.Increment = 1;
            nudDmgDelayMin.Minimum = 1;
            nudDmgDelayMin.Maximum = byte.MaxValue - 1;
            nudDmgDelayMax.Increment = 1;
            nudDmgDelayMax.Minimum = nudDmgDelayMin.Minimum;
            nudDmgDelayMax.Maximum = nudDmgDelayMin.Maximum;

            nudImmunityMin.Increment = 1;
            nudImmunityMin.Minimum = 1;
            nudImmunityMin.Maximum = byte.MaxValue - 1;
            nudImmunityMax.Increment = 1;
            nudImmunityMax.Minimum = nudImmunityMin.Minimum;
            nudImmunityMax.Maximum = nudImmunityMin.Maximum;

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

        private void LoadConfigurationFromDisk()
        {
            if (!File.Exists("Configuration.json"))
                return;
            var config = JsonSerializer.Deserialize<SimulationConfig>(File.ReadAllText("Configuration.json"));
            if (config is not null)
            {
                Configuration = config;
                AssignConfigurationToControls();
            }
        }

        private void AssignConfigurationToControls()
        {
            // Use reflection to get the properties from Config
            PropertyInfo[]? propertyInfos = Configuration.GetType().GetProperties();

            if (propertyInfos is not null)
            {
                foreach (var propertyInfo in propertyInfos)
                {
                    // Get the value of the property
                    object? value = propertyInfo.GetValue(Configuration);

                    // Get the name of the property
                    string propertyName = propertyInfo.Name;

                    // Get the control with the name of the property
                    Control? control = Controls.Find("nud" + propertyName, true).FirstOrDefault();

                    if (control is not null)
                    {
                        // Convert the value to the appropriate type
                        object? typedValue = Convert.ChangeType(value, ((NumericUpDown)(control)).Value.GetType());

                        // Set the control value
                        control.GetType().GetProperty("Value")?.SetValue(control, typedValue);
                    }
                    else
                    {
                        // Handle the case where the control is not found
                        Console.WriteLine($"Control 'nud{propertyName}' not found.");
                    }
                }
            }
            else
            {
                // Handle the case where the property is not found
                Console.WriteLine($"Properties not found.");
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
            // Save the configuration to disk
            if (Configuration.IsValid())
            {
                var jsonConfig = JsonSerializer.Serialize(Configuration);
                File.WriteAllText("Configuration.json", jsonConfig);
            }

            // Save the viruses to disk
            if (Viruses.Count > 0)
            {
                var jsonViruses = JsonSerializer.Serialize(Viruses);
                File.WriteAllText("Viruses.json", jsonViruses);
            }
        }
    }
}