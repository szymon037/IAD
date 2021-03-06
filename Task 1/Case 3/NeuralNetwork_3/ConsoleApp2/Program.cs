﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace NeuralNetwork_1
{
    class Program
    {    
        public static double[][] classification_train = new double[90][];
        public static double[][] classification_test = new double[93][];
        public static double[] inputArray;
        public static double[] targetArray;
        public static int numberOfInputs = 4;


        public Program() {
            for (int i = 0; i < 90; ++i)
            {
                classification_train[i] = new double[5];
            }
            for (int i = 0; i < 93; ++i)
            {
                classification_test[i] = new double[5];
            }

            inputArray = new double[numberOfInputs];
            targetArray = new double[3];
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.LoadFromFile();
            
            Random rnd = new Random();

            NeuralNetwork nn = new NeuralNetwork(4, 4, 3, true);

            /*for (int j = 0; j < 90; ++j)
            {
                Array.Clear(targetArray, 0, 3);
                for (int z = 0; z < numberOfInputs; ++z)
                {
                    inputArray[z] = classification_train[j][z];
                }
                targetArray[(int)classification_train[j][4] - 1] = 1;
                Console.WriteLine("inputArray: " + inputArray[0] + " " + inputArray[1] + " targetArray: " + targetArray[0] + " " + targetArray[1] + " " + targetArray[2]);
            }*/

            double[] second = new double[] { 0, 1, 0 };

            for (int i = 0; i < 1000; ++i)
            {
                foreach (int j in Enumerable.Range(0, 90).OrderBy(x => rnd.Next()))
                {
                    Array.Clear(targetArray, 0, 3);

                    //wypełnianie całej inputArray na początek
                    for (int z = 0; z < numberOfInputs; ++z)
                    {
                        inputArray[z] = classification_train[j][z];
                    }
                    targetArray[(int)classification_train[j][4] - 1] = 1;
                    //Console.WriteLine("inputArray: " + inputArray[0] + " " + inputArray[1]);
                    /*if (classification_train[j][4] == 2)
                    {
                        Console.WriteLine("DWAAAAAAAAAAAAA");
                        targetArray = second;
                    }*/
                    nn.train(inputArray, targetArray);
                }
            }

            Matrix.Matrix output = new Matrix.Matrix(3, 1);
            Matrix.Matrix confusionMatrix = new Matrix.Matrix(3, 3);
            double numberOfCorrect = 0;
            double percentOfCorectness = 0;
            int predictedOutput = 0;

            for (int i = 0; i < 93; ++i)
            {
                for (int z = 0; z < numberOfInputs; ++z)
                {
                    inputArray[z] = classification_test[i][z];
                }

                output = nn.feedForward(inputArray);
                output = output.TransposeMatrix();

                //POTRZEBNE DO LICZNIEA BŁĘDU ŚREDNIOKWADRATOWEGO
                /*Array.Clear(targetArray, 0, 3);
                targetArray[(int)classification_test[i][4] - 1] = 1;
                Console.WriteLine(" targetArray: " + targetArray[0] + " " + targetArray[1] + " " + targetArray[2]);*/


                //LICZENIE PROCENTA POPRAWNOŚCI
                if (output.tab[0,0] > output.tab[0, 1] && output.tab[0, 0] > output.tab[0, 2] && classification_test[i][4] == 1)
                {
                    ++numberOfCorrect;

                }
                else if (output.tab[0, 1] > output.tab[0, 0] && output.tab[0, 1] > output.tab[0, 2] && classification_test[i][4] == 2)
                {
                    ++numberOfCorrect;
                }
                else if (output.tab[0, 2] > output.tab[0, 0] && output.tab[0, 2] > output.tab[0, 1] && classification_test[i][4] == 3)
                {
                    ++numberOfCorrect;
                }

                predictedOutput = 0;
                if (output.tab[0, 0] > output.tab[0, 1] && output.tab[0, 0] > output.tab[0, 2])
                {
                    predictedOutput = 1;
                }
                else if (output.tab[0, 1] > output.tab[0, 0] && output.tab[0, 1] > output.tab[0, 2])
                {
                    predictedOutput = 2;
                }
                else if (output.tab[0, 2] > output.tab[0, 0] && output.tab[0, 2] > output.tab[0, 1])
                {
                    predictedOutput = 3;
                }

                //WYPEŁNIANIE MACIERZY POMYŁEK (confusionMatrix)
                if (predictedOutput > 0)
                {
                    confusionMatrix.tab[(int) classification_test[i][4] - 1, predictedOutput - 1]++;
                }

                    output.DisplayMatrix();
            }

            percentOfCorectness = numberOfCorrect/93;
            percentOfCorectness *= 100;
            //Console.WriteLine("\nNumber of correct: " + numberOfCorrect);
            Console.WriteLine("\nPercentage of corectness: " + percentOfCorectness);
            Console.WriteLine("\nConfusion matrix: ");
            confusionMatrix.DisplayMatrix();






        }


        public void LoadFromFile()
        {
            var fileStream_train = new FileStream(@"..\..\classification_files\classification_train.txt", FileMode.Open, FileAccess.Read);
            var fileStream_test = new FileStream(@"..\..\classification_files\classification_test.txt", FileMode.Open, FileAccess.Read);

            //var fileStream = new FileStream(@"D:\input2.txt", FileMode.Open, FileAccess.Read);

            int i = 0;
            string line;

            using (var streamReader = new StreamReader(fileStream_train, Encoding.UTF8))
            {                
                while ((line = streamReader.ReadLine()) != null)
                {
                    classification_train[i][0] = Convert.ToDouble(line.Split(' ')[0]);
                    classification_train[i][1] = Convert.ToDouble(line.Split(' ')[1]);
                    classification_train[i][2] = Convert.ToDouble(line.Split(' ')[2]);
                    classification_train[i][3] = Convert.ToDouble(line.Split(' ')[3]);
                    classification_train[i][4] = Convert.ToDouble(line.Split(' ')[4]);
                    i++;
                }
            }

            i = 0;

            using (var streamReader = new StreamReader(fileStream_test, Encoding.UTF8))
            {
                while ((line = streamReader.ReadLine()) != null)
                {
                    classification_test[i][0] = Convert.ToDouble(line.Split(' ')[0]);
                    classification_test[i][1] = Convert.ToDouble(line.Split(' ')[1]);
                    classification_test[i][2] = Convert.ToDouble(line.Split(' ')[2]);
                    classification_test[i][3] = Convert.ToDouble(line.Split(' ')[3]);
                    classification_test[i][4] = Convert.ToDouble(line.Split(' ')[4]);
                    i++;
                }
            }
        }

    }
}
