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

        public void AddBot(IBot bot)
        {
            this.botsOnTable.Add(bot);
        }

        public IBot TakeBotByIndex(int searchingIndex)
        {
            var bot = this.botsOnTable[searchingIndex];
            return bot;
        }

        public int BotsCount()
        {
            return this.botsOnTable.Count();
        }
    }
}
