using Simulator;

namespace PandemicSimulator
{
    public partial class ConfigurationForm : Form
    {
        public SimulationConfig? Configuration { get; } = new();

        public ConfigurationForm()
        {
            InitializeComponent();

            // Set the minimum values and increments for the numeric up down controls
            nudWidth.Increment = 1;
            nudWidth.Minimum = 20;
            nudHeight.Increment = 1;
            nudHeight.Minimum = 20;
            nudPopulation.Increment = 1;
            nudPopulation.Minimum = 10;
            nudInitialInfected.Increment = 1;
            nudInitialInfected.Minimum = 1;
        }

        private void btnCreateVirus_Click(object sender, EventArgs e)
        {
            using (var createVirusForm = new CreateVirusForm())
            {
                if (createVirusForm.ShowDialog() == DialogResult.OK)
                {
                    var virus = createVirusForm.CreatedVirus;
                    cbViruses.Items.Add(virus);
                }
            }
        }

        /// <summary>
        /// Updates the configuration when the width is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudWidth_ValueChanged(object sender, EventArgs e)
        {
            Configuration.Width = (int)nudWidth.Value;
        }

        /// <summary>
        /// Updates the configuration when the height is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudHeight_ValueChanged(object sender, EventArgs e)
        {
            Configuration.Height = (int)nudHeight.Value;
        }

        /// <summary>
        /// Updates the configuration when the person health is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudPopulation_ValueChanged(object sender, EventArgs e)
        {
            Configuration.PopulationSize = (int)nudPopulation.Value;
        }

        /// <summary>
        /// Updates the configuration when the initial infection rate is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudInitialInfected_ValueChanged(object sender, EventArgs e)
        {
            Configuration.InitialInfectionRate = (int)nudInitialInfected.Value;
        }

        /// <summary>
        /// Updates the configuration when the virus is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbViruses_DataContextChanged(object sender, EventArgs e)
        {
            Configuration.Virus = (Virus)cbViruses.SelectedItem;
        }
    }
}