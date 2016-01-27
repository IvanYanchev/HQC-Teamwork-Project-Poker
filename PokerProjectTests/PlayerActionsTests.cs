namespace PokerProjectTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Poker;
    using Poker.Core;
    using Poker.Interfaces;
    using Poker.Models;
    using System;
    using System.Windows.Forms;

    [TestClass]
    public class PlayerActionsTests
    {
        [TestMethod]
        public void Test_ActionManagerCall()
        {
            string playerName = PokerGameConstants.DefaultPlayerName;
            IActionManager manager = new ActionManager();
            IPlayer currentPlayer = new Player(playerName);
            currentPlayer.Status = new Label();
            bool isRisingActivated = true;
            int globalCall = 500;
            TextBox box = new TextBox();
            box.Text = "100000";
            int callResult = PokerGameConstants.DefaultStartingChips - PokerGameConstants.InitialCall;

            manager.Call(currentPlayer, ref isRisingActivated, globalCall, ref box);
            Assert.IsFalse(isRisingActivated, "Rising is still activated.");
            Assert.IsFalse(currentPlayer.CanPlay, "Player is still allowed to play.");
            Assert.AreEqual(callResult, currentPlayer.Chips,
                "Player's chips have not been lowered by the call.");
        }
    }
}
