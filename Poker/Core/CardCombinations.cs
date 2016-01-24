namespace Poker.Core
{
    using System;
    using System.Linq;
    using Poker.Interfaces;
    using System.Collections.Generic;
    using System.Windows.Forms;

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

        public static void rHighCard(IPlayer currentPlayer, int index, List<PokerType> winList, int[] reserveArray, ref PokerType sorted)
        {
            if (currentPlayer.Type == -1)
            {
                if (reserveArray[index] / 4 > reserveArray[index + 1] / 4)
                {
                    currentPlayer.Type = -1;
                    currentPlayer.Power = reserveArray[index] / 4;
                    winList.Add(new PokerType() { Power = currentPlayer.Power, Current = -1 });
                    sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
                else
                {
                    currentPlayer.Type = -1;
                    currentPlayer.Power = reserveArray[index + 1] / 4;
                    winList.Add(new PokerType() { Power = currentPlayer.Power, Current = -1 });
                    sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
                if (reserveArray[index] / 4 == 0 || reserveArray[index + 1] / 4 == 0)
                {
                    currentPlayer.Type = -1;
                    currentPlayer.Power = 13;
                    winList.Add(new PokerType() { Power = currentPlayer.Power, Current = -1 });
                    sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
            }
        }

        public static void rThreeOfAKind(IPlayer currentPlayer, List<PokerType> winList, int[] reserveArray, ref PokerType sorted, int[] Straight)
        {
            if (currentPlayer.Type >= -1)
            {
                for (int j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3)
                    {
                        if (fh.Max() / 4 == 0)
                        {
                            currentPlayer.Type = 3;
                            currentPlayer.Power = 13 * 3 + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 3 });
                            sorted = winList.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            currentPlayer.Type = 3;
                            currentPlayer.Power = fh[0] / 4 + fh[1] / 4 + fh[2] / 4 + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 3 });
                            sorted = winList.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }
                }
            }
        }

        public static void rStraight(IPlayer currentPlayer, List<PokerType> winList, int[] reserveArray, ref PokerType sorted, int[] Straight)
        {
            if (currentPlayer.Type >= -1)
            {
                var op = Straight.Select(o => o / 4).Distinct().ToArray();
                for (int j = 0; j < op.Length - 4; j++)
                {
                    if (op[j] + 4 == op[j + 4])
                    {
                        if (op.Max() - 4 == op[j])
                        {
                            currentPlayer.Type = 4;
                            currentPlayer.Power = op.Max() + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 4 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                        else
                        {
                            currentPlayer.Type = 4;
                            currentPlayer.Power = op[j + 4] + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 4 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                    }
                    if (op[j] == 0 && op[j + 1] == 9 && op[j + 2] == 10 && op[j + 3] == 11 && op[j + 4] == 12)
                    {
                        currentPlayer.Type = 4;
                        currentPlayer.Power = 13 + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 4 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        public static void rFlush(IPlayer currentPlayer, int index, ref bool vf, List<PokerType> winList, int[] reserveArray, ref PokerType sorted, int[] Straight)
        {
            if (currentPlayer.Type >= -1)
            {
                var f1 = Straight.Where(o => o % 4 == 0).ToArray();
                var f2 = Straight.Where(o => o % 4 == 1).ToArray();
                var f3 = Straight.Where(o => o % 4 == 2).ToArray();
                var f4 = Straight.Where(o => o % 4 == 3).ToArray();
                if (f1.Length == 3 || f1.Length == 4)
                {
                    if (reserveArray[index] % 4 == reserveArray[index + 1] % 4 && reserveArray[index] % 4 == f1[0] % 4)
                    {
                        if (reserveArray[index] / 4 > f1.Max() / 4)
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = reserveArray[index] + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (reserveArray[index + 1] / 4 > f1.Max() / 4)
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = reserveArray[index + 1] + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (reserveArray[index] / 4 < f1.Max() / 4 && reserveArray[index + 1] / 4 < f1.Max() / 4)
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = f1.Max() + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f1.Length == 4)//different cards in hand
                {
                    if (reserveArray[index] % 4 != reserveArray[index + 1] % 4 && reserveArray[index] % 4 == f1[0] % 4)
                    {
                        if (reserveArray[index] / 4 > f1.Max() / 4)
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = reserveArray[index] + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = f1.Max() + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (reserveArray[index + 1] % 4 != reserveArray[index] % 4 && reserveArray[index + 1] % 4 == f1[0] % 4)
                    {
                        if (reserveArray[index + 1] / 4 > f1.Max() / 4)
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = reserveArray[index + 1] + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = f1.Max() + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f1.Length == 5)
                {
                    if (reserveArray[index] % 4 == f1[0] % 4 && reserveArray[index] / 4 > f1.Min() / 4)
                    {
                        currentPlayer.Type = 5;
                        currentPlayer.Power = reserveArray[index] + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (reserveArray[index + 1] % 4 == f1[0] % 4 && reserveArray[index + 1] / 4 > f1.Min() / 4)
                    {
                        currentPlayer.Type = 5;
                        currentPlayer.Power = reserveArray[index + 1] + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (reserveArray[index] / 4 < f1.Min() / 4 && reserveArray[index + 1] / 4 < f1.Min())
                    {
                        currentPlayer.Type = 5;
                        currentPlayer.Power = f1.Max() + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f2.Length == 3 || f2.Length == 4)
                {
                    if (reserveArray[index] % 4 == reserveArray[index + 1] % 4 && reserveArray[index] % 4 == f2[0] % 4)
                    {
                        if (reserveArray[index] / 4 > f2.Max() / 4)
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = reserveArray[index] + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (reserveArray[index + 1] / 4 > f2.Max() / 4)
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = reserveArray[index + 1] + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (reserveArray[index] / 4 < f2.Max() / 4 && reserveArray[index + 1] / 4 < f2.Max() / 4)
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = f2.Max() + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f2.Length == 4)//different cards in hand
                {
                    if (reserveArray[index] % 4 != reserveArray[index + 1] % 4 && reserveArray[index] % 4 == f2[0] % 4)
                    {
                        if (reserveArray[index] / 4 > f2.Max() / 4)
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = reserveArray[index] + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = f2.Max() + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (reserveArray[index + 1] % 4 != reserveArray[index] % 4 && reserveArray[index + 1] % 4 == f2[0] % 4)
                    {
                        if (reserveArray[index + 1] / 4 > f2.Max() / 4)
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = reserveArray[index + 1] + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = f2.Max() + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f2.Length == 5)
                {
                    if (reserveArray[index] % 4 == f2[0] % 4 && reserveArray[index] / 4 > f2.Min() / 4)
                    {
                        currentPlayer.Type = 5;
                        currentPlayer.Power = reserveArray[index] + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (reserveArray[index + 1] % 4 == f2[0] % 4 && reserveArray[index + 1] / 4 > f2.Min() / 4)
                    {
                        currentPlayer.Type = 5;
                        currentPlayer.Power = reserveArray[index + 1] + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (reserveArray[index] / 4 < f2.Min() / 4 && reserveArray[index + 1] / 4 < f2.Min())
                    {
                        currentPlayer.Type = 5;
                        currentPlayer.Power = f2.Max() + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f3.Length == 3 || f3.Length == 4)
                {
                    if (reserveArray[index] % 4 == reserveArray[index + 1] % 4 && reserveArray[index] % 4 == f3[0] % 4)
                    {
                        if (reserveArray[index] / 4 > f3.Max() / 4)
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = reserveArray[index] + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (reserveArray[index + 1] / 4 > f3.Max() / 4)
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = reserveArray[index + 1] + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (reserveArray[index] / 4 < f3.Max() / 4 && reserveArray[index + 1] / 4 < f3.Max() / 4)
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = f3.Max() + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f3.Length == 4)//different cards in hand
                {
                    if (reserveArray[index] % 4 != reserveArray[index + 1] % 4 && reserveArray[index] % 4 == f3[0] % 4)
                    {
                        if (reserveArray[index] / 4 > f3.Max() / 4)
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = reserveArray[index] + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = f3.Max() + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (reserveArray[index + 1] % 4 != reserveArray[index] % 4 && reserveArray[index + 1] % 4 == f3[0] % 4)
                    {
                        if (reserveArray[index + 1] / 4 > f3.Max() / 4)
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = reserveArray[index + 1] + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = f3.Max() + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f3.Length == 5)
                {
                    if (reserveArray[index] % 4 == f3[0] % 4 && reserveArray[index] / 4 > f3.Min() / 4)
                    {
                        currentPlayer.Type = 5;
                        currentPlayer.Power = reserveArray[index] + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (reserveArray[index + 1] % 4 == f3[0] % 4 && reserveArray[index + 1] / 4 > f3.Min() / 4)
                    {
                        currentPlayer.Type = 5;
                        currentPlayer.Power = reserveArray[index + 1] + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (reserveArray[index] / 4 < f3.Min() / 4 && reserveArray[index + 1] / 4 < f3.Min())
                    {
                        currentPlayer.Type = 5;
                        currentPlayer.Power = f3.Max() + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f4.Length == 3 || f4.Length == 4)
                {
                    if (reserveArray[index] % 4 == reserveArray[index + 1] % 4 && reserveArray[index] % 4 == f4[0] % 4)
                    {
                        if (reserveArray[index] / 4 > f4.Max() / 4)
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = reserveArray[index] + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (reserveArray[index + 1] / 4 > f4.Max() / 4)
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = reserveArray[index + 1] + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (reserveArray[index] / 4 < f4.Max() / 4 && reserveArray[index + 1] / 4 < f4.Max() / 4)
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = f4.Max() + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f4.Length == 4)//different cards in hand
                {
                    if (reserveArray[index] % 4 != reserveArray[index + 1] % 4 && reserveArray[index] % 4 == f4[0] % 4)
                    {
                        if (reserveArray[index] / 4 > f4.Max() / 4)
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = reserveArray[index] + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = f4.Max() + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (reserveArray[index + 1] % 4 != reserveArray[index] % 4 && reserveArray[index + 1] % 4 == f4[0] % 4)
                    {
                        if (reserveArray[index + 1] / 4 > f4.Max() / 4)
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = reserveArray[index + 1] + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            currentPlayer.Type = 5;
                            currentPlayer.Power = f4.Max() + currentPlayer.Type * 100;
                            winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                            sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f4.Length == 5)
                {
                    if (reserveArray[index] % 4 == f4[0] % 4 && reserveArray[index] / 4 > f4.Min() / 4)
                    {
                        currentPlayer.Type = 5;
                        currentPlayer.Power = reserveArray[index] + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (reserveArray[index + 1] % 4 == f4[0] % 4 && reserveArray[index + 1] / 4 > f4.Min() / 4)
                    {
                        currentPlayer.Type = 5;
                        currentPlayer.Power = reserveArray[index + 1] + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (reserveArray[index] / 4 < f4.Min() / 4 && reserveArray[index + 1] / 4 < f4.Min())
                    {
                        currentPlayer.Type = 5;
                        currentPlayer.Power = f4.Max() + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }
                //ace
                if (f1.Length > 0)
                {
                    if (reserveArray[index] / 4 == 0 && reserveArray[index] % 4 == f1[0] % 4 && vf && f1.Length > 0)
                    {
                        currentPlayer.Type = 5.5;
                        currentPlayer.Power = 13 + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5.5 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (reserveArray[index + 1] / 4 == 0 && reserveArray[index + 1] % 4 == f1[0] % 4 && vf && f1.Length > 0)
                    {
                        currentPlayer.Type = 5.5;
                        currentPlayer.Power = 13 + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5.5 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f2.Length > 0)
                {
                    if (reserveArray[index] / 4 == 0 && reserveArray[index] % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        currentPlayer.Type = 5.5;
                        currentPlayer.Power = 13 + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5.5 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (reserveArray[index + 1] / 4 == 0 && reserveArray[index + 1] % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        currentPlayer.Type = 5.5;
                        currentPlayer.Power = 13 + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5.5 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f3.Length > 0)
                {
                    if (reserveArray[index] / 4 == 0 && reserveArray[index] % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        currentPlayer.Type = 5.5;
                        currentPlayer.Power = 13 + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5.5 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (reserveArray[index + 1] / 4 == 0 && reserveArray[index + 1] % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        currentPlayer.Type = 5.5;
                        currentPlayer.Power = 13 + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5.5 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f4.Length > 0)
                {
                    if (reserveArray[index] / 4 == 0 && reserveArray[index] % 4 == f4[0] % 4 && vf && f4.Length > 0)
                    {
                        currentPlayer.Type = 5.5;
                        currentPlayer.Power = 13 + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5.5 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (reserveArray[index + 1] / 4 == 0 && reserveArray[index + 1] % 4 == f4[0] % 4 && vf)
                    {
                        currentPlayer.Type = 5.5;
                        currentPlayer.Power = 13 + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 5.5 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        public static void rFourOfAKind(IPlayer currentPlayer, List<PokerType> winList, int[] reserveArray, ref PokerType sorted, int[] Straight)
        {
            if (currentPlayer.Type >= -1)
            {
                for (int j = 0; j <= 3; j++)
                {
                    if (Straight[j] / 4 == Straight[j + 1] / 4 && Straight[j] / 4 == Straight[j + 2] / 4 &&
                        Straight[j] / 4 == Straight[j + 3] / 4)
                    {
                        currentPlayer.Type = 7;
                        currentPlayer.Power = (Straight[j] / 4) * 4 + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 7 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (Straight[j] / 4 == 0 && Straight[j + 1] / 4 == 0 && Straight[j + 2] / 4 == 0 && Straight[j + 3] / 4 == 0)
                    {
                        currentPlayer.Type = 7;
                        currentPlayer.Power = 13 * 4 + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 7 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        public static void rFullHouse(IPlayer currentPlayer, ref double type, ref bool done, List<PokerType> winList, int[] reserveArray, ref PokerType sorted, int[] Straight)
        {
            if (currentPlayer.Type >= -1)
            {
                type = currentPlayer.Power;
                for (int j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3 || done)
                    {
                        if (fh.Length == 2)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                currentPlayer.Type = 6;
                                currentPlayer.Power = 13 * 2 + currentPlayer.Type * 100;
                                winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 6 });
                                sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                                break;
                            }
                            if (fh.Max() / 4 > 0)
                            {
                                currentPlayer.Type = 6;
                                currentPlayer.Power = fh.Max() / 4 * 2 + currentPlayer.Type * 100;
                                winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 6 });
                                sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
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

        public static void rStraightFlush(IPlayer currentPlayer, List<PokerType> winList, int[] reserveArray, ref PokerType sorted, int[] st1, int[] st2, int[] st3, int[] st4)
        {
            if (currentPlayer.Type >= -1)
            {
                if (st1.Length >= 5)
                {
                    if (st1[0] + 4 == st1[4])
                    {
                        currentPlayer.Type = 8;
                        currentPlayer.Power = (st1.Max()) / 4 + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 8 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st1[0] == 0 && st1[1] == 9 && st1[2] == 10 && st1[3] == 11 && st1[0] + 12 == st1[4])
                    {
                        currentPlayer.Type = 9;
                        currentPlayer.Power = (st1.Max()) / 4 + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 9 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (st2.Length >= 5)
                {
                    if (st2[0] + 4 == st2[4])
                    {
                        currentPlayer.Type = 8;
                        currentPlayer.Power = (st2.Max()) / 4 + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 8 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st2[0] == 0 && st2[1] == 9 && st2[2] == 10 && st2[3] == 11 && st2[0] + 12 == st2[4])
                    {
                        currentPlayer.Type = 9;
                        currentPlayer.Power = (st2.Max()) / 4 + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 9 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (st3.Length >= 5)
                {
                    if (st3[0] + 4 == st3[4])
                    {
                        currentPlayer.Type = 8;
                        currentPlayer.Power = (st3.Max()) / 4 + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 8 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st3[0] == 0 && st3[1] == 9 && st3[2] == 10 && st3[3] == 11 && st3[0] + 12 == st3[4])
                    {
                        currentPlayer.Type = 9;
                        currentPlayer.Power = (st3.Max()) / 4 + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 9 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (st4.Length >= 5)
                {
                    if (st4[0] + 4 == st4[4])
                    {
                        currentPlayer.Type = 8;
                        currentPlayer.Power = (st4.Max()) / 4 + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 8 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st4[0] == 0 && st4[1] == 9 && st4[2] == 10 && st4[3] == 11 && st4[0] + 12 == st4[4])
                    {
                        currentPlayer.Type = 9;
                        currentPlayer.Power = (st4.Max()) / 4 + currentPlayer.Type * 100;
                        winList.Add(new PokerType() { Power = currentPlayer.Power, Current = 9 });
                        sorted = winList.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        public static void HighCard(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool raising)
        {
            ActionManager.HP(currentPlayer, globalCall, potTextBox, ref globalRaise, ref raising, 20, 25);
        }

        public static void PairTable(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool raising)
        {
            ActionManager.HP(currentPlayer, globalCall, potTextBox, ref globalRaise, ref raising, 16, 25);
        }

        public static void PairHand(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool raising, ref int globalRounds)
        {
            int rCall = RandomGenerator.Next(10, 16);
            int rRaise = RandomGenerator.Next(10, 13);
            if (currentPlayer.Power <= 199 && currentPlayer.Power >= 140)
            {
                ActionManager.PH(currentPlayer, globalCall, potTextBox, ref globalRaise, ref raising, globalRounds, rCall, 6, rRaise);
            }

            if (currentPlayer.Power <= 139 && currentPlayer.Power >= 128)
            {
                ActionManager.PH(currentPlayer, globalCall, potTextBox, ref globalRaise, ref raising, globalRounds, rCall, 7, rRaise);
            }

            if (currentPlayer.Power < 128 && currentPlayer.Power >= 101)
            {
                ActionManager.PH(currentPlayer, globalCall, potTextBox, ref globalRaise, ref raising, globalRounds, rCall, 9, rRaise);
            }
        }

        public static void TwoPair(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool raising, ref int globalRounds)
        {
            int rCall = RandomGenerator.Next(6, 11);
            int rRaise = RandomGenerator.Next(6, 11);
            if (currentPlayer.Power <= 290 && currentPlayer.Power >= 246)
            {
                ActionManager.PH(currentPlayer, globalCall, potTextBox, ref globalRaise, ref raising, globalRounds, rCall, 3, rRaise);
            }

            if (currentPlayer.Power <= 244 && currentPlayer.Power >= 234)
            {
                ActionManager.PH(currentPlayer, globalCall, potTextBox, ref globalRaise, ref raising, globalRounds, rCall, 4, rRaise);
            }

            if (currentPlayer.Power < 234 && currentPlayer.Power >= 201)
            {
                ActionManager.PH(currentPlayer, globalCall, potTextBox, ref globalRaise, ref raising, globalRounds, rCall, 4, rRaise);
            }
        }

        public static void ThreeOfAKind(IPlayer currentPlayer, int name, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool raising, ref int globalRounds)
        {
            int tCall = RandomGenerator.Next(3, 7);
            int tRaise = RandomGenerator.Next(4, 8);
            if (currentPlayer.Power <= 390 && currentPlayer.Power >= 330)
            {
                Smooth(currentPlayer, globalCall, potTextBox, ref globalRaise, ref raising, ref globalRounds, name, tCall, tRaise);
            }

            if (currentPlayer.Power <= 327 && currentPlayer.Power >= 321)
            {
                Smooth(currentPlayer, globalCall, potTextBox, ref globalRaise, ref raising, ref globalRounds, name, tCall, tRaise);
            }

            if (currentPlayer.Power < 321 && currentPlayer.Power >= 303)
            {
                Smooth(currentPlayer, globalCall, potTextBox, ref globalRaise, ref raising, ref globalRounds, name, tCall, tRaise);
            }
        }

        private static void Smooth(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool raising, ref int globalRounds, int name, int n, int r)
        {
            int random = RandomGenerator.Next(1, 3);
            if (globalCall <= 0)
            {
                ActionManager.Check(currentPlayer, ref raising);
            }
            else
            {
                if (globalCall >= RoundN(currentPlayer.Chips, n))
                {
                    if (currentPlayer.Chips > globalCall)
                    {
                        ActionManager.Call(currentPlayer, ref raising, globalCall, ref potTextBox);
                    }
                    else if (currentPlayer.Chips <= globalCall)
                    {
                        raising = false;
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
                            ActionManager.Raised(currentPlayer, ref raising, ref globalRaise, ref globalCall, ref potTextBox);
                        }
                        else
                        {
                            ActionManager.Call(currentPlayer, ref raising, globalCall, ref potTextBox);
                        }
                    }
                    else
                    {
                        globalRaise = globalCall * 2;
                        ActionManager.Raised(currentPlayer, ref raising, ref globalRaise, ref globalCall, ref potTextBox);
                    }
                }
            }

            if (currentPlayer.Chips <= 0)
            {
                currentPlayer.OutOfChips = true;
            }
        }

        private static double RoundN(int playerChips, int n)
        {
            double result = Math.Round((playerChips / n) / 100d, 0) * 100;
            return result;
        }

        public static void Straight(IPlayer currentPlayer, Label sStatus, int name, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool raising, ref int globalRounds)
        {
            int straightCall = RandomGenerator.Next(3, 6);
            int straightRaise = RandomGenerator.Next(3, 8);
            if (currentPlayer.Power <= 480 && currentPlayer.Power >= 410)
            {
                Smooth(currentPlayer, globalCall, potTextBox, ref globalRaise, ref raising, ref globalRounds, name, straightCall, straightRaise);
            }

            if (currentPlayer.Power <= 409 && currentPlayer.Power >= 407)//10  8
            {
                Smooth(currentPlayer, globalCall, potTextBox, ref globalRaise, ref raising, ref globalRounds, name, straightCall, straightRaise);
            }

            if (currentPlayer.Power < 407 && currentPlayer.Power >= 404)
            {
                Smooth(currentPlayer, globalCall, potTextBox, ref globalRaise, ref raising, ref globalRounds, name, straightCall, straightRaise);
            }
        }

        public static void Flush(IPlayer currentPlayer, int name, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool raising, ref int globalRounds)
        {
            int flushCall = RandomGenerator.Next(2, 6);
            int flushRaise = RandomGenerator.Next(3, 7);
            Smooth(currentPlayer, globalCall, potTextBox, ref globalRaise, ref raising, ref globalRounds, name, flushCall, flushRaise);
        }

        public static void FullHouse(IPlayer currentPlayer, Label sStatus, int name, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool raising, ref int globalRounds)
        {
            int fullHouseCall = RandomGenerator.Next(1, 5);
            int fullHouseRaise = RandomGenerator.Next(2, 6);
            if (currentPlayer.Power <= 626 && currentPlayer.Power >= 620)
            {
                Smooth(currentPlayer, globalCall, potTextBox, ref globalRaise, ref raising, ref globalRounds, name, fullHouseCall, fullHouseRaise);
            }

            if (currentPlayer.Power < 620 && currentPlayer.Power >= 602)
            {
                Smooth(currentPlayer, globalCall, potTextBox, ref globalRaise, ref raising, ref globalRounds, name, fullHouseCall, fullHouseRaise);
            }
        }

        public static void FourOfAKind(IPlayer currentPlayer, Label sStatus, int name, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool raising, ref int globalRounds)
        {
            int fourOfAKindCall = RandomGenerator.Next(1, 4);
            int fourOfAKindRaise = RandomGenerator.Next(2, 5);
            if (currentPlayer.Power <= 752 && currentPlayer.Power >= 704)
            {
                Smooth(currentPlayer, globalCall, potTextBox, ref globalRaise, ref raising, ref globalRounds, name, fourOfAKindCall, fourOfAKindRaise);
            }
        }

        public static void StraightFlush(IPlayer currentPlayer, Label sStatus, int name, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool raising, ref int globalRounds)
        {
            int straightFlushCall = RandomGenerator.Next(1, 3);
            int straightFlushRaise = RandomGenerator.Next(1, 3);
            if (currentPlayer.Power <= 913 && currentPlayer.Power >= 804)
            {
                Smooth(currentPlayer, globalCall, potTextBox, ref globalRaise, ref raising, ref globalRounds, name, straightFlushCall, straightFlushRaise);
            }
        }
    }
}
