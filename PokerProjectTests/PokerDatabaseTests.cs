namespace PokerProjectTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Poker.Interfaces;
    using Poker.Models;
    using Poker.Core;

    [TestClass]
    public class PokerDatabaseTests
    {
        IPokerDatabase database = new PokerDatabase();
    }
}
