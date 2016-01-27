namespace Poker
{
    using System;
    using System.Windows.Forms;
    using Poker.Models;
    using Poker.Interfaces;
    using Poker.Core;

    static class PokerMain
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IBotEraser currentEraser = new BotEraser();
            IActionManager actionManager = new ActionManager();
            ICombinationDatabase combinationDatabase = new CombinationsDatabase(actionManager);
            Application.Run(new GameTable(actionManager, currentEraser, combinationDatabase));
        }
    }
}
