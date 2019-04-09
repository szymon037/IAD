using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class NeuralNetwork
    {
        public Matrix.Matrix hiddenWeights;
        public Matrix.Matrix outputWeights;
        private Matrix.Matrix biasHidden;
        private Matrix.Matrix biasOutput;
        private Matrix.Matrix momentumMatrixHidden;
        private Matrix.Matrix momentumMatrixOutput;
        private Matrix.Matrix momentumMatrixHiddenBias;
        private Matrix.Matrix momentumMatrixOutputBias;

        //confusionMatrix - macierz pomyłek        

        private bool useBias;
        private double learningRate;
        private double momentumRate;

        public NeuralNetwork(int inputAmount, int hiddenAmount, int outputAmount, bool bias)
        {
            learningRate = 0.04;
            momentumRate = 0.08;
            useBias = bias;

            hiddenWeights = new Matrix.Matrix(hiddenAmount, inputAmount);    // 2 x 3
            outputWeights = new Matrix.Matrix(outputAmount, hiddenAmount);   // 1 x 2

            momentumMatrixHidden = new Matrix.Matrix(hiddenAmount, inputAmount);
            momentumMatrixOutput = new Matrix.Matrix(outputAmount, hiddenAmount);

            hiddenWeights.RandomizeMatrix(-1, 1);
            outputWeights.RandomizeMatrix(-1, 1);

            biasHidden = new Matrix.Matrix(hiddenAmount, 1);
            biasOutput = new Matrix.Matrix(outputAmount, 1);
            momentumMatrixHiddenBias = new Matrix.Matrix(hiddenAmount, 1);
            momentumMatrixOutputBias = new Matrix.Matrix(outputAmount, 1);

            biasHidden.RandomizeMatrix(-1, 1);
            biasOutput.RandomizeMatrix(-1, 1);

        }

        public Matrix.Matrix FeedForward(double[] inputArray)
        {
            Matrix.Matrix inputMatrix = new Matrix.Matrix(inputArray);    // 3 x 1

            Matrix.Matrix hiddenOutput = hiddenWeights * inputMatrix;
            if (useBias)
            {
                hiddenOutput += biasHidden;
            }
            ActivationFunction(hiddenOutput);

            Matrix.Matrix outputsOutput = outputWeights * hiddenOutput;
            if (useBias)
            {
                outputsOutput += biasOutput;
            }
            

            OutputActivationFunction(outputsOutput);

            return outputsOutput;
        }

        public void Train(double[] inputArray, double[] targetArray)
        {
            Matrix.Matrix inputMatrix = new Matrix.Matrix(inputArray);    // 3 x 1
            Matrix.Matrix hiddenOutput = hiddenWeights * inputMatrix;
            if (useBias)
            {
                hiddenOutput += biasHidden;
            }
            ActivationFunction(hiddenOutput);

            //showHiddenOutputs(hiddenOutput);

            Matrix.Matrix outputsOutput = outputWeights * hiddenOutput;
            if (useBias)
            {
                outputsOutput += biasOutput;
            }
            OutputActivationFunction(outputsOutput);
            //ActivationFunction(outputsOutput);


            Matrix.Matrix targetMatrix = new Matrix.Matrix(targetArray);
            Matrix.Matrix outputErrorsMatrix = targetMatrix - outputsOutput;

             mapMatrixLinearry(outputsOutput);

            //mapMatrix(outputsOutput);


            Matrix.Matrix gradients_output = outputErrorsMatrix * learningRate;
            gradients_output.HadamardProduct(outputsOutput);
            Matrix.Matrix hiddenTransposed = hiddenOutput.TransposeMatrix();
            Matrix.Matrix outputs_deltas = gradients_output * hiddenTransposed;

            //momentumMatrixOutput += outputs_deltas;
            //momentumMatrixOutput *= momentumRate;

            outputWeights += outputs_deltas;
            //outputWeights += momentumMatrixOutput;
            biasOutput += gradients_output;
            //biasOutput += outputs_deltas;

            //hidden layer errors
            Matrix.Matrix outputWeights_transposed = outputWeights.TransposeMatrix();
            Matrix.Matrix hiddenErrorsMatrix = outputWeights_transposed * outputErrorsMatrix;

            mapMatrix(hiddenOutput);

            Matrix.Matrix gradients_hidden = hiddenErrorsMatrix * learningRate;
            gradients_hidden.HadamardProduct(hiddenOutput);

            Matrix.Matrix hidden_deltas = gradients_hidden * inputMatrix.TransposeMatrix();
            hiddenWeights += hidden_deltas;
            biasHidden += gradients_hidden;
            //biasHidden += hidden_deltas;

            //MOMENTUM

            outputWeights += momentumMatrixOutput;
            hiddenWeights += momentumMatrixHidden;
            biasOutput += momentumMatrixOutputBias;
            biasHidden += momentumMatrixHiddenBias;

            momentumMatrixOutput = outputs_deltas * momentumRate;
            momentumMatrixHidden = hidden_deltas * momentumRate;
            momentumMatrixOutputBias = gradients_output * momentumRate;
            momentumMatrixHiddenBias = gradients_hidden * momentumRate;

        }

        public void ShowHiddenOutputs(Matrix.Matrix a)
        {
            Console.WriteLine("Hidden outputs: ");
            a.DisplayMatrix();
            Console.Write('\n');
        }


        public void ActivationFunction(Matrix.Matrix m)
        {
            for (int i = 0; i < m.row; ++i)
            {
                for (int j = 0; j < m.column; ++j)
                {
                    m.tab[i, j] = sigmoid(m.tab[i, j]);
                }
            }
        }

        public void mapMatrix(Matrix.Matrix m)
        {
            for (int i = 0; i < m.row; ++i)
            {
                for (int j = 0; j < m.column; ++j)
                {
                    m.tab[i, j] = dSigmoid(m.tab[i, j]);
                }
            }
        }

        public void OutputActivationFunction(Matrix.Matrix m)
        {
            for (int i = 0; i < m.row; ++i)
            {
                for (int j = 0; j < m.column; ++j)
                {
                    m.tab[i, j] = Fun(m.tab[i, j]);
                }
            }
        }

        public void mapMatrixLinearry(Matrix.Matrix m)
        {
            for (int i = 0; i < m.row; ++i)
            {
                for (int j = 0; j < m.column; ++j)
                {
                    m.tab[i, j] = 1;
                }
            }
        }

        private double Fun(double x)
        {
            return x;
        }

        private double DFun(double x)
        {
            return 1;
        }

        private double sigmoid(double x)
        {
            return (1 / (1 + Math.Exp(-x)));
        }

        private double dSigmoid(double x)
        {
            //double sigX = sigmoid(x);
            //return sigX * (1 - sigX);
            return x * (1 - x);
        }
    }



}
