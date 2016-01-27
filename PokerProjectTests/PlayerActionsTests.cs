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

        [TestMethod]
        public void Test_ActionManagerFold()
        {
            string playerName = PokerGameConstants.DefaultPlayerName;
            IActionManager manager = new ActionManager();
            IPlayer currentPlayer = new Player(playerName);
            currentPlayer.Status = new Label();
            bool isRisingActivated = true;

            manager.Fold(currentPlayer, ref isRisingActivated);

            Assert.IsFalse(isRisingActivated, "Rising is still activated.");
            Assert.IsFalse(currentPlayer.CanPlay, "Player is still allowed to play.");
            Assert.IsTrue(currentPlayer.OutOfChips,
                "The player's chips are still available.");
            Assert.AreEqual("Fold", currentPlayer.Status.Text, 
                "The player's status text box is not displaying the correct mesage");
        }

        [TestMethod]
        public void Test_ActionManagerRaise()
        {
            string playerName = PokerGameConstants.DefaultPlayerName;
            IActionManager manager = new ActionManager();
            IPlayer currentPlayer = new Player(playerName);
            currentPlayer.Status = new Label();
            bool isRisingActivated = false;
            int globalCall = 0;
            int globalRaise = 500;
            TextBox box = new TextBox();
            box.Text = "0";
            int playerChipsResult = currentPlayer.Chips - globalRaise;

            manager.Raised(currentPlayer, ref isRisingActivated, ref globalRaise, ref globalCall, ref box);

            Assert.IsTrue(isRisingActivated, "Rising is still activated.");
            Assert.AreEqual(globalCall, globalCall, "The global call and raise are not equal.");
            Assert.AreEqual(playerChipsResult, currentPlayer.Chips, "The player's chips have not been lowered correctly.");
            Assert.IsFalse(currentPlayer.OutOfChips, "The player is out of chips.");
        }
    }
}
