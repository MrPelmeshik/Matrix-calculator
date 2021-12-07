using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Lab2.MatrixCalculator
{
    public static class FileHandler
    {

        public static string[] ReadFile(string nameFile)
        {
            string[] lines = File.ReadAllLines("../../../../../" + nameFile);

            return lines;
        }
    }
}