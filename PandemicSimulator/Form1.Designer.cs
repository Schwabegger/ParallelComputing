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
            ((System.ComponentModel.ISupportInitialize)pbWorld).BeginInit();
            SuspendLayout();
            // 
            // ctxms
            // 
            ctxms.Name = "ctxms";
            ctxms.Size = new Size(61, 4);
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pbWorld);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pbWorld).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grbConfig;
        private Button btnStart;
        private Button btnCancle;
        private ContextMenuStrip ctxms;
        private PictureBox pbWorld;
    }
}
