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
    }
}
