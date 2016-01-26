namespace PokerProjectTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using Poker.Core;
    using Poker.Interfaces;
    using Poker.Models;
    using Poker;

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

        [TestMethod]
        public void Test_EraseBotPower()
        {
            IPokerDatabase database = new PokerDatabase();
            int numberOfBots = 5;
            for (int i = 0; i < numberOfBots; i++)
            {
                IPlayer currentPlayer = new Player(i.ToString());
                currentPlayer.Power = 1000;
                database.AddBot(currentPlayer);
            }

            IBotEraser eraser = new BotEraser();
            eraser.EraseBotPower(database);

            bool isPowerErased = true;
            for (int i = 0; i < numberOfBots; i++)
            {
                if (database.TakeBotByIndex(i).Power != PokerGameConstants.PlayerDefaultPower)
                {
                    isPowerErased = false;
                    break;
                }
            }

            Assert.IsTrue(isPowerErased, "Bots power is not erased.");
        }

        [TestMethod]
        public void Test_EraseBotType()
        {
            IPokerDatabase database = new PokerDatabase();
            int numberOfBots = 5;
            for (int i = 0; i < numberOfBots; i++)
            {
                IPlayer currentPlayer = new Player(i.ToString());
                currentPlayer.Type = 1000;
                database.AddBot(currentPlayer);
            }

            IBotEraser eraser = new BotEraser();
            eraser.EraseBotType(database);

            bool isTypeErased = true;
            for (int i = 0; i < numberOfBots; i++)
            {
                if (database.TakeBotByIndex(i).Type != PokerGameConstants.PlayerDefaultType)
                {
                    isTypeErased = false;
                    break;
                }
            }

            Assert.IsTrue(isTypeErased, "Bots type is not erased.");
        }

        [TestMethod]
        public void Test_EraseBotCall()
        {
            IPokerDatabase database = new PokerDatabase();
            int numberOfBots = 5;
            for (int i = 0; i < numberOfBots; i++)
            {
                IPlayer currentPlayer = new Player(i.ToString());
                currentPlayer.Call = 1000;
                database.AddBot(currentPlayer);
            }

            IBotEraser eraser = new BotEraser();
            eraser.EraseBotCall(database);

            bool isCallErased = true;
            for (int i = 0; i < numberOfBots; i++)
            {
                if (database.TakeBotByIndex(i).Call != PokerGameConstants.PlayerDefaultCall)
                {
                    isCallErased = false;
                    break;
                }
            }

            Assert.IsTrue(isCallErased, "Bots call is not erased.");
        }

        [TestMethod]
        public void Test_EnableBotChips()
        {
            IPokerDatabase database = new PokerDatabase();
            int numberOfBots = 5;
            for (int i = 0; i < numberOfBots; i++)
            {
                IPlayer currentPlayer = new Player(i.ToString());
                currentPlayer.OutOfChips = true;
                database.AddBot(currentPlayer);
            }

            IBotEraser eraser = new BotEraser();
            eraser.EnableBotChips(database);

            bool areChipsEnabled = true;
            for (int i = 0; i < numberOfBots; i++)
            {
                if (database.TakeBotByIndex(i).OutOfChips != PokerGameConstants.PlayerDefaultOutOfChips)
                {
                    areChipsEnabled = false;
                    break;
                }
            }

            Assert.IsTrue(areChipsEnabled, "Bot chips are not enabled.");
        }

        [TestMethod]
        public void Test_UnFoldBots()
        {
            IPokerDatabase database = new PokerDatabase();
            int numberOfBots = 5;
            for (int i = 0; i < numberOfBots; i++)
            {
                IPlayer currentPlayer = new Player(i.ToString());
                currentPlayer.Folded = true;
                database.AddBot(currentPlayer);
            }

            IBotEraser eraser = new BotEraser();
            eraser.UnFoldBots(database);

            bool areAllFolded = true;
            for (int i = 0; i < numberOfBots; i++)
            {
                if (database.TakeBotByIndex(i).Folded != PokerGameConstants.PlayerDefaultFolded)
                {
                    areAllFolded = false;
                    break;
                }
            }

            Assert.IsTrue(areAllFolded, "Bot have folded.");
        }
    }
}
