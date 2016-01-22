using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Interfaces
{
    using Poker.Models;

    public interface IPokerDatabase
    {
        IEnumerable<Bot> BotsOnTable { get; }

        void AddBot(params Bot[] botsToBeAdded);

        Bot TakeBotByIndex(int searchingIndex);
    }
}
