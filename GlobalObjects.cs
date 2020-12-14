using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace geometry_notepad
{
    static class Global
    {
        public const float DefaultX = 1280, DefaultY = 720;
        public const float DefaultDistance = 80;
        public const float DefaultFontSize = 12;
        public static List<Polygon> polygons = new List<Polygon>();
        public static Font CurrentFont = new Font("Arial", 12);
        public static float CurrentDistance = Global.DefaultDistance;
        public static PointF PointO, Dimension;
    }
}
