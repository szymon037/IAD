using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    class Matrix
    {
        public int row;
        public int column;
        public double[,] tab;

        public Matrix(int x, int y)
        {
            this.row = x;
            this.column = y;
            tab = new double[x, y];
        }

        public Matrix(double[] array)
        {
            this.row = array.Length;
            this.column = 1;
           
            
            tab = new double[this.row, this.column];

            
            for (int i = 0; i < array.Length; ++i)
            {
                tab[i, 0] = array[i];
            }
        }

        public void DisplayMatrix()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    System.Console.Write(tab[i, j].ToString());
                    System.Console.Write(" ");
                }
                System.Console.WriteLine();
            }
        }

        public Matrix TransposeMatrix()
        {
            Matrix a = new Matrix(this.column, this.row);
            //double[,] temp = new double[row, column];
            for (int i = 0; i < a.row; i++)
            {
                for (int j = 0; j < a.column; j++)
                {
                    //temp[j, i] = tab[i, j];
                    a.tab[i, j] = this.tab[j, i];
                }
            }
            //tab = temp;
            return a;
        }

        public void RandomizeMatrix(int min, int max)
        {
            Random rnd = new Random();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    tab[i, j] = rnd.Next(min, max);
                }
            }
        }

        public void ArrayToMatrix(int[] array, bool bias)
        {
            for (int i = 0; i < array.Length; ++i)
            {
                tab[i, 0] = array[i];
            }
            if (bias && array.Length + 1 == this.row)
            {
                tab[this.row - 1, 0] = 1;
            }
        }

        public void HadamardProduct(Matrix a)
        {
            if (this.row == a.row && this.column == a.column)
            {
                for (int i = 0; i < this.row; ++i)
                {
                    for (int j = 0; j < this.column; ++j)
                    {
                        this.tab[i, j] *= a.tab[i, j];
                    }
                }
            }
        }
        
        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.row != b.row && a.column != b.column)
            {

            }

            Matrix result = new Matrix(a.row, a.column);
            for (int i = 0; i < result.row; i++)
            {
                for (int j = 0; j < result.column; j++)
                {
                    result.tab[i, j] = a.tab[i, j] + b.tab[i, j];
                }
            }
            return result;
        }


        public static Matrix operator -(Matrix a, Matrix b)
        {
            if (a.row != b.row && a.column != b.column)
            {

            }

            Matrix result = new Matrix(a.row, a.column);
            for (int i = 0; i < result.row; i++)
            {
                for (int j = 0; j < result.column; j++)
                {
                    result.tab[i, j] = a.tab[i, j] - b.tab[i, j];
                }
            }
            return result;
        }

        public static Matrix operator *(Matrix a , Matrix b)
        {
            Matrix result = new Matrix(a.row, b.column);
            for (int i = 0; i < a.row; i++)
            {
                for (int j = 0; j < b.column; j++)
                {
                    result.tab[i, j] = 0;
                    for(int k =0; k < a.column; k++)
                    {
                        result.tab[i, j] += a.tab[i, k] * b.tab[k, j];
                    }
                }
            }
            return result;
        }

        public static Matrix operator *(Matrix a, double b)
        {
            Matrix result = new Matrix(a.row, a.column);
            for (int i = 0; i < result.row; ++i)
            {
                for (int j = 0; j < result.column; ++j)
                {
                    result.tab[i, j] = a.tab[i, j] * b;
                }
            }
            return result;
        }
    }
}
