namespace PokerProjectTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Poker.Interfaces;
    using Poker.Models;
    using Poker;
    using System;
    using System.Windows.Forms;

    [TestClass]
    public class PokerPlayerTests
    {
        [TestMethod]
        public void Test_BotInitialization()
        {
            string botName = "Test Bot";
            IPlayer testBot = new Player(botName);
            testBot.Panel = new Panel();
            testBot.ChipsTextBox = new TextBox();
            Assert.AreEqual(botName, testBot.Name, "The bot's name was not set crrectly.");
            Assert.IsFalse(testBot.CanPlay, "The bot is allowed to play.");
            Assert.IsFalse(testBot.OutOfChips, "The bot is not initially out of chips.");
            Assert.IsFalse(testBot.Folded, "The bot has initially folded.");
            Assert.AreEqual(PokerGameConstants.DefaultStartingChips, testBot.Chips, 
                "The starting chips are lower than the default value.");
            Assert.AreEqual(-1, testBot.Type, "The initial bot type is not -1");
            Assert.IsNotNull(testBot.Panel, "The bot panel is null.");
            Assert.IsNotNull(testBot.ChipsTextBox, "The bot chips text box is null.");
        }

        [TestMethod]
        public void Test_BotChips()
        {
            string botName = "Test Bot";
            IPlayer testBot = new Player(botName);
            testBot.Chips -= 1000000;
            Assert.AreEqual(0, testBot.Chips, "Bot chips cannot be negative.");
        }
    }
}
