namespace Poker.Core
{
    using Poker.Interfaces;
    using System;
    using System.Windows.Forms;

    public static class ActionManager
    {
        public static void Check(IPlayer currentPlayer, ref bool raising)
        {
            raising = false;
            currentPlayer.Status.Text = "Check";
            currentPlayer.CanPlay = false;
        }

        public static void Fold(IPlayer currentPlayer, ref bool raising)
        {
            raising = false;
            currentPlayer.Status.Text = "Fold";
            currentPlayer.OutOfChips = true;
            currentPlayer.CanPlay = false;
        }

        public static void Call(IPlayer currentPlayer, ref bool raising, int globalCall, ref TextBox potTexBox)
        {
            raising = false;
            currentPlayer.CanPlay = false;
            currentPlayer.Chips -= globalCall;
            currentPlayer.Status.Text = "Call" + globalCall;
            potTexBox.Text = (int.Parse(potTexBox.Text) + globalCall).ToString();
        }

        public static void Raised(IPlayer currentPlayer, ref bool raising, ref int globalRaise, ref int globalCall, ref TextBox potTextBox)
        {
            currentPlayer.Chips -= globalRaise;
            currentPlayer.Status.Text = "Raise" + globalRaise;
            globalCall = globalRaise;
            raising = true;
            potTextBox.Text = (int.Parse(potTextBox.Text) + globalRaise).ToString();
            currentPlayer.CanPlay = false;
        }

        private static double RoundN(int playerChips, int n)
        {
            double result = Math.Round((playerChips / n) / 100d, 0) * 100;
            return result;
        }

        public static void HP(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool raising, int numberOne, int numberTwo)
        {
            int rnd = RandomGenerator.Next(1, 4);
            if (globalCall <= 0)
            {
                Check(currentPlayer, ref raising);
            }

            if (globalCall > 0)
            {
                if (rnd == 1)
                {
                    if (globalCall <= RoundN(currentPlayer.Chips, numberOne))
                    {
                        Call(currentPlayer, ref raising, globalCall, ref potTextBox);
                    }
                    else
                    {
                        Fold(currentPlayer, ref raising);
                    }
                }

                if (rnd == 2)
                {
                    if (globalCall <= RoundN(currentPlayer.Chips, numberTwo))
                    {
                        Call(currentPlayer, ref raising, globalCall, ref potTextBox);
                    }
                    else
                    {
                        Fold(currentPlayer, ref raising);
                    }
                }
            }

            if (rnd == 3)
            {
                if (globalRaise == 0)
                {
                    globalRaise = globalCall * 2;
                    Raised(currentPlayer, ref raising, ref globalRaise, ref globalCall, ref potTextBox);
                }
                else
                {
                    if (globalRaise <= RoundN(currentPlayer.Chips, numberOne))
                    {
                        globalRaise = globalCall * 2;
                        Raised(currentPlayer, ref raising, ref globalRaise, ref globalCall, ref potTextBox);
                    }
                    else
                    {
                        Fold(currentPlayer, ref raising);
                    }
                }
            }

            if (currentPlayer.Chips <= 0)
            {
                currentPlayer.OutOfChips = true;
            }
        }

        public static void PH(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool raising, int globalRounds, int n, int n1, int r)
        {
            int rnd = RandomGenerator.Next(1, 3);
            if (globalRounds < 2)
            {
                if (globalCall <= 0)
                {
                    Check(currentPlayer, ref raising);
                }

                if (globalCall > 0)
                {
                    if (globalCall >= RoundN(currentPlayer.Chips, n1))
                    {
                        Fold(currentPlayer, ref raising);
                    }

                    if (globalRaise > RoundN(currentPlayer.Chips, n))
                    {
                        Fold(currentPlayer, ref raising);
                    }

                    if (!currentPlayer.OutOfChips)
                    {
                        if (globalCall >= RoundN(currentPlayer.Chips, n) && globalCall <= RoundN(currentPlayer.Chips, n1))
                        {
                            Call(currentPlayer, ref raising, globalCall, ref potTextBox);
                        }

                        if (globalRaise <= RoundN(currentPlayer.Chips, n) && globalRaise >= (RoundN(currentPlayer.Chips, n)) / 2)
                        {
                            Call(currentPlayer, ref raising, globalCall, ref potTextBox);
                        }

                        if (globalRaise <= (RoundN(currentPlayer.Chips, n)) / 2)
                        {
                            if (globalRaise > 0)
                            {
                                globalRaise = (int)RoundN(currentPlayer.Chips, n);
                                Raised(currentPlayer, ref raising, ref globalRaise, ref globalCall, ref potTextBox);
                            }
                            else
                            {
                                globalRaise = globalCall * 2;
                                Raised(currentPlayer, ref raising, ref globalRaise, ref globalCall, ref potTextBox);
                            }
                        }

                    }
                }
            }

            if (globalRounds >= 2)
            {
                if (globalCall > 0)
                {
                    if (globalCall >= RoundN(currentPlayer.Chips, n1 - rnd))
                    {
                        Fold(currentPlayer, ref raising);
                    }

                    if (globalRaise > RoundN(currentPlayer.Chips, n - rnd))
                    {
                        Fold(currentPlayer, ref raising);
                    }

                    if (!currentPlayer.OutOfChips)
                    {
                        if (globalCall >= RoundN(currentPlayer.Chips, n - rnd) && globalCall <= RoundN(currentPlayer.Chips, n1 - rnd))
                        {
                            Call(currentPlayer, ref raising, globalCall, ref potTextBox);
                        }

                        if (globalRaise <= RoundN(currentPlayer.Chips, n - rnd) && globalRaise >= (RoundN(currentPlayer.Chips, n - rnd)) / 2)
                        {
                            Call(currentPlayer, ref raising, globalCall, ref potTextBox);
                        }

                        if (globalRaise <= (RoundN(currentPlayer.Chips, n - rnd)) / 2)
                        {
                            if (globalRaise > 0)
                            {
                                globalRaise = (int)RoundN(currentPlayer.Chips, n - rnd);
                                Raised(currentPlayer, ref raising, ref globalRaise, ref globalCall, ref potTextBox);
                            }
                            else
                            {
                                globalRaise = globalCall * 2;
                                Raised(currentPlayer, ref raising, ref globalRaise, ref globalCall, ref potTextBox);
                            }
                        }
                    }
                }

                if (globalCall <= 0)
                {
                    globalRaise = (int)RoundN(currentPlayer.Chips, r - rnd);
                    Raised(currentPlayer, ref raising, ref globalRaise, ref globalCall, ref potTextBox);
                }
            }

            if (currentPlayer.Chips <= 0)
            {
                currentPlayer.OutOfChips = true;
            }
        }
    }
}
