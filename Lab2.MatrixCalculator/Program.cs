using System;

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

            string[] mathSeq = FileHandler.ReadFile("Calc1.txt");

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