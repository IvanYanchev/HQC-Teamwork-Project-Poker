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
            this.Turn = false;
            this.FTurn = false;
            this.Folded = false;

            // TODO Initialize HoldedCard1 and HoldedCard2
            // TODO ADD butons intastances
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

        public Card HoldedCard1 { get; set; }

        public Card HoldedCard2 { get; set; }

        public bool Turn { get; set; }

        public bool FTurn { get; set; }

        public double Power { get; set; }

        public double Type { get; set; }

        public bool Folded { get; set; }

        public int Call { get; set; }

        public int Raise { get; set; }

        public Label Status { get; set; }


        public void FixCall(int rounds, int currentCall, int currentRaise, int options)
        {
            if (rounds != 4)
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
                    if (this.Raise != currentRaise && this.Raise <= currentRaise)
                    {
                        currentCall = Convert.ToInt32(currentRaise) - this.Raise;
                    }

                    if (this.Call != currentCall || this.Call <= currentCall)
                    {
                        currentCall = currentCall - this.Call;
                    }

                    if (this.Raise == currentRaise && currentRaise > 0)
                    {
                        currentCall = 0;
                        //bCall.Enabled = false;
                        //bCall.Text = "Callisfuckedup";
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