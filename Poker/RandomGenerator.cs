namespace Poker
{
    using System;

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
