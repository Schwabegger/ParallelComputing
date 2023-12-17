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
            groupBox5 = new GroupBox();
            label24 = new Label();
            label25 = new Label();
            nudHealAmountMax = new NumericUpDown();
            nudHealAmountMin = new NumericUpDown();
            groupBox4 = new GroupBox();
            label18 = new Label();
            label19 = new Label();
            nudImmunityMax = new NumericUpDown();
            nudImmunityMin = new NumericUpDown();
            label12 = new Label();
            label13 = new Label();
            nudDmgDelayMax = new NumericUpDown();
            nudDmgDelayMin = new NumericUpDown();
            label14 = new Label();
            label15 = new Label();
            nudIncubationTimeMax = new NumericUpDown();
            nudIncubationTimeMin = new NumericUpDown();
            label16 = new Label();
            label17 = new Label();
            nudContagiousTimeMax = new NumericUpDown();
            nudContagiousTimeMin = new NumericUpDown();
            groupBox3 = new GroupBox();
            label10 = new Label();
            label11 = new Label();
            nudAdditionalResistancePerDayWhenInfectedMax = new NumericUpDown();
            nudAdditionalResistancePerDayWhenInfectedMin = new NumericUpDown();
            label8 = new Label();
            label9 = new Label();
            nudInitialResistanceMax = new NumericUpDown();
            nudInitialResistanceMin = new NumericUpDown();
            label7 = new Label();
            label6 = new Label();
            nudIncreaseResistanceAfterCuringMax = new NumericUpDown();
            nudIncreaseResistanceAfterCuringMin = new NumericUpDown();
            groupBox6 = new GroupBox();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)nudInitialInfected).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPopulation).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudWidth).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudHealAmountMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudHealAmountMin).BeginInit();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudImmunityMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudImmunityMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDmgDelayMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDmgDelayMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudIncubationTimeMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudIncubationTimeMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudContagiousTimeMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudContagiousTimeMin).BeginInit();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudAdditionalResistancePerDayWhenInfectedMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudAdditionalResistancePerDayWhenInfectedMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudInitialResistanceMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudInitialResistanceMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudIncreaseResistanceAfterCuringMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudIncreaseResistanceAfterCuringMin).BeginInit();
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
            // 
            // nudInitialInfected
            // 
            nudInitialInfected.Location = new Point(94, 104);
            nudInitialInfected.Name = "nudInitialInfected";
            nudInitialInfected.Size = new Size(120, 23);
            nudInitialInfected.TabIndex = 2;
            nudInitialInfected.ValueChanged += Nud_ValueChanged;
            // 
            // nudPopulation
            // 
            nudPopulation.Location = new Point(94, 75);
            nudPopulation.Name = "nudPopulation";
            nudPopulation.Size = new Size(120, 23);
            nudPopulation.TabIndex = 3;
            nudPopulation.ValueChanged += Nud_ValueChanged;
            // 
            // nudHeight
            // 
            nudHeight.Location = new Point(94, 46);
            nudHeight.Name = "nudHeight";
            nudHeight.Size = new Size(120, 23);
            nudHeight.TabIndex = 4;
            nudHeight.ValueChanged += Nud_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 48);
            label2.Name = "label2";
            label2.Size = new Size(43, 15);
            label2.TabIndex = 5;
            label2.Text = "Height";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 77);
            label3.Name = "label3";
            label3.Size = new Size(65, 15);
            label3.TabIndex = 6;
            label3.Text = "Population";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 106);
            label4.Name = "label4";
            label4.Size = new Size(82, 15);
            label4.TabIndex = 7;
            label4.Text = "Initial infected";
            // 
            // nudWidth
            // 
            nudWidth.Location = new Point(94, 17);
            nudWidth.Name = "nudWidth";
            nudWidth.Size = new Size(120, 23);
            nudWidth.TabIndex = 8;
            nudWidth.ValueChanged += Nud_ValueChanged;
            // 
            // cbViruses
            // 
            cbViruses.FormattingEnabled = true;
            cbViruses.Location = new Point(93, 23);
            cbViruses.Name = "cbViruses";
            cbViruses.Size = new Size(121, 23);
            cbViruses.TabIndex = 9;
            cbViruses.SelectedIndexChanged += cbViruses_SelectedIndexChanged;
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
            // groupBox5
            // 
            groupBox5.Controls.Add(label24);
            groupBox5.Controls.Add(label25);
            groupBox5.Controls.Add(nudHealAmountMax);
            groupBox5.Controls.Add(nudHealAmountMin);
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
            // nudHealAmountMax
            // 
            nudHealAmountMax.DecimalPlaces = 1;
            nudHealAmountMax.Location = new Point(412, 24);
            nudHealAmountMax.Name = "nudHealAmountMax";
            nudHealAmountMax.Size = new Size(120, 23);
            nudHealAmountMax.TabIndex = 13;
            nudHealAmountMax.ValueChanged += Nud_ValueChanged;
            // 
            // nudHealAmountMin
            // 
            nudHealAmountMin.DecimalPlaces = 1;
            nudHealAmountMin.Location = new Point(268, 24);
            nudHealAmountMin.Name = "nudHealAmountMin";
            nudHealAmountMin.Size = new Size(120, 23);
            nudHealAmountMin.TabIndex = 14;
            nudHealAmountMin.ValueChanged += Nud_ValueChanged;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(label18);
            groupBox4.Controls.Add(label19);
            groupBox4.Controls.Add(nudImmunityMax);
            groupBox4.Controls.Add(nudImmunityMin);
            groupBox4.Controls.Add(label12);
            groupBox4.Controls.Add(label13);
            groupBox4.Controls.Add(nudDmgDelayMax);
            groupBox4.Controls.Add(nudDmgDelayMin);
            groupBox4.Controls.Add(label14);
            groupBox4.Controls.Add(label15);
            groupBox4.Controls.Add(nudIncubationTimeMax);
            groupBox4.Controls.Add(nudIncubationTimeMin);
            groupBox4.Controls.Add(label16);
            groupBox4.Controls.Add(label17);
            groupBox4.Controls.Add(nudContagiousTimeMax);
            groupBox4.Controls.Add(nudContagiousTimeMin);
            groupBox4.Location = new Point(6, 146);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(542, 138);
            groupBox4.TabIndex = 21;
            groupBox4.TabStop = false;
            groupBox4.Text = "Durations";
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
            // nudImmunityMax
            // 
            nudImmunityMax.Location = new Point(412, 111);
            nudImmunityMax.Name = "nudImmunityMax";
            nudImmunityMax.Size = new Size(120, 23);
            nudImmunityMax.TabIndex = 21;
            nudImmunityMax.ValueChanged += Nud_ValueChanged;
            // 
            // nudImmunityMin
            // 
            nudImmunityMin.Location = new Point(268, 111);
            nudImmunityMin.Name = "nudImmunityMin";
            nudImmunityMin.Size = new Size(120, 23);
            nudImmunityMin.TabIndex = 22;
            nudImmunityMin.ValueChanged += Nud_ValueChanged;
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
            // nudDmgDelayMax
            // 
            nudDmgDelayMax.Location = new Point(412, 82);
            nudDmgDelayMax.Name = "nudDmgDelayMax";
            nudDmgDelayMax.Size = new Size(120, 23);
            nudDmgDelayMax.TabIndex = 17;
            nudDmgDelayMax.ValueChanged += Nud_ValueChanged;
            // 
            // nudDmgDelayMin
            // 
            nudDmgDelayMin.Location = new Point(268, 82);
            nudDmgDelayMin.Name = "nudDmgDelayMin";
            nudDmgDelayMin.Size = new Size(120, 23);
            nudDmgDelayMin.TabIndex = 18;
            nudDmgDelayMin.ValueChanged += Nud_ValueChanged;
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
            // nudIncubationTimeMax
            // 
            nudIncubationTimeMax.Location = new Point(412, 24);
            nudIncubationTimeMax.Name = "nudIncubationTimeMax";
            nudIncubationTimeMax.Size = new Size(120, 23);
            nudIncubationTimeMax.TabIndex = 13;
            nudIncubationTimeMax.ValueChanged += Nud_ValueChanged;
            // 
            // nudIncubationTimeMin
            // 
            nudIncubationTimeMin.Location = new Point(268, 24);
            nudIncubationTimeMin.Name = "nudIncubationTimeMin";
            nudIncubationTimeMin.Size = new Size(120, 23);
            nudIncubationTimeMin.TabIndex = 14;
            nudIncubationTimeMin.ValueChanged += Nud_ValueChanged;
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
            // nudContagiousTimeMax
            // 
            nudContagiousTimeMax.Location = new Point(412, 53);
            nudContagiousTimeMax.Name = "nudContagiousTimeMax";
            nudContagiousTimeMax.Size = new Size(120, 23);
            nudContagiousTimeMax.TabIndex = 9;
            nudContagiousTimeMax.ValueChanged += Nud_ValueChanged;
            // 
            // nudContagiousTimeMin
            // 
            nudContagiousTimeMin.Location = new Point(268, 53);
            nudContagiousTimeMin.Name = "nudContagiousTimeMin";
            nudContagiousTimeMin.Size = new Size(120, 23);
            nudContagiousTimeMin.TabIndex = 10;
            nudContagiousTimeMin.ValueChanged += Nud_ValueChanged;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label10);
            groupBox3.Controls.Add(label11);
            groupBox3.Controls.Add(nudAdditionalResistancePerDayWhenInfectedMax);
            groupBox3.Controls.Add(nudAdditionalResistancePerDayWhenInfectedMin);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(nudInitialResistanceMax);
            groupBox3.Controls.Add(nudInitialResistanceMin);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(nudIncreaseResistanceAfterCuringMax);
            groupBox3.Controls.Add(nudIncreaseResistanceAfterCuringMin);
            groupBox3.Location = new Point(6, 22);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(542, 118);
            groupBox3.TabIndex = 13;
            groupBox3.TabStop = false;
            groupBox3.Text = "Resistance";
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
            // nudAdditionalResistancePerDayWhenInfectedMax
            // 
            nudAdditionalResistancePerDayWhenInfectedMax.DecimalPlaces = 1;
            nudAdditionalResistancePerDayWhenInfectedMax.Location = new Point(412, 82);
            nudAdditionalResistancePerDayWhenInfectedMax.Name = "nudAdditionalResistancePerDayWhenInfectedMax";
            nudAdditionalResistancePerDayWhenInfectedMax.Size = new Size(120, 23);
            nudAdditionalResistancePerDayWhenInfectedMax.TabIndex = 17;
            nudAdditionalResistancePerDayWhenInfectedMax.ValueChanged += Nud_ValueChanged;
            // 
            // nudAdditionalResistancePerDayWhenInfectedMin
            // 
            nudAdditionalResistancePerDayWhenInfectedMin.DecimalPlaces = 1;
            nudAdditionalResistancePerDayWhenInfectedMin.Location = new Point(268, 82);
            nudAdditionalResistancePerDayWhenInfectedMin.Name = "nudAdditionalResistancePerDayWhenInfectedMin";
            nudAdditionalResistancePerDayWhenInfectedMin.Size = new Size(120, 23);
            nudAdditionalResistancePerDayWhenInfectedMin.TabIndex = 18;
            nudAdditionalResistancePerDayWhenInfectedMin.ValueChanged += Nud_ValueChanged;
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
            // nudInitialResistanceMax
            // 
            nudInitialResistanceMax.DecimalPlaces = 1;
            nudInitialResistanceMax.Location = new Point(412, 24);
            nudInitialResistanceMax.Name = "nudInitialResistanceMax";
            nudInitialResistanceMax.Size = new Size(120, 23);
            nudInitialResistanceMax.TabIndex = 13;
            nudInitialResistanceMax.ValueChanged += Nud_ValueChanged;
            // 
            // nudInitialResistanceMin
            // 
            nudInitialResistanceMin.DecimalPlaces = 1;
            nudInitialResistanceMin.Location = new Point(268, 24);
            nudInitialResistanceMin.Name = "nudInitialResistanceMin";
            nudInitialResistanceMin.Size = new Size(120, 23);
            nudInitialResistanceMin.TabIndex = 14;
            nudInitialResistanceMin.ValueChanged += Nud_ValueChanged;
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
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(394, 55);
            label6.Name = "label6";
            label6.Size = new Size(12, 15);
            label6.TabIndex = 11;
            label6.Text = "/";
            // 
            // nudIncreaseResistanceAfterCuringMax
            // 
            nudIncreaseResistanceAfterCuringMax.DecimalPlaces = 1;
            nudIncreaseResistanceAfterCuringMax.Location = new Point(412, 53);
            nudIncreaseResistanceAfterCuringMax.Name = "nudIncreaseResistanceAfterCuringMax";
            nudIncreaseResistanceAfterCuringMax.Size = new Size(120, 23);
            nudIncreaseResistanceAfterCuringMax.TabIndex = 9;
            nudIncreaseResistanceAfterCuringMax.ValueChanged += Nud_ValueChanged;
            // 
            // nudIncreaseResistanceAfterCuringMin
            // 
            nudIncreaseResistanceAfterCuringMin.DecimalPlaces = 1;
            nudIncreaseResistanceAfterCuringMin.Location = new Point(268, 53);
            nudIncreaseResistanceAfterCuringMin.Name = "nudIncreaseResistanceAfterCuringMin";
            nudIncreaseResistanceAfterCuringMin.Size = new Size(120, 23);
            nudIncreaseResistanceAfterCuringMin.TabIndex = 10;
            nudIncreaseResistanceAfterCuringMin.ValueChanged += Nud_ValueChanged;
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
            // button1
            // 
            button1.Font = new Font("Segoe UI", 20F);
            button1.Location = new Point(12, 223);
            button1.Name = "button1";
            button1.Size = new Size(227, 146);
            button1.TabIndex = 15;
            button1.Text = "Save";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // ConfigurationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.test;
            ClientSize = new Size(812, 380);
            Controls.Add(button1);
            Controls.Add(groupBox6);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "ConfigurationForm";
            Text = "Configuration";
            FormClosing += ConfigurationForm_FormClosing;
            ((System.ComponentModel.ISupportInitialize)nudInitialInfected).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPopulation).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudWidth).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudHealAmountMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudHealAmountMin).EndInit();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudImmunityMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudImmunityMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDmgDelayMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDmgDelayMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudIncubationTimeMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudIncubationTimeMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudContagiousTimeMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudContagiousTimeMin).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudAdditionalResistancePerDayWhenInfectedMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudAdditionalResistancePerDayWhenInfectedMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudInitialResistanceMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudInitialResistanceMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudIncreaseResistanceAfterCuringMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudIncreaseResistanceAfterCuringMin).EndInit();
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
        private NumericUpDown nudIncreaseResistanceAfterCuringMin;
        private NumericUpDown nudIncreaseResistanceAfterCuringMax;
        private GroupBox groupBox3;
        private Label label8;
        private Label label9;
        private NumericUpDown nudInitialResistanceMax;
        private NumericUpDown nudInitialResistanceMin;
        private Label label7;
        private Label label6;
        private Label label10;
        private Label label11;
        private NumericUpDown nudAdditionalResistancePerDayWhenInfectedMax;
        private NumericUpDown nudAdditionalResistancePerDayWhenInfectedMin;
        private GroupBox groupBox5;
        private Label label24;
        private Label label25;
        private NumericUpDown nudHealAmountMax;
        private NumericUpDown nudHealAmountMin;
        private GroupBox groupBox4;
        private Label label18;
        private Label label19;
        private NumericUpDown nudImmunityMax;
        private NumericUpDown nudImmunityMin;
        private Label label12;
        private Label label13;
        private NumericUpDown nudDmgDelayMax;
        private NumericUpDown nudDmgDelayMin;
        private Label label14;
        private Label label15;
        private NumericUpDown nudIncubationTimeMax;
        private NumericUpDown nudIncubationTimeMin;
        private Label label16;
        private Label label17;
        private NumericUpDown nudContagiousTimeMax;
        private NumericUpDown nudContagiousTimeMin;
        private GroupBox groupBox6;
        private Button button1;
    }
}