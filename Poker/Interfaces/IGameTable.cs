namespace Poker.Interfaces
{
    using System;

    public interface IGameTable
    {
        IActionManager ActionManager { get; }

        IBotEraser BotEraser { get; }

        ICombinationDatabase CombinationsDatabase { get; }
    }
}
