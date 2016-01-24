namespace Poker.Models
{
    using System.Windows.Forms;
    using Poker.Interfaces;
    using System;

    public abstract class Player : IPlayer
    {
        private const int StartChipsDefault = 10000;

        private int chips;

        public Player()
        {
            this.Chips = StartChipsDefault;
            this.Type = -1;
            this.CanPlay = false;
            this.OutOfChips = false;
            this.Folded = false;

            // TODO Initialize HoldedCard1 and HoldedCard2
            // TODO ADD butons intastances
        }

        public Player(string name)
            :  this()
        {
            this.Name = name;
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

        public string Name { get; private set; }

        public ICard HoldedCard1 { get; set; }

        public ICard HoldedCard2 { get; set; }

        public bool CanPlay { get; set; }

        public bool OutOfChips { get; set; }

        public double Power { get; set; }

        public double Type { get; set; }

        public bool Folded { get; set; }

        public int Call { get; set; }

        public int Raise { get; set; }

        public Label Status { get; set; }

        public void FixCall(ref int globalRaise, ref int globalCall, int options, int globalRounds, ref Button callButton)
        {
            if (globalRounds != 4)
            {
                if (options == 1)
                {
                    if (this.Status.Text.Contains("Raise"))
                    {
                        var changeRaise = this.Status.Text.Substring(6);
                        this.Raise = int.Parse(changeRaise);
                    }

                    if (this.Status.Text.Contains("Call"))
                    {
                        var changeCall = this.Status.Text.Substring(5);
                        this.Call = int.Parse(changeCall);
                    }

                    if (this.Status.Text.Contains("Check"))
                    {
                        this.Raise = 0;
                        this.Call = 0;
                    }
                }

                if (options == 2)
                {
                    if (globalRaise != this.Raise && globalRaise <= this.Raise)
                    {
                        globalCall = (int)globalRaise - this.Raise;
                    }

                    if (globalCall != this.Call || globalCall <= this.Call)
                    {
                        globalCall = globalCall - this.Call;
                    }

                    if (globalRaise == this.Raise && this.Raise > 0)
                    {
                        globalCall = 0;
                        callButton.Enabled = false;
                        callButton.Text = "Callisfuckedup";
                    }
                }
            }
        }

        public void Rules()
        {
            throw new NotImplementedException();
        }
    }
}