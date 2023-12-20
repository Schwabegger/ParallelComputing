using Simulator;

namespace PandemicSimulator
{
    public partial class CreateVirusForm : Form
    {
        public Virus CreatedVirus { get; private set; }

        public CreateVirusForm()
        {
            InitializeComponent();

            // Set the minimum values and increments for the numeric up down controls
            nudInfectionRate.Increment = 1;
            nudInfectionRate.Minimum = 1;
            nudMortalityRate.Increment = 0.1M;
            nudMortalityRate.Minimum = 1M;
            nudDamageMin.Increment = 0.1M;
            nudDamageMin.Minimum = 1M;
            nudDamageMax.Increment = nudDamageMin.Increment;
            nudDamageMax.Minimum = nudDamageMin.Minimum;
            nudDamageMax.Maximum = 100M;
            nudDamageMin.Maximum = nudDamageMax.Maximum;
            txtName.Text = "Test";
            // Disable resizing
            FormBorderStyle = FormBorderStyle.FixedSingle;
            CenterToScreen();
        }

        /// <summary>
        /// Creates a new virus when the save button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            var errors = ValidateInput();
            if (errors != "")
            {
                MessageBox.Show(errors);
                return;
            }

            CreatedVirus = new Virus(txtName.Text, (byte)nudInfectionRate.Value, (float)nudMortalityRate.Value, (float)nudDamageMin.Value, (float)nudDamageMax.Value);
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Validates the input
        /// </summary>
        /// <returns></returns>
        private string ValidateInput()
        {
            string errorMessages = "";
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                errorMessages += "Please enter a name for the virus.\n";
            }
            if (nudInfectionRate.Value <= 0)
            {
                errorMessages += "Please enter an infection rate for the virus.\n";
            }
            if (nudMortalityRate.Value <= 0)
            {
                errorMessages += "Please enter a mortality rate for the virus.\n";
            }
            return errorMessages;
        }
    }
}