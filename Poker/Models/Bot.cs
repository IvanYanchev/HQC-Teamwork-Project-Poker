namespace Poker.Models
{
    using System.Windows.Forms;
    using Poker.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Bot : Player, IBot
    {
        public Bot(string name)
        {
            this.Name = name;
        }
        
        public string Name { get; set; }

        public void AI()
        {
            if (!this.FTurn)
            {
                if (this.Type == -1)
                {
                    HighCard(this.Chips, this.Turn, this.FTurn, this.Status, this.Power);
                }

                if (this.Type == 0)
                {
                    PairTable(this.Chips, this.Turn, this.FTurn, this.Status, this.Power);
                }

                if (this.Type == 1)
                {
                    PairHand(this.Chips, this.Turn, this.FTurn, this.Status, this.Power);
                }

                if (this.Type == 2)
                {
                    TwoPair(this.Chips, this.Turn, this.FTurn, this.Status, this.Power);
                }

                if (this.Type == 3)
                {
                    ThreeOfAKind(this.Chips, this.Turn, this.FTurn, this.Status, this.Power);
                }

                if (this.Type == 4)
                {
                    Straight(this.Chips, this.Turn, this.FTurn, this.Status, this.Power);
                }

                if (this.Type == 5 || this.Type == 5.5)
                {
                    Flush(this.Chips, this.Turn, this.FTurn, this.Status, this.Power);
                }

                if (this.Type == 6)
                {
                    FullHouse(this.Chips, this.Turn, this.FTurn, this.Status, this.Power);
                }

                if (this.Type == 7)
                {
                    FourOfAKind(this.Chips, this.Turn, this.FTurn, this.Status, this.Power);
                }

                if (this.Type == 8 || this.Type == 9)
                {
                    StraightFlush(this.Chips, this.Turn, this.FTurn, this.Status, this.Power);
                }
            }

            if (this.FTurn)
            {
                this.HoldedCard1.IsVisible = false;
                this.HoldedCard2.IsVisible = false;
            }
        }

        private void StraightFlush(int p1, bool p2, bool p3, Label label, double p4)
        {
            throw new NotImplementedException();
        }

        private void FourOfAKind(int p1, bool p2, bool p3, Label label, double p4)
        {
            throw new NotImplementedException();
        }

        private void FullHouse(int p1, bool p2, bool p3, Label label, double p4)
        {
            throw new NotImplementedException();
        }

        private void Flush(int p1, bool p2, bool p3, Label label, double p4)
        {
            throw new NotImplementedException();
        }

        private void Straight(int p1, bool p2, bool p3, Label label, double p4)
        {
            throw new NotImplementedException();
        }

        private void ThreeOfAKind(int p1, bool p2, bool p3, Label label, double p4)
        {
            throw new NotImplementedException();
        }

        private void TwoPair(int p1, bool p2, bool p3, Label label, double p4)
        {
            throw new NotImplementedException();
        }

        private void PairHand(int p1, bool p2, bool p3, Label label, double p4)
        {
            throw new NotImplementedException();
        }

        private void PairTable(int p1, bool p2, bool p3, Label label, double p4)
        {
            throw new NotImplementedException();
        }

        private void HighCard(int p1, bool p2, bool p3, Label label, double p4)
        {
            throw new NotImplementedException();
        }


        public void Check()
        {
            throw new NotImplementedException();
        }

        public void Fold()
        {
            throw new NotImplementedException();
        }

        public new void Call()
        {
            throw new NotImplementedException();
        }

        public void Raised()
        {
            throw new NotImplementedException();
        }
    }
}
