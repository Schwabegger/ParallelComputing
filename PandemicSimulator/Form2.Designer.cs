﻿using System.Threading;

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
            if (_simulationThread is not null && _simulationThread.IsAlive)
            {
                cancellationTokenSource.Cancel();
                _simulationThread.Join();
            }
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
            menuStrip1 = new MenuStrip();
            startToolStripMentsmiSimulationuItem = new ToolStripMenuItem();
            tsmiStart = new ToolStripMenuItem();
            tsmiCancle = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            tsmiConfig = new ToolStripMenuItem();
            timer1 = new System.Windows.Forms.Timer(components);
            lblFps = new Label();
            lblIterations = new Label();
            lblAlive = new Label();
            lblInfected = new Label();
            lblContagious = new Label();
            lblMoved = new Label();
            lblDied = new Label();
            groupBox1 = new GroupBox();
            menuStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
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
            startToolStripMentsmiSimulationuItem.DropDownItems.AddRange(new ToolStripItem[] { tsmiStart, tsmiCancle, toolStripSeparator2, tsmiConfig });
            startToolStripMentsmiSimulationuItem.Name = "startToolStripMentsmiSimulationuItem";
            startToolStripMentsmiSimulationuItem.Size = new Size(76, 20);
            startToolStripMentsmiSimulationuItem.Text = "Simulation";
            // 
            // tsmiStart
            // 
            tsmiStart.Enabled = false;
            tsmiStart.Name = "tsmiStart";
            tsmiStart.Size = new Size(180, 22);
            tsmiStart.Text = "Start";
            tsmiStart.Click += tsmiStart_Click;
            // 
            // tsmiCancle
            // 
            tsmiCancle.Enabled = false;
            tsmiCancle.Name = "tsmiCancle";
            tsmiCancle.Size = new Size(180, 22);
            tsmiCancle.Text = "Cancle";
            tsmiCancle.Click += tsmiCancle_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(177, 6);
            // 
            // tsmiConfig
            // 
            tsmiConfig.Name = "tsmiConfig";
            tsmiConfig.Size = new Size(180, 22);
            tsmiConfig.Text = "Configuration";
            tsmiConfig.Click += tsmiConfig_Click;
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
            // lblAlive
            // 
            lblAlive.AutoSize = true;
            lblAlive.Location = new Point(6, 23);
            lblAlive.Name = "lblAlive";
            lblAlive.Size = new Size(13, 15);
            lblAlive.TabIndex = 6;
            lblAlive.Text = "0";
            // 
            // lblInfected
            // 
            lblInfected.AutoSize = true;
            lblInfected.Location = new Point(6, 38);
            lblInfected.Name = "lblInfected";
            lblInfected.Size = new Size(13, 15);
            lblInfected.TabIndex = 7;
            lblInfected.Text = "0";
            // 
            // lblContagious
            // 
            lblContagious.AutoSize = true;
            lblContagious.Location = new Point(6, 53);
            lblContagious.Name = "lblContagious";
            lblContagious.Size = new Size(13, 15);
            lblContagious.TabIndex = 8;
            lblContagious.Text = "0";
            // 
            // lblMoved
            // 
            lblMoved.AutoSize = true;
            lblMoved.Location = new Point(6, 68);
            lblMoved.Name = "lblMoved";
            lblMoved.Size = new Size(13, 15);
            lblMoved.TabIndex = 9;
            lblMoved.Text = "0";
            // 
            // lblDied
            // 
            lblDied.AutoSize = true;
            lblDied.Location = new Point(6, 83);
            lblDied.Name = "lblDied";
            lblDied.Size = new Size(13, 15);
            lblDied.TabIndex = 10;
            lblDied.Text = "0";
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox1.Controls.Add(lblInfected);
            groupBox1.Controls.Add(lblDied);
            groupBox1.Controls.Add(lblAlive);
            groupBox1.Controls.Add(lblMoved);
            groupBox1.Controls.Add(lblContagious);
            groupBox1.Location = new Point(618, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(181, 107);
            groupBox1.TabIndex = 11;
            groupBox1.TabStop = false;
            groupBox1.Text = "Stats";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox1);
            Controls.Add(lblIterations);
            Controls.Add(lblFps);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ToolStripMenuItem tsmiStart;
        private ToolStripMenuItem tsmiCancle;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem startToolStripMentsmiSimulationuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem tsmiConfig;
        private System.Windows.Forms.Timer timer1;
        private Label lblFps;
        private Label lblIterations;
        private Label lblAlive;
        private Label lblInfected;
        private Label lblContagious;
        private Label lblMoved;
        private Label lblDied;
        private GroupBox groupBox1;
    }
}
