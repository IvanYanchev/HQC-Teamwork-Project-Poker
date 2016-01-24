namespace Poker.Interfaces
{
    using Poker.Models;
    using System;
    using System.Collections.Generic;

    public interface IPokerDatabase
    {
        IEnumerable<IBot> BotsOnTable { get; }

        void AddBot(IBot bot, params IBot[] botsToBeAdded);

        IBot TakeBotByIndex(int searchingIndex);
    }
}
