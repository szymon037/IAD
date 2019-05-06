using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_2
{
    class NeuralGas
    {
        public int numberOfNeurons;
        public double[][] neurons;
        public double range_init;
        public double range_fin;
        public double range_curr;
        public double learningRate_init;
        public double learningRate_fin;
        public double learningRate_curr;

        private void RandomiseNeurons(double min, double max)   //losowanie wektora wag neuronów
        {
            Random rnd = new Random();
            for (int i = 0; i < numberOfNeurons; ++i)
            {
                neurons[i][0] = rnd.NextDouble() * (max - min) + min;
                neurons[i][1] = rnd.NextDouble() * (max - min) + min;
            }
        }

        public NeuralGas(int neurons_quantity, double min, double max)
        {
            numberOfNeurons = neurons_quantity;
            range_init = 0.45;
            range_fin = 0.01;
            range_curr = range_init;

            learningRate_init = 0.1;
            learningRate_fin = 0.005;
            learningRate_curr = learningRate_init;

            neurons = new double[numberOfNeurons][];
            for (int i = 0; i < numberOfNeurons; ++i)
                neurons[i] = new double[3];                 //trzecia kolumna przechowuje distance

            RandomiseNeurons(min, max);
        }

        private double Distance(double[] point1, double[] point2)
        {
            return Math.Sqrt((point1[0] - point2[0]) * (point1[0] - point2[0]) + (point1[1] - point2[1]) * (point1[1] - point2[1]));
        }

        public void Train(double[] input, int iteration_fin, int iteration_curr)
        {
            Update(iteration_fin, iteration_curr);

            double[] temp = new double[2];

            ///////////////////////////
            ///OBLICZANIE DISTANCE DLA KAŻDEGO NEURONU

            for (int i = 0; i < numberOfNeurons; ++i)
            {
                neurons[i][2] = Distance(neurons[i], input);
            }

            ///////////////////////////
            ///SORTOWANIE

            for (int i = 0; i < numberOfNeurons; ++i)
            {
                for (int j = 0; j < numberOfNeurons - 1; ++j)
                {
                    if (neurons[j][2] > neurons[j + 1][2]/*Distance(neurons[j], input) > Distance(neurons[j + 1], input)*/)
                    {
                        temp = neurons[j + 1];
                        neurons[j + 1] = neurons[j];
                        neurons[j] = temp;
                    }
                }
            }

            ////////////////////////
            ///AKTUALIZACJA WAG

            for (int k = 0; k < numberOfNeurons; ++k)
            {
                neurons[k][0] += learningRate_curr * Math.Exp((-1) * k / range_curr) * (input[0] - neurons[k][0]);
                neurons[k][1] += learningRate_curr * Math.Exp((-1) * k / range_curr) * (input[1] - neurons[k][1]);
            }

        }

        public void Update(int iteration_fin, int iteration_curr)
        {
            range_curr = range_init * Math.Pow((range_fin / range_init), (iteration_curr / iteration_fin));
            learningRate_curr = learningRate_init * Math.Pow((learningRate_fin / learningRate_init), (iteration_curr / iteration_fin));
        }
    }
}
