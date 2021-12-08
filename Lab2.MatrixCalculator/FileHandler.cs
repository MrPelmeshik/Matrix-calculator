using System.IO;

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