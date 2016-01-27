namespace Poker.Interfaces
{
    using System;

    /// <summary>
    /// The interface/class is responsible for resetting the data of all non-human controlled players.
    /// It directly integrates with the bot database and iterates through its collection of bots.
    /// </summary>
    public interface IBotEraser
    {
        /// <summary>
        /// Sets the Type property of each bot to the default value of Type. Default values
        /// could be found in the PokerGameConstants class.
        /// </summary>
        /// <param name="pokerDatabase"></param>
        void EraseBotType(IPokerDatabase pokerDatabase);

        void EraseBotPower(IPokerDatabase pokerDatabase);

        /// <summary>
        /// Turns each bot panel's visibility to false.
        /// </summary>
        void DisableBotPanel(IPokerDatabase pokerDatabase);

        void EraseBotRaise(IPokerDatabase pokerDatabase);

        void EraseBotCall(IPokerDatabase pokerDatabase);

        /// <summary>
        /// Sets the text of each box indicating the bot's status to "".
        /// </summary>
        void EraseBotStatusText(IPokerDatabase pokerDatabase);

        /// <summary>
        /// Sets the Folded boolean property of each bot to false.
        /// </summary>
        void UnFoldBots(IPokerDatabase pokerDatabase);

        /// <summary>
        /// Sets the OutOfChips property of each bot to false.
        /// </summary>
        void EnableBotChips(IPokerDatabase pokerDatabase);

        /// <summary>
        /// Sets the CanPlay boolean property of all bots to false.
        /// </summary>
        void DisableBots(IPokerDatabase pokerDatabase);
    }
}
