using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_2
{
    class Kohonen
    {
        public int gridWidth;
        public int gridHeight;

        public Neuron[][] neurons;

        private double range_init;
        private double range_fin;
        private double range_curr;

        private double learningRate_init;
        private double learningRate_fin;
        private double learningRate_curr;

        public Kohonen (int width, int height)
        {
            gridWidth = width;
            gridHeight = height;            

            range_init = 1;
            range_fin = 0.1;
            range_curr = range_init;

            learningRate_init = 0.1;
            learningRate_fin = 0.005;
            learningRate_curr = learningRate_init;

            neurons = new Neuron[gridWidth][];
            for (int i = 0; i < gridWidth; ++i)
            {
                neurons[i] = new Neuron[gridWidth];
            }


        }

        public void Randomise(double min, double max)
        {
            Random rnd = new Random();

            for (int i = 0; i < gridWidth; ++i)
            {
                for (int j = 0; j < gridHeight; ++j)
                {
                    neurons[i][j] = new Neuron();

                    neurons[i][j].X = rnd.NextDouble() * (max - min) + min;
                    neurons[i][j].Y = rnd.NextDouble() * (max - min) + min;
                }
            }
        }  

        private double EuclideanDistance(Neuron neuron, double[] point)
        {
            return Math.Sqrt((neuron.X - point[0]) * (neuron.X - point[0]) + (neuron.Y - point[1]) * (neuron.Y - point[1]));
        }

        public void Train (double[] input, int iteration_fin, int iteration_curr)
        {
            ////////////////////////
            ///AKTUALIZOWANIE WSPOŁCZYNNIKÓW SIECI

            range_curr = range_init * Math.Pow((range_fin / range_init), (iteration_curr / iteration_fin));
            learningRate_curr = learningRate_init * Math.Pow((learningRate_fin / learningRate_init), (iteration_curr / iteration_fin));

            //////////////////// 
            ///ZNAJDOWANIE NAJBLIŻSZEGO NEURONU (BMU)

            double distance = EuclideanDistance(neurons[0][0], input);
            double tempDistance = 0;
            Neuron BMU = neurons[0][0];

            int x = 0, y = 0;

            for (int i = 0; i < gridWidth; ++i)               //znajdowanie najbliższego neuronu
            {
                for (int j = 0; j < gridHeight; ++j)
                {
                    tempDistance = EuclideanDistance(neurons[i][j], input);
                    if (tempDistance < distance)
                    {
                        distance = tempDistance;
                        BMU = neurons[i][j];
                        x = i;
                        y = j;
                    }
                }
            }

            ////////////////////////
            ///AKTUALIZOWANIE WAG NEURONÓW

            double dist = 0;

            for (int i = 0; i < gridWidth; ++i)               //znajdowanie najbliższego neuronu
            {
                for (int j = 0; j < gridHeight; ++j)
                {
                    dist = Math.Abs(x - i) + Math.Abs(y - j);

                    neurons[i][j].X += Math.Exp((-1) * (dist * dist) / (2 * range_curr * range_curr)) * learningRate_curr * (input[0] - neurons[i][j].X);
                    neurons[i][j].Y += Math.Exp((-1) * (dist * dist) / (2 * range_curr * range_curr)) * learningRate_curr * (input[1] - neurons[i][j].Y);
                }
            }

        }
    }
}
