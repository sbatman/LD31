using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD31.Math
{
    struct Colour
    {
        public Byte R;
        public Byte G;
        public Byte B;
        public Byte A;

        public Colour(Byte rgb, Byte a)
        {
            R = rgb;
            G = rgb;
            B = rgb;
            A = a;
        }

        public Colour(Byte r, Byte g, Byte b, Byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
    }
}
