namespace Poker.Interfaces
{
    using System;

    public interface IBotEraser
    {
        void EraseBotType(IPokerDatabase pokerDatabase);

        void EraseBotPower(IPokerDatabase pokerDatabase);

        void DisableBotPanel(IPokerDatabase pokerDatabase);

        void EraseBotRaise(IPokerDatabase pokerDatabase);

        void EraseBotCall(IPokerDatabase pokerDatabase);

        void EraseBotStatusText(IPokerDatabase pokerDatabase);

        void UnFoldBots(IPokerDatabase pokerDatabase);

        void EnableBotChips(IPokerDatabase pokerDatabase);

        void DisableBots(IPokerDatabase pokerDatabase);
    }
}
