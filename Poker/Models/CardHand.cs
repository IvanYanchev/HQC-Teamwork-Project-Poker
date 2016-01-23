namespace Poker.Models
{
    using System;
    using System.Windows.Forms;
    using Poker.Interfaces;

    public class CardHand
    {
        public void HighCard(IPlayer bot, Table table)
        {
            HP(bot, table, 20, 25);
        }

        public void PairTable(IPlayer bot, Table table)
        {
            HP(bot, table, 16, 25);
        }

        public void PairHand(IPlayer bot, Table table)
        {
            int rCall = RandomGenerator.Next(10, 16);
            int rRaise = RandomGenerator.Next(10, 13);
            if (bot.Power <= 199 && bot.Power >= 140)
            {
                PH(bot, table, rCall, 6, rRaise);
            }
            if (bot.Power <= 139 && bot.Power >= 128)
            {
                PH(bot, table, rCall, 7, rRaise);
            }
            if (bot.Power < 128 && bot.Power >= 101)
            {
                PH(bot, table, rCall, 9, rRaise);
            }
        }

        public void TwoPair(IPlayer bot, Table table)
        {
            int rCall = RandomGenerator.Next(6, 11);
            int rRaise = RandomGenerator.Next(6, 11);
            if (bot.Power <= 290 && bot.Power >= 246)
            {
                PH(bot, table, rCall, 3, rRaise);
            }
            if (bot.Power <= 244 && bot.Power >= 234)
            {
                PH(bot, table, rCall, 4, rRaise);
            }
            if (bot.Power < 234 && bot.Power >= 201)
            {
                PH(bot, table, rCall, 4, rRaise);
            }
        }
        public void ThreeOfAKind(IPlayer bot, Table table)
        {
            int tCall = RandomGenerator.Next(3, 7);
            int tRaise = RandomGenerator.Next(4, 8);
            if (bot.Power <= 390 && bot.Power >= 330)
            {
                Smooth(bot, table, tCall, tRaise);
            }
            if (bot.Power <= 327 && bot.Power >= 321) //10  8
            {
                Smooth(bot, table, tCall, tRaise);
            }
            if (bot.Power < 321 && bot.Power >= 303) //7 2
            {
                Smooth(bot, table, tCall, tRaise);
            }
        }
        public void Straight(IPlayer bot, Table table)
        {
            int sCall = RandomGenerator.Next(3, 6);
            int sRaise = RandomGenerator.Next(3, 8);
            if (bot.Power <= 480 && bot.Power >= 410)
            {
                Smooth(bot, table, sCall, sRaise);
            }
            if (bot.Power <= 409 && bot.Power >= 407) //10  8
            {
                Smooth(bot, table, sCall, sRaise);
            }
            if (bot.Power < 407 && bot.Power >= 404)
            {
                Smooth(bot, table, sCall, sRaise);
            }
        }
        public void Flush(IPlayer bot, Table table)
        {
            int fCall = RandomGenerator.Next(2, 6);
            int fRaise = RandomGenerator.Next(3, 7);
            Smooth(bot, table, fCall, fRaise);
        }

        public void FullHouse(IPlayer bot, Table table)
        {
            int fhCall = RandomGenerator.Next(1, 5);
            int fhRaise = RandomGenerator.Next(2, 6);
            if (bot.Power <= 626 && bot.Power >= 620)
            {
                Smooth(bot, table, fhCall, fhRaise);
            }
            if (bot.Power < 620 && bot.Power >= 602)
            {
                Smooth(bot, table, fhCall, fhRaise);
            }
        }
        public void FourOfAKind(IPlayer bot, Table table)
        {
            int fkCall = RandomGenerator.Next(1, 4);
            int fkRaise = RandomGenerator.Next(2, 5);
            if (bot.Power <= 752 && bot.Power >= 704)
            {
                Smooth(bot, table, fkCall, fkRaise);
            }
        }

        public void StraightFlush(IPlayer bot, Table table)
        {
            int sfCall = RandomGenerator.Next(1, 3);
            int sfRaise = RandomGenerator.Next(1, 3);
            if (bot.Power <= 913 && bot.Power >= 804)
            {
                Smooth(bot, table, sfCall, sfRaise);
            }
        }

        private void HP(IPlayer bot, Table table, int n, int n1)
        {
            int rnd = RandomGenerator.Next(1, 4);
            if (table.CurrentCall <= 0)
            {
                Check(bot, table);
            }
            if (table.CurrentCall > 0)
            {
                if (rnd == 1)
                {
                    if (table.CurrentCall <= RoundN(bot.Chips, n))
                    {
                        Call(bot, table);
                    }
                    else
                    {
                        Fold(bot, table);
                    }
                }
                if (rnd == 2)
                {
                    if (table.CurrentCall <= RoundN(bot.Chips, n1))
                    {
                        Call(bot, table);
                    }
                    else
                    {
                        Fold(bot, table);
                    }
                }
            }
            if (rnd == 3)
            {
                if (table.CurrentRaise == 0)
                {
                    table.CurrentRaise = table.CurrentCall * 2;
                    Raised(bot, table);
                }
                else
                {
                    if (table.CurrentRaise <= RoundN(bot.Chips, n))
                    {
                        table.CurrentRaise = table.CurrentCall * 2;
                        Raised(bot, table);
                    }
                    else
                    {
                        Fold(bot, table);
                    }
                }
            }
            if (bot.Chips <= 0)
            {
                bot.OutOfChips = true;
            }
        }

        private void PH(IPlayer bot, Table table, int n, int n1, int r)
        {
            int rnd = RandomGenerator.Next(1, 3);
            if (table.Rounds < 2)
            {
                if (table.CurrentCall <= 0)
                {
                    Check(bot, table);
                }
                if (table.CurrentCall > 0)
                {
                    if (table.CurrentCall >= RoundN(bot.Chips, n1))
                    {
                        Fold(bot, table);
                    }
                    if (table.CurrentRaise > RoundN(bot.Chips, n))
                    {
                        Fold(bot, table);
                    }
                    if (!bot.OutOfChips)
                    {
                        if (table.CurrentCall >= RoundN(bot.Chips, n) && table.CurrentCall <= RoundN(bot.Chips, n1))
                        {
                            Call(bot, table);
                        }
                        if (table.CurrentRaise <= RoundN(bot.Chips, n) && table.CurrentRaise >= (RoundN(bot.Chips, n)) / 2)
                        {
                            Call(bot, table);
                        }
                        if (table.CurrentRaise <= (RoundN(bot.Chips, n)) / 2)
                        {
                            if (table.CurrentRaise > 0)
                            {
                                table.CurrentRaise = RoundN(bot.Chips, n);
                                Raised(bot, table);
                            }
                            else
                            {
                                table.CurrentRaise = table.CurrentCall * 2;
                                Raised(bot,table);
                            }
                        }

                    }
                }
            }
            if (table.Rounds >= 2)
            {
                if (table.CurrentCall > 0)
                {
                    if (table.CurrentCall >= RoundN(bot.Chips, n1 - rnd))
                    {
                        Fold(bot, table);
                    }
                    if (table.CurrentRaise > RoundN(bot.Chips, n - rnd))
                    {
                        Fold(bot, table);
                    }
                    if (!bot.OutOfChips)
                    {
                        if (table.CurrentCall >= RoundN(bot.Chips, n - rnd) && table.CurrentCall <= RoundN(bot.Chips, n1 - rnd))
                        {
                            Call(bot, table);
                        }
                        if (table.CurrentRaise <= RoundN(bot.Chips, n - rnd) && table.CurrentRaise >= (RoundN(bot.Chips, n - rnd)) / 2)
                        {
                            Call(bot, table);
                        }
                        if (table.CurrentRaise <= (RoundN(bot.Chips, n - rnd)) / 2)
                        {
                            if (table.CurrentRaise > 0)
                            {
                                table.CurrentRaise = RoundN(bot.Chips, n - rnd);
                                Raised(bot, table);
                            }
                            else
                            {
                                table.CurrentRaise = table.CurrentCall * 2;
                                Raised(bot, table);
                            }
                        }
                    }
                }
                if (table.CurrentCall <= 0)
                {
                    table.CurrentRaise = RoundN(bot.Chips, r - rnd);
                    Raised(bot, table);
                }
            }
            if (bot.Chips <= 0)
            {
                bot.OutOfChips = true;
            }
        }

        private void Smooth(IPlayer bot, Table table, int n, int r)
        {
            int rnd = RandomGenerator.Next(1, 3);
            if (table.CurrentCall <= 0)
            {
                Check(bot, table);
            }
            else
            {
                if (table.CurrentCall >= RoundN(bot.Chips, n))
                {
                    if (bot.Chips > table.CurrentCall)
                    {
                        Call(bot, table);
                    }
                    else if (bot.Chips <= table.CurrentCall)
                    {
                        table.Raising = false;
                        bot.CanPlay = false;
                        bot.Chips = 0;
                        bot.Status.Text = "Call " + bot.Chips;
                        tbPot.Text = (int.Parse(tbPot.Text) + bot.Chips).ToString();
                    }
                }
                else
                {
                    if (table.CurrentRaise > 0)
                    {
                        if (bot.Chips >= table.CurrentRaise * 2)
                        {
                            table.CurrentRaise *= 2;
                            Raised(bot, table);
                        }
                        else
                        {
                            Call(bot, table);
                        }
                    }
                    else
                    {
                        table.CurrentRaise = table.CurrentCall * 2;
                        Raised(bot, table);
                    }
                }
            }
            if (bot.Chips <= 0)
            {
                bot.OutOfChips = true;
            }
        }

        private static double RoundN(int sChips, int n)
        {
            double a = Math.Round((sChips / n) / 100d, 0) * 100;
            return a;
        }

        private void Fold(IPlayer bot, Table table)
        {
            table.Raising = false;
            bot.Status.Text = "Fold";
            bot.CanPlay = false;
            bot.OutOfChips = true;
        }

        private void Check(IPlayer bot, Table table)
        {
            bot.Status.Text = "Check";
            bot.CanPlay = false;
            table.Raising = false;
        }

        private void Call(IPlayer bot, Table table)
        {
            table.Raising = false;
            bot.CanPlay = false;
            bot.Chips -= table.CurrentCall;
            bot.Status.Text = "Call " + table.CurrentCall;
            tbPot.Text = (int.Parse(tbPot.Text) + table.CurrentCall).ToString();
        }

        private void Raised(IPlayer bot, Table table)
        {
            bot.Chips -= Convert.ToInt32(table.CurrentRaise);
            bot.Status.Text = "Raise " + table.CurrentRaise;
            tbPot.Text = (int.Parse(tbPot.Text) + Convert.ToInt32(table.CurrentRaise)).ToString();
            table.CurrentCall = Convert.ToInt32(table.CurrentRaise);
            table.Raising = true;
            bot.CanPlay = false;
        }
    }
}
