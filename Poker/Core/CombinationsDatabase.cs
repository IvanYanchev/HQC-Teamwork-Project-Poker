namespace Poker.Core
{
    using System;
    using System.Linq;
    using Poker.Interfaces;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    /// <summary>
    /// Utility class for containing game logic for calculating cards power of each
    /// </summary>
    public class CombinationsDatabase : ICombinationDatabase
    {
        public CombinationsDatabase(IActionManager actionManager)
        {
            this.ActionManager = actionManager;
        }

        public IActionManager ActionManager { get; private set; }

        public void rPairFromHand(IPlayer currentPlayer, int index, List<PokerType> winList, int[] reserveArray, ref PokerType sorted)
        {
            if (currentPlayer.Type >= -1)
            {
                bool msgbox = false;
                double playerType = 1;
                double multiplyer = 0;
                if (reserveArray[index] / 4 == reserveArray[index + 1] / 4)
                {
                    if (!msgbox)
                    {
                        if (reserveArray[index] / 4 == 0)
                        {
                            multiplyer = 13 * 4 + currentPlayer.Type * 100;
                        }
                        else
                        {
                            multiplyer = (reserveArray[index + 1] / 4) * 4;
                        }

                        this.ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                    }
                    msgbox = true;
                }
                for (int tc = 16; tc >= 12; tc--)
                {
                    if (reserveArray[index + 1] / 4 == reserveArray[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (reserveArray[index + 1] / 4 == 0)
                            {
                                multiplyer = 13 * 4 + reserveArray[index] / 4;
                            }
                            else
                            {
                                multiplyer = (reserveArray[index + 1] / 4) * 4 + reserveArray[index] / 4;
                            }

                            this.ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                        }
                        msgbox = true;
                    }
                    if (reserveArray[index] / 4 == reserveArray[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (reserveArray[index] / 4 == 0)
                            {
                                multiplyer = 13 * 4 + reserveArray[index + 1] / 4;
                            }
                            else
                            {
                                multiplyer = (reserveArray[tc] / 4) * 4 + reserveArray[index + 1] / 4;
                            }

                            this.ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                        }

                        msgbox = true;
                    }
                }
            }
        }

        /// <summary>
        /// Calculating the power of pair of cards in the current player hand
        /// </summary>
        /// <param name="currentPlayer"></param>
        /// <param name="index"></param>
        /// <param name="winList"></param>
        /// <param name="reserveArray"></param>
        /// <param name="sorted"></param>
        public void rPairTwoPair(IPlayer currentPlayer, int index, List<PokerType> winList, int[] reserveArray, ref PokerType sorted)
        {
            if (currentPlayer.Type >= -1)
            {
                bool msgbox = false;
                bool msgbox1 = false;
                for (int tc = 16; tc >= 12; tc--)
                {
                    int max = tc - 12;
                    for (int k = 1; k <= max; k++)
                    {
                        if (tc - k < 12)
                        {
                            max--;
                        }
                        if (tc - k >= 12)
                        {
                            if (reserveArray[tc] / 4 == reserveArray[tc - k] / 4)
                            {
                                if (reserveArray[tc] / 4 != reserveArray[index] / 4 && reserveArray[tc] / 4 != reserveArray[index + 1] / 4 && currentPlayer.Type == 1)
                                {
                                    if (!msgbox)
                                    {
                                        double playerType = 2;
                                        double multiplyer = 0;
                                        if (reserveArray[index + 1] / 4 == 0)
                                        {
                                            multiplyer = (reserveArray[index] / 4) * 2 + 13 * 4;
                                            this.ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                                        }

                                        if (reserveArray[index] / 4 == 0)
                                        {
                                            multiplyer = (reserveArray[index + 1] / 4) * 2 + 13 * 4;
                                            this.ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                                        }

                                        if (reserveArray[index + 1] / 4 != 0)
                                        {
                                            multiplyer = (reserveArray[tc] / 4) * 2 + (reserveArray[index + 1] / 4) * 2;
                                            this.ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                                        }

                                        if (reserveArray[index] / 4 != 0)
                                        {
                                            multiplyer = (reserveArray[tc] / 4) * 2 + (reserveArray[index] / 4) * 2;
                                            this.ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                                        }
                                    }

                                    msgbox = true;
                                }

                                if (currentPlayer.Type == -1)
                                {
                                    if (!msgbox1)
                                    {
                                        double playerType = 0;
                                        double multiplyer = 0;
                                        if (reserveArray[index] / 4 > reserveArray[index + 1] / 4)
                                        {
                                            if (reserveArray[tc] / 4 == 0)
                                            {
                                                multiplyer = 13 + reserveArray[index] / 4;
                                            }

                                            else
                                            {
                                                multiplyer = reserveArray[tc] / 4 + reserveArray[index] / 4 + currentPlayer.Type * 100;
                                            }
                                        }
                                        else
                                        {
                                            if (reserveArray[tc] / 4 == 0)
                                            {
                                                multiplyer = 13 + reserveArray[index + 1];
                                            }
                                            else
                                            {
                                                multiplyer = reserveArray[tc] / 4 + reserveArray[index + 1] / 4 + currentPlayer.Type * 100;
                                            }
                                        }

                                        ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                                    }

                                    msgbox1 = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Calculating the power of one pair of cards in the hand and two pairs on the table
        /// </summary>
        /// <param name="currentPlayer"></param>
        /// <param name="index"></param>
        /// <param name="winList"></param>
        /// <param name="reserveArray"></param>
        /// <param name="sorted"></param>
        public void rTwoPair(IPlayer currentPlayer, int index, List<PokerType> winList, int[] reserveArray, ref PokerType sorted)
        {
            if (currentPlayer.Type >= -1)
            {
                bool msgbox = false;
                for (int tc = 16; tc >= 12; tc--)
                {
                    int max = tc - 12;
                    if (reserveArray[index] / 4 != reserveArray[index + 1] / 4)
                    {
                        for (int k = 1; k <= max; k++)
                        {
                            if (tc - k < 12)
                            {
                                max--;
                            }
                            if (tc - k >= 12)
                            {
                                if (reserveArray[index] / 4 == reserveArray[tc] / 4 && reserveArray[index + 1] / 4 == reserveArray[tc - k] / 4 ||
                                    reserveArray[index + 1] / 4 == reserveArray[tc] / 4 && reserveArray[index] / 4 == reserveArray[tc - k] / 4)
                                {
                                    if (!msgbox)
                                    {
                                        double playerType = 2;
                                        double multiplyer = 0;
                                        if (reserveArray[index] / 4 == 0)
                                        {
                                            multiplyer = 13 * 4 + (reserveArray[index + 1] / 4) * 2;
                                            this.ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);

                                        }

                                        if (reserveArray[index + 1] / 4 == 0)
                                        {
                                            multiplyer = 13 * 4 + (reserveArray[index] / 4) * 2;
                                            this.ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                                        }

                                        if (reserveArray[index + 1] / 4 != 0 && reserveArray[index] / 4 != 0)
                                        {
                                            multiplyer = (reserveArray[index] / 4) * 2 + (reserveArray[index + 1] / 4) * 2;
                                            this.ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                                        }
                                    }

                                    msgbox = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Calculating the power of two pairs of cards
        /// </summary>
        /// <param name="currentPlayer"></param>
        /// <param name="index"></param>
        /// <param name="winList"></param>
        /// <param name="reserveArray"></param>
        /// <param name="sorted"></param>
        public void rHighCard(IPlayer currentPlayer, int index, List<PokerType> winList, int[] reserveArray, ref PokerType sorted)
        {
            if (currentPlayer.Type == -1)
            {
                currentPlayer.Power = -1;
                if (reserveArray[index] / 4 > reserveArray[index + 1] / 4)
                {
                    currentPlayer.Power = reserveArray[index] / 4;
                }

                else
                {
                    currentPlayer.Power = reserveArray[index + 1] / 4;
                }

                winList.Add(new PokerType() { Power = currentPlayer.Power, Current = -1 });
                sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();

                if (reserveArray[index] / 4 == 0 || reserveArray[index + 1] / 4 == 0)
                {
                    currentPlayer.Power = 13;
                    winList.Add(new PokerType() { Power = currentPlayer.Power, Current = -1 });
                    sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
            }
        }

        /// <summary>
        /// Calculating the power of a current card when there are no pairs or higher priority card combinations
        /// </summary>
        /// <param name="currentPlayer"></param>
        /// <param name="index"></param>
        /// <param name="winList"></param>
        /// <param name="reserveArray"></param>
        /// <param name="sorted"></param>
        public void rThreeOfAKind(IPlayer currentPlayer, List<PokerType> winList, int[] reserveArray, ref PokerType sorted, int[] Straight)
        {
            if (currentPlayer.Type >= -1)
            {
                for (int j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3)
                    {
                        double playerType = 3;
                        double multiplyer = 0;
                        if (fh.Max() / 4 == 0)
                        {
                            multiplyer = 13 * 3;
                        }
                        else
                        {
                            multiplyer = fh[0] / 4 + fh[1] / 4 + fh[2] / 4;
                        }

                        this.ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                    }
                }
            }
        }

        /// <summary>
        /// Calculating the power of three cards from the same kind. Stronger than any type of pair.
        /// </summary>
        /// <param name="currentPlayer"></param>
        /// <param name="winList"></param>
        /// <param name="reserveArray"></param>
        /// <param name="sorted"></param>
        /// <param name="Straight"></param>
        public void rStraight(IPlayer currentPlayer, List<PokerType> winList, int[] reserveArray, ref PokerType sorted, int[] Straight)
        {
            if (currentPlayer.Type >= -1)
            {
                var op = Straight.Select(o => o / 4).Distinct().ToArray();
                double playerType = 4;
                for (int j = 0; j < op.Length - 4; j++)
                {
                    if (op[j] + 4 == op[j + 4])
                    {
                        double multiplyer = 0;
                        if (op.Max() - 4 == op[j])
                        {
                            multiplyer = op.Max();
                        }

                        else
                        {
                            multiplyer = op[j + 4];
                        }

                        this.ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                    }

                    if (op[j] == 0 && op[j + 1] == 9 && op[j + 2] == 10 && op[j + 3] == 11 && op[j + 4] == 12)
                    {
                        double multiplyer = 13;
                        this.ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                    }
                }
            }
        }

        /// <summary>
        /// Calculates the power of Five cards of mixed suits in sequence. Stronger than any type of three cards.
        /// </summary>
        /// <param name="currentPlayer"></param>
        /// <param name="winList"></param>
        /// <param name="reserveArray"></param>
        /// <param name="sorted"></param>
        /// <param name="Straight"></param>
        public void rFlush(IPlayer currentPlayer, int index, ref bool vf, List<PokerType> winList, int[] reserveArray, ref PokerType sorted, int[] Straight)
        {
            if (currentPlayer.Type >= -1)
            {
                var clubs = Straight.Where(o => o % 4 == 0).ToArray();
                var diamonds = Straight.Where(o => o % 4 == 1).ToArray();
                var hearths = Straight.Where(o => o % 4 == 2).ToArray();
                var spades = Straight.Where(o => o % 4 == 3).ToArray();

                var flushTypes = new List<int[]>
                                        {
                                            clubs,
                                            diamonds,
                                            hearths,
                                            spades
                                        };
                foreach (var currentFlushType in flushTypes)
                {
                    if (currentFlushType.Length == 3 || currentFlushType.Length == 4)
                    {
                        double playerType = 5;
                        double multiplyer = 0;
                        if (reserveArray[index] % 4 == reserveArray[index + 1] % 4 && reserveArray[index] % 4 == currentFlushType[0] % 4)
                        {
                            if (reserveArray[index] / 4 > currentFlushType.Max() / 4)
                            {
                                multiplyer = reserveArray[index];
                                this.ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                                vf = true;
                            }

                            if (reserveArray[index + 1] / 4 > currentFlushType.Max() / 4)
                            {
                                multiplyer = reserveArray[index + 1];
                                this.ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                                vf = true;
                            }
                            else if (reserveArray[index] / 4 < currentFlushType.Max() / 4 && reserveArray[index + 1] / 4 < currentFlushType.Max() / 4)
                            {
                                multiplyer = currentFlushType.Max();
                                this.ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                                vf = true;
                            }
                        }
                    }

                    if (currentFlushType.Length == 4)//different cards in hand
                    {
                        double playerType = 5;
                        if (reserveArray[index] % 4 != reserveArray[index + 1] % 4 && reserveArray[index] % 4 == currentFlushType[0] % 4)
                        {
                            double multiplyer = 0;
                            if (reserveArray[index] / 4 > currentFlushType.Max() / 4)
                            {
                                multiplyer = reserveArray[index];
                            }
                            else
                            {
                                multiplyer = currentFlushType.Max();
                            }

                            ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                            vf = true;
                        }

                        if (reserveArray[index + 1] % 4 != reserveArray[index] % 4 && reserveArray[index + 1] % 4 == currentFlushType[0] % 4)
                        {
                            double multiplyer = 0;
                            if (reserveArray[index + 1] / 4 > currentFlushType.Max() / 4)
                            {
                                multiplyer = reserveArray[index + 1];
                            }

                            else
                            {
                                multiplyer = currentFlushType.Max();
                            }

                            ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                            vf = true;
                        }
                    }

                    if (currentFlushType.Length == 5)
                    {
                        double playerType = 5;
                        double multiplyer = 0;
                        if (reserveArray[index] % 4 == currentFlushType[0] % 4 && reserveArray[index] / 4 > currentFlushType.Min() / 4)
                        {
                            multiplyer = reserveArray[index];
                            ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                            vf = true;
                        }

                        if (reserveArray[index + 1] % 4 == currentFlushType[0] % 4 && reserveArray[index + 1] / 4 > currentFlushType.Min() / 4)
                        {
                            multiplyer = reserveArray[index + 1];
                            ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                            vf = true;
                        }
                        else if (reserveArray[index] / 4 < currentFlushType.Min() / 4 && reserveArray[index + 1] / 4 < currentFlushType.Min())
                        {
                            multiplyer = currentFlushType.Max();
                            ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                            vf = true;
                        }
                    }

                    //ace
                    if (currentFlushType.Length > 0)
                    {
                        double playerType = 5.5;
                        double multiplyer = 13;
                        if (reserveArray[index] / 4 == 0 && reserveArray[index] % 4 == currentFlushType[0] % 4 && vf && currentFlushType.Length > 0)
                        {
                            ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                        }
                        if (reserveArray[index + 1] / 4 == 0 && reserveArray[index + 1] % 4 == currentFlushType[0] % 4 && vf
                            && clubs.Length > 0)
                        {
                            ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// <summary>
        /// Calculate the power of four cards from the same kind. Stronger than any type of full house.
        /// </summary>
        /// <param name="currentPlayer"></param>
        /// <param name="winList"></param>
        /// <param name="reserveArray"></param>
        /// <param name="sorted"></param>
        /// <param name="Straight"></param>
        public void rFourOfAKind(IPlayer currentPlayer, List<PokerType> winList, int[] reserveArray, ref PokerType sorted, int[] Straight)
        {
            if (currentPlayer.Type >= -1)
            {
                double playerType = 7;
                for (int j = 0; j <= 3; j++)
                {
                    double multiplyer = 0;
                    if (Straight[j] / 4 == Straight[j + 1] / 4 && Straight[j] / 4 == Straight[j + 2] / 4 &&
                        Straight[j] / 4 == Straight[j + 3] / 4)
                    {
                        multiplyer = (Straight[j] / 4) * 4;
                        this.ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                    }

                    if (Straight[j] / 4 == 0 && Straight[j + 1] / 4 == 0 && Straight[j + 2] / 4 == 0 && Straight[j + 3] / 4 == 0)
                    {
                        multiplyer = 13 * 4;
                        this.ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                    }
                }
            }
        }

        /// <summary>
        /// Calculate the power of one pair and three cards of the same kind. Stronger than any flush.
        /// </summary>
        /// <param name="currentPlayer"></param>
        /// <param name="type"></param>
        /// <param name="done"></param>
        /// <param name="winList"></param>
        /// <param name="reserveArray"></param>
        /// <param name="sorted"></param>
        /// <param name="Straight"></param>
        public void rFullHouse(IPlayer currentPlayer, ref double type, ref bool done, List<PokerType> winList, int[] reserveArray, ref PokerType sorted, int[] Straight)
        {
            if (currentPlayer.Type >= -1)
            {
                double playerType = 6;
                type = currentPlayer.Power;
                for (int j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3 || done)
                    {
                        if (fh.Length == 2)
                        {
                            double multiplyer = 0;
                            if (fh.Max() / 4 == 0)
                            {
                                multiplyer = 13 * 2;
                                this.ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                                break;
                            }
                            if (fh.Max() / 4 > 0)
                            {
                                multiplyer = fh.Max() / 4 * 2;
                                this.ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                                break;
                            }
                        }
                        if (!done)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                currentPlayer.Power = 13;
                                done = true;
                                j = -1;
                            }
                            else
                            {
                                currentPlayer.Power = fh.Max() / 4;
                                done = true;
                                j = -1;
                            }
                        }
                    }
                }

                if (currentPlayer.Type != 6)
                {
                    currentPlayer.Power = type;
                }
            }
        }

        /// <summary>
        /// Calculates the power of five cards from same suit in a sequance. The strongest combination in the game.
        /// </summary>
        /// <param name="currentPlayer"></param>
        /// <param name="winList"></param>
        /// <param name="reserveArray"></param>
        /// <param name="sorted"></param>
        /// <param name="clubs"></param>
        /// <param name="diamonds"></param>
        /// <param name="hearts"></param>
        /// <param name="spades"></param>
        public void rStraightFlush(IPlayer currentPlayer, List<PokerType> winList, int[] reserveArray, ref PokerType sorted, int[] clubs, int[] diamonds, int[] hearts, int[] spades)
        {
            var straightTypes = new List<int[]>() { clubs, diamonds, hearts, spades };
            if (currentPlayer.Type >= -1)
            {
                foreach (var currentStraightType in straightTypes)
                {
                    if (currentStraightType.Length >= 5)
                    {
                        double multiplyer = currentStraightType.Max() / 4;
                        if (currentStraightType[0] + 4 == currentStraightType[4])
                        {
                            double playerType = 8;
                            this.ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                        }

                        if (currentStraightType[0] == 0 && currentStraightType[1] == 9 &&
                            currentStraightType[2] == 10 && currentStraightType[3] == 11 &&
                            currentStraightType[0] + 12 == currentStraightType[4])
                        {
                            double playerType = 9;
                            this.ApplyingCombination(currentPlayer, winList, playerType, multiplyer, ref sorted);
                        }
                    }
                }

            }
        }

        public void HighCard(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated)
        {
            this.ActionManager.HP(currentPlayer, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated, 20, 25);
        }

        public void PairTable(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated)
        {
            this.ActionManager.HP(currentPlayer, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated, 16, 25);
        }

        public void PairHand(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds)
        {
            int rCall = RandomGenerator.Next(10, 16);
            int rRaise = RandomGenerator.Next(10, 13);
            if (currentPlayer.Power <= 199 && currentPlayer.Power >= 140)
            {
                this.ActionManager.PH(currentPlayer, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated, globalRounds, rCall, 6, rRaise);
            }

            if (currentPlayer.Power <= 139 && currentPlayer.Power >= 128)
            {
                this.ActionManager.PH(currentPlayer, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated, globalRounds, rCall, 7, rRaise);
            }

            if (currentPlayer.Power < 128 && currentPlayer.Power >= 101)
            {
                this.ActionManager.PH(currentPlayer, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated, globalRounds, rCall, 9, rRaise);
            }
        }

        public void TwoPair(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds)
        {
            int rCall = RandomGenerator.Next(6, 11);
            int rRaise = RandomGenerator.Next(6, 11);
            if (currentPlayer.Power <= 290 && currentPlayer.Power >= 246)
            {
                this.ActionManager.PH(currentPlayer, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated, globalRounds, rCall, 3, rRaise);
            }

            if (currentPlayer.Power <= 244 && currentPlayer.Power >= 234)
            {
                this.ActionManager.PH(currentPlayer, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated, globalRounds, rCall, 4, rRaise);
            }

            if (currentPlayer.Power < 234 && currentPlayer.Power >= 201)
            {
                this.ActionManager.PH(currentPlayer, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated, globalRounds, rCall, 4, rRaise);
            }
        }

        public void ThreeOfAKind(IPlayer currentPlayer, int name, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds)
        {
            int tCall = RandomGenerator.Next(3, 7);
            int tRaise = RandomGenerator.Next(4, 8);
            if (currentPlayer.Power <= 390 && currentPlayer.Power >= 330)
            {
                this.Smooth(currentPlayer, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated, ref globalRounds, name, tCall, tRaise);
            }

            if (currentPlayer.Power <= 327 && currentPlayer.Power >= 321)
            {
                this.Smooth(currentPlayer, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated, ref globalRounds, name, tCall, tRaise);
            }

            if (currentPlayer.Power < 321 && currentPlayer.Power >= 303)
            {
                Smooth(currentPlayer, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated, ref globalRounds, name, tCall, tRaise);
            }
        }

        private void Smooth(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds, int name, int n, int r)
        {
            int random = RandomGenerator.Next(1, 3);
            if (globalCall <= 0)
            {
                this.ActionManager.Check(currentPlayer, ref isRaisingActivated);
            }
            else
            {
                if (globalCall >= RoundN(currentPlayer.Chips, n))
                {
                    if (currentPlayer.Chips > globalCall)
                    {
                        this.ActionManager.Call(currentPlayer, ref isRaisingActivated, globalCall, ref potTextBox);
                    }
                    else if (currentPlayer.Chips <= globalCall)
                    {
                        isRaisingActivated = false;
                        currentPlayer.CanPlay = false;
                        currentPlayer.Chips = 0;
                        currentPlayer.Status.Text = "Call " + currentPlayer.Chips;
                        potTextBox.Text = (int.Parse(potTextBox.Text) + currentPlayer.Chips).ToString();
                    }
                }
                else
                {
                    if (globalRaise > 0)
                    {
                        if (currentPlayer.Chips >= globalRaise * 2)
                        {
                            globalRaise *= 2;
                            this.ActionManager.Raised(currentPlayer, ref isRaisingActivated, ref globalRaise, ref globalCall, ref potTextBox);
                        }
                        else
                        {
                            this.ActionManager.Call(currentPlayer, ref isRaisingActivated, globalCall, ref potTextBox);
                        }
                    }
                    else
                    {
                        globalRaise = globalCall * 2;
                        this.ActionManager.Raised(currentPlayer, ref isRaisingActivated, ref globalRaise, ref globalCall, ref potTextBox);
                    }
                }
            }

            if (currentPlayer.Chips <= 0)
            {
                currentPlayer.OutOfChips = true;
            }
        }

        public void Straight(IPlayer currentPlayer, int name, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds)
        {
            int straightCall = RandomGenerator.Next(3, 6);
            int straightRaise = RandomGenerator.Next(3, 8);
            if (currentPlayer.Power <= 480 && currentPlayer.Power >= 410)
            {
                this.Smooth(currentPlayer, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated, ref globalRounds, name, straightCall, straightRaise);
            }

            if (currentPlayer.Power <= 409 && currentPlayer.Power >= 407)//10  8
            {
                this.Smooth(currentPlayer, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated, ref globalRounds, name, straightCall, straightRaise);
            }

            if (currentPlayer.Power < 407 && currentPlayer.Power >= 404)
            {
                this.Smooth(currentPlayer, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated, ref globalRounds, name, straightCall, straightRaise);
            }
        }

        public void Flush(IPlayer currentPlayer, int name, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds)
        {
            int flushCall = RandomGenerator.Next(2, 6);
            int flushRaise = RandomGenerator.Next(3, 7);
            this.Smooth(currentPlayer, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated, ref globalRounds, name, flushCall, flushRaise);
        }

        public void FullHouse(IPlayer currentPlayer, int name, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds)
        {
            int fullHouseCall = RandomGenerator.Next(1, 5);
            int fullHouseRaise = RandomGenerator.Next(2, 6);
            if (currentPlayer.Power <= 626 && currentPlayer.Power >= 620)
            {
                this.Smooth(currentPlayer, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated, ref globalRounds, name, fullHouseCall, fullHouseRaise);
            }

            if (currentPlayer.Power < 620 && currentPlayer.Power >= 602)
            {
                this.Smooth(currentPlayer, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated, ref globalRounds, name, fullHouseCall, fullHouseRaise);
            }
        }

        public void FourOfAKind(IPlayer currentPlayer, int name, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds)
        {
            int fourOfAKindCall = RandomGenerator.Next(1, 4);
            int fourOfAKindRaise = RandomGenerator.Next(2, 5);
            if (currentPlayer.Power <= 752 && currentPlayer.Power >= 704)
            {
                this.Smooth(currentPlayer, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated, ref globalRounds, name, fourOfAKindCall, fourOfAKindRaise);
            }
        }

        public void StraightFlush(IPlayer currentPlayer, int name, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds)
        {
            int straightFlushCall = RandomGenerator.Next(1, 3);
            int straightFlushRaise = RandomGenerator.Next(1, 3);
            if (currentPlayer.Power <= 913 && currentPlayer.Power >= 804)
            {
                this.Smooth(currentPlayer, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated, ref globalRounds, name, straightFlushCall, straightFlushRaise);
            }
        }

        private double RoundN(int playerChips, int n)
        {
            double result = Math.Round((playerChips / n) / 100d, 0) * 100;
            return result;
        }

        private void ApplyingCombination(IPlayer currentPlayer, List<PokerType> winList, double type, double multiplyer, ref PokerType sorted)
        {
            currentPlayer.Type = type;
            currentPlayer.Power = multiplyer + currentPlayer.Type * 100;
            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = type });
            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        }
    }
}

