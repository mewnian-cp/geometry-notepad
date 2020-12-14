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
            this.draw_triangle = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.drawWindow)).BeginInit();
            this.drawWindow.Panel2.SuspendLayout();
            this.drawWindow.SuspendLayout();
            this.SuspendLayout();
            // 
            // debug
            // 
            this.debug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.debug.AutoSize = true;
            this.debug.Location = new System.Drawing.Point(8, 430);
            this.debug.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.debug.Name = "debug";
            this.debug.Size = new System.Drawing.Size(0, 15);
            this.debug.TabIndex = 0;
            // 
            // drawWindow
            // 
            this.drawWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.drawWindow.Location = new System.Drawing.Point(0, 0);
            this.drawWindow.Margin = new System.Windows.Forms.Padding(2);
            this.drawWindow.Name = "drawWindow";
            // 
            // drawWindow.Panel1
            // 
            this.drawWindow.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.drawWindow_Panel1_Paint);
            // 
            // drawWindow.Panel2
            // 
            this.drawWindow.Panel2.Controls.Add(this.draw_triangle);
            this.drawWindow.Size = new System.Drawing.Size(800, 450);
            this.drawWindow.SplitterDistance = 600;
            this.drawWindow.SplitterWidth = 3;
            this.drawWindow.TabIndex = 1;
            this.drawWindow.Text = "splitContainer1";
            // 
            // draw_triangle
            // 
            this.draw_triangle.Dock = System.Windows.Forms.DockStyle.Top;
            this.draw_triangle.Location = new System.Drawing.Point(0, 0);
            this.draw_triangle.Name = "draw_triangle";
            this.draw_triangle.Size = new System.Drawing.Size(197, 26);
            this.draw_triangle.TabIndex = 0;
            this.draw_triangle.Text = "Draw Triangle";
            this.draw_triangle.UseVisualStyleBackColor = true;
            this.draw_triangle.Click += new System.EventHandler(this.draw_triangle_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.debug);
            this.Controls.Add(this.drawWindow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged_1);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.drawWindow.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.drawWindow)).EndInit();
            this.drawWindow.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label debug;
        private System.Windows.Forms.SplitContainer drawWindow;
        private System.Windows.Forms.Button draw_triangle;
    }
}

