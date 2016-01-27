namespace Poker.Interfaces
{
    using System;

    /// <summary>
    /// The interface holds information on some public getters and dependencies
    /// of the poker game table class. A poker game table to be initialized
    /// an Action Manager / Bot Eraser and a Combinations Database must be
    /// present.
    /// </summary>
    public interface IGameTable
    {
        /// <summary>
        /// The class which brings the poker-specific actions utility 
        /// on the poker table.
        /// </summary>
        IActionManager ActionManager { get; }

        /// <summary>
        /// The class responsible of resetting the statistics of
        /// non-human controlled players.
        /// </summary>
        IBotEraser BotEraser { get; }

        /// <summary>
        /// The class which allows acess to all poker-specific card
        /// combinations.
        /// </summary>
        ICombinationDatabase CombinationsDatabase { get; }

        /// <summary>
        /// A class which holds a collection of all
        /// non-human controlled players on the poker table.
        /// Each of the players can be accessed by a given index.
        /// </summary>
        IPokerDatabase PokerDatabase { get; }

        /// <summary>
        /// The property represents the human-controlled player on the 
        /// poker table.
        /// </summary>
        IPlayer Player { get; }
    }
}
