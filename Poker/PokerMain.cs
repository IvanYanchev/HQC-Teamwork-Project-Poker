namespace Poker
{
    using System;
    using System.Windows.Forms;
    using Poker.Models;
    using Poker.Interfaces;

    static class PokerMain
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IBotEraser currentEraser = new BotEraser();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameTable());
        }
    }
}
