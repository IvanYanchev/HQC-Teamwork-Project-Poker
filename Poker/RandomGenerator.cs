namespace Poker
{
    using System;

    public static class RandomGenerator
    {
        private static readonly Random generator = new Random();

        public static int Next(int startIndex, int endIndex)
        {
            return generator.Next(startIndex, endIndex);
        }
    }
}
