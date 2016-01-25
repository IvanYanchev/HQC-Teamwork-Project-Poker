namespace Poker.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using Poker.Interfaces;
    using Poker.Core;

    public class Bot : Player, IBot
    {
        public Bot(string name)
            : base(name)
        {
        }
    }
}
