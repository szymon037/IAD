﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBF_NeuralNetwork
{
    class NeuralNetwork
    {

        public Matrix.Matrix hiddenOutput;
        public Matrix.Matrix outputWeights;
        private Matrix.Matrix centre;
        private Matrix.Matrix range;
        private Matrix.Matrix inp;

        private Matrix.Matrix biasHidden;
        private Matrix.Matrix biasOutput;

        private int numberOfHiddenNeurons;

        private bool useBias;
        private double learningRate;
        private double momentumRate;

        public NeuralNetwork (int numberOfInputs, int numberOfHidden, int numberOfOutput, Matrix.Matrix inputs)
        {
            learningRate = 0.1;
            momentumRate = 0.5;

            numberOfHiddenNeurons = numberOfHidden;

            inp = inputs;

            hiddenOutput = new Matrix.Matrix(numberOfHidden, numberOfInputs);    
            outputWeights = new Matrix.Matrix(numberOfOutput, numberOfHidden);

            centre = new Matrix.Matrix(numberOfHidden, numberOfInputs);
            range = new Matrix.Matrix(numberOfHidden, 1);

            //hiddenOutput.RandomizeMatrix(-1, 1);
            outputWeights.RandomizeMatrix(-5, 5);

            Random rnd = new Random();

            for (int i = 0; i < numberOfHidden; ++i)
            {
                //wypełnianie macierzy c - centrów
                int index = rnd.Next(0, inputs.row);
                for (int j = 0; j < numberOfInputs; ++j)
                {
                    centre.tab[i, j] = inputs.tab[index, j];
                    //Console.WriteLine(centre.tab[i, j]);
                }
                //wypełnianie macierzy r - zasięgu
                range.tab[i, 0] = rnd.NextDouble() * 1;
            }


        }

        public Matrix.Matrix FeedForward (double[] input_x)
        {
            //Matrix.Matrix x = new Matrix.Matrix(input_x);
            FillHiddenNeurons(input_x);   //fills hiddenNeurons matrix
            //hiddenOutput.DisplayMatrix();

            Matrix.Matrix outputsOutput = outputWeights * hiddenOutput;
            return outputsOutput;
        }

        public void Train (double[] inputArray, double[] targetArray)
        {
            /*Random rnd = new Random();
            for (int i = 0; i < 10; ++i)
            {
                //wypełnianie macierzy c - centrów
                int index = rnd.Next(0, inp.row);
                for (int j = 0; j < 1; ++j)
                {
                    centre.tab[i, j] = inp.tab[index, j];
                }
                //wypełnianie macierzy r - zasięgu
                range.tab[i, 0] = rnd.NextDouble() * 50;
            }*/


            //Matrix.Matrix inputMatrix = new Matrix.Matrix(inputArray);
            FillHiddenNeurons(inputArray);
            //hiddenOutput.DisplayMatrix();

            Matrix.Matrix outputsOutput = outputWeights * hiddenOutput;

            Matrix.Matrix targetMatrix = new Matrix.Matrix(targetArray);
            Matrix.Matrix outputErrorsMatrix = targetMatrix - outputsOutput;

            mapMatrixLinearry(outputsOutput);   //!!! do zmiany w wariancie 2 na normalne mapowanie

            Matrix.Matrix gradients_output = outputErrorsMatrix * learningRate;
            
            gradients_output.HadamardProduct(outputsOutput);
            //gradients_output.DisplayMatrix();
            Matrix.Matrix hiddenTransposed = hiddenOutput.TransposeMatrix();
            //hiddenTransposed.DisplayMatrix();
            Matrix.Matrix outputs_deltas = gradients_output * hiddenTransposed;
            //outputs_deltas.DisplayMatrix();

            outputWeights += outputs_deltas;
            //Console.WriteLine("outputWeights: ");
            //outputWeights.DisplayMatrix();
        }

        private void FillHiddenNeurons (double[] x)
        {
            for (int i = 0; i < numberOfHiddenNeurons; ++i)
            {
                hiddenOutput.tab[i, 0] = GaussianBasisFunction(x[0], centre.tab[i, 0], range.tab[i, 0]);
                //Console.WriteLine(centre.tab[i, 0]);
            }            
        }

        private double GaussianBasisFunction (double x, double c, double r)         //??? wzór do sprawdzenia
        {
            return Math.Exp((-1) * ((x - c) * (x - c)) / r * r);            
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
    }
}
