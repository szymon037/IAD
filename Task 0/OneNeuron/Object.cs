using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrix;

namespace OneNeuron
{
    class Object
    {
        public int X;
        public int Y;
        public int ClassType;
        public Object(Random rnd, int maxW, int maxH)
        {
            Matrix.Matrix a = new Matrix.Matrix(2, 3)
            {
                tab = new double[,]
                {
                    { 1,2 },
                    { 2,1 }
                }
            };
            X = rnd.Next(0, maxW);
            Y = rnd.Next(0, maxH);
            ClassType = 0;
        }
    }
}
