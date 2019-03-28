using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneNeuron
{
    class Object
    {
        public int X;
        public int Y;
        public int ClassType;
        public Object(Random rnd, int maxW, int maxH)
        {
            X = rnd.Next(0, maxW);
            Y = rnd.Next(0, maxH);
            ClassType = 0;
        }
    }
}
