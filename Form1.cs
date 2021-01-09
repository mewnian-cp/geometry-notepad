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
        const int dotsize = 5;
        Pen OAxisLine = new Pen(Color.Black, 2 * thickness);
        Pen NormalLine = new Pen(Color.Black, thickness);
        SolidBrush FontColor = new SolidBrush(Color.Black);

        Pen poly_stroke = new Pen(Color.FromArgb(0, 0, 255), thickness * 3);
        SolidBrush poly_fill = new SolidBrush(Color.FromArgb(64, 0, 0, 255));

        float PanelWidth, PanelHeight;

        bool IsDrawing = false, IsMoving = false;

        PointF StartCursor;

        Bitmap org_grid;
        List<Polygon> polygons = new List<Polygon>();
        List<Bitmap> bitmap_list = new List<Bitmap>();
        List<PointF> cur_dots = new List<PointF>();
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        PointF PointToPixel(PointF point)
        {
            var v = point;
            v.X = Global.PointO.X + point.X * Global.CurrentDistance;
            v.Y = Global.PointO.Y - point.Y * Global.CurrentDistance;
            return v;

        }
        void PolygonDraw(ref Graphics g, Polygon p, Pen ptstroke, SolidBrush ptfill)
        {
            List<PointF> pts = p.GetPolygon();
            List<PointF> temp_pts = new List<PointF>();
            for (int i = 0; i < pts.Count; ++i)
            {
                temp_pts.Add(PointToPixel(pts[i]));
            }    
            g.FillPolygon(ptfill, temp_pts.ToArray());
            g.DrawPolygon(ptstroke, temp_pts.ToArray());
        }

        void PrintLog(Polygon p, int stt)
        {
            if (stt > 0) log.Text += ("===============================" + Environment.NewLine);
            log.Text += ("Tọa độ các điểm của đa giác " + stt.ToString() + " là:" + Environment.NewLine);
            List<PointF> pts = p.GetPolygon();
            for (int i = 0; i < p.GetSize(); ++i)
                log.Text += ("(" + pts[i].X.ToString() + ", " + pts[i].Y.ToString() + ")" + Environment.NewLine);
            log.Text += ("Chu vi: " + p.GetPerimeter().ToString() + Environment.NewLine);
            log.Text += ("Diện tích: " + p.GetArea().ToString() + Environment.NewLine);
        }
        private void PaintGrid(object sender, ref Graphics e, float distance)
        {
            var gridpanel = sender as Panel;
            grid = e;
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
                labelSize = grid.MeasureString("0", Global.CurrentFont, DrawPoint, DrawFormat);
                // DrawPoint is at upper-right so we temporarily move it to upper-left
                // to draw rectangle more accurately
                DrawPoint.X -= labelSize.Width;
                grid.DrawRectangle(new Pen(this.BackColor, thickness * 2), DrawPoint.X, DrawPoint.Y, labelSize.Width, labelSize.Height);
                grid.FillRectangle(new SolidBrush(this.BackColor), new RectangleF(DrawPoint, labelSize));
                // Recover the coordinates to draw the text
                DrawPoint.X += labelSize.Width;
                grid.DrawString("0", Global.CurrentFont, FontColor, DrawPoint, DrawFormat);
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
                labelSize = grid.MeasureString(j.ToString(), Global.CurrentFont);
                grid.DrawRectangle(new Pen(this.BackColor, thickness * 2), DrawPoint.X, DrawPoint.Y, labelSize.Width, labelSize.Height);
                grid.DrawString(j.ToString(), Global.CurrentFont, FontColor, DrawPoint, DrawFormat);
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
                labelSize = grid.MeasureString(j.ToString(), Global.CurrentFont);
                grid.DrawRectangle(new Pen(this.BackColor, thickness * 2), DrawPoint.X, DrawPoint.Y, labelSize.Width, labelSize.Height);
                grid.DrawString(j.ToString(), Global.CurrentFont, FontColor, DrawPoint, DrawFormat);
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
                labelSize = grid.MeasureString(j.ToString(), Global.CurrentFont, DrawPoint, DrawFormat);
                // DrawPoint is at bottom-right so we temporarily move it to upper-left
                // to draw rectangle more accurately
                DrawPoint.X -= labelSize.Width;
                DrawPoint.Y -= labelSize.Height;
                grid.DrawRectangle(new Pen(this.BackColor, thickness * 2), DrawPoint.X, DrawPoint.Y, labelSize.Width, labelSize.Height);
                grid.FillRectangle(new SolidBrush(this.BackColor), new RectangleF(DrawPoint, labelSize));
                // Recover the coordinates to draw the text
                DrawPoint.X += labelSize.Width;
                DrawPoint.Y += labelSize.Height;
                grid.DrawString(j.ToString(), Global.CurrentFont, FontColor, DrawPoint, DrawFormat);
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
                labelSize = grid.MeasureString(j.ToString(), Global.CurrentFont, DrawPoint, DrawFormat);
                // DrawPoint is at bottom-right so we temporarily move it to upper-left
                // to draw rectangle more accurately
                DrawPoint.X -= labelSize.Width;
                DrawPoint.Y -= labelSize.Height;
                grid.DrawRectangle(new Pen(this.BackColor, thickness * 2), DrawPoint.X, DrawPoint.Y, labelSize.Width, labelSize.Height);
                grid.FillRectangle(new SolidBrush(this.BackColor), new RectangleF(DrawPoint, labelSize));
                // Recover the coordinates to draw the text
                DrawPoint.X += labelSize.Width;
                DrawPoint.Y += labelSize.Height;
                grid.DrawString(j.ToString(), Global.CurrentFont, FontColor, DrawPoint, DrawFormat);
            }
            e = grid;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_SizeChanged_1(object sender, EventArgs e)
        {
        }

        private void drawWindow_Panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(bitmap_list[bitmap_list.Count - 1], 0, 0, PanelWidth, PanelHeight);
            g.Dispose();
        }

        private void drawWindow_Panel1_MouseHover(object sender, EventArgs e)
        {
           
        }

        private void move_button_Click(object sender, EventArgs e)
        {
            IsMoving = !(IsMoving);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IsDrawing = true;
            draw_button.BackColor = Color.MediumTurquoise;
        }

        private void drawWindow_Panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!IsDrawing) return;
            PointF mouse_loc = e.Location;
            double orgX = (mouse_loc.X - Global.PointO.X) / Global.CurrentDistance, orgY = (Global.PointO.Y - mouse_loc.Y) / Global.CurrentDistance;
            double posX = Math.Round(orgX), posY = Math.Round(orgY);
            double dist = (posX - orgX) * (posX - orgX) + (posY - orgY) * (posY - orgY);
            if (dist <= Global.radius)
            {
                PointF comp = new PointF((float)posX, (float)posY);
                if (cur_dots.Contains(comp) == true && cur_dots[0] != comp) return;
                if (cur_dots.Count == 1 && cur_dots[0] == comp) return;
                if (cur_dots.Count > 1 && cur_dots[0] == comp)
                {
                    bitmap_list.Clear();
                    Bitmap new_bm = org_grid;
                    Graphics g;
                    g = Graphics.FromImage(new_bm);
                    bitmap_list.Add(new_bm);
                    Polygon p = new Polygon(cur_dots);
                    polygons.Add(p);
                    PolygonDraw(ref g, polygons[polygons.Count - 1], poly_stroke, poly_fill);
                    PrintLog(p, polygons.Count - 1);
                    g.Dispose();
                    cur_dots.Clear();
                    this.Refresh();
                    IsDrawing = false;
                    draw_button.BackColor = Color.White;
                    return;
                }   
                else
                {
                    bitmap_list.Add((Bitmap)bitmap_list[bitmap_list.Count - 1].Clone());
                    cur_dots.Add(new PointF((float)posX, (float)posY));
                    Bitmap new_bm = bitmap_list[bitmap_list.Count - 1];
                    Graphics g;
                    g = Graphics.FromImage(new_bm);
                    SolidBrush ptfill = new SolidBrush(Color.White);
                    Pen ptstroke = new Pen(Color.Black, thickness);
                    g.FillEllipse(ptfill, new RectangleF(Global.PointO.X + Global.CurrentDistance * (float)posX - dotsize,
                                                         Global.PointO.Y - Global.CurrentDistance * (float)posY - dotsize,
                                                         dotsize * 2, dotsize * 2));
                    g.DrawEllipse(ptstroke, new RectangleF(Global.PointO.X + Global.CurrentDistance * (float)posX - dotsize,
                                                         Global.PointO.Y - Global.CurrentDistance * (float)posY - dotsize,
                                                         dotsize * 2, dotsize * 2));
                    ptstroke = new Pen(Color.Black, thickness * 2f);
                    if (cur_dots.Count > 1)
                    {
                        g.DrawLine(ptstroke, PointToPixel(cur_dots[cur_dots.Count - 2]),
                                             PointToPixel(cur_dots[cur_dots.Count - 1]));
                    }
                    bitmap_list[bitmap_list.Count - 1] = new_bm;
                    g.Dispose();
                }    
                this.Refresh();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            org_grid = new Bitmap(this.drawWindow.Panel1.Width, this.drawWindow.Panel1.Height, 
                                    System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Graphics g;
            g = Graphics.FromImage(org_grid); 
            g.Clear(Color.White);
            Global.CurrentDistance = Global.DefaultDistance * this.drawWindow.Panel1.Height / Global.DefaultY;
            Global.CurrentFont = new Font("Arial", Math.Min(12, Global.DefaultFontSize * this.drawWindow.Panel1.Height / Global.DefaultY));
            PaintGrid(this.drawWindow.Panel1, ref g, Global.CurrentDistance);
            PanelWidth = drawWindow.Panel1.Width;
            PanelHeight = drawWindow.Panel1.Height;
            bitmap_list.Add(org_grid);
            g.Dispose();
        }
    }
}
