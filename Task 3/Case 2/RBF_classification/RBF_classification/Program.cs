using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RBF_classification
{
    class Program
    {
        public static double[][] classification_train = new double[90][];
        public static double[][] classification_test = new double[93][];
        public static double[] inputArray;
        public static double[] targetArray;
        public static int numberOfInputs = 4;
        public static int epochs = 1000;
        Matrix.Matrix inputs;


        public Program()
        {
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

            NeuralNetwork nn = new NeuralNetwork(4, 10, 1, p.inputs);

            for (int i = 0; i < epochs; ++i)
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
                    
                    nn.Train(inputArray, targetArray);
                }
            }

        }

        public void LoadFromFile()
        {
            var fileStream_train = new FileStream(@"..\..\classification_files\classification_train.txt", FileMode.Open, FileAccess.Read);
            var fileStream_test = new FileStream(@"..\..\classification_files\classification_test.txt", FileMode.Open, FileAccess.Read);

            //var fileStream = new FileStream(@"D:\input2.txt", FileMode.Open, FileAccess.Read);

            int i = 0;
            string line;

            inputs = new Matrix.Matrix(90, 4);

            using (var streamReader = new StreamReader(fileStream_train, Encoding.UTF8))
            {
                while ((line = streamReader.ReadLine()) != null)
                {
                    classification_train[i][0] = Convert.ToDouble(line.Split(' ')[0]);
                    classification_train[i][1] = Convert.ToDouble(line.Split(' ')[1]);
                    classification_train[i][2] = Convert.ToDouble(line.Split(' ')[2]);
                    classification_train[i][3] = Convert.ToDouble(line.Split(' ')[3]);
                    classification_train[i][4] = Convert.ToDouble(line.Split(' ')[4]);

                    inputs.tab[i, 0] = classification_train[i][0];
                    inputs.tab[i, 1] = classification_train[i][1];
                    inputs.tab[i, 2] = classification_train[i][2];
                    inputs.tab[i, 3] = classification_train[i][3];
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
