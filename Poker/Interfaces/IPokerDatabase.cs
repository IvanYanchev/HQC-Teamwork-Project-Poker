namespace Poker.Interfaces
{
    using Poker.Models;
    using System;
    using System.Collections.Generic;

    public interface IPokerDatabase
    {
        IEnumerable<IPlayer> BotsOnTable { get; }

        void AddBot(IPlayer bot);

        IPlayer TakeBotByIndex(int searchingIndex);

        int BotsCount();
    }
}
