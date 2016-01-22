namespace Poker.Interfaces
{
<<<<<<< HEAD
    using System;

    public interface IPlayer
    {
=======
    using System.Windows.Forms;

    using Poker.Models;

    public interface IPlayer
    {
        int Chips { get; set; }

        Card HoldedCard1 { get; set; }

        Card HoldedCard2 { get; set; }

        bool Turn { get; set; }

        bool FTurn { get; set; }

        double Power { get; set; }

        double Type { get; set; }

        bool Folded { get; set; }

        int Call { get; set; }

        int Raise { get; set; }

        Label Status { get; set; }

        void FixCall(int rounds, int currentCall, int currentRaise, int options);

        void Rules();
>>>>>>> 3be611bd7db5d646aee13ea6509cbb16b14990e4

    }
}
