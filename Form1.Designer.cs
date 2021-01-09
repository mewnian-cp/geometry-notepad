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
            this.move_button = new System.Windows.Forms.Button();
            this.draw_button = new System.Windows.Forms.Button();
            this.log = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.drawWindow)).BeginInit();
            this.drawWindow.Panel2.SuspendLayout();
            this.drawWindow.SuspendLayout();
            this.SuspendLayout();
            // 
            // debug
            // 
            this.debug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.debug.AutoSize = true;
            this.debug.Location = new System.Drawing.Point(8, 484);
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
            this.drawWindow.Panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.drawWindow_Panel1_MouseClick);
            this.drawWindow.Panel1.MouseHover += new System.EventHandler(this.drawWindow_Panel1_MouseHover);
            this.drawWindow.Panel1MinSize = 672;
            // 
            // drawWindow.Panel2
            // 
            this.drawWindow.Panel2.Controls.Add(this.log);
            this.drawWindow.Panel2.Controls.Add(this.move_button);
            this.drawWindow.Panel2.Controls.Add(this.draw_button);
            this.drawWindow.Size = new System.Drawing.Size(896, 504);
            this.drawWindow.SplitterDistance = 672;
            this.drawWindow.SplitterWidth = 3;
            this.drawWindow.TabIndex = 1;
            this.drawWindow.Text = "splitContainer1";
            // 
            // move_button
            // 
            this.move_button.Location = new System.Drawing.Point(118, 58);
            this.move_button.Name = "move_button";
            this.move_button.Size = new System.Drawing.Size(62, 33);
            this.move_button.TabIndex = 1;
            this.move_button.Text = "Move";
            this.move_button.UseMnemonic = false;
            this.move_button.UseVisualStyleBackColor = true;
            this.move_button.Click += new System.EventHandler(this.move_button_Click);
            // 
            // draw_button
            // 
            this.draw_button.Location = new System.Drawing.Point(40, 58);
            this.draw_button.Name = "draw_button";
            this.draw_button.Size = new System.Drawing.Size(62, 33);
            this.draw_button.TabIndex = 0;
            this.draw_button.Text = "Draw";
            this.draw_button.UseMnemonic = false;
            this.draw_button.UseVisualStyleBackColor = true;
            this.draw_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // log
            // 
            this.log.Location = new System.Drawing.Point(12, 160);
            this.log.Multiline = true;
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(197, 332);
            this.log.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(896, 504);
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
            this.drawWindow.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawWindow)).EndInit();
            this.drawWindow.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label debug;
        private System.Windows.Forms.SplitContainer drawWindow;
        private System.Windows.Forms.Button draw_button;
        private System.Windows.Forms.Button move_button;
        private System.Windows.Forms.TextBox log;
    }
}

