using NUnit.Framework;
using Lab2.MatrixCalculator;

public class TestMatrixCalculator
{
    private static readonly int[,] m1 = MatrixHandler.CreateMatrixFromStringLines(FileHandler.ReadFile("M1.txt"));
    private static readonly int[,] m2 = MatrixHandler.CreateMatrixFromStringLines(FileHandler.ReadFile("M2.txt"));
    private static readonly string[,] str_m1 = MatrixHandler.ConvertMatrixIntToStr(m1);
    private static readonly string[,] str_m2 = MatrixHandler.ConvertMatrixIntToStr(m2);
    private static readonly string[,] error_m = MatrixHandler.ConvertStrToMatrixStr("E");
    
    private MatrixCalculator _matrixCalculator;

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

    [SetUp]
    public void Setup()
    {
        _matrixCalculator = new MatrixCalculator();
    }

    [Test]
    public void Test_MulMatrix()
    {
        string[,] result_mul = MatrixHandler.ConvertMatrixIntToStr(MatrixHandler.MulMatrix(m1, m2));

        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("M1"), str_m1));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("*"), str_m1));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("M2"), str_m2));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("="), result_mul));
    }

    [Test]
    public void Test_SumMatrix()
    {
        string[,] result_mul = MatrixHandler.ConvertMatrixIntToStr(MatrixHandler.SumMatrix(m1, m1));

        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("M1"), str_m1));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("+"), str_m1));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("M1"), str_m1));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("="), result_mul));
    }

    [Test]
    public void Test_MulMatrixByNum()
    {
        int number = 4;
        string[,] str_number = MatrixHandler.ConvertStrToMatrixStr(4.ToString());
        string[,] result_mul = MatrixHandler.ConvertMatrixIntToStr(MatrixHandler.MulMatrixByNumber(m1, number));

        Assert.IsTrue(VerifResult(_matrixCalculator.Execute(number.ToString()), str_number));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("*"), str_number));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("M1"), str_m1));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("="), result_mul));

    }
    
    [Test]
    public void Test_MatrixTranspose()
    {
        string[,] str_tr_m1 = MatrixHandler.ConvertMatrixIntToStr(MatrixHandler.Transpose(m1));

        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("M1"), str_m1));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("TR"), str_tr_m1));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("="), str_tr_m1));

    }
    
    [Test]
    public void Test_Error_MulMatrix()
    {
        string[,] result_mul = MatrixHandler.ConvertMatrixIntToStr(MatrixHandler.MulMatrix(m1, m2));

        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("M1"), str_m1));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("*"), str_m1));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("M1"), str_m1));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("="), error_m));
    }

    [Test]
    public void Test_Error_SumMatrix()
    {
        string[,] result_mul = MatrixHandler.ConvertMatrixIntToStr(MatrixHandler.SumMatrix(m1, m1));

        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("M1"), str_m1));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("+"), str_m1));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("M2"), str_m2));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("="), error_m));
    }

    [Test]
    public void Test_Error_MulMatrixByNum()
    {
        int number = 4;
        string[,] str_number = MatrixHandler.ConvertStrToMatrixStr(4.ToString());
        
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute(number.ToString()), str_number));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("*"), str_number));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("*"), error_m));

    }
    
    [Test]
    public void Test_Error_MatrixTranspose()
    {
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("M3"), error_m));
    }
    
    [Test]
    public void Test_Seq_1()
    {
        int number = 4;
        string[,] str_number = MatrixHandler.ConvertStrToMatrixStr(number.ToString());
        int[,] result_1 = MatrixHandler.MulMatrixByNumber(m1, number);
        string[,] str_result_1 = MatrixHandler.ConvertMatrixIntToStr(result_1);
        int[,] tr_m1 = MatrixHandler.Transpose(m1);
        string[,] str_tr_m1 = MatrixHandler.ConvertMatrixIntToStr(tr_m1);
        string[,] str_result_2 = MatrixHandler.ConvertMatrixIntToStr(MatrixHandler.MulMatrix(result_1, tr_m1));

        Assert.IsTrue(VerifResult(_matrixCalculator.Execute(number.ToString()), str_number));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("*"), str_number));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("M1"), str_m1));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("*"), str_result_1));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("M1"), str_m1));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("TR"), str_tr_m1));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("="), str_result_2));
    }

    [Test]
    public void Test_Seq_2()
    {
        int number = 4;
        string[,] str_number = MatrixHandler.ConvertStrToMatrixStr(number.ToString());
        int[,] result_1 = MatrixHandler.MulMatrixByNumber(m1, number);
        string[,] str_result_1 = MatrixHandler.ConvertMatrixIntToStr(result_1);
        string[,] str_result_2 = MatrixHandler.ConvertMatrixIntToStr(MatrixHandler.SumMatrix(result_1, m1));

        Assert.IsTrue(VerifResult(_matrixCalculator.Execute(number.ToString()), str_number));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("*"), str_number));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("M1"), str_m1));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("+"), str_result_1));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("M1"), str_m1));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("="), str_result_2));
    }

    [Test]
    public void Test_Seq_3()
    {
        // прроверка на транспонирвание в середине последовательности
        int number = 4;
        string[,] str_number = MatrixHandler.ConvertStrToMatrixStr(number.ToString());
        int[,] result_1 = MatrixHandler.MulMatrixByNumber(m1, number);
        string[,] str_result_1 = MatrixHandler.ConvertMatrixIntToStr(result_1);
        int[,] tr_m2 = MatrixHandler.Transpose(m2);
        string[,] str_tr_m2 = MatrixHandler.ConvertMatrixIntToStr(tr_m2);
        int[,] result_2 = MatrixHandler.SumMatrix(result_1, tr_m2);
        string[,] str_result_2 = MatrixHandler.ConvertMatrixIntToStr(result_2);
        string[,] str_result_3 = MatrixHandler.ConvertMatrixIntToStr(MatrixHandler.MulMatrix(result_2, m2));

        Assert.IsTrue(VerifResult(_matrixCalculator.Execute(number.ToString()), str_number));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("*"), str_number));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("M1"), str_m1));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("+"), str_result_1));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("M2"), str_m2));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("TR"), str_tr_m2));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("*"), str_result_2));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("M2"), str_m2));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("="), str_result_3));
    }
    
    [Test]
    public void Test_Seq_4()
    {
        // прроверка нескольких транспонирваний
        int number = 4;
        string[,] str_number = MatrixHandler.ConvertStrToMatrixStr(number.ToString());
        int[,] tr_m2 = MatrixHandler.Transpose(m2);
        string[,] str_tr_m2 = MatrixHandler.ConvertMatrixIntToStr(tr_m2);
        int[,] result_1 = MatrixHandler.MulMatrixByNumber(tr_m2, number);
        string[,] str_result_1 = MatrixHandler.ConvertMatrixIntToStr(result_1);
        int[,] tr_m1 = MatrixHandler.Transpose(m1);
        string[,] str_tr_m1 = MatrixHandler.ConvertMatrixIntToStr(tr_m1);
        
        int[,] result_2 = MatrixHandler.MulMatrix(result_1, tr_m1);
        string[,] str_result_2 = MatrixHandler.ConvertMatrixIntToStr(result_2);

        Assert.IsTrue(VerifResult(_matrixCalculator.Execute(number.ToString()), str_number));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("*"), str_number));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("M2"), str_m2));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("TR"), str_tr_m2));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("*"), str_result_1));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("M1"), str_m1));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("TR"), str_tr_m1));
        Assert.IsTrue(VerifResult(_matrixCalculator.Execute("="), str_result_2));
    }
}