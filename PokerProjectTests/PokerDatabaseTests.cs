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

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Duplicate bots.")]
        public void Test_AddBotDuplicateException()
        {
            IPokerDatabase database = new PokerDatabase();
            IBot testBot = new Bot("Test Bot");
            IBot duplicateBot = new Bot("Test Bot");
            database.AddBot(testBot);
            database.AddBot(duplicateBot);
        }

        [TestMethod]
        public void Test_TakeBotByIndex()
        {
            IPokerDatabase database = new PokerDatabase();
            IBot testBot = new Bot("Test Bot");
            database.AddBot(testBot);

            IBot currentBot = database.TakeBotByIndex(0);
            Assert.AreEqual(currentBot, testBot, "The take bot by index method does not work correctly.");
        }
    }
}
