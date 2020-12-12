using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace geometry_notepad
{
    public partial class Form1 : Form
    {
        public Graphics grid;
        public const int thickness = 1;
        public Pen OAxisLine = new Pen(Color.Black, 2 * thickness);
        public Pen NormalLine = new Pen(Color.Black, thickness);
        public SolidBrush FontColor = new SolidBrush(Color.Black);
        public PointF PointO;
        public const float DefaultX = 1280, DefaultY = 720;
        public const float DefaultDistance = 80;
        public const float DefaultFontSize = 12;
        public Font CurrentFont = new Font("Arial", 12);
        public float CurrentDistance = DefaultDistance;
        public Form1()
        {
            InitializeComponent();
        }
        private void PaintGrid(object sender, PaintEventArgs e, float distance)
        {
            var gridpanel = sender as Panel;
            grid = e.Graphics;
            PointO = new PointF(gridpanel.Width / 2, gridpanel.Height / 2);
            debug.Text = "(" + PointO.X + "; " + PointO.Y + ")";
            // Process of drawing 2D grid with 1pt = (distance) pixels
            // Width starts at 0 from the LEFT
            // Height starts at 0 from the TOP
            for (float i = PointO.Y; i < gridpanel.Height; i += distance)
            {
                PointF StartPoint = new PointF(0, i);
                PointF EndPoint = new PointF(gridpanel.Width, i);
                Pen CurrentPen = NormalLine;
                if (i == PointO.Y) CurrentPen = OAxisLine;
                grid.DrawLine(CurrentPen, StartPoint, EndPoint);
            }
            for (float i = PointO.Y - distance; i > 0; i -= distance)
            {
                PointF StartPoint = new PointF(0, i);
                PointF EndPoint = new PointF(gridpanel.Width, i);
                Pen CurrentPen = NormalLine;
                if (i == PointO.Y) CurrentPen = OAxisLine;
                grid.DrawLine(CurrentPen, StartPoint, EndPoint);
            }
            for (float i = PointO.X; i < gridpanel.Width; i += distance)
            {
                PointF StartPoint = new PointF(i, 0);
                PointF EndPoint = new PointF(i, gridpanel.Height);
                Pen CurrentPen = NormalLine;
                if (i == PointO.X) CurrentPen = OAxisLine;
                grid.DrawLine(CurrentPen, StartPoint, EndPoint);
            }
            for (float i = PointO.X - distance; i > 0; i -= distance)
            {
                PointF StartPoint = new PointF(i, 0);
                PointF EndPoint = new PointF(i, gridpanel.Height);
                Pen CurrentPen = NormalLine;
                grid.DrawLine(CurrentPen, StartPoint, EndPoint);
            }
            // Drawing label for each integer point
            // From LEFT to RIGHT, label increases
            // From TOP to BOTTOM, label decreases

            // O(0, 0) is drawn separately
            {
                PointF DrawPoint = new PointF((gridpanel.Width / 2) - thickness * 5, (gridpanel.Height / 2) + thickness * 5);
                StringFormat DrawFormat = new StringFormat();
                DrawFormat.Alignment = StringAlignment.Far;
                DrawFormat.LineAlignment = StringAlignment.Near;
                // DrawString only draws text, which can be covered by grid line.
                // Therefore, a rectangle should be drawn in its place to make label
                // easier to see.
                SizeF labelSize = new SizeF();
                labelSize = grid.MeasureString("0", CurrentFont, DrawPoint, DrawFormat);
                // DrawPoint is at upper-right so we temporarily move it to upper-left
                // to draw rectangle more accurately
                DrawPoint.X -= labelSize.Width;
                grid.DrawRectangle(new Pen(this.BackColor, thickness * 2), DrawPoint.X, DrawPoint.Y, labelSize.Width, labelSize.Height);
                grid.FillRectangle(new SolidBrush(this.BackColor), new RectangleF(DrawPoint, labelSize));
                // Recover the coordinates to draw the text
                DrawPoint.X += labelSize.Width;
                grid.DrawString("0", CurrentFont, FontColor, DrawPoint, DrawFormat);
            }
            // Non-zero points on Ox axis
            for (float i = PointO.X + distance, j = 1; i < gridpanel.Width; i += distance, ++j)
            {
                PointF DrawPoint = new PointF(i, (gridpanel.Height / 2) + thickness * 5);
                StringFormat DrawFormat = new StringFormat();
                DrawFormat.Alignment = StringAlignment.Center;
                // DrawString only draws text, which can be covered by grid line.
                // Therefore, a rectangle should be drawn in its place to make label
                // easier to see.
                SizeF labelSize = new SizeF();
                labelSize = grid.MeasureString(j.ToString(), CurrentFont);
                grid.DrawRectangle(new Pen(this.BackColor, thickness * 2), DrawPoint.X, DrawPoint.Y, labelSize.Width, labelSize.Height);
                grid.DrawString(j.ToString(), CurrentFont, FontColor, DrawPoint, DrawFormat);
            }
            for (float i = PointO.X - distance, j = -1; i > 0; i -= distance, --j)
            {
                PointF DrawPoint = new PointF(i, (gridpanel.Height / 2) + thickness * 5);
                StringFormat DrawFormat = new StringFormat();
                DrawFormat.Alignment = StringAlignment.Center;
                // DrawString only draws text, which can be covered by grid line.
                // Therefore, a rectangle should be drawn in its place to make label
                // easier to see.
                SizeF labelSize = new SizeF();
                labelSize = grid.MeasureString(j.ToString(), CurrentFont);
                grid.DrawRectangle(new Pen(this.BackColor, thickness * 2), DrawPoint.X, DrawPoint.Y, labelSize.Width, labelSize.Height);
                grid.DrawString(j.ToString(), CurrentFont, FontColor, DrawPoint, DrawFormat);
            }
            // Non-zero points on Oy axis
            for (float i = PointO.Y + distance, j = -1; i < gridpanel.Height; i += distance, --j)
            {
                PointF DrawPoint = new PointF((gridpanel.Width / 2) - thickness * 5, i);
                StringFormat DrawFormat = new StringFormat();
                DrawFormat.Alignment = StringAlignment.Far;
                DrawFormat.LineAlignment = StringAlignment.Center;
                // DrawString only draws text, which can be covered by grid line.
                // Therefore, a rectangle should be drawn in its place to make label
                // easier to see.
                SizeF labelSize = new SizeF();
                labelSize = grid.MeasureString(j.ToString(), CurrentFont, DrawPoint, DrawFormat);
                // DrawPoint is at bottom-right so we temporarily move it to upper-left
                // to draw rectangle more accurately
                DrawPoint.X -= labelSize.Width;
                DrawPoint.Y -= labelSize.Height;
                grid.DrawRectangle(new Pen(this.BackColor, thickness * 2), DrawPoint.X, DrawPoint.Y, labelSize.Width, labelSize.Height);
                grid.FillRectangle(new SolidBrush(this.BackColor), new RectangleF(DrawPoint, labelSize));
                // Recover the coordinates to draw the text
                DrawPoint.X += labelSize.Width;
                DrawPoint.Y += labelSize.Height;
                grid.DrawString(j.ToString(), CurrentFont, FontColor, DrawPoint, DrawFormat);
            }
            for (float i = PointO.Y - distance, j = 1; i > 0; i -= distance, ++j)
            {
                PointF DrawPoint = new PointF((gridpanel.Width / 2) - thickness * 5, i);
                StringFormat DrawFormat = new StringFormat();
                DrawFormat.Alignment = StringAlignment.Far;
                DrawFormat.LineAlignment = StringAlignment.Center;
                // DrawString only draws text, which can be covered by grid line.
                // Therefore, a rectangle should be drawn in its place to make label
                // easier to see.
                SizeF labelSize = new SizeF();
                labelSize = grid.MeasureString(j.ToString(), CurrentFont, DrawPoint, DrawFormat);
                // DrawPoint is at bottom-right so we temporarily move it to upper-left
                // to draw rectangle more accurately
                DrawPoint.X -= labelSize.Width;
                DrawPoint.Y -= labelSize.Height;
                grid.DrawRectangle(new Pen(this.BackColor, thickness * 2), DrawPoint.X, DrawPoint.Y, labelSize.Width, labelSize.Height);
                grid.FillRectangle(new SolidBrush(this.BackColor), new RectangleF(DrawPoint, labelSize));
                // Recover the coordinates to draw the text
                DrawPoint.X += labelSize.Width;
                DrawPoint.Y += labelSize.Height;
                grid.DrawString(j.ToString(), CurrentFont, FontColor, DrawPoint, DrawFormat);
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_SizeChanged_1(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void drawWindow_Panel1_Paint(object sender, PaintEventArgs e)
        {
            drawWindow.Panel1.Paint += new PaintEventHandler(drawWindow_Panel1_Paint);
            CurrentDistance = DefaultDistance * this.drawWindow.Panel1.Height / DefaultY;
            CurrentFont = new Font("Arial", Math.Min(12, DefaultFontSize * this.drawWindow.Panel1.Height / DefaultY));
            PaintGrid(sender, e, CurrentDistance);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
