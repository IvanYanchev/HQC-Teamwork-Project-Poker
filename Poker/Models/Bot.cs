namespace Poker.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using Poker.Interfaces;
    using Poker.Core;

    public class Bot : Player, IBot
    {
        public Bot(string name)
            : base(name)
        {
        }

        //public void AI()
        //{
        //    if (!this.OutOfChips)
        //    {
        //        if (this.Type == -1)
        //        {
        //            CardCombinations.HighCard(this.Chips, this.CanPlay, this.OutOfChips, this.Status, this.Power);
        //        }

        //        if (this.Type == 0)
        //        {
        //            CardCombinations.PairTable(this.Chips, this.CanPlay, this.OutOfChips, this.Status, this.Power);
        //        }

        //        if (this.Type == 1)
        //        {
        //            CardCombinations.PairHand(this.Chips, this.CanPlay, this.OutOfChips, this.Status, this.Power);
        //        }

        //        if (this.Type == 2)
        //        {
        //            CardCombinations.TwoPair(this.Chips, this.CanPlay, this.OutOfChips, this.Status, this.Power);
        //        }

        //        if (this.Type == 3)
        //        {
        //            CardCombinations.ThreeOfAKind(this.Chips, this.CanPlay, this.OutOfChips, this.Status, this.Power);
        //        }

        //        if (this.Type == 4)
        //        {
        //            CardCombinations.Straight(this.Chips, this.CanPlay, this.OutOfChips, this.Status, this.Power);
        //        }

        //        if (this.Type == 5 || this.Type == 5.5)
        //        {
        //            CardCombinations.Flush(this.Chips, this.CanPlay, this.OutOfChips, this.Status, this.Power);
        //        }

        //        if (this.Type == 6)
        //        {
        //            CardCombinations.FullHouse(this.Chips, this.CanPlay, this.OutOfChips, this.Status, this.Power);
        //        }

        //        if (this.Type == 7)
        //        {
        //            CardCombinations.FourOfAKind(this.Chips, this.CanPlay, this.OutOfChips, this.Status, this.Power);
        //        }

        //        if (this.Type == 8 || this.Type == 9)
        //        {
        //            CardCombinations.StraightFlush(this.Chips, this.CanPlay, this.OutOfChips, this.Status, this.Power);
        //        }
        //    }

        //    if (this.OutOfChips)
        //    {
        //        this.HoldedCard1.IsVisible = false;
        //        this.HoldedCard2.IsVisible = false;
        //    }
        //}
    }
}
