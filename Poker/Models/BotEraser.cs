namespace Poker.Models
{
    using System;
    using Poker.Interfaces;

    public class BotEraser : IBotEraser
    {
        public void EraseBotType(IPokerDatabase pokerDatabase)
        {
            for (int i = 0; i < PokerGameConstants.NumberOfBots; i++)
            {
                pokerDatabase.TakeBotByIndex(i).Type = -1;
            }
        }

        public void EraseBotPower(IPokerDatabase pokerDatabase)
        {
            for (int i = 0; i < PokerGameConstants.NumberOfBots; i++)
            {
                pokerDatabase.TakeBotByIndex(i).Power = 0;
            }
        }

        public void DisableBotPanel(IPokerDatabase pokerDatabase)
        {
            for (int i = 0; i < PokerGameConstants.NumberOfBots; i++)
            {
                pokerDatabase.TakeBotByIndex(i).Panel.Visible = false;
            }
        }

        public void EraseBotRaise(IPokerDatabase pokerDatabase)
        {
            for (int i = 0; i < PokerGameConstants.NumberOfBots; i++)
            {
                pokerDatabase.TakeBotByIndex(i).Raise = 0;
            }
        }

        public void EraseBotCall(IPokerDatabase pokerDatabase)
        {
            for (int i = 0; i < PokerGameConstants.NumberOfBots; i++)
            {
                pokerDatabase.TakeBotByIndex(i).Call = 0;
            }
        }

        public void EraseBotStatusText(IPokerDatabase pokerDatabase)
        {
            for (int i = 0; i < PokerGameConstants.NumberOfBots; i++)
            {
                pokerDatabase.TakeBotByIndex(i).Status.Text = "";
            }
        }

        public void UnFoldBots(IPokerDatabase pokerDatabase)
        {
            for (int i = 0; i < PokerGameConstants.NumberOfBots; i++)
            {
                pokerDatabase.TakeBotByIndex(i).Folded = false;
            }
        }

        public void EnableBotChips(IPokerDatabase pokerDatabase)
        {
            for (int i = 0; i < PokerGameConstants.NumberOfBots; i++)
            {
                pokerDatabase.TakeBotByIndex(i).OutOfChips = false;
            }
        }

        public void DisableBots(IPokerDatabase pokerDatabase)
        {
            for (int i = 0; i < PokerGameConstants.NumberOfBots; i++)
            {
                pokerDatabase.TakeBotByIndex(i).CanPlay = false;
            }
        }
    }
}
