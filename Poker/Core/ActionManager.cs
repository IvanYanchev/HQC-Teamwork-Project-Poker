namespace Poker.Core
{
    using Poker.Interfaces;
    using System;
    using System.Windows.Forms;

    public static class ActionManager
    {
        public static void Check(IPlayer currentPlayer, ref bool raising)
        {
            raising = false;
            currentPlayer.Status.Text = "Check";
            currentPlayer.CanPlay = false;
        }

        public static void Fold(IPlayer currentPlayer, ref bool raising)
        {
            raising = false;
            currentPlayer.Status.Text = "Fold";
            currentPlayer.OutOfChips = true;
            currentPlayer.CanPlay = false;
        }

        public static void Call(IPlayer currentPlayer, ref bool raising, int globalCall, ref TextBox potTexBox)
        {
            raising = false;
            currentPlayer.CanPlay = false;
            currentPlayer.Chips -= globalCall;
            currentPlayer.Status.Text = "Call" + globalCall;
            potTexBox.Text = (int.Parse(potTexBox.Text) + globalCall).ToString();
        }

        public static void Raised(IPlayer currentPlayer, ref bool raising, ref int globalRaise, ref int globalCall, ref TextBox potTextBox)
        {
            currentPlayer.Chips -= globalRaise;
            currentPlayer.Status.Text = "Raise" + globalRaise;
            globalCall = globalRaise;
            raising = true;
            potTextBox.Text = (int.Parse(potTextBox.Text) + globalRaise).ToString();
            currentPlayer.CanPlay = false;
        }
    }
}
