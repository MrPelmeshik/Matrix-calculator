using System;
using System.IO;

namespace Lab2.MatrixCalculator
{
    static class Program
    {
        const string error = "E";

        static void Main(string[] args)
        {
            const string error = "E";

            MatrixCalculator matrixCalculator = new MatrixCalculator();
            string[,] response;
            string[] mathSeq = { };

            bool file_is_not_open = true;
            do
            {
                Console.Write("\nnamefile -> ");
                string namefile = Console.ReadLine();

                try
                {
                    mathSeq = FileHandler.ReadFile(namefile);
                    file_is_not_open = false;
                }
                catch (FileNotFoundException)
                {
                    Console.Write("Warning -> File not found!");
                    file_is_not_open = true;
                }
                catch (Exception e)
                {
                    Console.Write("Error -> error open file!");
                    file_is_not_open = true;
                }
            } while (file_is_not_open);

            for (int i = 0; i < mathSeq.Length; i++)
            {
                Console.Write("\n\ninput -> {0}\noutput ->", mathSeq[i]);
                response = matrixCalculator.Execute(mathSeq[i]);
                if (response[0, 0] == error)
                {
                    Console.Write("\n\terror");
                    break;
                }

                MatrixHandler.OutputMatrix(
                    MatrixHandler.ConvertMatrixStrToInt(response)
                );
            }

            Console.WriteLine("\n\nExit");
        }
    }
}