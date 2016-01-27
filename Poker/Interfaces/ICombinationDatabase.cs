namespace Poker.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    /// <summary>
    /// An interface which holds all possible poker combinations of cards.
    /// Source of method definitions: http://dictionary.pokerzone.com/
    /// </summary>
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

        /// <summary>
        /// A hand whose ranking is determined solely on the basis of the greatest card value
        /// held because it does not contain a pair, two pair, three of a kind, a straight, 
        /// a flush, a full house, four of a kind, or a straight flush; in the hierarchy of hand values, 
        /// the hand ranking immediately below a pair.
        /// </summary>
        void HighCard(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated);

        void PairTable(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated);

        void PairHand(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds);

        void TwoPair(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds);

        /// <summary>
        /// A hand containing three cards of the same value, 
        /// such as Qs-Qh-Qd-8s-9d; the hand ranking immediately below a straight 
        /// and immediately above two pair. When multiple players have three of a kind, 
        /// the winner is determined using the three of a kind with the highest card value.
        /// </summary>
        void ThreeOfAKind(IPlayer currentPlayer, int name, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds);

        /// <summary>
        /// A hand containing five cards of consecutive value regardless of each card's suit, 
        /// such as 7h-8d-9s-10c-Jh; the hand ranking immediately below a flush and immediately 
        /// above three of a kind. When multiple players each have a straight, the winner is 
        /// determined by the value of the highest card held as part of the straight.
        /// </summary>
        void Straight(IPlayer currentPlayer, int name, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds);

        /// <summary>
        /// A hand containing five cards of the same suit, diamonds, hearts, spades, or clubs; 
        /// the hand ranking immediately below a full house and immediately above a straight. 
        /// When multiple players each have a flush, the winner is determined by the value of 
        /// the highest card held as part of the flush.
        /// </summary>
        void Flush(IPlayer currentPlayer, int name, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds);

        /// <summary>
        /// A hand containing both a three of a kind and a pair, such as Qs-Qh-Qd-7s-7d; 
        /// the hand ranking immediately below four of a kind and immediately above a flush. 
        /// When multiple players have a full house, the winner the three of a kind with the highest card value.
        /// </summary>
        void FullHouse(IPlayer currentPlayer, int name, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds);

        /// <summary>
        /// A hand containing four cards of the same value, such as 9s-9c-9h-9d-x; 
        /// the hand ranking immediately below a straight flush and immediately above a full house; quads; quadruplets.
        /// </summary>
        void FourOfAKind(IPlayer currentPlayer, int name, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds);

        /// <summary>
        /// A hand containing five cards of the same suit and of consecutive value, 
        /// such as 7h-8h-9h-10h-Jh; the hand ranking immediately above four of a kind, 
        /// and outranking all natural hands When playing with wild cards, a straight flush 
        /// ranks immediately below five of a kind. When multiple players each have a straight flush, 
        /// the winner is determined by the value of the highest card held.
        /// </summary>
        void StraightFlush(IPlayer currentPlayer, int name, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds);
    }
}
