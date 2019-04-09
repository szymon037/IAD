using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks
{
    class NeuralNetwork
    {
        public Matrix.Matrix hiddenWeights;
        public Matrix.Matrix outputWeights;
        private Matrix.Matrix biasHidden;
        private Matrix.Matrix biasOutput;
        private readonly bool useBias;
        private readonly double learningRate;

        public NeuralNetwork(int inputAmount, int hiddenAmount, int outputAmount, bool bias)
        {
            learningRate = 0.1;

            useBias = bias;          

            hiddenWeights = new Matrix.Matrix(hiddenAmount, inputAmount);
            outputWeights = new Matrix.Matrix(outputAmount, hiddenAmount);
            hiddenWeights.RandomizeMatrix(-5, 5);
            outputWeights.RandomizeMatrix(-5, 5);


            biasHidden = new Matrix.Matrix(hiddenAmount, 1);
            biasOutput = new Matrix.Matrix(outputAmount, 1);
            biasHidden.RandomizeMatrix(-5, 5);
            biasOutput.RandomizeMatrix(-5, 5);

            
        }

        public Matrix.Matrix FeedForward(double[] inputArray)
        {
            Matrix.Matrix inputMatrix = new Matrix.Matrix(inputArray);
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
            ActivationFunction(outputsOutput);
            return outputsOutput;
        }

        public void Train(double[] inputArray, double[] targetArray)
        {
            Matrix.Matrix inputMatrix = new Matrix.Matrix(inputArray);  
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
            ActivationFunction(outputsOutput);

            Matrix.Matrix targetMatrix = new Matrix.Matrix(targetArray);
            Matrix.Matrix outputErrorsMatrix = targetMatrix - outputsOutput;

            MapMatrix(outputsOutput);

            Matrix.Matrix gradients_output = outputErrorsMatrix * learningRate;
            gradients_output.HadamardProduct(outputsOutput);
            Matrix.Matrix hiddenTransposed = hiddenOutput.TransposeMatrix();
            Matrix.Matrix outputs_deltas = gradients_output * hiddenTransposed;

            outputWeights += outputs_deltas;
            biasOutput += gradients_output;

            //hidden layer errors
            Matrix.Matrix outputWeights_transposed = outputWeights.TransposeMatrix();
            Matrix.Matrix hiddenErrorsMatrix = outputWeights_transposed * outputErrorsMatrix;

            MapMatrix(hiddenOutput);

            Matrix.Matrix gradients_hidden = hiddenErrorsMatrix * learningRate;
            gradients_hidden.HadamardProduct(hiddenOutput);

            Matrix.Matrix hidden_deltas = gradients_hidden * inputMatrix.TransposeMatrix();
            hiddenWeights += hidden_deltas;
            biasHidden += gradients_hidden;

        }

        public void ActivationFunction(Matrix.Matrix m)
        {
            for (int i = 0; i < m.row; ++i)
            {
                for (int j = 0; j < m.column; ++j)
                {
                    m.tab[i, j] = Fun(m.tab[i, j]);
                }
            }
        }

        public void MapMatrix(Matrix.Matrix m)
        {
            for (int i = 0; i < m.row; ++i)
            {
                for (int j = 0; j < m.column; ++j)
                {
                    m.tab[i, j] = DFun(m.tab[i, j]);
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
    }



}
