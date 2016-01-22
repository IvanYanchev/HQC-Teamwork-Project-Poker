namespace Poker.Models
{
    using System;
    using System.Windows.Forms;

    using Poker.Interfaces;

    public class BotHand
    {
        public void HighCard(IBot bot, Table table)
        {
            HP(bot, table, 20, 25);
        }
        public void PairTable(IBot bot, Table table)
        {
            HP(bot, table, 16, 25);
        }


        public void PairHand(IBot bot, Table table)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(10, 16);
            int rRaise = rPair.Next(10, 13);
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

        public void TwoPair(IBot bot, Table table)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(6, 11);
            int rRaise = rPair.Next(6, 11);
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
        public void ThreeOfAKind(IBot bot, Table table)
        {
            Random tk = new Random();
            int tCall = tk.Next(3, 7);
            int tRaise = tk.Next(4, 8);
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
        public void Straight(IBot bot, Table table)
        {
            Random str = new Random();
            int sCall = str.Next(3, 6);
            int sRaise = str.Next(3, 8);
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
        public void Flush(IBot bot, Table table)
        {
            Random fsh = new Random();
            int fCall = fsh.Next(2, 6);
            int fRaise = fsh.Next(3, 7);
            Smooth(bot, table, fCall, fRaise);
        }

        public void FullHouse(IBot bot, Table table)
        {
            Random flh = new Random();
            int fhCall = flh.Next(1, 5);
            int fhRaise = flh.Next(2, 6);
            if (bot.Power <= 626 && bot.Power >= 620)
            {
                Smooth(bot, table, fhCall, fhRaise);
            }
            if (bot.Power < 620 && bot.Power >= 602)
            {
                Smooth(bot, table, fhCall, fhRaise);
            }
        }
        public void FourOfAKind(IBot bot, Table table)
        {
            Random fk = new Random();
            int fkCall = fk.Next(1, 4);
            int fkRaise = fk.Next(2, 5);
            if (bot.Power <= 752 && bot.Power >= 704)
            {
                Smooth(bot, table, fkCall, fkRaise);
            }
        }

        public void StraightFlush(IBot bot, Table table)
        {
            Random sf = new Random();
            int sfCall = sf.Next(1, 3);
            int sfRaise = sf.Next(1, 3);
            if (bot.Power <= 913 && bot.Power >= 804)
            {
                Smooth(bot, table, sfCall, sfRaise);
            }
        }

        private void HP(IBot bot, Table table, int n, int n1)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 4);
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
                bot.FTurn = true;
            }
        }

        private void PH(IBot bot, Table table, int n, int n1, int r)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
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
                    if (!bot.FTurn)
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
                    if (!bot.FTurn)
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
                bot.FTurn = true;
            }
        }

        private void Smooth(IBot bot, Table table, int n, int r)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
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
                        bot.Turn = false;
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
                bot.FTurn = true;
            }
        }

        private static double RoundN(int sChips, int n)
        {
            double a = Math.Round((sChips / n) / 100d, 0) * 100;
            return a;
        }

        private void Fold(IBot bot, Table table)
        {
            table.Raising = false;
            bot.Status.Text = "Fold";
            bot.Turn = false;
            bot.FTurn = true;
        }

        private void Check(IBot bot, Table table)
        {
            bot.Status.Text = "Check";
            bot.Turn = false;
            table.Raising = false;
        }

        private void Call(IBot bot, Table table)
        {
            table.Raising = false;
            bot.Turn = false;
            bot.Chips -= table.CurrentCall;
            bot.Status.Text = "Call " + table.CurrentCall;
            tbPot.Text = (int.Parse(tbPot.Text) + table.CurrentCall).ToString();
        }

        private void Raised(IBot bot, Table table)
        {
            bot.Chips -= Convert.ToInt32(table.CurrentRaise);
            bot.Status.Text = "Raise " + table.CurrentRaise;
            tbPot.Text = (int.Parse(tbPot.Text) + Convert.ToInt32(table.CurrentRaise)).ToString();
            table.CurrentCall = Convert.ToInt32(table.CurrentRaise);
            table.Raising = true;
            bot.Turn = false;
        }
    }
}
