namespace Poker
{
    using System;

    /// <summary>
    /// A static class which provides the generation of random numbers
    /// and is accessible by all other classes in the project. Accepts both
    /// one and two arguments. In the case of a negative single argument
    /// throws an exception.
    /// </summary>
    public static class RandomGenerator
    {
        private static readonly Random generator = new Random();

        public static int Next(int startIndex, int endIndex)
        {
            if (startIndex > endIndex)
            {
                throw new ArgumentException("The starting index must be lower or equal to the end index");
            }

            return generator.Next(startIndex, endIndex);
        }

        public static int Next(int end)
        {
            return generator.Next(end);
        }
    }
}
