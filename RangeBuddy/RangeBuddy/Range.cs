using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangeBuddy
{
    public class Range
    {
        public bool Enabled;
        public int Radius;
        public Color Color;
        public int Index;

        public Range(int index, Color color, bool enabled = true, int radius = 50)
        {
            Color = color;
            Radius = radius;
            Enabled = enabled;
            Index = index;
        }
    }
}
