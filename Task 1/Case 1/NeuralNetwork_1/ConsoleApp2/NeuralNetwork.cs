using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork_1
{
    class NeuralNetwork
    {
        public Matrix.Matrix hiddenWeights;
        public Matrix.Matrix outputWeights;
        private Matrix.Matrix biasHidden;
        private Matrix.Matrix biasOutput;
        private bool useBias;
        private double learningRate;

        public NeuralNetwork(int inputAmount, int hiddenAmount, int outputAmount, bool bias)
        {
            learningRate = 0.1;

            useBias = bias;          

            hiddenWeights = new Matrix.Matrix(hiddenAmount, inputAmount);    // 2 x 3
            outputWeights = new Matrix.Matrix(outputAmount, hiddenAmount);   // 1 x 2
            hiddenWeights.RandomizeMatrix(-5, 5);
            outputWeights.RandomizeMatrix(-5, 5);


            biasHidden = new Matrix.Matrix(hiddenAmount, 1);
            biasOutput = new Matrix.Matrix(outputAmount, 1);
            biasHidden.RandomizeMatrix(-5, 5);
            biasOutput.RandomizeMatrix(-5, 5);

            
        }

        public Matrix.Matrix feedForward(int[] inputArray)
        {
            Matrix.Matrix inputMatrix = new Matrix.Matrix(inputArray);    // 3 x 1
            Matrix.Matrix hiddenOutput = hiddenWeights * inputMatrix;
            if (useBias)
            {
                hiddenOutput += biasHidden;
            }
            activationFunction(hiddenOutput);

            Matrix.Matrix outputsOutput = outputWeights * hiddenOutput;
            if (useBias)
            {
                outputsOutput += biasOutput;
            }
            activationFunction(outputsOutput);
            return outputsOutput;
        }

        public void train(int[] inputArray, int[] targetArray)
        {
            Matrix.Matrix inputMatrix = new Matrix.Matrix(inputArray);    // 3 x 1
            Matrix.Matrix hiddenOutput = hiddenWeights * inputMatrix;
            if (useBias)
            {
                hiddenOutput += biasHidden;
            }
            activationFunction(hiddenOutput);

            Matrix.Matrix outputsOutput = outputWeights * hiddenOutput;
            if (useBias)
            {
                outputsOutput += biasOutput;
            }
            activationFunction(outputsOutput);

            Matrix.Matrix targetMatrix = new Matrix.Matrix(targetArray);
            Matrix.Matrix outputErrorsMatrix = targetMatrix - outputsOutput;

            mapMatrix(outputsOutput);

            Matrix.Matrix gradients_output = outputErrorsMatrix * learningRate;
            gradients_output.HadamardProduct(outputsOutput);
            Matrix.Matrix hiddenTransposed = hiddenOutput.TransposeMatrix();
            Matrix.Matrix outputs_deltas = gradients_output * hiddenTransposed;

            outputWeights += outputs_deltas;
            biasOutput += gradients_output;

            //hidden layer errors
            Matrix.Matrix outputWeights_transposed = outputWeights.TransposeMatrix();
            Matrix.Matrix hiddenErrorsMatrix = outputWeights_transposed * outputErrorsMatrix;

            mapMatrix(hiddenOutput);

            Matrix.Matrix gradients_hidden = hiddenErrorsMatrix * learningRate;
            gradients_hidden.HadamardProduct(hiddenOutput);

            Matrix.Matrix hidden_deltas = gradients_hidden * inputMatrix.TransposeMatrix();
            hiddenWeights += hidden_deltas;
            biasHidden += gradients_hidden;

        }

        public void activationFunction(Matrix.Matrix m)
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
