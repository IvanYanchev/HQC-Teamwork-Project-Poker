namespace Poker.Models
{
    using System;

    public class Card
    {
        public Card(int cardNum, bool isVisible)
        {
            this.CardNum = cardNum;
            this.IsVisible = isVisible;
        }

        public int CardNum { get; set; }

        public bool IsVisible { get; set; }
    }
}
