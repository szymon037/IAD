using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pictures2
{
    class FrameOfPixels
    {
        private int sizeOfFrame;
        public List<Pixel> pixels;

        public FrameOfPixels(int SIZE_OF_FRAME)
        {
            sizeOfFrame = SIZE_OF_FRAME;
            pixels = new List<Pixel>();
        }

        public FrameOfPixels (int SIZE_OF_FRAME, int MODE, Random rnd)
        {
            sizeOfFrame = SIZE_OF_FRAME;
            pixels = new List<Pixel>();
            for (int i = 0; i < sizeOfFrame * sizeOfFrame; ++i)
            {
                pixels.Add(new Pixel());

                
                if (MODE == 0)
                    pixels[i].RandomisePixelGray(rnd);
                else if (MODE == 1)
                    pixels[i].RandomisePixelColor(rnd);
                
            }
        }

    }
}
