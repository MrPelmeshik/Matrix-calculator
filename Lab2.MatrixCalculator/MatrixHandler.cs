using System;
using System.IO;

namespace Lab2.MatrixCalculator
{
    public static class MatrixHandler
    {
        public static void OutputMatrix(int[,] m)
        {
            for (int i = 0; i < m.GetLength(0); i++)
            {
                Console.Write("\n\t");
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    Console.Write("{0}\t", m[i,j]);
                }
            }
        }
        
        public static bool TryParseToMatrix(string value, out int[,] m)
        {
            m = null!;
            string[] line;

            try
            {
                line = FileHandler.ReadFile(value + ".txt");
            }
            catch (FileNotFoundException)
            {
                Console.Write("\nError -> File not found! Check the variable in the file");
                return false;
            }
            catch (Exception e)
            {
                Console.Write("\nError -> error open file!");
                return false;
            }

            try
            {
                m = MatrixHandler.CreateMatrixFromStringLines(line);
            }
            catch (Exception e)
            {
                Console.Write("Error -> error file processing!");
                return false;
            }

            return true;
        }

        public static string[,] ConvertStrToMatrixStr(string value)
        {
            string[,] str_m = new string[1,1];

            str_m[0,0] = value;
            
            return str_m;
        }
        
        public static string[,] ConvertMatrixIntToStr(int[,] m)
        {
            string[,] str_m = new string[m.GetLength(0), m.GetLength(1)];

            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    str_m[i, j] = m[i, j].ToString();
                }
            }
            
            return str_m;
        }
        
        public static int[,] ConvertMatrixStrToInt(string[,] m)
        {
            int[,] int_m = new int[m.GetLength(0), m.GetLength(1)];

            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    int_m[i, j] = int.Parse(m[i, j]);
                }
            }
            
            return int_m;
        }
        
        public static int[,] CreateMatrixFromStringLines(string[] lines)
        {
            int[][] m = new int[lines.Length][];
            
            for (int i = 0; i < lines.Length; i++)
            {
                string s = lines[i];
                string[] elements = s.Split(' ');
                
                m[i] = new int[elements.Length];
                for (int j = 0; j < elements.Length; j++)
                {
                    string element = elements[j];
                    if (int.TryParse(element, out int num))
                    {
                        m[i][j] = num;
                    }
                    else
                    {
                        Console.Write("Error -> When processing a string, not a number was encountered!");
                        throw new NotImplementedException();
                    }
                }
            }

            return ConvertStepArrToArr(m);
        }
        
        public static int[,] MulMatrixByNumber(int[,] m, int n)
        {
            // умножени матрицы на число
            int[,] m2 = new int[m.GetLength(0),m.GetLength(1)];
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    m2[i,j] = m[i,j] * n;
                }
            }

            return m2;
        }
        
        public static int[,] SumMatrix(int[,] m1, int[,] m2)
        {
            //сложени матриц
            if (m1.GetLength(0) != m2.GetLength(0) ||
                m1.GetLength(1) != m2.GetLength(1))
            {
                Console.Write("\nError -> Array summation error!");
                throw new NotImplementedException();
            }

            int[,] m3 = new int[m1.GetLength(0),m1.GetLength(1)];

            for (int i = 0; i < m1.GetLength(0); i++)
            {
                for (int j = 0; j < m1.GetLength(1); j++)
                {
                    m3[i,j] = m1[i,j] + m2[i,j];
                }
            }

            return m3;
        }
        
        public static int[,] MulMatrix(int[,] m1, int[,] m2)
        {
            // произведение матрицы
            if (m1.GetLength(1) != m2.GetLength(0))
            {
                Console.Write("\nError -> Array multiplication error!");
                throw new NotImplementedException();
            }

            int[,] m3 = new int[m1.GetLength(0), m2.GetLength(1)];
            for (int i = 0; i < m1.GetLength(0); i++)
            {
                for (int j = 0; j < m2.GetLength(1); j++)
                {
                    for (int k = 0; k < m2.GetLength(0); k++)
                    {
                        m3[i,j] += m1[i,k] * m2[k,j];
                    }
                }
            }
            
            return m3;
        }
        
        public static int[,] Transpose(int[,] m)
        {
            // транспонирование матриц
            int[,] m2 = new int[m.GetLength(1), m.GetLength(0)];
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    m2[j, i] = m[i, j];
                }
            }

            return m2;
        }

        private static int[,] ConvertStepArrToArr(int[][] m)
        {
            int size = m[0].Length;
            int[,] m2 = new int [m.Length, size];
            for (int i = 0; i < m.Length; i++)
            {
                if (size != m[i].Length)
                {
                    Console.Write("\nError -> Error converting a stepped array to an array!");
                    throw new NotImplementedException();
                }

                for (int j = 0; j < m[i].Length; j++)
                {
                    m2[i,j] = m[i][j];
                }
            }

            return m2;
        }
    }
}