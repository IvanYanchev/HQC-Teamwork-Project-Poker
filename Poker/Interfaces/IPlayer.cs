namespace Poker.Interfaces
{
    using System.Windows.Forms;

    /// <summary>
    /// An interface which holds information about the properties
    /// each player in the game should have.
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// The name of each player cannot be null, white space or an empty string.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Each player's chips cannot fall below zero.
        /// </summary>
        int Chips { get; set; }

        ICard HoldedCard1 { get; set; }

        ICard HoldedCard2 { get; set; }

        int CardOne { get; set; }

        int CardTwo { get; set; }

        Panel Panel { get; set; }

        /// <summary>
        /// Each player has a text box which displays the player's chips amount.
        /// </summary>
        TextBox ChipsTextBox { get; set; }

        bool CanPlay { get; set; }

        bool OutOfChips { get; set; }

        double Power { get; set; }

        double Type { get; set; }

        bool Folded { get; set; }

        int Call { get; set; }

        int Raise { get; set; }

        Label Status { get; set; }

        void FixCall(ref int globalRaise, ref int globalCall, int options, int globalRounds, ref Button callButton);
    }
}