using NUnit.Framework;
using Lab2.MatrixCalculator;

public class TestMatrixCalculator
{
    const string error = "E";
    const string ok = "OK";
        
    private MatrixCalculator _matrixCalculator;
    
    [SetUp]
    public void Setup()
    {
        _matrixCalculator = new MatrixCalculator();
    }

    [Test]
   public void Test1()
    {
        Assert.IsTrue(_matrixCalculator.Execute("M1") == ok);
        Assert.IsTrue(_matrixCalculator.Execute("*") == ok);
        Assert.IsTrue(_matrixCalculator.Execute("M2") == ok);
        Assert.IsTrue(_matrixCalculator.Execute("=") == ok);
    }

    [Test]
    public void Test2()
    {
        Assert.IsTrue(_matrixCalculator.Execute("*") == error);
        Assert.IsTrue(_matrixCalculator.Execute("*") == error);
        Assert.IsTrue(_matrixCalculator.Execute("M2") == ok);
        Assert.IsTrue(_matrixCalculator.Execute("=") == ok);
    }
        
    [Test]
    public void Test3()
    {
        Assert.IsTrue(_matrixCalculator.Execute("M1") == ok);
        Assert.IsTrue(_matrixCalculator.Execute("-") == error);
        Assert.IsTrue(_matrixCalculator.Execute("M2") == ok);
        Assert.IsTrue(_matrixCalculator.Execute("=") == ok);
    }
}