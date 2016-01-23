namespace Poker.Core
{
    using System;
    using System.Linq;
    using Poker.Interfaces;
    using System.Collections.Generic;

    public static class CardCombinations
    {
        public static void rPairFromHand(IPlayer currentPlayer, int index, List<PokerType> winList, int[] reserveArray, ref PokerType sorted)
        {
            if (currentPlayer.Type >= -1)
            {
                bool msgbox = false;
                if (reserveArray[index] / 4 == reserveArray[index + 1] / 4)
                {
                    if (!msgbox)
                    {
                        if (reserveArray[index] / 4 == 0)
                        {
                            currentPlayer.Type = 1;
                            currentPlayer.Power = 13 * 4 + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 1 });
                            sorted = winList.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            currentPlayer.Type = 1;
                            currentPlayer.Power = (reserveArray[index + 1] / 4) * 4 + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 1 });
                            sorted = winList.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
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
                                currentPlayer.Type = 1;
                                currentPlayer.Power = 13 * 4 + reserveArray[index] / 4 + currentPlayer.Type * 100;
                                winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 1 });
                                sorted = winList.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                currentPlayer.Type = 1;
                                currentPlayer.Power = (reserveArray[index + 1] / 4) * 4 + reserveArray[index] / 4 + currentPlayer.Type * 100;
                                winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 1 });
                                sorted = winList.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                        }
                        msgbox = true;
                    }
                    if (reserveArray[index] / 4 == reserveArray[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (reserveArray[index] / 4 == 0)
                            {
                                currentPlayer.Type = 1;
                                currentPlayer.Power = 13 * 4 + reserveArray[index + 1] / 4 + currentPlayer.Type * 100;
                                winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 1 });
                                sorted = winList.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                currentPlayer.Type = 1;
                                currentPlayer.Power = (reserveArray[tc] / 4) * 4 + reserveArray[index + 1] / 4 + currentPlayer.Type * 100;
                                winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 1 });
                                sorted = winList.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                        }
                        msgbox = true;
                    }
                }
            }
        }

        public static void rPairTwoPair(IPlayer currentPlayer, int index, List<PokerType> winList, int[] reserveArray, ref PokerType sorted)
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
                                        if (reserveArray[index + 1] / 4 == 0)
                                        {
                                            currentPlayer.Type = 2;
                                            currentPlayer.Power = (reserveArray[index] / 4) * 2 + 13 * 4 + currentPlayer.Type * 100;
                                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 2 });
                                            sorted = winList.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (reserveArray[index] / 4 == 0)
                                        {
                                            currentPlayer.Type = 2;
                                            currentPlayer.Power = (reserveArray[index + 1] / 4) * 2 + 13 * 4 + currentPlayer.Type * 100;
                                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 2 });
                                            sorted = winList.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (reserveArray[index + 1] / 4 != 0)
                                        {
                                            currentPlayer.Type = 2;
                                            currentPlayer.Power = (reserveArray[tc] / 4) * 2 + (reserveArray[index + 1] / 4) * 2 + currentPlayer.Type * 100;
                                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 2 });
                                            sorted = winList.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (reserveArray[index] / 4 != 0)
                                        {
                                            currentPlayer.Type = 2;
                                            currentPlayer.Power = (reserveArray[tc] / 4) * 2 + (reserveArray[index] / 4) * 2 + currentPlayer.Type * 100;
                                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 2 });
                                            sorted = winList.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                    }
                                    msgbox = true;
                                }
                                if (currentPlayer.Type == -1)
                                {
                                    if (!msgbox1)
                                    {
                                        if (reserveArray[index] / 4 > reserveArray[index + 1] / 4)
                                        {
                                            if (reserveArray[tc] / 4 == 0)
                                            {
                                                currentPlayer.Type = 0;
                                                currentPlayer.Power = 13 + reserveArray[index] / 4 + currentPlayer.Type * 100;
                                                winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 1 });
                                                sorted = winList.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                            else
                                            {
                                                currentPlayer.Type = 0;
                                                currentPlayer.Power = reserveArray[tc] / 4 + reserveArray[index] / 4 + currentPlayer.Type * 100;
                                                winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 1 });
                                                sorted = winList.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                        }
                                        else
                                        {
                                            if (reserveArray[tc] / 4 == 0)
                                            {
                                                currentPlayer.Type = 0;
                                                currentPlayer.Power = 13 + reserveArray[index + 1] + currentPlayer.Type * 100;
                                                winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 1 });
                                                sorted = winList.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                            else
                                            {
                                                currentPlayer.Type = 0;
                                                currentPlayer.Power = reserveArray[tc] / 4 + reserveArray[index + 1] / 4 + currentPlayer.Type * 100;
                                                winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 1 });
                                                sorted = winList.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                        }
                                    }
                                    msgbox1 = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void rTwoPair(IPlayer currentPlayer, int index, List<PokerType> winList, int[] reserveArray, ref PokerType sorted)
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
                                        if (reserveArray[index] / 4 == 0)
                                        {
                                            currentPlayer.Type = 2;
                                            currentPlayer.Power = 13 * 4 + (reserveArray[index + 1] / 4) * 2 + currentPlayer.Type * 100;
                                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 2 });
                                            sorted = winList.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (reserveArray[index + 1] / 4 == 0)
                                        {
                                            currentPlayer.Type = 2;
                                            currentPlayer.Power = 13 * 4 + (reserveArray[index] / 4) * 2 + currentPlayer.Type * 100;
                                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 2 });
                                            sorted = winList.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (reserveArray[index + 1] / 4 != 0 && reserveArray[index] / 4 != 0)
                                        {
                                            currentPlayer.Type = 2;
                                            currentPlayer.Power = (reserveArray[index] / 4) * 2 + (reserveArray[index + 1] / 4) * 2 + currentPlayer.Type * 100;
                                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 2 });
                                            sorted = winList.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
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
    }
}
