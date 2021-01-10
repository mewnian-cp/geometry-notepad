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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.drawWindow = new System.Windows.Forms.SplitContainer();
            this.about_button = new System.Windows.Forms.Button();
            this.reset_button = new System.Windows.Forms.Button();
            this.clear_all = new System.Windows.Forms.Button();
            this.zoom_button = new System.Windows.Forms.Button();
            this.origin_button = new System.Windows.Forms.Button();
            this.unselect_button = new System.Windows.Forms.Button();
            this.label_p = new System.Windows.Forms.Label();
            this.exit_button = new System.Windows.Forms.Button();
            this.choosePolygon = new System.Windows.Forms.NumericUpDown();
            this.log = new System.Windows.Forms.TextBox();
            this.move_button = new System.Windows.Forms.Button();
            this.draw_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.drawWindow)).BeginInit();
            this.drawWindow.Panel2.SuspendLayout();
            this.drawWindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.choosePolygon)).BeginInit();
            this.SuspendLayout();
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
            this.drawWindow.Panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawWindow_Panel1_MouseDown);
            this.drawWindow.Panel1.MouseHover += new System.EventHandler(this.drawWindow_Panel1_MouseHover);
            this.drawWindow.Panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawWindow_Panel1_MouseMove);
            this.drawWindow.Panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drawWindow_Panel1_MouseUp);
            this.drawWindow.Panel1MinSize = 672;
            // 
            // drawWindow.Panel2
            // 
            this.drawWindow.Panel2.Controls.Add(this.about_button);
            this.drawWindow.Panel2.Controls.Add(this.reset_button);
            this.drawWindow.Panel2.Controls.Add(this.clear_all);
            this.drawWindow.Panel2.Controls.Add(this.zoom_button);
            this.drawWindow.Panel2.Controls.Add(this.origin_button);
            this.drawWindow.Panel2.Controls.Add(this.unselect_button);
            this.drawWindow.Panel2.Controls.Add(this.label_p);
            this.drawWindow.Panel2.Controls.Add(this.exit_button);
            this.drawWindow.Panel2.Controls.Add(this.choosePolygon);
            this.drawWindow.Panel2.Controls.Add(this.log);
            this.drawWindow.Panel2.Controls.Add(this.move_button);
            this.drawWindow.Panel2.Controls.Add(this.draw_button);
            this.drawWindow.Size = new System.Drawing.Size(896, 504);
            this.drawWindow.SplitterDistance = 672;
            this.drawWindow.SplitterWidth = 3;
            this.drawWindow.TabIndex = 1;
            this.drawWindow.Text = "splitContainer1";
            // 
            // about_button
            // 
            this.about_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.about_button.AutoSize = true;
            this.about_button.BackColor = System.Drawing.Color.DarkSlateGray;
            this.about_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.about_button.ForeColor = System.Drawing.Color.White;
            this.about_button.Location = new System.Drawing.Point(123, 3);
            this.about_button.Name = "about_button";
            this.about_button.Size = new System.Drawing.Size(52, 27);
            this.about_button.TabIndex = 9;
            this.about_button.Text = "About";
            this.about_button.UseMnemonic = false;
            this.about_button.UseVisualStyleBackColor = false;
            this.about_button.Click += new System.EventHandler(this.about_button_Click);
            // 
            // reset_button
            // 
            this.reset_button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.reset_button.Location = new System.Drawing.Point(113, 72);
            this.reset_button.Name = "reset_button";
            this.reset_button.Size = new System.Drawing.Size(100, 25);
            this.reset_button.TabIndex = 14;
            this.reset_button.Text = "Reset Settings";
            this.reset_button.UseMnemonic = false;
            this.reset_button.UseVisualStyleBackColor = true;
            this.reset_button.Click += new System.EventHandler(this.reset_button_Click);
            // 
            // clear_all
            // 
            this.clear_all.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.clear_all.AutoSize = true;
            this.clear_all.BackColor = System.Drawing.Color.Red;
            this.clear_all.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clear_all.ForeColor = System.Drawing.Color.White;
            this.clear_all.Location = new System.Drawing.Point(11, 138);
            this.clear_all.Name = "clear_all";
            this.clear_all.Size = new System.Drawing.Size(202, 27);
            this.clear_all.TabIndex = 13;
            this.clear_all.Text = "Clear All Polygons";
            this.clear_all.UseMnemonic = false;
            this.clear_all.UseVisualStyleBackColor = false;
            this.clear_all.Click += new System.EventHandler(this.clear_all_Click);
            // 
            // zoom_button
            // 
            this.zoom_button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.zoom_button.Location = new System.Drawing.Point(11, 72);
            this.zoom_button.Name = "zoom_button";
            this.zoom_button.Size = new System.Drawing.Size(100, 25);
            this.zoom_button.TabIndex = 12;
            this.zoom_button.Text = "Zoom";
            this.zoom_button.UseMnemonic = false;
            this.zoom_button.UseVisualStyleBackColor = true;
            this.zoom_button.Click += new System.EventHandler(this.zoom_button_Click);
            // 
            // origin_button
            // 
            this.origin_button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.origin_button.AutoSize = true;
            this.origin_button.Location = new System.Drawing.Point(113, 44);
            this.origin_button.Name = "origin_button";
            this.origin_button.Size = new System.Drawing.Size(100, 25);
            this.origin_button.TabIndex = 11;
            this.origin_button.Text = "Point to Origin";
            this.origin_button.UseMnemonic = false;
            this.origin_button.UseVisualStyleBackColor = true;
            this.origin_button.Click += new System.EventHandler(this.origin_button_Click);
            // 
            // unselect_button
            // 
            this.unselect_button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.unselect_button.Location = new System.Drawing.Point(113, 111);
            this.unselect_button.Name = "unselect_button";
            this.unselect_button.Size = new System.Drawing.Size(100, 25);
            this.unselect_button.TabIndex = 10;
            this.unselect_button.Text = "Unhighlight";
            this.unselect_button.UseMnemonic = false;
            this.unselect_button.UseVisualStyleBackColor = true;
            this.unselect_button.Click += new System.EventHandler(this.unselect_button_Click);
            // 
            // label_p
            // 
            this.label_p.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_p.AutoSize = true;
            this.label_p.BackColor = System.Drawing.Color.Transparent;
            this.label_p.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label_p.Location = new System.Drawing.Point(23, 171);
            this.label_p.Name = "label_p";
            this.label_p.Size = new System.Drawing.Size(121, 19);
            this.label_p.TabIndex = 9;
            this.label_p.Text = "Current Polygon #";
            this.label_p.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // exit_button
            // 
            this.exit_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exit_button.AutoSize = true;
            this.exit_button.BackColor = System.Drawing.Color.Red;
            this.exit_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exit_button.ForeColor = System.Drawing.Color.White;
            this.exit_button.Location = new System.Drawing.Point(178, 3);
            this.exit_button.Name = "exit_button";
            this.exit_button.Size = new System.Drawing.Size(46, 27);
            this.exit_button.TabIndex = 8;
            this.exit_button.Text = "Exit";
            this.exit_button.UseMnemonic = false;
            this.exit_button.UseVisualStyleBackColor = false;
            this.exit_button.Click += new System.EventHandler(this.exit_button_Click);
            // 
            // choosePolygon
            // 
            this.choosePolygon.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.choosePolygon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.choosePolygon.Location = new System.Drawing.Point(150, 169);
            this.choosePolygon.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.choosePolygon.Name = "choosePolygon";
            this.choosePolygon.Size = new System.Drawing.Size(50, 23);
            this.choosePolygon.TabIndex = 6;
            this.choosePolygon.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.choosePolygon.ValueChanged += new System.EventHandler(this.choosePolygon_ValueChanged);
            // 
            // log
            // 
            this.log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.log.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.log.Location = new System.Drawing.Point(3, 198);
            this.log.Multiline = true;
            this.log.Name = "log";
            this.log.ReadOnly = true;
            this.log.Size = new System.Drawing.Size(217, 303);
            this.log.TabIndex = 2;
            // 
            // move_button
            // 
            this.move_button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.move_button.AutoSize = true;
            this.move_button.Location = new System.Drawing.Point(11, 44);
            this.move_button.Name = "move_button";
            this.move_button.Size = new System.Drawing.Size(100, 25);
            this.move_button.TabIndex = 1;
            this.move_button.Text = "Move Grid";
            this.move_button.UseMnemonic = false;
            this.move_button.UseVisualStyleBackColor = true;
            this.move_button.Click += new System.EventHandler(this.move_button_Click);
            // 
            // draw_button
            // 
            this.draw_button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.draw_button.Location = new System.Drawing.Point(11, 111);
            this.draw_button.Name = "draw_button";
            this.draw_button.Size = new System.Drawing.Size(100, 25);
            this.draw_button.TabIndex = 0;
            this.draw_button.Text = "Draw Polygon";
            this.draw_button.UseMnemonic = false;
            this.draw_button.UseVisualStyleBackColor = true;
            this.draw_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(896, 504);
            this.Controls.Add(this.drawWindow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged_1);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.drawWindow.Panel2.ResumeLayout(false);
            this.drawWindow.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawWindow)).EndInit();
            this.drawWindow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.choosePolygon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer drawWindow;
        private System.Windows.Forms.Button draw_button;
        private System.Windows.Forms.Button move_button;
        private System.Windows.Forms.TextBox log;
        private System.Windows.Forms.NumericUpDown choosePolygon;
        private System.Windows.Forms.Button exit_button;
        private System.Windows.Forms.Label label_p;
        private System.Windows.Forms.Button unselect_button;
        private System.Windows.Forms.Button origin_button;
        private System.Windows.Forms.Button zoom_button;
        private System.Windows.Forms.Button reset_button;
        private System.Windows.Forms.Button clear_all;
        private System.Windows.Forms.Button about_button;
    }
}

