using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnglishDictionary
{
    public class Utils
    {
        public static int GetLSD(String str1, String str2)
        {
            int column = str1.Length;
            int row = str2.Length;

            int[,] matrix = new int[row + 1, column + 1];
            matrix[0, 0] = 0;

            for (int i = 1; i < column + 1; i++)
                matrix[0, i] = i;

            for (int j = 1; j < row + 1; j++)
                matrix[j, 0] = j;

            for (int i = 1; i < row + 1; i++)
            {
                for (int j = 1; j < column + 1; j++)
                {
                    int cost = str1[j - 1] == str2[i - 1] ? 0 : 1;

                    int delection = matrix[i - 1, j] + 1;
                    int insertion = matrix[i, j - 1] + 1;
                    int susbstitution = matrix[i - 1, j - 1] + cost;

                    matrix[i, j] = Math.Min(Math.Min(delection, insertion), susbstitution);

                }
            }

            //for (int i = 0; i < row + 1; i++)
            //{
            //    for (int j = 0; j < column + 1; j++)
            //    {
            //        Console.Write(matrix[i, j] + " ");
            //    }
            //    Console.WriteLine();
            //}


            return matrix[row, column];
        }

    }
}
