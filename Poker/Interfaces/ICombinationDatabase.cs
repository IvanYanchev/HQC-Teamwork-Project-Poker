namespace Poker.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public interface ICombinationDatabase
    {
        IActionManager ActionManager { get; }

        void rPairFromHand(IPlayer currentPlayer, int index, List<PokerType> winList, int[] reserveArray, ref PokerType sorted);

        void rPairTwoPair(IPlayer currentPlayer, int index, List<PokerType> winList, int[] reserveArray, ref PokerType sorted);

        void rTwoPair(IPlayer currentPlayer, int index, List<PokerType> winList, int[] reserveArray, ref PokerType sorted);

        void rHighCard(IPlayer currentPlayer, int index, List<PokerType> winList, int[] reserveArray, ref PokerType sorted);

        void rThreeOfAKind(IPlayer currentPlayer, List<PokerType> winList, int[] reserveArray, ref PokerType sorted, int[] Straight);

        void rStraight(IPlayer currentPlayer, List<PokerType> winList, int[] reserveArray, ref PokerType sorted, int[] Straight);

        void rFlush(IPlayer currentPlayer, int index, ref bool vf, List<PokerType> winList, int[] reserveArray, ref PokerType sorted, int[] Straight);

        void rFourOfAKind(IPlayer currentPlayer, List<PokerType> winList, int[] reserveArray, ref PokerType sorted, int[] Straight);

        void rFullHouse(IPlayer currentPlayer, ref double type, ref bool done, List<PokerType> winList, int[] reserveArray, ref PokerType sorted, int[] Straight);

        void rStraightFlush(IPlayer currentPlayer, List<PokerType> winList, int[] reserveArray, ref PokerType sorted, int[] clubs, int[] diamonds, int[] hearts, int[] spades);

        void HighCard(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated);

        void PairTable(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated);

        void PairHand(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds);

        void TwoPair(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds);

        void ThreeOfAKind(IPlayer currentPlayer, int name, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds);

        void Straight(IPlayer currentPlayer, int name, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds);

        void Flush(IPlayer currentPlayer, int name, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds);

        void FullHouse(IPlayer currentPlayer, int name, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds);

        void FourOfAKind(IPlayer currentPlayer, int name, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds);

        void StraightFlush(IPlayer currentPlayer, int name, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds);
    }
}
