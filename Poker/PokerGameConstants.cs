namespace Poker
{
    using System;

    public static class PokerGameConstants
    {
        public const int DefaultStartingChips = 10000;
        public const int MaximalChipsAmount = 10000000;

        public const int NumberOfBots = 5;
        public const int NumberOfCards = 52;

        public const int BotOneCardOne = 2;
        public const int BotOneCardTwo = 3;

        public const int BotTwoCardOne = 4;
        public const int BotTwoCardTwo = 5;

        public const int BotThreeCardOne = 6;
        public const int BotThreeCardTwo = 7;

        public const int BotFourCardOne = 8;
        public const int BotFourCardTwo = 9;

        public const int BotFiveCardOne = 10;
        public const int BotFiveCardTwo = 11;

        public const int BigBlindValue = 500;
        public const int SmallBlindValue = 250;
        public const int InitialCall = 500;
        public const int InitialFoldedPlayers = 5;
        public const bool RestartRequestedDefault = false;
        public const bool RaisingActivatedDefault = false;

        public const int PlayerDefaultType = -1;
        public const int PlayerDefaultCall = 0;
        public const int PlayerDefaultRaise = 0;
        public const int PlayerDefaultPower = 0;
        public const bool PlayerDefaultOutOfChips = false;
        public const bool PlayerDefaultCanPlay = false;
        public const bool PlayerDefaultFolded = false;
    }
}
