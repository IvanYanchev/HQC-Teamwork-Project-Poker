namespace Poker.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Poker.Interfaces;
    using Poker.Models;

    public class PokerDatabase : IPokerDatabase
    {
        private List<Bot> botsOnTable;

        public PokerDatabase()
        {
            this.botsOnTable=new List<Bot>();
        }

        public IEnumerable<Bot> BotsOnTable
        {
            get
            {
                return this.botsOnTable;
            }
        }

        public void AddBot(params Bot[] botsToBeAdded)
        {
            foreach (var currentBot in botsToBeAdded)
            {
                this.botsOnTable.Add(currentBot);
            }
        }

        public Bot TakeBotByIndex(int searchingIndex)
        {
            var bot = this.botsOnTable[searchingIndex];
            return bot;
        }
    }
}
