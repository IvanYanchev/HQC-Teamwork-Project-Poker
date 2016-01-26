namespace Poker.Models
{
    using System;
    using Poker.Interfaces;

    public class BotEraser : IBotEraser
    {
        public virtual void EraseBotType(IPokerDatabase pokerDatabase)
        {
            for (int i = 0; i < PokerGameConstants.NumberOfBots; i++)
            {
                pokerDatabase.TakeBotByIndex(i).Type = PokerGameConstants.PlayerDefaultType;
            }
        }

        public virtual void EraseBotPower(IPokerDatabase pokerDatabase)
        {
            for (int i = 0; i < PokerGameConstants.NumberOfBots; i++)
            {
                pokerDatabase.TakeBotByIndex(i).Power = 0;
            }
        }

        public virtual void DisableBotPanel(IPokerDatabase pokerDatabase)
        {
            for (int i = 0; i < PokerGameConstants.NumberOfBots; i++)
            {
                pokerDatabase.TakeBotByIndex(i).Panel.Visible = false;
            }
        }

        public virtual void EraseBotRaise(IPokerDatabase pokerDatabase)
        {
            for (int i = 0; i < PokerGameConstants.NumberOfBots; i++)
            {
                pokerDatabase.TakeBotByIndex(i).Raise = 0;
            }
        }

        public virtual void EraseBotCall(IPokerDatabase pokerDatabase)
        {
            for (int i = 0; i < PokerGameConstants.NumberOfBots; i++)
            {
                pokerDatabase.TakeBotByIndex(i).Call = 0;
            }
        }

        public virtual void EraseBotStatusText(IPokerDatabase pokerDatabase)
        {
            for (int i = 0; i < PokerGameConstants.NumberOfBots; i++)
            {
                pokerDatabase.TakeBotByIndex(i).Status.Text = "";
            }
        }

        public virtual void UnFoldBots(IPokerDatabase pokerDatabase)
        {
            for (int i = 0; i < PokerGameConstants.NumberOfBots; i++)
            {
                pokerDatabase.TakeBotByIndex(i).Folded = false;
            }
        }

        public virtual void EnableBotChips(IPokerDatabase pokerDatabase)
        {
            for (int i = 0; i < PokerGameConstants.NumberOfBots; i++)
            {
                pokerDatabase.TakeBotByIndex(i).OutOfChips = false;
            }
        }

        public virtual void DisableBots(IPokerDatabase pokerDatabase)
        {
            for (int i = 0; i < PokerGameConstants.NumberOfBots; i++)
            {
                pokerDatabase.TakeBotByIndex(i).CanPlay = false;
            }
        }
    }
}
