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

        Pen highlight_stroke = new Pen(Color.FromArgb(255, 0, 0), thickness * 3);
        SolidBrush highlight_fill = new SolidBrush(Color.FromArgb(64, 255, 0, 0));

        float PanelWidth, PanelHeight;

        bool IsDrawing = false, IsMovingAllowed = false, IsMoving = false, IsZooming = false;

        int CURRENT_SIZE = 100;
        float SmallestFactor = 1;

        PointF StartCursor;

        Bitmap org_grid;
        List<Polygon> polygons = new List<Polygon>();
        List<Bitmap> bitmap_list = new List<Bitmap>();
        List<PointF> cur_dots = new List<PointF>();
        Bitmap highlight;

        int index = -1;
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true; 
            this.MouseWheel += new MouseEventHandler(drawWindow_Panel1_MouseWheel);
        }

        PointF PointToPixel(PointF point)
        {
            var v = point;
            v.X = Global.PointO.X + point.X * Global.CurrentDistance / SmallestFactor;
            v.Y = Global.PointO.Y - point.Y * Global.CurrentDistance / SmallestFactor;
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

        void PrintLog(Polygon p)
        {
            log.ReadOnly = false;
            log.Text = ("Coordinates of vertices:" + Environment.NewLine);
            List<PointF> pts = p.GetPolygon();
            for (int i = 0; i < p.GetSize(); ++i)
                log.Text += ("(" + pts[i].X.ToString() + ", " + pts[i].Y.ToString() + ")" + Environment.NewLine);
            log.Text += Environment.NewLine;
            log.Text += ("Perimeter: " + p.GetPerimeter().ToString() + Environment.NewLine);
            log.Text += ("Area: " + p.GetArea().ToString() + Environment.NewLine);
            log.ReadOnly = true;
        }
        private void PaintGrid(object sender, ref Graphics e, float distance)
        {
            var gridpanel = sender as Panel;
            grid = e;
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
                PointF DrawPoint = new PointF(Global.PointO.X - thickness * 5, Global.PointO.Y + thickness * 5);
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
            for (float i = Global.PointO.X + distance, j = SmallestFactor;
                                                       i < gridpanel.Width; 
                                                       i += distance, j += SmallestFactor)
            {
                PointF DrawPoint = new PointF(i, Global.PointO.Y + thickness * 5);
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
            for (float i = Global.PointO.X - distance, j = -SmallestFactor; 
                                                       i > 0; 
                                                       i -= distance, j -= SmallestFactor)
            {
                PointF DrawPoint = new PointF(i, Global.PointO.Y + thickness * 5);
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
            for (float i = Global.PointO.Y + distance, j = -SmallestFactor; 
                                                       i < gridpanel.Height; 
                                                       i += distance, j -= SmallestFactor)
            {
                PointF DrawPoint = new PointF(Global.PointO.X - thickness * 5, i);
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
            for (float i = Global.PointO.Y - distance, j = SmallestFactor; 
                                                       i > 0; 
                                                       i -= distance, j += SmallestFactor)
            {
                PointF DrawPoint = new PointF(Global.PointO.X - thickness * 5, i);
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

        void ResetGrid()
        {
            bitmap_list.Clear();
            Graphics g;
            if (org_grid != null) org_grid.Dispose();
            org_grid = new Bitmap(this.drawWindow.Panel1.Width, this.drawWindow.Panel1.Height,
                                    System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            g = Graphics.FromImage(org_grid);
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, drawWindow.Panel1.Width, drawWindow.Panel1.Height);
            Global.CurrentFont = new Font("Arial", Math.Min(12, Global.DefaultFontSize * this.drawWindow.Panel1.Height / Global.DefaultY));
            PaintGrid(this.drawWindow.Panel1, ref g, Global.CurrentDistance);
            PanelWidth = drawWindow.Panel1.Width;
            PanelHeight = drawWindow.Panel1.Height;
            for (int i = 0; i < polygons.Count; ++i)
            {
                if (i == index) continue;
                PolygonDraw(ref g, polygons[i], poly_stroke, poly_fill);
            }
            if (index >= 0) PolygonDraw(ref g, polygons[index], highlight_stroke, highlight_fill);
            SolidBrush ptfill = new SolidBrush(Color.White);
            for (int i = 0; i < cur_dots.Count; ++i)
            {
                Pen ptstroke = new Pen(Color.Black, thickness);
                g.FillEllipse(ptfill, new RectangleF(Global.PointO.X + Global.CurrentDistance / SmallestFactor * cur_dots[i].X - dotsize,
                                                     Global.PointO.Y - Global.CurrentDistance / SmallestFactor * cur_dots[i].Y - dotsize,
                                                     dotsize * 2, dotsize * 2));
                g.DrawEllipse(ptstroke, new RectangleF(Global.PointO.X + Global.CurrentDistance / SmallestFactor * cur_dots[i].X - dotsize,
                                                     Global.PointO.Y - Global.CurrentDistance / SmallestFactor * cur_dots[i].Y - dotsize,
                                                     dotsize * 2, dotsize * 2));
                ptstroke = new Pen(Color.Black, thickness * 2f);
                if (i > 0)
                {
                    g.DrawLine(ptstroke, PointToPixel(cur_dots[i - 1]),
                                         PointToPixel(cur_dots[i]));
                }
            }
            bitmap_list.Add((Bitmap)org_grid.Clone());
            g.Dispose();
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
            ResetGrid();
            if (highlight == null) g.DrawImage((Bitmap)bitmap_list[bitmap_list.Count - 1].Clone(), 0, 0, PanelWidth, PanelHeight);
            else
            {
                DrawHighlight();
                g.DrawImage((Bitmap)highlight.Clone(), 0, 0, PanelWidth, PanelHeight);
            }
            g.Dispose();
        }
        private void drawWindow_Panel1_MouseHover(object sender, EventArgs e)
        {
           
        }

        private void move_button_Click(object sender, EventArgs e)
        {
            if (!IsMovingAllowed)
            {
                IsMovingAllowed = true;
                move_button.BackColor = Color.MediumTurquoise;
            }    
            else
            {
                IsMovingAllowed = false; IsMoving = false;
                move_button.BackColor = Color.White;
            }    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IsDrawing = true;
            draw_button.BackColor = Color.MediumTurquoise;
        }

        private void size_up_Click(object sender, EventArgs e)
        {

        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        void DrawHighlight()
        {
            index = (int)choosePolygon.Value - 1;
            highlight = (Bitmap)org_grid.Clone();
            Graphics g = Graphics.FromImage(highlight);
            for (int i = 0; i < polygons.Count; ++i)
            {
                if (i == index) continue;
                PolygonDraw(ref g, polygons[i], poly_stroke, poly_fill);
            }
            PolygonDraw(ref g, polygons[index], highlight_stroke, highlight_fill);
            SolidBrush ptfill = new SolidBrush(Color.White);
            for (int i = 0; i < cur_dots.Count; ++i)
            {
                Pen ptstroke = new Pen(Color.Black, thickness);
                g.FillEllipse(ptfill, new RectangleF(Global.PointO.X + Global.CurrentDistance / SmallestFactor * cur_dots[i].X - dotsize,
                                                     Global.PointO.Y - Global.CurrentDistance / SmallestFactor * cur_dots[i].Y - dotsize,
                                                     dotsize * 2, dotsize * 2));
                g.DrawEllipse(ptstroke, new RectangleF(Global.PointO.X + Global.CurrentDistance / SmallestFactor * cur_dots[i].X - dotsize,
                                                     Global.PointO.Y - Global.CurrentDistance / SmallestFactor * cur_dots[i].Y - dotsize,
                                                     dotsize * 2, dotsize * 2));
                ptstroke = new Pen(Color.Black, thickness * 2f);
                if (i > 0)
                {
                    g.DrawLine(ptstroke, PointToPixel(cur_dots[i - 1]),
                                         PointToPixel(cur_dots[i]));
                }
            }
            PrintLog(polygons[index]);
        }
        private void choosePolygon_ValueChanged(object sender, EventArgs e)
        {
            index = (int)choosePolygon.Value - 1;
            if (choosePolygon.Value == 0)
            {
                highlight = null;
                log.Text = "";
                ResetGrid();
            }
            else DrawHighlight();
            this.Refresh();
        }

        private void drawWindow_Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsMovingAllowed)
            {
                IsMoving = true;
                StartCursor = e.Location;
            }
        }

        private void drawWindow_Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMoving)
            {
                PointF current_loc = e.Location;
                Global.PointO.X += (current_loc.X - StartCursor.X);
                Global.PointO.Y += (current_loc.Y - StartCursor.Y);
                ResetGrid();
                if (highlight != null) DrawHighlight();
                this.Refresh();
            }    
        }

        private void drawWindow_Panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!IsZooming) return;
            int percentageChange = e.Delta;
            CURRENT_SIZE = Math.Min(Math.Max(100, CURRENT_SIZE + (percentageChange / 6)), (int)1e9);
            float NewDistance = Global.BaseDistance * ((float)CURRENT_SIZE / 100f);
            float Factor = 1;
            for (int i = 1; i < 22; ++i)
            {
                float ftr = Factor;
                if (i % 3 == 2) ftr *= 2.5f;
                else ftr *= 2f;
                float iDist = Global.BaseDistance * ftr;
                if (iDist < NewDistance) Factor = ftr;
                else
                {
                    float diffprev = NewDistance - Factor * Global.BaseDistance;
                    float diffnext = iDist - NewDistance;
                    if (diffnext < diffprev) Factor = ftr;
                    break;
                }    
            }
            Global.CurrentDistance = Global.BaseDistance / ((NewDistance / Factor) / Global.BaseDistance);
            SmallestFactor = Factor;
            ResetGrid();
            if (highlight != null) DrawHighlight();
            this.Refresh();
        }
        private void zoom_button_Click(object sender, EventArgs e)
        {
            if (!IsZooming)
            {
                IsZooming = true;
                zoom_button.BackColor = Color.MediumTurquoise;
            }
            else
            {
                IsZooming = false;
                zoom_button.BackColor = Color.White;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void origin_button_Click(object sender, EventArgs e)
        {
            Global.PointO = new PointF(drawWindow.Panel1.Width / 2, drawWindow.Panel1.Height / 2);
            ResetGrid();
            if (highlight != null) DrawHighlight();
            this.Refresh();
        }

        private void reset_button_Click(object sender, EventArgs e)
        {
            Global.CurrentDistance = Global.BaseDistance;
            Global.PointO = new PointF(this.drawWindow.Panel1.Width / 2, this.drawWindow.Panel1.Height / 2);
            CURRENT_SIZE = 100;
            SmallestFactor = 1.0f;
            ResetGrid();
            if (highlight != null) DrawHighlight();
            this.Refresh();
        }

        private void clear_all_Click(object sender, EventArgs e)
        {
            if (IsDrawing)
            {
                MessageBox.Show("You must finish drawing this polygon!", "Geometry Notepad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult result = MessageBox.Show("Do you want to erase ALL POLYGONS?" + Environment.NewLine
                                                  + "You can't undo this action once it is done.", "Geometry Notepad", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                polygons.Clear();
                highlight = null;
                choosePolygon.Maximum = 0;
                ResetGrid();
                this.Refresh();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to close the app?", "Geometry Notepad", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            e.Cancel = (result == DialogResult.No);
        }

        private void about_button_Click(object sender, EventArgs e)
        {
            Form2 aboutform = new Form2();
            aboutform.Show();
        }

        private void drawWindow_Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            IsMoving = false;
        }

        private void unselect_button_Click(object sender, EventArgs e)
        {
            choosePolygon.Value = 0;
        }

        private void drawWindow_Panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!IsDrawing) return;
            PointF mouse_loc = e.Location;
            double orgX = (mouse_loc.X - Global.PointO.X) / Global.CurrentDistance * SmallestFactor, 
                   orgY = (Global.PointO.Y - mouse_loc.Y) / Global.CurrentDistance * SmallestFactor;
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
                    Bitmap new_bm = (Bitmap)org_grid.Clone();
                    Graphics g;
                    g = Graphics.FromImage(new_bm);
                    bitmap_list.Add(new_bm);
                    Polygon p = new Polygon(cur_dots);
                    cur_dots.Clear();
                    polygons.Add(p);
                    PolygonDraw(ref g, polygons[polygons.Count - 1], poly_stroke, poly_fill);
                    choosePolygon.Maximum = polygons.Count;
                    choosePolygon.Value = polygons.Count;
                    g.Dispose();
                    this.Refresh();
                    IsDrawing = false;
                    draw_button.BackColor = Color.White;
                    return;
                }   
                else
                {
                    bitmap_list.Add((Bitmap)bitmap_list[bitmap_list.Count - 1].Clone());
                    cur_dots.Add(new PointF((float)posX, (float)posY));
                    Bitmap new_bm;
                    if (highlight == null) new_bm = (Bitmap)bitmap_list[bitmap_list.Count - 1].Clone();
                    else new_bm = (Bitmap)highlight.Clone();
                    Graphics g;
                    g = Graphics.FromImage(new_bm);
                    SolidBrush ptfill = new SolidBrush(Color.White);
                    for (int i = 0; i < cur_dots.Count; ++i)
                    {
                        Pen ptstroke = new Pen(Color.Black, thickness);
                        g.FillEllipse(ptfill, new RectangleF(Global.PointO.X + Global.CurrentDistance / SmallestFactor * cur_dots[i].X - dotsize,
                                                             Global.PointO.Y - Global.CurrentDistance / SmallestFactor * cur_dots[i].Y - dotsize,
                                                             dotsize * 2, dotsize * 2));
                        g.DrawEllipse(ptstroke, new RectangleF(Global.PointO.X + Global.CurrentDistance / SmallestFactor * cur_dots[i].X - dotsize,
                                                             Global.PointO.Y - Global.CurrentDistance / SmallestFactor * cur_dots[i].Y - dotsize,
                                                             dotsize * 2, dotsize * 2));
                        ptstroke = new Pen(Color.Black, thickness * 2f);
                        if (i > 0)
                        {
                            g.DrawLine(ptstroke, PointToPixel(cur_dots[i - 1]),
                                                 PointToPixel(cur_dots[i]));
                        }
                    }
                    if (highlight == null) bitmap_list[bitmap_list.Count - 1] = new_bm;
                    else highlight = new_bm;
                    g.Dispose();
                    this.Refresh();
                }    
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var loading = new LoadForm();
            this.Visible = false;
            loading.Show();
            Global.BaseDistance = Global.DefaultDistance * this.drawWindow.Panel1.Height / Global.DefaultY;
            Global.CurrentDistance = Global.BaseDistance;
            Global.PointO = new PointF(this.drawWindow.Panel1.Width / 2, this.drawWindow.Panel1.Height / 2);
            ResetGrid();
            loading.Close();
            this.Visible = true;
        }
    }
}
