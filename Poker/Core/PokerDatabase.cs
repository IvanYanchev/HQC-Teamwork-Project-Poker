namespace Poker.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Poker.Interfaces;
    using Poker.Models;

    public class PokerDatabase : IPokerDatabase
    {
        private List<IBot> botsOnTable;

        public PokerDatabase()
        {
            this.botsOnTable = new List<IBot>();
        }

        public IEnumerable<IBot> BotsOnTable
        {
            get
            {
                return this.botsOnTable;
            }
        }

        public virtual void AddBot(IBot bot)
        {
            bool duplicateBot = this.botsOnTable.Any(x => x.Name == bot.Name);

            if (duplicateBot)
            {
                throw new ArgumentException("There is already a bot with the same name.");
            }

            this.botsOnTable.Add(bot);
        }

        public virtual IBot TakeBotByIndex(int searchingIndex)
        {
            if (searchingIndex < 0)
            {
                throw new ArgumentOutOfRangeException("The bot index must be equal or greater than zero.");
            }

            if (searchingIndex >= this.botsOnTable.Count)
            {
                throw new ArgumentOutOfRangeException("The bot index must be lower than the size of the bot collection.");
            }

            var bot = this.botsOnTable[searchingIndex];
            return bot;
        }

        public int BotsCount()
        {
            return this.botsOnTable.Count();
        }
    }
}
