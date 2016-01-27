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
        IActionManager ActionManager { get; }

        IBotEraser BotEraser { get; }

        ICombinationDatabase CombinationsDatabase { get; }
    }
}
