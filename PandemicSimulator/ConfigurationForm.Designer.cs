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
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            numericUpDown1 = new NumericUpDown();
            numericUpDown2 = new NumericUpDown();
            groupBox3 = new GroupBox();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            numericUpDown3 = new NumericUpDown();
            numericUpDown4 = new NumericUpDown();
            label10 = new Label();
            label11 = new Label();
            numericUpDown5 = new NumericUpDown();
            numericUpDown6 = new NumericUpDown();
            groupBox4 = new GroupBox();
            label12 = new Label();
            label13 = new Label();
            numericUpDown7 = new NumericUpDown();
            numericUpDown8 = new NumericUpDown();
            label14 = new Label();
            label15 = new Label();
            numericUpDown9 = new NumericUpDown();
            numericUpDown10 = new NumericUpDown();
            label16 = new Label();
            label17 = new Label();
            numericUpDown11 = new NumericUpDown();
            numericUpDown12 = new NumericUpDown();
            label18 = new Label();
            label19 = new Label();
            numericUpDown13 = new NumericUpDown();
            numericUpDown14 = new NumericUpDown();
            groupBox5 = new GroupBox();
            label24 = new Label();
            label25 = new Label();
            numericUpDown19 = new NumericUpDown();
            numericUpDown20 = new NumericUpDown();
            groupBox6 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)nudInitialInfected).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPopulation).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudWidth).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown6).BeginInit();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown11).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown13).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown14).BeginInit();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown19).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown20).BeginInit();
            groupBox6.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 19);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 1;
            label1.Text = "Width";
            label1.Click += label1_Click;
            // 
            // nudInitialInfected
            // 
            nudInitialInfected.Location = new Point(94, 104);
            nudInitialInfected.Name = "nudInitialInfected";
            nudInitialInfected.Size = new Size(120, 23);
            nudInitialInfected.TabIndex = 2;
            nudInitialInfected.ValueChanged += nudInitialInfected_ValueChanged;
            // 
            // nudPopulation
            // 
            nudPopulation.Location = new Point(94, 75);
            nudPopulation.Name = "nudPopulation";
            nudPopulation.Size = new Size(120, 23);
            nudPopulation.TabIndex = 3;
            nudPopulation.ValueChanged += nudPopulation_ValueChanged;
            // 
            // nudHeight
            // 
            nudHeight.Location = new Point(94, 46);
            nudHeight.Name = "nudHeight";
            nudHeight.Size = new Size(120, 23);
            nudHeight.TabIndex = 4;
            nudHeight.ValueChanged += nudHeight_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 48);
            label2.Name = "label2";
            label2.Size = new Size(43, 15);
            label2.TabIndex = 5;
            label2.Text = "Height";
            label2.Click += this.label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 77);
            label3.Name = "label3";
            label3.Size = new Size(65, 15);
            label3.TabIndex = 6;
            label3.Text = "Population";
            label3.Click += this.label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 106);
            label4.Name = "label4";
            label4.Size = new Size(82, 15);
            label4.TabIndex = 7;
            label4.Text = "Initial infected";
            label4.Click += this.label4_Click;
            // 
            // nudWidth
            // 
            nudWidth.Location = new Point(94, 17);
            nudWidth.Name = "nudWidth";
            nudWidth.Size = new Size(120, 23);
            nudWidth.TabIndex = 8;
            nudWidth.ValueChanged += nudWidth_ValueChanged;
            // 
            // cbViruses
            // 
            cbViruses.FormattingEnabled = true;
            cbViruses.Location = new Point(93, 23);
            cbViruses.Name = "cbViruses";
            cbViruses.Size = new Size(121, 23);
            cbViruses.TabIndex = 9;
            cbViruses.DataContextChanged += cbViruses_DataContextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 26);
            label5.Name = "label5";
            label5.Size = new Size(33, 15);
            label5.TabIndex = 10;
            label5.Text = "Virus";
            // 
            // btnCreateVirus
            // 
            btnCreateVirus.Font = new Font("Segoe UI", 8F);
            btnCreateVirus.Location = new Point(64, 23);
            btnCreateVirus.Name = "btnCreateVirus";
            btnCreateVirus.Size = new Size(23, 23);
            btnCreateVirus.TabIndex = 11;
            btnCreateVirus.Text = "+";
            btnCreateVirus.TextAlign = ContentAlignment.TopCenter;
            btnCreateVirus.UseVisualStyleBackColor = true;
            btnCreateVirus.Click += btnCreateVirus_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(nudWidth);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(nudInitialInfected);
            groupBox1.Controls.Add(nudPopulation);
            groupBox1.Controls.Add(nudHeight);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label3);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(227, 140);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            groupBox1.Text = "World";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(groupBox5);
            groupBox2.Controls.Add(groupBox4);
            groupBox2.Controls.Add(groupBox3);
            groupBox2.Location = new Point(245, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(557, 357);
            groupBox2.TabIndex = 13;
            groupBox2.TabStop = false;
            groupBox2.Text = "Person";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(412, 53);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(120, 23);
            numericUpDown1.TabIndex = 9;
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(268, 53);
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(120, 23);
            numericUpDown2.TabIndex = 10;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label10);
            groupBox3.Controls.Add(label11);
            groupBox3.Controls.Add(numericUpDown5);
            groupBox3.Controls.Add(numericUpDown6);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(numericUpDown3);
            groupBox3.Controls.Add(numericUpDown4);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(numericUpDown1);
            groupBox3.Controls.Add(numericUpDown2);
            groupBox3.Location = new Point(6, 22);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(542, 118);
            groupBox3.TabIndex = 13;
            groupBox3.TabStop = false;
            groupBox3.Text = "Resistance";
            groupBox3.Enter += groupBox3_Enter;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(394, 55);
            label6.Name = "label6";
            label6.Size = new Size(12, 15);
            label6.TabIndex = 11;
            label6.Text = "/";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 55);
            label7.Name = "label7";
            label7.Size = new Size(242, 15);
            label7.TabIndex = 12;
            label7.Text = "Increase resistance after curing (%) min/max";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 26);
            label8.Name = "label8";
            label8.Size = new Size(164, 15);
            label8.TabIndex = 16;
            label8.Text = "Initial resistance (%) min/max";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(394, 26);
            label9.Name = "label9";
            label9.Size = new Size(12, 15);
            label9.TabIndex = 15;
            label9.Text = "/";
            // 
            // numericUpDown3
            // 
            numericUpDown3.Location = new Point(412, 24);
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(120, 23);
            numericUpDown3.TabIndex = 13;
            // 
            // numericUpDown4
            // 
            numericUpDown4.Location = new Point(268, 24);
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(120, 23);
            numericUpDown4.TabIndex = 14;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(6, 84);
            label10.Name = "label10";
            label10.Size = new Size(256, 15);
            label10.TabIndex = 20;
            label10.Text = "Additional resistamce each day of infection (%)";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(394, 84);
            label11.Name = "label11";
            label11.Size = new Size(12, 15);
            label11.TabIndex = 19;
            label11.Text = "/";
            // 
            // numericUpDown5
            // 
            numericUpDown5.Location = new Point(412, 82);
            numericUpDown5.Name = "numericUpDown5";
            numericUpDown5.Size = new Size(120, 23);
            numericUpDown5.TabIndex = 17;
            // 
            // numericUpDown6
            // 
            numericUpDown6.Location = new Point(268, 82);
            numericUpDown6.Name = "numericUpDown6";
            numericUpDown6.Size = new Size(120, 23);
            numericUpDown6.TabIndex = 18;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(label18);
            groupBox4.Controls.Add(label19);
            groupBox4.Controls.Add(numericUpDown13);
            groupBox4.Controls.Add(numericUpDown14);
            groupBox4.Controls.Add(label12);
            groupBox4.Controls.Add(label13);
            groupBox4.Controls.Add(numericUpDown7);
            groupBox4.Controls.Add(numericUpDown8);
            groupBox4.Controls.Add(label14);
            groupBox4.Controls.Add(label15);
            groupBox4.Controls.Add(numericUpDown9);
            groupBox4.Controls.Add(numericUpDown10);
            groupBox4.Controls.Add(label16);
            groupBox4.Controls.Add(label17);
            groupBox4.Controls.Add(numericUpDown11);
            groupBox4.Controls.Add(numericUpDown12);
            groupBox4.Location = new Point(6, 146);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(542, 138);
            groupBox4.TabIndex = 21;
            groupBox4.TabStop = false;
            groupBox4.Text = "Durations";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(6, 84);
            label12.Name = "label12";
            label12.Size = new Size(169, 15);
            label12.TabIndex = 20;
            label12.Text = "Damage delay (days) min/max";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(394, 84);
            label13.Name = "label13";
            label13.Size = new Size(12, 15);
            label13.TabIndex = 19;
            label13.Text = "/";
            // 
            // numericUpDown7
            // 
            numericUpDown7.Location = new Point(412, 82);
            numericUpDown7.Name = "numericUpDown7";
            numericUpDown7.Size = new Size(120, 23);
            numericUpDown7.TabIndex = 17;
            // 
            // numericUpDown8
            // 
            numericUpDown8.Location = new Point(268, 82);
            numericUpDown8.Name = "numericUpDown8";
            numericUpDown8.Size = new Size(120, 23);
            numericUpDown8.TabIndex = 18;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(6, 26);
            label14.Name = "label14";
            label14.Size = new Size(178, 15);
            label14.TabIndex = 16;
            label14.Text = "Incubation time (days) min/max";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(394, 26);
            label15.Name = "label15";
            label15.Size = new Size(12, 15);
            label15.TabIndex = 15;
            label15.Text = "/";
            // 
            // numericUpDown9
            // 
            numericUpDown9.Location = new Point(412, 24);
            numericUpDown9.Name = "numericUpDown9";
            numericUpDown9.Size = new Size(120, 23);
            numericUpDown9.TabIndex = 13;
            // 
            // numericUpDown10
            // 
            numericUpDown10.Location = new Point(268, 24);
            numericUpDown10.Name = "numericUpDown10";
            numericUpDown10.Size = new Size(120, 23);
            numericUpDown10.TabIndex = 14;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(6, 113);
            label16.Name = "label16";
            label16.Size = new Size(146, 15);
            label16.TabIndex = 12;
            label16.Text = "Immunity (days) min/max";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(394, 55);
            label17.Name = "label17";
            label17.Size = new Size(12, 15);
            label17.TabIndex = 11;
            label17.Text = "/";
            // 
            // numericUpDown11
            // 
            numericUpDown11.Location = new Point(412, 53);
            numericUpDown11.Name = "numericUpDown11";
            numericUpDown11.Size = new Size(120, 23);
            numericUpDown11.TabIndex = 9;
            // 
            // numericUpDown12
            // 
            numericUpDown12.Location = new Point(268, 53);
            numericUpDown12.Name = "numericUpDown12";
            numericUpDown12.Size = new Size(120, 23);
            numericUpDown12.TabIndex = 10;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(6, 55);
            label18.Name = "label18";
            label18.Size = new Size(182, 15);
            label18.TabIndex = 24;
            label18.Text = "Contaigous time (days) min/max";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(394, 113);
            label19.Name = "label19";
            label19.Size = new Size(12, 15);
            label19.TabIndex = 23;
            label19.Text = "/";
            // 
            // numericUpDown13
            // 
            numericUpDown13.Location = new Point(412, 111);
            numericUpDown13.Name = "numericUpDown13";
            numericUpDown13.Size = new Size(120, 23);
            numericUpDown13.TabIndex = 21;
            // 
            // numericUpDown14
            // 
            numericUpDown14.Location = new Point(268, 111);
            numericUpDown14.Name = "numericUpDown14";
            numericUpDown14.Size = new Size(120, 23);
            numericUpDown14.TabIndex = 22;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(label24);
            groupBox5.Controls.Add(label25);
            groupBox5.Controls.Add(numericUpDown19);
            groupBox5.Controls.Add(numericUpDown20);
            groupBox5.Location = new Point(6, 290);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(542, 59);
            groupBox5.TabIndex = 25;
            groupBox5.TabStop = false;
            groupBox5.Text = "Health";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(6, 26);
            label24.Name = "label24";
            label24.Size = new Size(149, 15);
            label24.TabIndex = 16;
            label24.Text = "Heal amount (%) min/max";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(394, 26);
            label25.Name = "label25";
            label25.Size = new Size(12, 15);
            label25.TabIndex = 15;
            label25.Text = "/";
            // 
            // numericUpDown19
            // 
            numericUpDown19.Location = new Point(412, 24);
            numericUpDown19.Name = "numericUpDown19";
            numericUpDown19.Size = new Size(120, 23);
            numericUpDown19.TabIndex = 13;
            // 
            // numericUpDown20
            // 
            numericUpDown20.Location = new Point(268, 24);
            numericUpDown20.Name = "numericUpDown20";
            numericUpDown20.Size = new Size(120, 23);
            numericUpDown20.TabIndex = 14;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(label5);
            groupBox6.Controls.Add(btnCreateVirus);
            groupBox6.Controls.Add(cbViruses);
            groupBox6.Location = new Point(12, 158);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(227, 59);
            groupBox6.TabIndex = 14;
            groupBox6.TabStop = false;
            groupBox6.Text = "Virus";
            // 
            // ConfigurationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(807, 374);
            Controls.Add(groupBox6);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "ConfigurationForm";
            Text = "Configuration";
            ((System.ComponentModel.ISupportInitialize)nudInitialInfected).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPopulation).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudWidth).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown6).EndInit();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown7).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown8).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown9).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown10).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown11).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown12).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown13).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown14).EndInit();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown19).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown20).EndInit();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            ResumeLayout(false);
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
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private NumericUpDown numericUpDown2;
        private NumericUpDown numericUpDown1;
        private GroupBox groupBox3;
        private Label label8;
        private Label label9;
        private NumericUpDown numericUpDown3;
        private NumericUpDown numericUpDown4;
        private Label label7;
        private Label label6;
        private Label label10;
        private Label label11;
        private NumericUpDown numericUpDown5;
        private NumericUpDown numericUpDown6;
        private GroupBox groupBox5;
        private Label label24;
        private Label label25;
        private NumericUpDown numericUpDown19;
        private NumericUpDown numericUpDown20;
        private GroupBox groupBox4;
        private Label label18;
        private Label label19;
        private NumericUpDown numericUpDown13;
        private NumericUpDown numericUpDown14;
        private Label label12;
        private Label label13;
        private NumericUpDown numericUpDown7;
        private NumericUpDown numericUpDown8;
        private Label label14;
        private Label label15;
        private NumericUpDown numericUpDown9;
        private NumericUpDown numericUpDown10;
        private Label label16;
        private Label label17;
        private NumericUpDown numericUpDown11;
        private NumericUpDown numericUpDown12;
        private GroupBox groupBox6;
    }
}