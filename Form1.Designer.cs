namespace geometry_notepad
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
            this.debug = new System.Windows.Forms.Label();
            this.drawWindow = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.drawWindow)).BeginInit();
            this.drawWindow.SuspendLayout();
            this.SuspendLayout();
            // 
            // debug
            // 
            this.debug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.debug.AutoSize = true;
            this.debug.Location = new System.Drawing.Point(12, 686);
            this.debug.Name = "debug";
            this.debug.Size = new System.Drawing.Size(0, 25);
            this.debug.TabIndex = 0;
            // 
            // drawWindow
            // 
            this.drawWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.drawWindow.Location = new System.Drawing.Point(0, 0);
            this.drawWindow.Name = "drawWindow";
            // 
            // drawWindow.Panel1
            // 
            this.drawWindow.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.drawWindow_Panel1_Paint);
            this.drawWindow.Panel1MinSize = 960;
            this.drawWindow.Size = new System.Drawing.Size(1280, 720);
            this.drawWindow.SplitterDistance = 960;
            this.drawWindow.TabIndex = 1;
            this.drawWindow.Text = "splitContainer1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.debug);
            this.Controls.Add(this.drawWindow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged_1);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.drawWindow)).EndInit();
            this.drawWindow.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label debug;
        private System.Windows.Forms.SplitContainer drawWindow;
    }
}

