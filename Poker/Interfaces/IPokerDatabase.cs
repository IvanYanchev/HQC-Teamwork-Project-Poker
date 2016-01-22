namespace Poker.Interfaces
{
    using Poker.Models;
    using System;
    using System.Collections.Generic;

    public interface IPokerDatabase
    {
        IEnumerable<Bot> BotsOnTable { get; }

        void AddBot(params Bot[] botsToBeAdded);

        Bot TakeBotByIndex(int searchingIndex);
    }
}
