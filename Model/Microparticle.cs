using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Microparticle
    {
        public int Number { get; set; }

        public List<Pixel> Pixels { get; set; }
        public int CountOfPixels => Pixels.Count;
        public List<Pixel> Border { get; set; }

        public int BorderEdges { get; set; }
        public double F { get; set; }

        public int MinX1;
        public int MinX2;
        public int MaxX1;
        public int MaxX2;
        public Bitmap Image { get; set; }
        public Phase Phase { get; set; }
        public Class ClassOfMicroparticle { get; set; }

        public double Coefficient { get; set; }
        public double R { get; set; }
        public double D { get; set; }
        public double L { get; set; }
    }
}
