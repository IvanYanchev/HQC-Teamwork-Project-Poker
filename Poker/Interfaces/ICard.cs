namespace Poker.Interfaces
{
    using System;

    public interface ICard
    {
        string Color { get; set; }

        string Power { get; set; }

        int CardNum { get; set; }

        bool IsVisible { get; set; }
    }
}
