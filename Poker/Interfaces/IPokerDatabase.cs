namespace Poker.Interfaces
{
    using Poker.Models;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The interface provides the mandatory properties for a player database.
    /// </summary>
    public interface IPokerDatabase
    {
        /// <summary>
        /// A collection of all non-human controlled players on the poker table.
        /// Can be easily iterated through in order to reset the stats of all players.
        /// </summary>
        IEnumerable<IPlayer> BotsOnTable { get; }

        void AddBot(IPlayer bot);

        /// <summary>
        /// A method used to integrate with each bot in the poker database.
        /// In case the index is negative or bigger than the size of the collection
        /// the method throws an exception.
        /// </summary>
        /// <param name="searchingIndex">The index of the current bot.</param>
        IPlayer TakeBotByIndex(int searchingIndex);

        /// <summary>
        /// Returns the number of non-human controlled players on the game table.
        /// </summary>
        /// <returns></returns>
        int BotsCount();
    }
}
