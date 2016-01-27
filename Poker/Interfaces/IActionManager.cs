namespace Poker.Interfaces
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// The interface is responsible to provide all possible poker game moves
    /// a player can do.
    /// Poker methods description are written in accordance with:
    /// http://learn.pokernews.com/
    /// </summary>
    public interface IActionManager
    {
        /// <summary>
        /// The poker table which is affected by the methods used in the current Action Manager class.
        /// </summary>
        IGameTable GameTable { get; set; }

        /// <summary>
        /// When checking, a player declines to make a bet.
        /// </summary>
        /// <param name="currentPlayer">The player who Checks.</param>
        void Check(IPlayer currentPlayer, ref bool isRaisingActivated);

        /// <summary>
        /// Fold is to discard one's hand and forfeit interest in the current pot. 
        /// No further bets are required by the folding player, but the player but the Folded player cannot win.
        /// </summary>
        /// <param name="currentPlayer">The player who Folded.</param>
        void Fold(IPlayer currentPlayer, ref bool isRaisingActivated);

        /// <summary>
        /// Calling is the mechanism used to call a bet. This is essentially matching the amount
        /// that has been put in by another player in the form of a bet or a raise. 
        /// If nobody calls, the hand is over and the uncalled player wins the hand.
        /// </summary>
        /// <param name="currentPlayer">The player who Calls.</param>
        void Call(IPlayer currentPlayer, ref bool isRaisingActivated, int globalCall, ref TextBox potTexBox);

        /// <summary>
        /// Raising is the action one takes when they want to increase the opening bet. 
        /// After raising it up, one will have to deal with either a call, fold or re-raise 
        /// from the other players in the hand. Raising is associated with either 
        /// having a strong hand or trying to win the pot on the spot with a well timed bluff.
        /// </summary>
        /// <param name="currentPlayer">The player who Rises.</param>
        void Raised(IPlayer currentPlayer, ref bool isRaisingActivated, ref int globalRaise, ref int globalCall, ref TextBox potTextBox);

        void HP(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, int numberOne, int numberTwo);

        void PH(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, int globalRounds, int n, int n1, int r);

        /// <summary>
        /// The method is used to provide utility to the non-human controlled players.
        /// </summary>
        void AI(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds, int name);
    }
}
