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
            ctxms = new ContextMenuStrip(components);
            pbWorld = new PictureBox();
            tsmiStart = new ToolStripMenuItem();
            tsmiCancle = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            tsmiConfig = new ToolStripMenuItem();
            ctxms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbWorld).BeginInit();
            SuspendLayout();
            // 
            // ctxms
            // 
            ctxms.Items.AddRange(new ToolStripItem[] { tsmiStart, tsmiCancle, toolStripSeparator1, tsmiConfig });
            ctxms.Name = "ctxms";
            ctxms.Size = new Size(181, 98);
            // 
            // pbWorld
            // 
            pbWorld.Dock = DockStyle.Fill;
            pbWorld.Location = new Point(0, 0);
            pbWorld.Name = "pbWorld";
            pbWorld.Size = new Size(800, 450);
            pbWorld.TabIndex = 1;
            pbWorld.TabStop = false;
            // 
            // tsmiStart
            // 
            tsmiStart.Name = "tsmiStart";
            tsmiStart.Size = new Size(180, 22);
            tsmiStart.Text = "Start";
            tsmiStart.Click += tsmiStart_Click;
            // 
            // tsmiCancle
            // 
            tsmiCancle.Name = "tsmiCancle";
            tsmiCancle.Size = new Size(180, 22);
            tsmiCancle.Text = "Cancle";
            tsmiCancle.Click += tsmiCancle_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(177, 6);
            // 
            // tsmiConfig
            // 
            tsmiConfig.Name = "tsmiConfig";
            tsmiConfig.Size = new Size(180, 22);
            tsmiConfig.Text = "Configuration";
            tsmiConfig.Click += tsmiConfig_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pbWorld);
            Name = "Form1";
            Text = "Form1";
            ctxms.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbWorld).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grbConfig;
        private Button btnStart;
        private Button btnCancle;
        private ContextMenuStrip ctxms;
        private PictureBox pbWorld;
        private ToolStripMenuItem tsmiStart;
        private ToolStripMenuItem tsmiCancle;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem tsmiConfig;
    }
}
