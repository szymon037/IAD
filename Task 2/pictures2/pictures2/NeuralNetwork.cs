using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pictures2
{
    class NeuralNetwork
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
        private int sizeOfFrame;
        private int mode;

        public NeuralNetwork (int width, int height, int size, int MODE, double RINIT, double RFIN, double LRINIT, double LRFIN)
        {
            mode = MODE;
            sizeOfFrame = size;
            gridWidth = width;
            gridHeight = height;
            /*
            range_init = 1;//0.8
            range_fin = 0.0001;//0.1;
            range_curr = range_init;

            learningRate_init = 0.15; //0.15
            learningRate_fin = 0.005;//0.05;   //0.001
            learningRate_curr = learningRate_init;
            */
            range_init = RINIT;//0.8
            range_fin = RFIN;//0.1;
            range_curr = range_init;

            learningRate_init = LRINIT; //0.15
            learningRate_fin = LRFIN;//0.05;   //0.001
            learningRate_curr = learningRate_init;
            Random rnd = new Random();

            neurons = new Neuron[gridWidth][];
            for (int i = 0; i < gridWidth; ++i)
            {
                neurons[i] = new Neuron[gridHeight];
                for (int j = 0; j < gridHeight; ++j)
                {
                    neurons[i][j] = new Neuron(sizeOfFrame, MODE, rnd);
                }
            }
        }

        public double Distance (FrameOfPixels a, FrameOfPixels b)
        {
            double distance = 0;
            for (int i = 0; i < a.pixels.Count(); ++i)
            {
                distance += (a.pixels[i].R - b.pixels[i].R) * (a.pixels[i].R - b.pixels[i].R);
                distance += (a.pixels[i].G - b.pixels[i].G) * (a.pixels[i].G - b.pixels[i].G);
                distance += (a.pixels[i].B - b.pixels[i].B) * (a.pixels[i].B - b.pixels[i].B);
            }
            return Math.Sqrt(distance);
        }


        public double Train (FrameOfPixels data, int iteration_fin, int iteration_curr)
        {
            range_curr = range_init * Math.Pow((range_fin / range_init), (iteration_curr / iteration_fin));
            learningRate_curr = learningRate_init * Math.Pow((learningRate_fin / learningRate_init), (iteration_curr / iteration_fin));

            Random rnd = new Random();

            double distance = Distance(neurons[0][0].weights, data);
            double tempDistance = 0;
            Neuron BMU = new Neuron(sizeOfFrame, 0, rnd);
            BMU = neurons[0][0];
            int x = 0, y = 0;
            for (int i = 0; i < gridWidth; ++i)               //znajdowanie najbliższego neuronu
            {
                for (int j = 0; j < gridHeight; ++j)
                {
                    tempDistance = Distance(neurons[i][j].weights, data);
                    if (tempDistance < distance)
                    {
                        distance = tempDistance;
                        BMU = neurons[i][j];
                        x = i;
                        y = j;
                    }
                }
            }

            double dist = 0;

            for (int i = 0; i < gridWidth; ++i)               //znajdowanie najbliższego neuronu
            {
                for (int j = 0; j < gridHeight; ++j)
                {
                    dist = Math.Abs(x - i) + Math.Abs(y - j);

                    for (int z = 0; z < neurons[i][j].weights.pixels.Count(); ++z)
                    {
                        neurons[i][j].weights.pixels[z].R += (int) (Math.Exp((-1) * (dist * dist) / (2 * range_curr * range_curr)) * learningRate_curr * (data.pixels[z].R - neurons[i][j].weights.pixels[z].R));
                        neurons[i][j].weights.pixels[z].G += (int) (Math.Exp((-1) * (dist * dist) / (2 * range_curr * range_curr)) * learningRate_curr * (data.pixels[z].G - neurons[i][j].weights.pixels[z].G));
                        neurons[i][j].weights.pixels[z].B += (int) (Math.Exp((-1) * (dist * dist) / (2 * range_curr * range_curr)) * learningRate_curr * (data.pixels[z].B - neurons[i][j].weights.pixels[z].B));

                        //im so sorry for that
                        if (neurons[i][j].weights.pixels[z].R > 255) 
                            neurons[i][j].weights.pixels[z].R = 255;
                        if (neurons[i][j].weights.pixels[z].G > 255)
                            neurons[i][j].weights.pixels[z].G = 255;
                        if (neurons[i][j].weights.pixels[z].B > 255)
                            neurons[i][j].weights.pixels[z].B = 255;

                        if (neurons[i][j].weights.pixels[z].R < 0)
                            neurons[i][j].weights.pixels[z].R = 0;
                        if (neurons[i][j].weights.pixels[z].G < 0)
                            neurons[i][j].weights.pixels[z].G = 0;
                        if (neurons[i][j].weights.pixels[z].B < 0)
                            neurons[i][j].weights.pixels[z].B = 0;

                        if (mode == 0)
                        {
                            int q = (neurons[i][j].weights.pixels[z].R + neurons[i][j].weights.pixels[z].G + neurons[i][j].weights.pixels[z].B) / 3;
                            neurons[i][j].weights.pixels[z].R = q;
                            neurons[i][j].weights.pixels[z].G = q;
                            neurons[i][j].weights.pixels[z].B = q;
                        }

                    }                   
                }
            }

            distance = Distance(BMU.weights, data);
            return distance * distance;
        }

        public FrameOfPixels Compare (FrameOfPixels data)
        {
            Random rnd = new Random();

            double distance = Distance(neurons[0][0].weights, data);
            double tempDistance = 0;
            Neuron BMU = new Neuron(sizeOfFrame, 0, rnd);
            BMU = neurons[0][0];
            for (int i = 0; i < gridWidth; ++i)               //znajdowanie najbliższego neuronu
            {
                for (int j = 0; j < gridHeight; ++j)
                {
                    tempDistance = Distance(neurons[i][j].weights, data);
                    if (tempDistance < distance)
                    {
                        distance = tempDistance;
                        BMU = neurons[i][j];                        
                    }
                }
            }

            return BMU.weights;
        }
    }
}
