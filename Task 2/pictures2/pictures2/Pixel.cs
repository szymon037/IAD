using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pictures2
{
    class Pixel
    {
        public int R;
        public int G;
        public int B;

        public Pixel() { }

        public Pixel(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }

        public void RandomisePixelGray(Random rnd)
        {
            int val = rnd.Next(0, 256);

            R = G = B = val;
      
        }

        public void RandomisePixelColor(Random rnd)
        {
            R = rnd.Next(0, 256);
            G = rnd.Next(0, 256);
            B = rnd.Next(0, 256); 
        }

    }


}
