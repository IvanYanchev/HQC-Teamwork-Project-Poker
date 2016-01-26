namespace PokerProjectTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using Poker.Core;
    using Poker.Interfaces;
    using Poker.Models;

    [TestClass]
    public class BotEraserTests
    {
        [TestMethod]
        public void Test_DisableBots()
        {
            IPokerDatabase database = new PokerDatabase();
            int numberOfBots = 5;
            for (int i = 0; i < numberOfBots; i++)
            {
                IPlayer currentPlayer = new Player(i.ToString());
                currentPlayer.CanPlay = true;
                database.AddBot(currentPlayer);
            }

            IBotEraser eraser = new BotEraser();
            eraser.DisableBots(database);

            bool areAllDisabled = true;
            for (int i = 0; i < numberOfBots; i++)
            {
                if (database.TakeBotByIndex(i).CanPlay)
                {
                    areAllDisabled = false;
                    break;
                }
            }

            Assert.IsTrue(areAllDisabled, "Bots are not disabled.");
        }
    }
}
