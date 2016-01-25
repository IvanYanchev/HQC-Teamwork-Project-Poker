namespace Poker.Interfaces
{
    using System.Windows.Forms;

    public interface IPlayer
    {
        string Name { get; }

        int Chips { get; set; }

        ICard HoldedCard1 { get; set; }

        ICard HoldedCard2 { get; set; }

        public int CardOne { get; set; }

        public int CardTwo { get; set; }

        bool CanPlay { get; set; }

        bool OutOfChips { get; set; }

        double Power { get; set; }

        double Type { get; set; }

        bool Folded { get; set; }

        int Call { get; set; }

        int Raise { get; set; }

        Label Status { get; set; }

        void FixCall(ref int globalRaise, ref int globalCall, int options, int globalRounds, ref Button callButton);

        void Rules();
    }
}