namespace Poker.Models
{
    using System;

    public class Table
    {
        //TODO: ADD ALL BUTTONS AND MAKE INSTANCE IN PLAYER (bCall, bRaise, tbPot e.t.)

        private const double DefaultTableRaise = 0;

        private const int DefaultTableCall = 0;

        public Table(double raise, int call)
        {
            this.CurrentRaise = DefaultTableRaise;
            this.CurrentCall = DefaultTableCall;
            this.Rounds = 0;
            this.Raising = false;
        }

        public double CurrentRaise { get; set; }

        public int CurrentCall { get; set; }

        public double Rounds { get; set; }

        public bool Raising { get; set; }
    }
}
