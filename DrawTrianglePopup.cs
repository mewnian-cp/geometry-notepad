using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace geometry_notepad
{
    public partial class DrawTrianglePopup : Form
    {
        public DrawTrianglePopup()
        {
            InitializeComponent();
        }
        private void draw_button_Click(object sender, EventArgs e)
        {
            float PtAX = 0, PtAY = 0, PtBX = 0, PtBY = 0, PtCX = 0, PtCY = 0;
            bool ok = float.TryParse(PtA_X.Text, out PtAX) && float.TryParse(PtA_Y.Text, out PtAY) 
                    && float.TryParse(PtB_X.Text, out PtBX) && float.TryParse(PtB_Y.Text, out PtBY)
                    && float.TryParse(PtC_X.Text, out PtCX) && float.TryParse(PtC_Y.Text, out PtCY);
            while (ok == false)
            {
                MessageBox.Show("Invalid input!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Polygon new_polygon = new Polygon(new List<PointF>());
            new_polygon.Add(new PointF(PtAX, PtAY));
            new_polygon.Add(new PointF(PtBX, PtBY));
            new_polygon.Add(new PointF(PtCX, PtCY));
            Global.polygons.Add(new_polygon);
            this.Close();
        }
    }
}
