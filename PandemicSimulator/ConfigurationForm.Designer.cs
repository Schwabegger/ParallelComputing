namespace PandemicSimulator
{
    partial class ConfigurationForm
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
            nudInitialInfected = new NumericUpDown();
            nudPopulation = new NumericUpDown();
            nudHeight = new NumericUpDown();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            nudWidth = new NumericUpDown();
            cbViruses = new ComboBox();
            label5 = new Label();
            btnCreateVirus = new Button();
            ((System.ComponentModel.ISupportInitialize)nudInitialInfected).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPopulation).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudWidth).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 1;
            label1.Text = "Width";
            // 
            // nudInitialInfected
            // 
            nudInitialInfected.Location = new Point(100, 94);
            nudInitialInfected.Name = "nudInitialInfected";
            nudInitialInfected.Size = new Size(120, 23);
            nudInitialInfected.TabIndex = 2;
            nudInitialInfected.ValueChanged += nudInitialInfected_ValueChanged;
            // 
            // nudPopulation
            // 
            nudPopulation.Location = new Point(100, 65);
            nudPopulation.Name = "nudPopulation";
            nudPopulation.Size = new Size(120, 23);
            nudPopulation.TabIndex = 3;
            nudPopulation.ValueChanged += nudPopulation_ValueChanged;
            // 
            // nudHeight
            // 
            nudHeight.Location = new Point(100, 36);
            nudHeight.Name = "nudHeight";
            nudHeight.Size = new Size(120, 23);
            nudHeight.TabIndex = 4;
            nudHeight.ValueChanged += nudHeight_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 38);
            label2.Name = "label2";
            label2.Size = new Size(43, 15);
            label2.TabIndex = 5;
            label2.Text = "Height";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 67);
            label3.Name = "label3";
            label3.Size = new Size(65, 15);
            label3.TabIndex = 6;
            label3.Text = "Population";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 96);
            label4.Name = "label4";
            label4.Size = new Size(82, 15);
            label4.TabIndex = 7;
            label4.Text = "Initial infected";
            // 
            // nudWidth
            // 
            nudWidth.Location = new Point(100, 7);
            nudWidth.Name = "nudWidth";
            nudWidth.Size = new Size(120, 23);
            nudWidth.TabIndex = 8;
            nudWidth.ValueChanged += nudWidth_ValueChanged;
            // 
            // cbViruses
            // 
            cbViruses.FormattingEnabled = true;
            cbViruses.Location = new Point(100, 123);
            cbViruses.Name = "cbViruses";
            cbViruses.Size = new Size(121, 23);
            cbViruses.TabIndex = 9;
            cbViruses.DataContextChanged += cbViruses_DataContextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 126);
            label5.Name = "label5";
            label5.Size = new Size(33, 15);
            label5.TabIndex = 10;
            label5.Text = "Virus";
            // 
            // btnCreateVirus
            // 
            btnCreateVirus.Font = new Font("Segoe UI", 8F);
            btnCreateVirus.Location = new Point(71, 123);
            btnCreateVirus.Name = "btnCreateVirus";
            btnCreateVirus.Size = new Size(23, 23);
            btnCreateVirus.TabIndex = 11;
            btnCreateVirus.Text = "+";
            btnCreateVirus.TextAlign = ContentAlignment.TopCenter;
            btnCreateVirus.UseVisualStyleBackColor = true;
            btnCreateVirus.Click += btnCreateVirus_Click;
            // 
            // ConfigurationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(227, 152);
            Controls.Add(btnCreateVirus);
            Controls.Add(label5);
            Controls.Add(cbViruses);
            Controls.Add(nudWidth);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(nudHeight);
            Controls.Add(nudPopulation);
            Controls.Add(nudInitialInfected);
            Controls.Add(label1);
            Name = "ConfigurationForm";
            Text = "Configuration";
            ((System.ComponentModel.ISupportInitialize)nudInitialInfected).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPopulation).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudWidth).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private NumericUpDown nudInitialInfected;
        private NumericUpDown nudPopulation;
        private NumericUpDown nudHeight;
        private Label label2;
        private Label label3;
        private Label label4;
        private NumericUpDown nudWidth;
        private ComboBox cbViruses;
        private Label label5;
        private Button btnCreateVirus;
    }
}