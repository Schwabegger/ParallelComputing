namespace PandemicSimulator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pbWorld = new PictureBox();
            menuStrip1 = new MenuStrip();
            startToolStripMentsmiSimulationuItem = new ToolStripMenuItem();
            tsmiStart = new ToolStripMenuItem();
            tsmiCancle = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            tsmiConfig = new ToolStripMenuItem();
            tsmiTest = new ToolStripMenuItem();
            timer1 = new System.Windows.Forms.Timer(components);
            lblFps = new Label();
            lblIterations = new Label();
            ((System.ComponentModel.ISupportInitialize)pbWorld).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pbWorld
            // 
            pbWorld.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pbWorld.Location = new Point(0, 27);
            pbWorld.Name = "pbWorld";
            pbWorld.Size = new Size(800, 423);
            pbWorld.SizeMode = PictureBoxSizeMode.StretchImage;
            pbWorld.TabIndex = 1;
            pbWorld.TabStop = false;
            // 
            // menuStrip1
            // 
            menuStrip1.Dock = DockStyle.None;
            menuStrip1.Items.AddRange(new ToolStripItem[] { startToolStripMentsmiSimulationuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(84, 24);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // startToolStripMentsmiSimulationuItem
            // 
            startToolStripMentsmiSimulationuItem.DropDownItems.AddRange(new ToolStripItem[] { tsmiStart, tsmiCancle, toolStripSeparator2, tsmiConfig, tsmiTest });
            startToolStripMentsmiSimulationuItem.Name = "startToolStripMentsmiSimulationuItem";
            startToolStripMentsmiSimulationuItem.Size = new Size(76, 20);
            startToolStripMentsmiSimulationuItem.Text = "Simulation";
            // 
            // tsmiStart
            // 
            tsmiStart.Name = "tsmiStart";
            tsmiStart.Size = new Size(148, 22);
            tsmiStart.Text = "Start";
            tsmiStart.Click += tsmiStart_Click;
            // 
            // tsmiCancle
            // 
            tsmiCancle.Name = "tsmiCancle";
            tsmiCancle.Size = new Size(148, 22);
            tsmiCancle.Text = "Cancle";
            tsmiCancle.Click += tsmiCancle_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(145, 6);
            // 
            // tsmiConfig
            // 
            tsmiConfig.Name = "tsmiConfig";
            tsmiConfig.Size = new Size(148, 22);
            tsmiConfig.Text = "Configuration";
            tsmiConfig.Click += tsmiConfig_Click;
            // 
            // tsmiTest
            // 
            tsmiTest.Name = "tsmiTest";
            tsmiTest.Size = new Size(148, 22);
            tsmiTest.Text = "Test";
            tsmiTest.Click += tsmiTest_Click;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // lblFps
            // 
            lblFps.AutoSize = true;
            lblFps.Location = new Point(87, 9);
            lblFps.Name = "lblFps";
            lblFps.Size = new Size(31, 15);
            lblFps.TabIndex = 4;
            lblFps.Text = "0000";
            // 
            // lblIterations
            // 
            lblIterations.AutoSize = true;
            lblIterations.Location = new Point(150, 9);
            lblIterations.Name = "lblIterations";
            lblIterations.Size = new Size(13, 15);
            lblIterations.TabIndex = 5;
            lblIterations.Text = "0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblIterations);
            Controls.Add(lblFps);
            Controls.Add(menuStrip1);
            Controls.Add(pbWorld);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pbWorld).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbWorld;
        private ToolStripMenuItem tsmiStart;
        private ToolStripMenuItem tsmiCancle;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem startToolStripMentsmiSimulationuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem tsmiConfig;
        private ToolStripMenuItem tsmiTest;
        private System.Windows.Forms.Timer timer1;
        private Label lblFps;
        private Label lblIterations;
    }
}
