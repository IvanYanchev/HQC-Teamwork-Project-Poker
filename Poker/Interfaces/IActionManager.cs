namespace Poker.Interfaces
{
    using System;
    using System.Windows.Forms;

    public interface IActionManager
    {
        IGameTable GameTable { get; set; }

        void Check(IPlayer currentPlayer, ref bool isRaisingActivated);

        void Fold(IPlayer currentPlayer, ref bool isRaisingActivated);

        void Call(IPlayer currentPlayer, ref bool isRaisingActivated, int globalCall, ref TextBox potTexBox);

        void Raised(IPlayer currentPlayer, ref bool isRaisingActivated, ref int globalRaise, ref int globalCall, ref TextBox potTextBox);

        void HP(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, int numberOne, int numberTwo);

        void PH(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, int globalRounds, int n, int n1, int r);

        void AI(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds, int name);
    }
}
