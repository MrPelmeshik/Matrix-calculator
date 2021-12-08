using NUnit.Framework;
using Lab2.MatrixCalculator;

public class TestMatrixHandler
{
    private static bool VerifResult(string[,] test_m, string[,] verified_m)
    {
        if (test_m.GetLength(0) != verified_m.GetLength(0)
            || test_m.GetLength(1) != verified_m.GetLength(1))
        {
            return false;
        }

        for (int i = 0; i < verified_m.GetLength(0); i++)
        {
            for (int j = 0; j < verified_m.GetLength(1); j++)
            {
                if (verified_m[i, j] != test_m[i, j])
                {
                    return false;
                }
            }
        }

        return true;
    }

    [Test]
    public void Test_GetMatrixFromFile()
    {
        string[,] verifResult =
        {
            {"1", "2", "3"},
            {"4", "5", "6"}
        };

        Assert.IsTrue(VerifResult(
            MatrixHandler.ConvertMatrixIntToStr(
                MatrixHandler.CreateMatrixFromStringLines(
                    FileHandler.ReadFile("M1.txt")
                )
            ),
            verifResult
        ));

    }

    [Test]
    public void Test_MulMatrixByNumber()
    {
        string[,] verifResult =
        {
            {"4", "8", "12"},
            {"16", "20", "24"}
        };
        int[,] m1 = MatrixHandler.CreateMatrixFromStringLines(
            FileHandler.ReadFile("M1.txt")
        );

        string[,] testResult = MatrixHandler.ConvertMatrixIntToStr(
            MatrixHandler.MulMatrixByNumber(m1, 4)
        );

        Assert.IsTrue(VerifResult(testResult, verifResult));

    }

    [Test]
    public void Test_MulMatrix()
    {
        string[,] verifResult =
        {
            {"17", "20"},
            {"38", "44"}
        };
        int[,] m1 = MatrixHandler.CreateMatrixFromStringLines(
            FileHandler.ReadFile("M1.txt")
        );
        int[,] m2 = MatrixHandler.CreateMatrixFromStringLines(
            FileHandler.ReadFile("M2.txt")
        );
        string[,] testResult = MatrixHandler.ConvertMatrixIntToStr(
            MatrixHandler.MulMatrix(m1, m2)
        );

        Assert.IsTrue(VerifResult(testResult, verifResult));

    }

    [Test]
    public void Test_SumMatrix()
    {
        string[,] verifResult =
        {
            {"2", "4", "6"},
            {"8", "10", "12"}
        };
        int[,] m1 = MatrixHandler.CreateMatrixFromStringLines(
            FileHandler.ReadFile("M1.txt")
        );
        string[,] testResult = MatrixHandler.ConvertMatrixIntToStr(
            MatrixHandler.SumMatrix(m1, m1)
        );

        Assert.IsTrue(VerifResult(testResult, verifResult));

    }

    [Test]
    public void Test_MatrixTranspose()
    {
        string[,] verifResult =
        {
            {"1", "4"},
            {"2", "5"},
            {"3", "6"}
        };
        int[,] m1 = MatrixHandler.CreateMatrixFromStringLines(
            FileHandler.ReadFile("M1.txt")
        );

        string[,] testResult = MatrixHandler.ConvertMatrixIntToStr(
            MatrixHandler.Transpose(m1)
        );

        Assert.IsTrue(VerifResult(testResult, verifResult));

    }
}