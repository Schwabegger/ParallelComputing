namespace PandemicSimulator
{
    partial class CreateVirusForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtName = new TextBox();
            nudMortalityRate = new NumericUpDown();
            nudInfectionRate = new NumericUpDown();
            btnSave = new Button();
            ((System.ComponentModel.ISupportInitialize)nudMortalityRate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudInfectionRate).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 0;
            label1.Text = "Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 37);
            label2.Name = "label2";
            label2.Size = new Size(77, 15);
            label2.TabIndex = 1;
            label2.Text = "Infection rate";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 66);
            label3.Name = "label3";
            label3.Size = new Size(78, 15);
            label3.TabIndex = 2;
            label3.Text = "Mortality rate";
            // 
            // txtName
            // 
            txtName.Location = new Point(93, 6);
            txtName.Name = "txtName";
            txtName.Size = new Size(120, 23);
            txtName.TabIndex = 3;
            // 
            // nudMortalityRate
            // 
            nudMortalityRate.Location = new Point(93, 64);
            nudMortalityRate.Name = "nudMortalityRate";
            nudMortalityRate.Size = new Size(120, 23);
            nudMortalityRate.TabIndex = 4;
            // 
            // nudInfectionRate
            // 
            nudInfectionRate.Location = new Point(93, 35);
            nudInfectionRate.Name = "nudInfectionRate";
            nudInfectionRate.Size = new Size(120, 23);
            nudInfectionRate.TabIndex = 5;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(12, 93);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(201, 23);
            btnSave.TabIndex = 6;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // CreateVirusForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(222, 122);
            Controls.Add(btnSave);
            Controls.Add(nudInfectionRate);
            Controls.Add(nudMortalityRate);
            Controls.Add(txtName);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "CreateVirusForm";
            Text = "CreateVirusForm";
            ((System.ComponentModel.ISupportInitialize)nudMortalityRate).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudInfectionRate).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtName;
        private NumericUpDown nudMortalityRate;
        private NumericUpDown nudInfectionRate;
        private Button btnSave;
    }
}