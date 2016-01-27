namespace Poker.Core
{
    using Poker.Interfaces;
    using System;
    using System.Windows.Forms;

    public class ActionManager : IActionManager
    {
        public void Check(IPlayer currentPlayer, ref bool isRaisingActivated)
        {
            isRaisingActivated = false;
            currentPlayer.Status.Text = "Check";
            currentPlayer.CanPlay = false;
        }

        public void Fold(IPlayer currentPlayer, ref bool isRaisingActivated)
        {
            isRaisingActivated = false;
            currentPlayer.Status.Text = "Fold";
            currentPlayer.OutOfChips = true;
            currentPlayer.CanPlay = false;
        }

        public void Call(IPlayer currentPlayer, ref bool isRaisingActivated, int globalCall, ref TextBox potTexBox)
        {
            isRaisingActivated = false;
            currentPlayer.CanPlay = false;
            currentPlayer.Chips -= globalCall;
            currentPlayer.Status.Text = "Call" + globalCall;
            potTexBox.Text = (int.Parse(potTexBox.Text) + globalCall).ToString();
        }

        public void Raised(IPlayer currentPlayer, ref bool isRaisingActivated, ref int globalRaise, ref int globalCall, ref TextBox potTextBox)
        {
            currentPlayer.Chips -= globalRaise;
            currentPlayer.Status.Text = "Raise" + globalRaise;
            globalCall = globalRaise;
            isRaisingActivated = true;
            potTextBox.Text = (int.Parse(potTextBox.Text) + globalRaise).ToString();
            currentPlayer.CanPlay = false;
        }

        private double RoundN(int playerChips, int n)
        {
            double result = Math.Round((playerChips / n) / 100d, 0) * 100;
            return result;
        }

        public void HP(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, int numberOne, int numberTwo)
        {
            int rnd = RandomGenerator.Next(1, 4);
            if (globalCall <= 0)
            {
                Check(currentPlayer, ref isRaisingActivated);
            }

            if (globalCall > 0)
            {
                if (rnd == 1)
                {
                    if (globalCall <= RoundN(currentPlayer.Chips, numberOne))
                    {
                        Call(currentPlayer, ref isRaisingActivated, globalCall, ref potTextBox);
                    }
                    else
                    {
                        Fold(currentPlayer, ref isRaisingActivated);
                    }
                }

                if (rnd == 2)
                {
                    if (globalCall <= RoundN(currentPlayer.Chips, numberTwo))
                    {
                        Call(currentPlayer, ref isRaisingActivated, globalCall, ref potTextBox);
                    }
                    else
                    {
                        Fold(currentPlayer, ref isRaisingActivated);
                    }
                }
            }

            if (rnd == 3)
            {
                if (globalRaise == 0)
                {
                    globalRaise = globalCall * 2;
                    Raised(currentPlayer, ref isRaisingActivated, ref globalRaise, ref globalCall, ref potTextBox);
                }
                else
                {
                    if (globalRaise <= RoundN(currentPlayer.Chips, numberOne))
                    {
                        globalRaise = globalCall * 2;
                        Raised(currentPlayer, ref isRaisingActivated, ref globalRaise, ref globalCall, ref potTextBox);
                    }
                    else
                    {
                        Fold(currentPlayer, ref isRaisingActivated);
                    }
                }
            }

            if (currentPlayer.Chips <= 0)
            {
                currentPlayer.OutOfChips = true;
            }
        }

        public void PH(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, int globalRounds, int n, int n1, int r)
        {
            int rnd = RandomGenerator.Next(1, 3);
            if (globalRounds < 2)
            {
                if (globalCall <= 0)
                {
                    Check(currentPlayer, ref isRaisingActivated);
                }

                if (globalCall > 0)
                {
                    if (globalCall >= RoundN(currentPlayer.Chips, n1))
                    {
                        Fold(currentPlayer, ref isRaisingActivated);
                    }

                    if (globalRaise > RoundN(currentPlayer.Chips, n))
                    {
                        Fold(currentPlayer, ref isRaisingActivated);
                    }

                    if (!currentPlayer.OutOfChips)
                    {
                        if (globalCall >= RoundN(currentPlayer.Chips, n) && globalCall <= RoundN(currentPlayer.Chips, n1))
                        {
                            Call(currentPlayer, ref isRaisingActivated, globalCall, ref potTextBox);
                        }

                        if (globalRaise <= RoundN(currentPlayer.Chips, n) && globalRaise >= (RoundN(currentPlayer.Chips, n)) / 2)
                        {
                            Call(currentPlayer, ref isRaisingActivated, globalCall, ref potTextBox);
                        }

                        if (globalRaise <= (RoundN(currentPlayer.Chips, n)) / 2)
                        {
                            if (globalRaise > 0)
                            {
                                globalRaise = (int)RoundN(currentPlayer.Chips, n);
                                Raised(currentPlayer, ref isRaisingActivated, ref globalRaise, ref globalCall, ref potTextBox);
                            }
                            else
                            {
                                globalRaise = globalCall * 2;
                                Raised(currentPlayer, ref isRaisingActivated, ref globalRaise, ref globalCall, ref potTextBox);
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
                        Fold(currentPlayer, ref isRaisingActivated);
                    }

                    if (globalRaise > RoundN(currentPlayer.Chips, n - rnd))
                    {
                        Fold(currentPlayer, ref isRaisingActivated);
                    }

                    if (!currentPlayer.OutOfChips)
                    {
                        if (globalCall >= RoundN(currentPlayer.Chips, n - rnd) && globalCall <= RoundN(currentPlayer.Chips, n1 - rnd))
                        {
                            Call(currentPlayer, ref isRaisingActivated, globalCall, ref potTextBox);
                        }

                        if (globalRaise <= RoundN(currentPlayer.Chips, n - rnd) && globalRaise >= (RoundN(currentPlayer.Chips, n - rnd)) / 2)
                        {
                            Call(currentPlayer, ref isRaisingActivated, globalCall, ref potTextBox);
                        }

                        if (globalRaise <= (RoundN(currentPlayer.Chips, n - rnd)) / 2)
                        {
                            if (globalRaise > 0)
                            {
                                globalRaise = (int)RoundN(currentPlayer.Chips, n - rnd);
                                Raised(currentPlayer, ref isRaisingActivated, ref globalRaise, ref globalCall, ref potTextBox);
                            }
                            else
                            {
                                globalRaise = globalCall * 2;
                                Raised(currentPlayer, ref isRaisingActivated, ref globalRaise, ref globalCall, ref potTextBox);
                            }
                        }
                    }
                }

                if (globalCall <= 0)
                {
                    globalRaise = (int)RoundN(currentPlayer.Chips, r - rnd);
                    Raised(currentPlayer, ref isRaisingActivated, ref globalRaise, ref globalCall, ref potTextBox);
                }
            }

            if (currentPlayer.Chips <= 0)
            {
                currentPlayer.OutOfChips = true;
            }
        }

        public void AI(IPlayer currentPlayer, int globalCall, TextBox potTextBox, ref int globalRaise, ref bool isRaisingActivated, ref int globalRounds, int name)
        {
            if (currentPlayer.Type == -1)
            {
                CombinationsDatabase.HighCard(currentPlayer, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated);
            }

            if (currentPlayer.Type == 0)
            {
                CombinationsDatabase.PairTable(currentPlayer, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated);
            }

            if (currentPlayer.Type == 1)
            {
                CombinationsDatabase.PairHand(currentPlayer, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated, ref globalRounds);
            }

            if (currentPlayer.Type == 2)
            {
                CombinationsDatabase.TwoPair(currentPlayer, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated, ref globalRounds);
            }

            if (currentPlayer.Type == 3)
            {
                CombinationsDatabase.ThreeOfAKind(currentPlayer, name, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated, ref globalRounds);
            }

            if (currentPlayer.Type == 4)
            {
                CombinationsDatabase.Straight(currentPlayer, name, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated, ref globalRounds);
            }

            if (currentPlayer.Type == 5 || currentPlayer.Type == 5.5)
            {
                CombinationsDatabase.Flush(currentPlayer, name, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated, ref globalRounds);
            }

            if (currentPlayer.Type == 6)
            {
                CombinationsDatabase.FullHouse(currentPlayer, name, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated, ref globalRounds);
            }

            if (currentPlayer.Type == 7)
            {
                CombinationsDatabase.FourOfAKind(currentPlayer, name, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated, ref globalRounds);
            }

            if (currentPlayer.Type == 8 || currentPlayer.Type == 9)
            {
                CombinationsDatabase.StraightFlush(currentPlayer, name, globalCall, potTextBox, ref globalRaise, ref isRaisingActivated, ref globalRounds);
            }

            if (currentPlayer.OutOfChips)
            {
                currentPlayer.HoldedCard1.IsVisible = false;
                currentPlayer.HoldedCard2.IsVisible = false;
            }
        }
    }
}

