using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kmeans;

namespace RBF_classification
{
    class NeuralNetwork
    {

        public Matrix.Matrix hiddenOutput;
        public Matrix.Matrix outputWeights;
        private List<Kmeans.Centroid> centre;
        private Matrix.Matrix range;
        private Matrix.Matrix inp;

        private Matrix.Matrix biasHidden;
        private Matrix.Matrix biasOutput;

        private int numberOfHiddenNeurons;

        private bool useBias;
        private double learningRate;
        private double momentumRate;

        public NeuralNetwork(int numberOfInputs, int numberOfHidden, int numberOfOutput, Matrix.Matrix inputs)
        {
            learningRate = 0.01;
            momentumRate = 0.05;

            numberOfHiddenNeurons = numberOfHidden;

            inp = inputs;

            hiddenOutput = new Matrix.Matrix(numberOfHidden, 1);
            outputWeights = new Matrix.Matrix(numberOfOutput, numberOfHidden);

            range = new Matrix.Matrix(numberOfHidden, 1);

            //hiddenOutput.RandomizeMatrix(-1, 1);
            outputWeights.RandomizeMatrix(-1, 1);

            Random rnd = new Random();

            List<Kmeans.Point> Data = new List<Kmeans.Point>();
            for ( int i = 0; i < inputs.row; i++)
            {
                Data.Add(new Kmeans.Point(inputs.tab[i, 0], inputs.tab[i, 1], inputs.tab[i, 2], inputs.tab[i, 3]));
            }

            var km = new Kmeans.KMeans(numberOfHidden, Data, rnd);

            km.Train();
            Console.WriteLine("centroids done");

            centre = km.Centroids;

            for (int i = 0; i < numberOfHidden; ++i)
            {
                //wypełnianie macierzy r - zasięgu
                range.tab[i, 0] = rnd.NextDouble() * 4;
            }


        }

        public Matrix.Matrix FeedForward(double[] input_x)
        {
            //Matrix.Matrix x = new Matrix.Matrix(input_x);
            FillHiddenNeurons(input_x);   //fills hiddenNeurons matrix
            //hiddenOutput.DisplayMatrix();

            Matrix.Matrix outputsOutput = outputWeights * hiddenOutput;
            return outputsOutput;
        }

        public void Train(double[] inputArray, double[] targetArray)
        {
           


            //Matrix.Matrix inputMatrix = new Matrix.Matrix(inputArray);
            FillHiddenNeurons(inputArray);
            //hiddenOutput.DisplayMatrix();

            Matrix.Matrix outputsOutput = outputWeights * hiddenOutput;

            Matrix.Matrix targetMatrix = new Matrix.Matrix(targetArray);

            Matrix.Matrix outputErrorsMatrix = targetMatrix - outputsOutput;

           // mapMatrixLinearry(outputsOutput);   //!!! do zmiany w wariancie 2 na normalne mapowanie

            Matrix.Matrix gradients_output = outputErrorsMatrix * learningRate;

            //gradients_output.HadamardProduct(outputsOutput);
            //gradients_output.DisplayMatrix();
            Matrix.Matrix hiddenTransposed = hiddenOutput.TransposeMatrix();
            //hiddenTransposed.DisplayMatrix();
            Matrix.Matrix outputs_deltas = gradients_output * hiddenTransposed;
            //outputs_deltas.DisplayMatrix();

            outputWeights += outputs_deltas;
            //Console.WriteLine("outputWeights: ");
            //outputWeights.DisplayMatrix();
        }

        private void FillHiddenNeurons(double[] x)
        {
            for (int i = 0; i < numberOfHiddenNeurons; ++i)
            {
                hiddenOutput.tab[i, 0] = GaussianBasisFunction(new Point(x[0],x[1],x[2],x[3]), centre[i], range.tab[i, 0]);
                //Console.WriteLine(centre.tab[i, 0]);
            }
        }

        private double GaussianBasisFunction(Kmeans.Point x, Kmeans.Centroid c, double r)         //??? wzór do sprawdzenia
        {
            return Math.Exp((-1) * (EDistance(x, c)) / r * r);
        }

        private void mapMatrixLinearry(Matrix.Matrix m)
        {
            for (int i = 0; i < m.row; ++i)
            {
                for (int j = 0; j < m.column; ++j)
                {
                    m.tab[i, j] = 1;
                }
            }
        }

        public static double EDistance(Kmeans.Point a, Kmeans.Centroid c)
        {
            return Math.Pow(a.X - c.X, 2) + Math.Pow(a.Y - c.Y, 2) + Math.Pow(a.Z - c.Z, 2) + Math.Pow(a.Q - c.Q, 2);
        }
    }
}
