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
    class Polygon
    {
        class ComparePointsCW : IComparer<PointF>
        {
            public int Compare(PointF A, PointF B)
            {
                int typA = 0, typB = 0;
                if (A.X < 0) typA = Convert.ToInt32(A.Y < 0);
                else typA = 3 - Convert.ToInt32(A.Y < 0);
                if (B.X < 0) typB = Convert.ToInt32(B.Y < 0);
                else typB = 3 - Convert.ToInt32(B.Y < 0);
                int comp = typA - typB;
                if (comp != 0)
                {
                    comp = Convert.ToInt32(A.X * B.Y - B.X * A.Y);
                    if (comp != 0) return comp;
                    else
                    {
                        comp = Convert.ToInt32(A.X * A.X + A.Y * A.Y - B.X * B.X);
                        return comp;
                    }
                }
                else if (typA < typB) return -1;
                else return 1;
            }
        }

        List<PointF> points = new List<PointF>();
        float Perimeter, Area;
        public Polygon(List<PointF> new_points)
        {
            points = new_points;
        }
        public void Add(PointF new_point)
        {
            new_point.X = new_point.X * Global.CurrentDistance + Global.PointO.X;
            new_point.Y = Global.Dimension.Y - (new_point.Y * Global.CurrentDistance + Global.PointO.Y);
            points.Add(new_point);
        }

        public int GetSize()
        {
            return points.Count;
        }

        public PointF GetPoint(int index)
        {
            if (index >= 0 && index <= points.Count) return points[index];
            else
            {
                throw new IndexOutOfRangeException("No such index exists in the array.");
            }
        }
        public List<PointF> GetPolygon()
        {
            return points;
        }

        public float GetPerimeter()
        {
            int n = points.Count;
            float r = 0;
            for (int i = 0; i < n; ++i)
            {
                float len = (points[i].X - points[(i + 1) % n].X) * (points[i].X - points[(i + 1) % n].X)
                          + (points[i].Y - points[(i + 1) % n].Y) * (points[i].Y - points[(i + 1) % n].Y);
                r += (float)Math.Sqrt(len);
            }
            Perimeter = r;
            return r;
        }

        public float GetArea()
        {
            List<PointF> temp = points;
            temp.Sort(new ComparePointsCW());
            int n = temp.Count;
            float r = 0;
            for (int i = 0; i < n; ++i)
            {
                r += (temp[(i + 1) % n].X + temp[i].X) * (temp[(i + 1) % n].Y - temp[i].Y);
            }
            return Math.Abs(r / 2.0f);
        }
    }
}
