using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_2
{
    class NeuralNetwork
    {
        /*public double x;
        public double y;*/
        public int numberOfNeurons;
        public double[][] neurons;
        public double range;
        private double learningRate;

        private void RandomiseNeurons(double min, double max)   //losowanie wektora wag neuronów
        {
            Random rnd = new Random();
            for (int i = 0; i < numberOfNeurons; ++i)
            {
                neurons[i][0] = rnd.NextDouble() * (max - min) + min;
                neurons[i][1] = rnd.NextDouble() * (max - min) + min;
            }
        }

        public NeuralNetwork(int neurons_quantity, double ran, double min, double max)
        {
            learningRate = 0.01;//0.001;
            range = ran;
            numberOfNeurons = neurons_quantity;

            neurons = new double[numberOfNeurons][];
            for (int i = 0; i < numberOfNeurons; ++i)
                neurons[i] = new double[2];

            /*for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    neurons[i * 10 + j][0] = 30 + i * 10;
                    neurons[i * 10 + j][1] = 30 + j * 10;
                }
            }*/
/*
            neurons[0][0] = 40;
            neurons[0][1] = 40;

            neurons[1][0] = 60;
            neurons[1][1] = 40;

            neurons[2][0] = 40;
            neurons[2][1] = 60;
*/
            RandomiseNeurons(min, max);      
        }


        private double Distance(double[] point1, double[] point2)
        {
            return Math.Sqrt((point1[0] - point2[0]) * (point1[0] - point2[0]) + (point1[1] - point2[1]) * (point1[1] - point2[1]));
        }


        public void Train(double[] input)
        {
            //////////////////// 
            ///ZNAJDOWANIE NAJBLIŻSZEGO NEURONU (BMU)

            double distance = Distance(neurons[0], input);
            double tempDistance = 0;
            int bmu = 0;

            for (int i = 1; i < numberOfNeurons; ++i)               //znajdowanie najbliższego neuronu
            {
                tempDistance = Distance(neurons[i], input);
                if (tempDistance < distance)
                {
                    distance = tempDistance;
                    bmu = i;
                }
            }

            //Console.WriteLine(bmu);
            //////////////////// 
            ///ZNAJDOWANIE SĄSIADÓW BMU, ZMIANA WAG SĄSIADÓW I SAMEGO BMU   

            //int neighbours = 0;
            for (int i = 0; i < numberOfNeurons; ++i)
            {
                if (i != bmu && Distance(neurons[bmu], neurons[i]) <= range)
                {
                    //++neighbours;
                    neurons[i][0] += Math.Exp((-1) * (Distance(neurons[i], input)) * (Distance(neurons[i], input)) / (2 * range * range)) * learningRate * (input[0] - neurons[i][0]);
                    neurons[i][1] += Math.Exp((-1) * (Distance(neurons[i], input)) * (Distance(neurons[i], input)) / (2 * range * range)) * learningRate * (input[1] - neurons[i][1]);
                }
            }
            //Console.WriteLine("neighbours: " + neighbours);

            neurons[bmu][0] += Math.Exp((-1) * (Distance(neurons[bmu], input)) * (Distance(neurons[bmu], input)) / (2 * range * range)) * learningRate * (input[0] - neurons[bmu][0]);
            neurons[bmu][1] += Math.Exp((-1) * (Distance(neurons[bmu], input)) * (Distance(neurons[bmu], input)) / (2 * range * range)) * learningRate * (input[1] - neurons[bmu][1]);
            //Console.WriteLine((-1) * (Distance(neurons[bmu], input)) * (Distance(neurons[bmu], input)) / (2 * range * range));
            /*range -= 0.01;
            Console.WriteLine("range: " + range);*/
        }






    }
}
