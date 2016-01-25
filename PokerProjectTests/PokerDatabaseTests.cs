namespace PokerProjectTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Poker.Interfaces;
    using Poker.Models;
    using Poker.Core;

    [TestClass]
    public class PokerDatabaseTests
    {
        [TestMethod]
        public void Test_PokerDatabaseInitialization()
        {
            IPokerDatabase database = new PokerDatabase();
            Assert.AreEqual(0, database.BotsCount(), "The initial bot collection is not empty");
        }

        [TestMethod]
        public void Test_AddBot()
        {
            IPokerDatabase database = new PokerDatabase();
            IBot testBot = new Bot("Test Bot");
            database.AddBot(testBot);
            Assert.AreEqual(1, database.BotsCount(), "The bot was not added to the collection.");
        }
    }
}
