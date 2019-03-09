using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneNeuron
{
    class Object
    {
        public double X;
        public double Y;
        public int ClassType;
        public Object(Random rnd)
        {
            X = rnd.Next(0, 10) + rnd.NextDouble();
            Y = rnd.Next(0, 10) + rnd.NextDouble();
            ClassType = 0;
        }
        public static  bool operator >(Object a, Object b)
        {
            if (a.X > b.X)
            {
                return true;
            }
            else
                return false;
        }

        public static bool operator <(Object a, Object b)
        {
            if (a.X < b.X)
            {
                return true;
            }
            else
                return false;
        }
    }
}
