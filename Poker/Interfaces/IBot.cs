namespace Poker.Interfaces
{
    using System;

    public interface IBot : IPlayer
    {
        string Name { get; set; }

        void AI();

        void Check();

        void Fold();

        void Call();

        void Raised();
    }
}
