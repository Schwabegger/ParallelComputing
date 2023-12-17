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
            nudDamageMin = new NumericUpDown();
            label4 = new Label();
            nudDamageMax = new NumericUpDown();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)nudMortalityRate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudInfectionRate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDamageMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDamageMax).BeginInit();
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
            txtName.Size = new Size(119, 23);
            txtName.TabIndex = 3;
            // 
            // nudMortalityRate
            // 
            nudMortalityRate.DecimalPlaces = 1;
            nudMortalityRate.Location = new Point(93, 64);
            nudMortalityRate.Name = "nudMortalityRate";
            nudMortalityRate.Size = new Size(119, 23);
            nudMortalityRate.TabIndex = 4;
            // 
            // nudInfectionRate
            // 
            nudInfectionRate.DecimalPlaces = 1;
            nudInfectionRate.Location = new Point(93, 35);
            nudInfectionRate.Name = "nudInfectionRate";
            nudInfectionRate.Size = new Size(119, 23);
            nudInfectionRate.TabIndex = 5;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(12, 151);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(200, 23);
            btnSave.TabIndex = 6;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // nudDamageMin
            // 
            nudDamageMin.DecimalPlaces = 1;
            nudDamageMin.Location = new Point(93, 93);
            nudDamageMin.Name = "nudDamageMin";
            nudDamageMin.Size = new Size(119, 23);
            nudDamageMin.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 95);
            label4.Name = "label4";
            label4.Size = new Size(75, 15);
            label4.TabIndex = 7;
            label4.Text = "Damage min";
            // 
            // nudDamageMax
            // 
            nudDamageMax.DecimalPlaces = 1;
            nudDamageMax.Location = new Point(93, 122);
            nudDamageMax.Name = "nudDamageMax";
            nudDamageMax.Size = new Size(119, 23);
            nudDamageMax.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 124);
            label5.Name = "label5";
            label5.Size = new Size(77, 15);
            label5.TabIndex = 9;
            label5.Text = "Damage max";
            // 
            // CreateVirusForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(224, 180);
            Controls.Add(nudDamageMax);
            Controls.Add(label5);
            Controls.Add(nudDamageMin);
            Controls.Add(label4);
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
            ((System.ComponentModel.ISupportInitialize)nudDamageMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDamageMax).EndInit();
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
        private NumericUpDown nudDamageMin;
        private Label label4;
        private NumericUpDown nudDamageMax;
        private Label label5;
    }
}