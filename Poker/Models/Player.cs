namespace Poker.Models
{
    using System.Windows.Forms;
    using Poker.Interfaces;
    using System;

    public class Player : IPlayer
    {
        private int chips;
        private string name;

        public Player()
        {
            this.Chips = PokerGameConstants.DefaultStartingChips;
            this.Type = PokerGameConstants.PlayerDefaultType;
            this.CanPlay = PokerGameConstants.PlayerDefaultCanPlay;
            this.OutOfChips = PokerGameConstants.PlayerDefaultOutOfChips;
            this.Folded = PokerGameConstants.PlayerDefaultFolded;
            this.Power = PokerGameConstants.PlayerDefaultPower;
            this.Call = PokerGameConstants.PlayerDefaultCall;
            this.Raise = PokerGameConstants.PlayerDefaultRaise;
            this.Panel = new Panel();
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
                this.chips = value;

                if (this.chips < 0)
                {
                    this.chips = 0;
                }
            }
        }

        public string Name 
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Player's name cannot be empty.");
                }

                this.name = value;
            }
        }

        public ICard HoldedCard1 { get; set; }

        public ICard HoldedCard2 { get; set; }

        public int CardOne { get; set; }

        public int CardTwo { get; set; }

        public Panel Panel { get; set; }

        public TextBox ChipsTextBox { get; set; }

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