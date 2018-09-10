using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum TypeOfPixel { NotDetermined, Line, InnerCorner, OuterCorner }

    public class Pixel
    {
        public int X1;
        public int X2;
        public Color Color;
        public TypeOfPixel Type;
        public int PixelBorderEdges;
    }
}
