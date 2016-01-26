namespace Poker.Models
{
    using Poker.Interfaces;
    using System;

    public class Card : ICard
    {
        public Card(int cardNum, bool isVisible)
        {
            this.CardNum = cardNum;
            this.IsVisible = isVisible;
        }

        public Card()
            : this(0, false)
        {
        }

        public string Color { get; set; }

        public string Power { get; set; }

        public int CardNum { get; set; }

        public bool IsVisible { get; set; }
    }
}
