namespace Poker.Models
{
    using System;

    public class Chip
    {
        public Chip(int value)
        {
            this.Value = value;
        }

        public int Value { get; private set; }
    }
}
