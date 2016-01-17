using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Models
{
    public class Bot
    {
        private const int StartChipsDefault = 10000;

        private int chips;

        public Bot()
        {
            this.Chips = StartChipsDefault;
            this.Type = -1;
            this.Turn = false;
            this.FTurn = false;
            this.Folded = false;

        }

        public int Chips
        {
            get
            {
                return this.chips;
            }
            set
            {
                if (this.chips < 0)
                {
                    value = 0;
                }

                this.chips = value;
            }
        }

        public bool Turn { get; set; }

        public bool FTurn { get; set; }

        public double Power { get; set; }

        public double Type { get; set; }

        public bool Folded { get; set; }

        public int Call { get; set; }

        public int Raise { get; set; }
    }
}
