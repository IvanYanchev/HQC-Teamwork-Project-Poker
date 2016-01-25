namespace PokerProjectTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Poker;

    [TestClass]
    public class BasicUtilitiesTests
    {
        [TestMethod]
        public void Test_RandomGeneratorZeroRange()
        {
            int number = RandomGenerator.Next(0);
            Assert.AreEqual(0, 0, "The Random Generator utility is not working properly.");
        }

        [TestMethod]
        public void Test_RandomGeneratorSpecificRange()
        {
            int number = RandomGenerator.Next(0, 5);
            bool isInRange = number >= 0 && number <= 5;
            Assert.AreEqual(true, isInRange, "The Random Generator utility is not working properly.");
        }

        [TestMethod]
        public void Test_RandomGeneratorNegativeRange()
        {
            int number = RandomGenerator.Next(-10, -5);
            bool isInRange = number >= -10 && number <= -5;
            Assert.AreEqual(true, isInRange, "The Random Generator utility is not working properly.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The starting index is bigger than the end index.")]
        public void Test_RandomGeneratorException()
        {
            int number = RandomGenerator.Next(-5, -10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "If a single index is provided it should be equal or greater than zero.")]
        public void Test_RandomGeneratorExceptionX()
        {
            int number = RandomGenerator.Next(-10);
        }
    }
}
