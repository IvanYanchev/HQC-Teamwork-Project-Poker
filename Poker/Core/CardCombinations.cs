namespace Poker.Core
{
    using System;
    using System.Linq;
    using Poker.Interfaces;
    using System.Collections.Generic;

    public static class CardCombinations
    {
        private void rStraightFlush(ref double current, ref double Power, int[] st1, int[] st2, int[] st3, int[] st4)
        {
            if (current >= -1)
            {
                if (st1.Length >= 5)
                {
                    if (st1[0] + 4 == st1[4])
                    {
                        current = 8;
                        Power = (st1.Max()) / 4 + current * 100;
                        winList.Add(new Type() { Power = Power, Current = 8 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st1[0] == 0 && st1[1] == 9 && st1[2] == 10 && st1[3] == 11 && st1[0] + 12 == st1[4])
                    {
                        current = 9;
                        Power = (st1.Max()) / 4 + current * 100;
                        winList.Add(new Type() { Power = Power, Current = 9 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (st2.Length >= 5)
                {
                    if (st2[0] + 4 == st2[4])
                    {
                        current = 8;
                        Power = (st2.Max()) / 4 + current * 100;
                        winList.Add(new Type() { Power = Power, Current = 8 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st2[0] == 0 && st2[1] == 9 && st2[2] == 10 && st2[3] == 11 && st2[0] + 12 == st2[4])
                    {
                        current = 9;
                        Power = (st2.Max()) / 4 + current * 100;
                        winList.Add(new Type() { Power = Power, Current = 9 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (st3.Length >= 5)
                {
                    if (st3[0] + 4 == st3[4])
                    {
                        current = 8;
                        Power = (st3.Max()) / 4 + current * 100;
                        winList.Add(new Type() { Power = Power, Current = 8 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st3[0] == 0 && st3[1] == 9 && st3[2] == 10 && st3[3] == 11 && st3[0] + 12 == st3[4])
                    {
                        current = 9;
                        Power = (st3.Max()) / 4 + current * 100;
                        winList.Add(new Type() { Power = Power, Current = 9 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (st4.Length >= 5)
                {
                    if (st4[0] + 4 == st4[4])
                    {
                        current = 8;
                        Power = (st4.Max()) / 4 + current * 100;
                        winList.Add(new Type() { Power = Power, Current = 8 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st4[0] == 0 && st4[1] == 9 && st4[2] == 10 && st4[3] == 11 && st4[0] + 12 == st4[4])
                    {
                        current = 9;
                        Power = (st4.Max()) / 4 + current * 100;
                        winList.Add(new Type() { Power = Power, Current = 9 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

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
    }
}
