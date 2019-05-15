using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pictures2
{
    class Neuron
    {
        private int sizeOfFrame;
        public FrameOfPixels weights;

        public Neuron(int SIZE_OF_FRAME, int MODE, Random rnd)      //mode = 0 -> gray, 1 -> color
        {
            weights = new FrameOfPixels(SIZE_OF_FRAME, MODE, rnd);      
        }
    }
}
