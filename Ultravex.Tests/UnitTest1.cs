using NUnit.Framework;

namespace Ultravex.Tests;

public class Tests
{
    [Test]
    public void GetOpposedSide()
    {
        Assert.AreEqual(0, BitUtils.GetOpposedSide(2));
        Assert.AreEqual(2, BitUtils.GetOpposedSide(0));
        Assert.AreEqual(1, BitUtils.GetOpposedSide(3));
        Assert.AreEqual(3, BitUtils.GetOpposedSide(1));
    }

    [Test]
    public void RotateClockwise()
    {
        Assert.AreEqual(1, BitUtils.RotateClockwise(0));
        Assert.AreEqual(2, BitUtils.RotateClockwise(1));
        Assert.AreEqual(3, BitUtils.RotateClockwise(2));
        Assert.AreEqual(0, BitUtils.RotateClockwise(3));
    }

    [Test]
    public void RotateCounterClockwise()
    {
        Assert.AreEqual(3, BitUtils.RotateCounterClockwise(0));
        Assert.AreEqual(0, BitUtils.RotateCounterClockwise(1));
        Assert.AreEqual(1, BitUtils.RotateCounterClockwise(2));
        Assert.AreEqual(2, BitUtils.RotateCounterClockwise(3));
    }

    [Test]
    public void Solve2x2()
    {
        var set = TilesExtensions.CreateValidSet(2);

        set.Print();

        Assert.IsTrue(set.IsCompleted());

        set.Shuffle();

        bool atLeastOneSolution = false;

        foreach (var solution in set.Solve2x2())
        {
            atLeastOneSolution = true;
            solution.Print();
            Assert.IsTrue(solution.IsCompleted());
        }

        Assert.IsTrue(atLeastOneSolution);
    }
}
