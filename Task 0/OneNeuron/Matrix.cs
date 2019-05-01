using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    class Matrix
    {
        private int row;
        private int column;
        public double[,] tab;

        public Matrix( int x, int y)
        {
            this.row = x;
            this.column = y;
            tab = new double[x, y];
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

        public void TransposeMatrix()
        {
            double[,] temp = new double[row, column];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    temp[j, i] = tab[i, j];
                }
            }
            tab = temp;
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
    }
}
