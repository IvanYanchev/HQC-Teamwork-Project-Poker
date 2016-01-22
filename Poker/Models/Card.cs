using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Models
{
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
