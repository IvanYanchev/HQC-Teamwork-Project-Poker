namespace Poker.Interfaces
{
    using System;

    public interface IBot : IPlayer
    {
        string Name { get; }

        void AI();
    }
}
