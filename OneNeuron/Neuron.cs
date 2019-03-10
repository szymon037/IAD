using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneNeuron
{
    class Neuron
    {
        public double w0, w1, w2;
        public int correct, incorrect;
        public Neuron()
        {
            Random rnd = new Random();

            w0 = rnd.Next(0, 20);
            w1 = rnd.Next(0, 20);
            w2 = rnd.Next(0, 20);

            correct = 0;
            incorrect = 0;
        }

        public void operation(Object a)
        {
            double u = w0 + w1 * a.X + w2 * a.Y;
            int f_u = 0;

            if (u > 0)
                f_u = 1;
            else f_u = 0;

            changeParameters(a, f_u);
        }

        private void changeParameters(Object a, int f_u)
        {
            if (a.ClassType == 0 && f_u == 1)
            {
                w0 = w0 - f_u;
                w1 = w1 - f_u * a.X;
                w2 = w2 - f_u * a.Y;

                ++incorrect;
            }
            else if (a.ClassType == 1 && f_u == 0)
            {
                w0 = w0 + f_u;
                w1 = w1 + f_u * a.X;
                w2 = w2 + f_u * a.Y;

                ++incorrect;
            }
            else if (a.ClassType == f_u)
                ++correct;
        }        
    }
}
