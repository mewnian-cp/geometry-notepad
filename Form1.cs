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
        Graphics grid;
        const int thickness = 1;
        Pen OAxisLine = new Pen(Color.Black, 2 * thickness);
        Pen NormalLine = new Pen(Color.Black, thickness);
        SolidBrush FontColor = new SolidBrush(Color.Black);
        Font CurrentFont = new Font("Arial", 12);
        public Form1()
        {
            InitializeComponent();
        }
        private void PaintGrid(object sender, PaintEventArgs e, float distance)
        {
            var gridpanel = sender as Panel;
            grid = e.Graphics;
            Global.Dimension = new PointF(gridpanel.Width, gridpanel.Height);
            Global.PointO = new PointF(gridpanel.Width / 2, gridpanel.Height / 2);
            debug.Text = "(" + Global.PointO.X + "; " + Global.PointO.Y + ")";
            // Process of drawing 2D grid with 1pt = (distance) pixels
            // Width starts at 0 from the LEFT
            // Height starts at 0 from the TOP
            for (float i = Global.PointO.Y; i < gridpanel.Height; i += distance)
            {
                PointF StartPoint = new PointF(0, i);
                PointF EndPoint = new PointF(gridpanel.Width, i);
                Pen CurrentPen = NormalLine;
                if (i == Global.PointO.Y) CurrentPen = OAxisLine;
                grid.DrawLine(CurrentPen, StartPoint, EndPoint);
            }
            for (float i = Global.PointO.Y - distance; i > 0; i -= distance)
            {
                PointF StartPoint = new PointF(0, i);
                PointF EndPoint = new PointF(gridpanel.Width, i);
                Pen CurrentPen = NormalLine;
                if (i == Global.PointO.Y) CurrentPen = OAxisLine;
                grid.DrawLine(CurrentPen, StartPoint, EndPoint);
            }
            for (float i = Global.PointO.X; i < gridpanel.Width; i += distance)
            {
                PointF StartPoint = new PointF(i, 0);
                PointF EndPoint = new PointF(i, gridpanel.Height);
                Pen CurrentPen = NormalLine;
                if (i == Global.PointO.X) CurrentPen = OAxisLine;
                grid.DrawLine(CurrentPen, StartPoint, EndPoint);
            }
            for (float i = Global.PointO.X - distance; i > 0; i -= distance)
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
            for (float i = Global.PointO.X + distance, j = 1; i < gridpanel.Width; i += distance, ++j)
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
            for (float i = Global.PointO.X - distance, j = -1; i > 0; i -= distance, --j)
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
            for (float i = Global.PointO.Y + distance, j = -1; i < gridpanel.Height; i += distance, --j)
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
            for (float i = Global.PointO.Y - distance, j = 1; i > 0; i -= distance, ++j)
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
            Global.CurrentDistance = Global.DefaultDistance * this.drawWindow.Panel1.Height / Global.DefaultY;
            CurrentFont = new Font("Arial", Math.Min(12, Global.DefaultFontSize * this.drawWindow.Panel1.Height / Global.DefaultY));
            PaintGrid(sender, e, Global.CurrentDistance);
        }
        private void PaintTriangle(object sender, PaintEventArgs e)
        {
            var p = sender as Panel;
            grid = e.Graphics;
            Global.polygons[Global.polygons.Count - 1].SortCW();
            Polygon cur = Global.polygons[Global.polygons.Count - 1];
            PointF[] polycur = (cur.GetPolygon()).ToArray();
            int count = cur.GetSize();
            Pen pointline = new Pen(Color.FromArgb(0, 0, 0), thickness * 3);
            SolidBrush pointfill = new SolidBrush(Color.FromArgb(255, 0, 0, 255));
            for (int i = 0; i < count; ++i)
            {
                grid.DrawEllipse(pointline, polycur[i].X - thickness * 2, polycur[i].Y - thickness * 2,
                                            thickness * 4, thickness * 4);
                grid.FillEllipse(pointfill, polycur[i].X - thickness * 2, polycur[i].Y - thickness * 2,
                                            thickness * 4, thickness * 4);
            }    
            Pen line = new Pen(Color.FromArgb(0, 0, 255), thickness * 3);
            SolidBrush fillcol = new SolidBrush(Color.FromArgb(64, 0, 0, 255));
            grid.DrawPolygon(line, polycur);
            grid.FillPolygon(fillcol, polycur);
        }

        private void draw_triangle_Click(object sender, EventArgs e)
        {
            DrawTrianglePopup form = new DrawTrianglePopup();
            form.ShowDialog();
            List<PointF> cur = Global.polygons[Global.polygons.Count - 1].GetPolygon();
            debug.Text = "";
            for (int i = 0; i < cur.Count; ++i)
            {
                debug.Text += "(" + cur[i].X + "; " + cur[i].Y + ")" + Environment.NewLine;
            }
            drawWindow.Panel1.Paint += PaintTriangle;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
