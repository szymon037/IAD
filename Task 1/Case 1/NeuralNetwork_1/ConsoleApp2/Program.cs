using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork_1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Matrix.Matrix a = new Matrix.Matrix(5, 5);
            Matrix.Matrix b = new Matrix.Matrix(5, 5);

            a.RandomizeMatrix(-5, 5);
            a.DisplayMatrix();
            b = a.TransposeMatrix();
            Console.Write('\n');
            b.DisplayMatrix();*/
            /*NeuralNetwork nn = new NeuralNetwork(3, 2, 1, true);
            int[] a = new int[3];
            for (int i = 0; i < 3; ++i)
                a[i] = i;

            Matrix.Matrix output = new Matrix.Matrix(1, 1);

            output = nn.feedForward(a);
            output.DisplayMatrix();*/
            /*Matrix.Matrix a = new Matrix.Matrix(3, 3);
            a.RandomizeMatrix(-3, 3);
            a.DisplayMatrix();
            Console.Write('\n');
            nn.activationFunction(a);
            a.DisplayMatrix();*/


            /*Matrix.Matrix a = new Matrix.Matrix(2, 1);
            Matrix.Matrix b = new Matrix.Matrix(1, 2);
            a.RandomizeMatrix(0, 5);
            b.RandomizeMatrix(0, 5);

            a.DisplayMatrix();
            b.DisplayMatrix();


            Matrix.Matrix c = a * b;
            c.DisplayMatrix();*/


            /*int[] a = new int[3];
            for (int i = 0; i < 3; ++i)
                a[i] = i;
            Console.Write(a.Length);
            Matrix.Matrix m = new Matrix.Matrix(a, true);
            m.DisplayMatrix();*/


            // XOR

            /*NeuralNetwork nn = new NeuralNetwork(2, 4, 1, true);

            int[] input = new int[2];
            int[] target = new int[1];
            Random rnd = new Random();

            for (int i = 0; i < 10000; ++i)
            {
                input[0] = rnd.Next(0, 2);
                input[1] = rnd.Next(0, 2);

                target[0] = input[0] ^ input[1];

                //Console.WriteLine("\ninput0: " + input[0] + " input1: " + input[1] + " target: " + target[0]);
                nn.train(input, target);
            }

            input[0] = 0;
            input[1] = 0;
            target[0] = 0;

            Matrix.Matrix output = nn.feedForward(input);
            output.DisplayMatrix();
            Console.Write('\n');

            input[0] = 1;
            input[1] = 1;
            target[0] = 0;

            output = nn.feedForward(input);
            output.DisplayMatrix();
            Console.Write('\n');

            input[0] = 0;
            input[1] = 1;
            target[0] = 1;

            output = nn.feedForward(input);
            output.DisplayMatrix();
            Console.Write('\n');

            input[0] = 1;
            input[1] = 0;
            target[0] = 1;

            output = nn.feedForward(input);
            output.DisplayMatrix();
            Console.Write('\n');
            */


            //ZADANIE 1

            double[][] data = new double[4][];
            data[0] = new double[] { 1, 0, 0, 0 };
            data[1] = new double[] { 0, 1, 0, 0 };
            data[2] = new double[] { 0, 0, 1, 0 };
            data[3] = new double[] { 0, 0, 0, 1 };

            //NeuralNetwork nn = new NeuralNetwork(4, 3, 4, true);
            int n = 0;
            double sum = 0;


            Random rnd = new Random();
            Matrix.Matrix output = new Matrix.Matrix(4, 1);
            int epochs = 0;
            double averageSquareOfEpochs = 0;

            for (int i = 0; i < 20; ++i)
            {
                NeuralNetwork nn = new NeuralNetwork(4, 2, 4, true);
                do
                {
                    sum = 0;
                    //trenowanie
                    foreach (int j in Enumerable.Range(0, 4).OrderBy(x => rnd.Next()))
                    {
                        nn.train(data[j], data[j]);
                    }

                    for (int j = 0; j < 4; ++j)
                    {
                        output = nn.feedForward(data[j]);
                        for (int z = 0; z < 4; ++z)
                        {
                            sum += (output.tab[z, 0] - data[j][z]) * (output.tab[z, 0] - data[j][z]);
                        }
                    }
                    ++epochs;
                   // Console.WriteLine("Mean squared error: " + sum / 2);

                } while (sum / 2> 0.1);

                Console.WriteLine("epochs: " + epochs);
                averageSquareOfEpochs += epochs;
                epochs = 0;
            }
            averageSquareOfEpochs /= 20;

            Console.WriteLine("averageSquareOfEpochs: " + averageSquareOfEpochs);

           /* for (int i = 0; i < 30000; ++i)
            {
                foreach (int j in Enumerable.Range(0, 4).OrderBy(x => rnd.Next()))
                {
                    nn.train(data[j], data[j]);
                    //Console.WriteLine(j);
                }

                sum = 0;


                for (int j = 0; j < 4; ++j)
                {
                    output = nn.feedForward(data[j]);
                    for (int z = 0; z < 4; ++z)
                    {
                        //Console.WriteLine("(output.tab[z, 0]: " + (output.tab[z, 0] + " data[j][z]: " + data[j][z]));

                        sum += (output.tab[z, 0] - data[j][z]) * (output.tab[z, 0] - data[j][z]);
                    }
                    //Console.WriteLine("Mean squared error: " + sum / 2);
                    //sum = 0;                    
                }

                Console.WriteLine("Mean squared error: " + sum / 2);

                
            }*/


           


            /*for (int i = 0; i < 4; ++i)
            {
                output = nn.feedForward(data[i]);
                output.DisplayMatrix();
                Console.Write('\n');
            }*/
            

            
        }
}
}
