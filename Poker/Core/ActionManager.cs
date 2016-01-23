namespace Poker.Core
{
    using Poker.Interfaces;
    using System;

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

        public static void Call()
        {

        }

        public static void Raised()
        {

        }
    }
}
