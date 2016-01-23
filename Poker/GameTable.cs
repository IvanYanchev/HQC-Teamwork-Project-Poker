namespace Poker
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Runtime.InteropServices;
    using Poker.Core;
    using Poker.Interfaces;
    using Poker.Models;

    public partial class GameTable : Form
    {
        private IPokerDatabase pokerDatabase = new PokerDatabase();

        private readonly IBot botOne = new Bot("botOne");
        private readonly IBot botTwo = new Bot("botTwo");
        private readonly IBot botThree = new Bot("botThree");
        private readonly IBot botFour = new Bot("botFour");
        private readonly IBot botFive = new Bot("botFive");

        private const int NumberOfBots = 5;

        private const int BotOneCardOne = 2;
        private const int BotOneCardTwo = 3;

        private const int BotTwoCardOne = 4;
        private const int BotTwoCardTwo = 5;

        private const int BotThreeCardOne = 6;
        private const int BotThreeCardTwo = 7;

        private const int BotFourCardOne = 8;
        private const int BotFourCardTwo = 9;

        private const int BotFiveCardOne = 10;
        private const int BotFiveCardTwo = 11;

        ProgressBar progressBar = new ProgressBar();
        public int Nm;

        #region Panel
        Panel playerPanel = new Panel();
        Panel botOnePanel = new Panel();
        Panel botTwoPanel = new Panel();
        Panel botThreePanel = new Panel();
        Panel botFourPanel = new Panel();
        Panel botFivePanel = new Panel();
        #endregion

        private int globalCall = 500;
        private int foldedPlayers = 5;
        private int Chips = 10000;

        private IPlayer player = new Human();

        #region PrivateDoubles
        private double type;
        private double globalRounds = 0;
        private double globalRaise = 0;
        #endregion

        #region Private bools

        private bool intsadded;
        private bool changed;
        private bool restart = false;
        private bool raising = false;

        #region Some other privete ints
        private int height;
        private int width;
        private int winners = 0;
        private int Flop = 1;
        private int Turn = 2;
        private int River = 3;
        private int End = 4;
        private int maxLeft = 6;
        private int last = 123;
        private int raisedTurn = 1;
        #endregion

        List<bool?> bools = new List<bool?>();
        List<PokerType> winList = new List<PokerType>();
        List<string> CheckWinners = new List<string>();
        List<int> ints = new List<int>();

        PokerType sorted;
        private string[] ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
        /*string[] ImgLocation ={
                   "Assets\\Cards\\33.png","Assets\\Cards\\22.png",
                    "Assets\\Cards\\29.png","Assets\\Cards\\21.png",
                    "Assets\\Cards\\36.png","Assets\\Cards\\17.png",
                    "Assets\\Cards\\40.png","Assets\\Cards\\16.png",
                    "Assets\\Cards\\5.png","Assets\\Cards\\47.png",
                    "Assets\\Cards\\37.png","Assets\\Cards\\13.png",
                    
                    "Assets\\Cards\\12.png",
                    "Assets\\Cards\\8.png","Assets\\Cards\\18.png",
                    "Assets\\Cards\\15.png","Assets\\Cards\\27.png"};*/
        int[] reserveArray = new int[17];
        Image[] Deck = new Image[52];
        PictureBox[] Holder = new PictureBox[52];
        Timer timer = new Timer();
        Timer Updates = new Timer();

        private int t = 60;
        private int i;
        private int bb = 500;
        private int sb = 250;
        private int up = 10000000;
        private int turnCount = 0;
        #endregion

        public GameTable()
        {
            //bools.Add(PFturn); bools.Add(B1Fturn); bools.Add(B2Fturn); bools.Add(B3Fturn); bools.Add(B4Fturn); bools.Add(B5Fturn);
            globalCall = bb;

            this.botOne.Status = this.botOneStatus;
            this.botTwo.Status = this.botTwoStatus;
            this.botThree.Status = this.botThreeStatus;
            this.botFour.Status = this.botFourStatus;
            this.botFive.Status = this.botFiveStatus;
            this.player.Status = this.playerStatus;

            this.pokerDatabase.AddBot(this.botOne, this.botTwo, this.botThree, this.botFour, this.botFive);
            this.player.OutOfChips = true;

            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.Updates.Start();
            this.InitializeComponent();

            width = this.Width;
            height = this.Height;

            Shuffle();

            this.potTexBox.Enabled = false;
            this.chipsTexBox.Enabled = false;

            this.botOneChips.Enabled = false;
            this.botTwoChips.Enabled = false;
            this.botThreeChips.Enabled = false;
            this.botFourChips.Enabled = false;
            this.botFiveChips.Enabled = false;

            this.chipsTexBox.Text = "Chips : " + Chips.ToString();
            this.botOneChips.Text = "Chips : " + botOne.Chips.ToString();
            this.botTwoChips.Text = "Chips : " + botTwo.Chips.ToString();
            this.botThreeChips.Text = "Chips : " + botThree.Chips.ToString();
            this.botFourChips.Text = "Chips : " + botFour.Chips.ToString();
            this.botFiveChips.Text = "Chips : " + botFive.Chips.ToString();

            this.timer.Interval = (1 * 1 * 1000);
            this.timer.Tick += timer_Tick;
            this.Updates.Interval = (1 * 1 * 100);

            this.Updates.Tick += Update_Tick;
            this.tbBB.Visible = true;
            this.tbSB.Visible = true;
            this.bBB.Visible = true;
            this.buttonSB.Visible = true;
            this.tbBB.Visible = true;
            this.tbSB.Visible = true;
            this.bBB.Visible = true;
            this.buttonSB.Visible = true;
            this.tbBB.Visible = false;
            this.tbSB.Visible = false;
            this.bBB.Visible = false;
            this.buttonSB.Visible = false;
            this.raiseTexBox.Text = (bb * 2).ToString();
        }

        async Task Shuffle()
        {
            this.bools.Add(player.OutOfChips);
            this.bools.Add(botOne.OutOfChips);
            this.bools.Add(botTwo.OutOfChips);
            this.bools.Add(botThree.OutOfChips);
            this.bools.Add(botFour.OutOfChips);
            this.bools.Add(botFive.OutOfChips);

            this.callButton.Enabled = false;
            this.raiseButton.Enabled = false;
            this.foldButton.Enabled = false;
            this.checkButton.Enabled = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            bool check = false;

            Bitmap backImage = new Bitmap("Assets\\Back\\Back.png");
            int horizontal = 580;
            int vertical = 480;

            for (i = ImgLocation.Length; i > 0; i--)
            {
                int j = RandomGenerator.Next(i);
                var k = ImgLocation[j];
                ImgLocation[j] = ImgLocation[i - 1];
                ImgLocation[i - 1] = k;
            }

            for (i = 0; i < 17; i++)
            {
                Deck[i] = Image.FromFile(ImgLocation[i]);
                var charsToRemove = new string[] { "Assets\\Cards\\", ".png" };
                foreach (var c in charsToRemove)
                {
                    ImgLocation[i] = ImgLocation[i].Replace(c, string.Empty);
                }

                this.reserveArray[i] = int.Parse(ImgLocation[i]) - 1;
                this.Holder[i] = new PictureBox();
                this.Holder[i].SizeMode = PictureBoxSizeMode.StretchImage;
                this.Holder[i].Height = 130;
                this.Holder[i].Width = 80;
                this.Controls.Add(Holder[i]);
                this.Holder[i].Name = "pb" + i.ToString();
                await Task.Delay(200);
                #region Throwing Cards
                if (i < 2)
                {
                    if (Holder[0].Tag != null)
                    {
                        Holder[1].Tag = reserveArray[1];
                    }

                    this.Holder[0].Tag = reserveArray[0];
                    this.Holder[i].Image = Deck[i];
                    this.Holder[i].Anchor = (AnchorStyles.Bottom);
                    //Holder[i].Dock = DockStyle.Top;
                    this.Holder[i].Location = new Point(horizontal, vertical);
                    horizontal += Holder[i].Width;
                    this.Controls.Add(playerPanel);
                    this.playerPanel.Location = new Point(Holder[0].Left - 10, Holder[0].Top - 10);
                    this.playerPanel.BackColor = Color.DarkBlue;
                    this.playerPanel.Height = 150;
                    this.playerPanel.Width = 180;
                    this.playerPanel.Visible = false;
                }
                if (botOne.Chips > 0)
                {
                    foldedPlayers--;
                    if (i >= 2 && i < 4)
                    {
                        if (Holder[2].Tag != null)
                        {
                            Holder[3].Tag = reserveArray[3];
                        }
                        Holder[2].Tag = reserveArray[2];
                        if (!check)
                        {
                            horizontal = 15;
                            vertical = 420;
                        }
                        check = true;
                        Holder[i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
                        Holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        Holder[i].Location = new Point(horizontal, vertical);
                        horizontal += Holder[i].Width;
                        Holder[i].Visible = true;
                        this.Controls.Add(botOnePanel);
                        this.botOnePanel.Location = new Point(Holder[2].Left - 10, Holder[2].Top - 10);
                        this.botOnePanel.BackColor = Color.DarkBlue;
                        this.botOnePanel.Height = 150;
                        this.botOnePanel.Width = 180;
                        this.botOnePanel.Visible = false;
                        if (i == 3)
                        {
                            check = false;
                        }
                    }
                }
                if (botTwo.Chips > 0)
                {
                    foldedPlayers--;
                    if (i >= 4 && i < 6)
                    {
                        if (Holder[4].Tag != null)
                        {
                            Holder[5].Tag = reserveArray[5];
                        }
                        Holder[4].Tag = reserveArray[4];
                        if (!check)
                        {
                            horizontal = 75;
                            vertical = 65;
                        }
                        check = true;
                        Holder[i].Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                        Holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        Holder[i].Location = new Point(horizontal, vertical);
                        horizontal += Holder[i].Width;
                        Holder[i].Visible = true;
                        this.Controls.Add(botTwoPanel);
                        this.botTwoPanel.Location = new Point(Holder[4].Left - 10, Holder[4].Top - 10);
                        this.botTwoPanel.BackColor = Color.DarkBlue;
                        this.botTwoPanel.Height = 150;
                        this.botTwoPanel.Width = 180;
                        this.botTwoPanel.Visible = false;
                        if (i == 5)
                        {
                            check = false;
                        }
                    }
                }
                if (botThree.Chips > 0)
                {
                    foldedPlayers--;
                    if (i >= 6 && i < 8)
                    {
                        if (Holder[6].Tag != null)
                        {
                            Holder[7].Tag = reserveArray[7];
                        }
                        Holder[6].Tag = reserveArray[6];
                        if (!check)
                        {
                            horizontal = 590;
                            vertical = 25;
                        }
                        check = true;
                        Holder[i].Anchor = (AnchorStyles.Top);
                        Holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        Holder[i].Location = new Point(horizontal, vertical);
                        horizontal += Holder[i].Width;
                        Holder[i].Visible = true;
                        this.Controls.Add(botThreePanel);
                        this.botThreePanel.Location = new Point(Holder[6].Left - 10, Holder[6].Top - 10);
                        this.botThreePanel.BackColor = Color.DarkBlue;
                        this.botThreePanel.Height = 150;
                        this.botThreePanel.Width = 180;
                        this.botThreePanel.Visible = false;
                        if (i == 7)
                        {
                            check = false;
                        }
                    }
                }
                if (botFour.Chips > 0)
                {
                    foldedPlayers--;
                    if (i >= 8 && i < 10)
                    {
                        if (Holder[8].Tag != null)
                        {
                            Holder[9].Tag = reserveArray[9];
                        }
                        Holder[8].Tag = reserveArray[8];
                        if (!check)
                        {
                            horizontal = 1115;
                            vertical = 65;
                        }
                        check = true;
                        Holder[i].Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                        Holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        Holder[i].Location = new Point(horizontal, vertical);
                        horizontal += Holder[i].Width;
                        Holder[i].Visible = true;
                        this.Controls.Add(botFourPanel);
                        this.botFourPanel.Location = new Point(Holder[8].Left - 10, Holder[8].Top - 10);
                        this.botFourPanel.BackColor = Color.DarkBlue;
                        this.botFourPanel.Height = 150;
                        this.botFourPanel.Width = 180;
                        this.botFourPanel.Visible = false;
                        if (i == 9)
                        {
                            check = false;
                        }
                    }
                }
                if (botFive.Chips > 0)
                {
                    foldedPlayers--;
                    if (i >= 10 && i < 12)
                    {
                        if (Holder[10].Tag != null)
                        {
                            Holder[11].Tag = reserveArray[11];
                        }
                        Holder[10].Tag = reserveArray[10];
                        if (!check)
                        {
                            horizontal = 1160;
                            vertical = 420;
                        }
                        check = true;
                        Holder[i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
                        Holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        this.Holder[i].Location = new Point(horizontal, vertical);
                        horizontal += Holder[i].Width;
                        this.Holder[i].Visible = true;
                        this.Controls.Add(botFivePanel);
                        this.botFivePanel.Location = new Point(Holder[10].Left - 10, Holder[10].Top - 10);
                        this.botFivePanel.BackColor = Color.DarkBlue;
                        this.botFivePanel.Height = 150;
                        this.botFivePanel.Width = 180;
                        this.botFivePanel.Visible = false;
                        if (i == 11)
                        {
                            check = false;
                        }
                    }
                }
                if (i >= 12)
                {
                    Holder[12].Tag = reserveArray[12];
                    if (i > 12) Holder[13].Tag = reserveArray[13];
                    if (i > 13) Holder[14].Tag = reserveArray[14];
                    if (i > 14) Holder[15].Tag = reserveArray[15];
                    if (i > 15)
                    {
                        Holder[16].Tag = reserveArray[16];

                    }
                    if (!check)
                    {
                        horizontal = 410;
                        vertical = 265;
                    }
                    check = true;
                    if (Holder[i] != null)
                    {
                        Holder[i].Anchor = AnchorStyles.None;
                        Holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        Holder[i].Location = new Point(horizontal, vertical);
                        horizontal += 110;
                    }
                }
                #endregion
                if (this.botOne.Chips <= 0)
                {
                    this.botOne.OutOfChips = true;
                    this.Holder[2].Visible = false;
                    this.Holder[3].Visible = false;
                }
                else
                {
                    this.botOne.OutOfChips = false;
                    if (i == 3)
                    {
                        if (this.Holder[3] != null)
                        {
                            this.Holder[2].Visible = true;
                            this.Holder[3].Visible = true;
                        }
                    }
                }
                if (this.botTwo.Chips <= 0)
                {
                    this.botTwo.OutOfChips = true;
                    this.Holder[4].Visible = false;
                    this.Holder[5].Visible = false;
                }
                else
                {
                    this.botTwo.OutOfChips = false;
                    if (i == 5)
                    {
                        if (this.Holder[5] != null)
                        {
                            this.Holder[4].Visible = true;
                            this.Holder[5].Visible = true;
                        }
                    }
                }
                if (this.botThree.Chips <= 0)
                {
                    this.botThree.OutOfChips = true;
                    this.Holder[6].Visible = false;
                    this.Holder[7].Visible = false;
                }
                else
                {
                    this.botThree.OutOfChips = false;
                    if (i == 7)
                    {
                        if (this.Holder[7] != null)
                        {
                            this.Holder[6].Visible = true;
                            this.Holder[7].Visible = true;
                        }
                    }
                }
                if (this.botFour.Chips <= 0)
                {
                    this.botFour.OutOfChips = true;
                    this.Holder[8].Visible = false;
                    this.Holder[9].Visible = false;
                }
                else
                {
                    this.botFour.OutOfChips = false;
                    if (i == 9)
                    {
                        if (this.Holder[9] != null)
                        {
                            this.Holder[8].Visible = true;
                            this.Holder[9].Visible = true;
                        }
                    }
                }
                if (this.botFive.Chips <= 0)
                {
                    this.botFive.OutOfChips = true;
                    this.Holder[10].Visible = false;
                    this.Holder[11].Visible = false;
                }
                else
                {
                    this.botFive.OutOfChips = false;
                    if (i == 11)
                    {
                        if (this.Holder[11] != null)
                        {
                            this.Holder[10].Visible = true;
                            this.Holder[11].Visible = true;
                        }
                    }
                }
                if (i == 16)
                {
                    if (!restart)
                    {
                        this.MaximizeBox = true;
                        this.MinimizeBox = true;
                    }
                    this.timer.Start();
                }
            }
            if (this.foldedPlayers == 5)
            {
                DialogResult dialogResult = MessageBox.Show("Would You Like To Play Again ?", "You Won , Congratulations ! ", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Application.Restart();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            else
            {
                this.foldedPlayers = 5;
            }
            if (i == 17)
            {
                this.raiseButton.Enabled = true;
                this.callButton.Enabled = true;
                this.raiseButton.Enabled = true;
                this.raiseButton.Enabled = true;
                this.foldButton.Enabled = true;
            }
        }

        async Task Turns()
        {
            #region Rotating
            if (!this.player.OutOfChips && this.player.CanPlay)
            {
                this.player.FixCall(ref this.globalCall, ref this.globalRaise, 1, this.globalRounds, ref this.callButton);
                //MessageBox.Show("Player's Turn");
                this.progressBarTimer.Visible = true;
                this.progressBarTimer.Value = 1000;
                this.t = 60;
                this.up = 10000000;
                this.timer.Start();
                this.raiseButton.Enabled = true;
                this.callButton.Enabled = true;
                this.raiseButton.Enabled = true;
                this.raiseButton.Enabled = true;
                this.foldButton.Enabled = true;
                this.turnCount++;
                this.player.FixCall(ref this.globalCall, ref this.globalRaise, 1, this.globalRounds, ref this.callButton);
            }

            if (this.player.OutOfChips || !this.player.CanPlay)
            {
                await AllIn();
                if (this.player.OutOfChips && !this.player.Folded)
                {
                    if (this.callButton.Text.Contains("All in") == false || this.raiseButton.Text.Contains("All in") == false)
                    {
                        this.bools.RemoveAt(0);
                        this.bools.Insert(0, null);
                        this.maxLeft--;
                        this.player.Folded = true;
                    }
                }

                await CheckRaise(0, 0);
                this.progressBarTimer.Visible = false;
                this.raiseButton.Enabled = false;
                this.callButton.Enabled = false;
                this.raiseButton.Enabled = false;
                this.raiseButton.Enabled = false;
                this.foldButton.Enabled = false;
                this.timer.Stop();

                this.botOne.CanPlay = true;

                for (int botNumber = 1; botNumber <= NumberOfBots; botNumber++)
                {
                    IPlayer currentBot = this.pokerDatabase.TakeBotByIndex(botNumber - 1);
                    if (!currentBot.OutOfChips && currentBot.CanPlay)
                    {
                        currentBot.FixCall(ref this.globalCall, ref this.globalRaise, 1, this.globalRounds, ref this.callButton);
                        currentBot.FixCall(ref this.globalCall, ref this.globalRaise, 2, this.globalRounds, ref this.callButton);

                        int cardOne = this.GetCardOne(botNumber);
                        int cardTwo = this.GetCardTwo(botNumber);
                        this.Rules(cardOne, cardTwo, string.Format("Bot {0}", botNumber), currentBot);
                        MessageBox.Show(string.Format("Bot {0}'s Turn", botNumber));
                        (currentBot as IBot).AI();

                        this.turnCount++;
                        this.last = botNumber;
                        currentBot.CanPlay = false;
                        this.pokerDatabase.TakeBotByIndex(botNumber).CanPlay = true;
                    }
                    if (currentBot.OutOfChips && !currentBot.Folded)
                    {
                        this.bools.RemoveAt(botNumber);
                        this.bools.Insert(botNumber, null);
                        this.maxLeft--;
                        currentBot.Folded = true;
                    }
                    if (currentBot.OutOfChips || !currentBot.CanPlay)
                    {
                        await CheckRaise(botNumber, botNumber);
                        this.pokerDatabase.TakeBotByIndex(botNumber).CanPlay = true;
                    }
                }

                if (this.player.OutOfChips && !this.player.Folded)
                {
                    if (this.callButton.Text.Contains("All in") == false || this.raiseButton.Text.Contains("All in") == false)
                    {
                        this.bools.RemoveAt(0);
                        this.bools.Insert(0, null);
                        this.maxLeft--;
                        this.player.Folded = true;
                    }
                }
            #endregion
                await AllIn();
                if (!this.restart)
                {
                    await Turns();
                }
                this.restart = false;
            }
        }

        private int GetCardOne(int botNumber)
        {
            switch (botNumber)
            {
                case 1: return BotOneCardOne;
                case 2: return BotTwoCardOne;
                case 3: return BotThreeCardOne;
                case 4: return BotFourCardOne;
                case 5: return BotFiveCardOne;
                default:
                    {
                        throw new ArgumentException("Unknown bot number.");
                    }
            }
        }

        private int GetCardTwo(int botNumber)
        {
            switch (botNumber)
            {
                case 1: return BotOneCardTwo;
                case 2: return BotTwoCardTwo;
                case 3: return BotThreeCardTwo;
                case 4: return BotFourCardTwo;
                case 5: return BotFiveCardTwo;
                default:
                    {
                        throw new ArgumentException("Unknown bot number.");
                    }
            }
        }

        private void Rules(int card1, int card2, string currentText, IPlayer currentPlayer)
        {
            if (card1 == 0 && card2 == 1)
            {
            }
            if (!currentPlayer.OutOfChips || card1 == 0 && card2 == 1 && this.playerStatus.Text.Contains("Fold") == false)
            {
                #region Variables
                bool done = false;
                bool vf = false;
                int[] StraightOne = new int[5];
                int[] StraightTwo = new int[7];
                StraightTwo[0] = reserveArray[card1];
                StraightTwo[1] = reserveArray[card2];
                StraightOne[0] = StraightTwo[2] = reserveArray[12];
                StraightOne[1] = StraightTwo[3] = reserveArray[13];
                StraightOne[2] = StraightTwo[4] = reserveArray[14];
                StraightOne[3] = StraightTwo[5] = reserveArray[15];
                StraightOne[4] = StraightTwo[6] = reserveArray[16];
                var a = StraightTwo.Where(o => o % 4 == 0).ToArray();
                var b = StraightTwo.Where(o => o % 4 == 1).ToArray();
                var c = StraightTwo.Where(o => o % 4 == 2).ToArray();
                var d = StraightTwo.Where(o => o % 4 == 3).ToArray();
                var st1 = a.Select(o => o / 4).Distinct().ToArray();
                var st2 = b.Select(o => o / 4).Distinct().ToArray();
                var st3 = c.Select(o => o / 4).Distinct().ToArray();
                var st4 = d.Select(o => o / 4).Distinct().ToArray();
                Array.Sort(StraightTwo);
                Array.Sort(st1);
                Array.Sort(st2);
                Array.Sort(st3);
                Array.Sort(st4);
                #endregion
                for (i = 0; i < 16; i++)
                {
                    if (reserveArray[i] == int.Parse(Holder[card1].Tag.ToString()) && reserveArray[i + 1] == int.Parse(Holder[card2].Tag.ToString()))
                    {
                        CardCombinations.rPairFromHand(currentPlayer, i, this.winList, this.reserveArray, ref this.sorted);

                        CardCombinations.rPairTwoPair(currentPlayer, i, this.winList, this.reserveArray, ref this.sorted);

                        CardCombinations.rTwoPair(currentPlayer, i, this.winList, this.reserveArray, ref this.sorted);

                        CardCombinations.rThreeOfAKind(currentPlayer, this.winList, this.reserveArray, ref this.sorted, StraightTwo);

                        CardCombinations.rStraight(currentPlayer, this.winList, this.reserveArray, ref this.sorted, StraightTwo);

                        CardCombinations.rFlush(currentPlayer, i, ref vf, this.winList, this.reserveArray, ref this.sorted, StraightOne);

                        CardCombinations.rFullHouse(currentPlayer, ref this.type, ref done, this.winList, this.reserveArray, ref this.sorted, StraightTwo);

                        CardCombinations.rFourOfAKind(currentPlayer, this.winList, this.reserveArray, ref this.sorted, StraightTwo);

                        CardCombinations.rStraightFlush(currentPlayer, this.winList, this.reserveArray, ref this.sorted, st1, st2, st3, st4);

                        CardCombinations.rHighCard(currentPlayer, i, this.winList, this.reserveArray, ref this.sorted);
                    }
                }
            }
        }

        void Winner(double current, double Power, string currentText, int chips, string lastly)
        {
            if (lastly == " ")
            {
                lastly = "Bot 5";
            }

            for (int j = 0; j <= 16; j++)
            {
                //await Task.Delay(5);
                if (Holder[j].Visible)
                    Holder[j].Image = Deck[j];
            }

            if (current == sorted.Current)
            {
                if (Power == sorted.Power)
                {
                    winners++;
                    CheckWinners.Add(currentText);
                    if (current == -1)
                    {
                        MessageBox.Show(currentText + " High Card ");
                    }
                    if (current == 1 || current == 0)
                    {
                        MessageBox.Show(currentText + " Pair ");
                    }
                    if (current == 2)
                    {
                        MessageBox.Show(currentText + " Two Pair ");
                    }
                    if (current == 3)
                    {
                        MessageBox.Show(currentText + " Three of a Kind ");
                    }
                    if (current == 4)
                    {
                        MessageBox.Show(currentText + " Straight ");
                    }
                    if (current == 5 || current == 5.5)
                    {
                        MessageBox.Show(currentText + " Flush ");
                    }
                    if (current == 6)
                    {
                        MessageBox.Show(currentText + " Full House ");
                    }
                    if (current == 7)
                    {
                        MessageBox.Show(currentText + " Four of a Kind ");
                    }
                    if (current == 8)
                    {
                        MessageBox.Show(currentText + " Straight Flush ");
                    }
                    if (current == 9)
                    {
                        MessageBox.Show(currentText + " Royal Flush ! ");
                    }
                }
            }
            if (currentText == lastly)//lastfixed
            {
                if (winners > 1)
                {
                    if (CheckWinners.Contains("Player"))
                    {
                        Chips += int.Parse(potTexBox.Text) / winners;
                        chipsTexBox.Text = Chips.ToString();
                        //pPanel.Visible = true;

                    }
                    if (CheckWinners.Contains("Bot 1"))
                    {
                        botOne.Chips += int.Parse(potTexBox.Text) / winners;
                        botOneChips.Text = botOne.Chips.ToString();
                        //b1Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 2"))
                    {
                        botTwo.Chips += int.Parse(potTexBox.Text) / winners;
                        botTwoChips.Text = botTwo.Chips.ToString();
                        //b2Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 3"))
                    {
                        botThree.Chips += int.Parse(potTexBox.Text) / winners;
                        botThreeChips.Text = botThree.Chips.ToString();
                        //b3Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 4"))
                    {
                        botFour.Chips += int.Parse(potTexBox.Text) / winners;
                        botFourChips.Text = botFour.Chips.ToString();
                        //b4Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 5"))
                    {
                        botFive.Chips += int.Parse(potTexBox.Text) / winners;
                        botFiveChips.Text = botFive.Chips.ToString();
                        //b5Panel.Visible = true;
                    }
                    //await Finish(1);
                }
                if (winners == 1)
                {
                    if (CheckWinners.Contains("Player"))
                    {
                        Chips += int.Parse(potTexBox.Text);
                        //await Finish(1);
                        //pPanel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 1"))
                    {
                        botOne.Chips += int.Parse(potTexBox.Text);
                        //await Finish(1);
                        //b1Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 2"))
                    {
                        botTwo.Chips += int.Parse(potTexBox.Text);
                        //await Finish(1);
                        //b2Panel.Visible = true;

                    }
                    if (CheckWinners.Contains("Bot 3"))
                    {
                        botThree.Chips += int.Parse(potTexBox.Text);
                        //await Finish(1);
                        //b3Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 4"))
                    {
                        botFour.Chips += int.Parse(potTexBox.Text);
                        //await Finish(1);
                        //b4Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 5"))
                    {
                        botFive.Chips += int.Parse(potTexBox.Text);
                        //await Finish(1);
                        //b5Panel.Visible = true;
                    }
                }
            }
        }

        async Task CheckRaise(int currentTurn, int raiseTurn)
        {
            if (raising)
            {
                this.turnCount = 0;
                this.raising = false;
                this.raisedTurn = currentTurn;
                this.changed = true;
            }
            else
            {
                if (turnCount >= maxLeft - 1 || !changed && turnCount == maxLeft)
                {
                    if (currentTurn == raisedTurn - 1 || !changed && turnCount == maxLeft || raisedTurn == 0 && currentTurn == 5)
                    {
                        this.changed = false;
                        this.turnCount = 0;
                        this.globalRaise = 0;
                        this.globalCall = 0;
                        this.raisedTurn = 123;
                        this.globalRounds++;
                        if (!player.OutOfChips)
                            playerStatus.Text = "";
                        if (!this.botOne.OutOfChips)
                            botOneStatus.Text = "";
                        if (!this.botTwo.OutOfChips)
                            botTwoStatus.Text = "";
                        if (!this.botThree.OutOfChips)
                            botThreeStatus.Text = "";
                        if (!this.botFour.OutOfChips)
                            botFourStatus.Text = "";
                        if (!this.botFive.OutOfChips)
                            botFiveStatus.Text = "";
                    }
                }
            }
            if (globalRounds == Flop)
            {
                for (int j = 12; j <= 14; j++)
                {
                    if (Holder[j].Image != Deck[j])
                    {
                        Holder[j].Image = Deck[j];
                        player.Call = 0; player.Raise = 0;
                        this.EraseBotCall();
                        this.EraseBotRaise();
                    }
                }
            }
            if (globalRounds == Turn)
            {
                for (int j = 14; j <= 15; j++)
                {
                    if (Holder[j].Image != Deck[j])
                    {
                        Holder[j].Image = Deck[j];
                        player.Call = 0; player.Raise = 0;
                        this.EraseBotCall();
                        this.EraseBotRaise();
                    }
                }
            }
            if (globalRounds == River)
            {
                for (int j = 15; j <= 16; j++)
                {
                    if (Holder[j].Image != Deck[j])
                    {
                        Holder[j].Image = Deck[j];
                        player.Call = 0; player.Raise = 0;
                        this.EraseBotRaise();
                        this.EraseBotCall();
                    }
                }
            }
            if (globalRounds == End && maxLeft == 6)
            {
                string fixedLast = "qwerty";
                if (!playerStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Player";
                    Rules(0, 1, "Player", this.player);
                }
                if (!botOneStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 1";
                    Rules(2, 3, "Bot 1", this.botOne);
                }
                if (!botTwoStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 2";
                    Rules(4, 5, "Bot 2", this.botTwo);
                }
                if (!botThreeStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 3";
                    Rules(6, 7, "Bot 3", this.botThree);
                }
                if (!botFourStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 4";
                    Rules(8, 9, "Bot 4", this.botFour);
                }
                if (!botFiveStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 5";
                    Rules(10, 11, "Bot 5", this.botFive);
                }

                this.Winner(player.Type, player.Power, "Player", Chips, fixedLast);
                this.Winner(this.botOne.Type, this.botOne.Power, "Bot 1", botOne.Chips, fixedLast);
                this.Winner(this.botTwo.Type, this.botTwo.Power, "Bot 2", botTwo.Chips, fixedLast);
                this.Winner(this.botThree.Type, this.botThree.Power, "Bot 3", botThree.Chips, fixedLast);
                this.Winner(this.botFour.Type, this.botFour.Power, "Bot 4", botFour.Chips, fixedLast);
                this.Winner(this.botFive.Type, this.botFive.Power, "Bot 5", botFive.Chips, fixedLast);
                this.restart = true;
                this.player.CanPlay = true;
                this.player.OutOfChips = false;

                this.EnableBotChips();

                if (this.Chips <= 0)
                {
                    AddChips f2 = new AddChips();
                    f2.ShowDialog();
                    if (f2.a != 0)
                    {
                        this.Chips = f2.a;
                        this.botOne.Chips += f2.a;
                        this.botTwo.Chips += f2.a;
                        this.botThree.Chips += f2.a;
                        this.botFour.Chips += f2.a;
                        this.botFive.Chips += f2.a;
                        this.player.OutOfChips = false;
                        this.player.CanPlay = true;
                        this.raiseButton.Enabled = true;
                        this.foldButton.Enabled = true;
                        this.checkButton.Enabled = true;
                        this.raiseButton.Text = "Raise";
                    }
                }

                this.DisableBotPanel();
                this.EraseBotCall();
                this.EraseBotRaise();
                this.EraseBotPower();
                this.EraseBotType();

                this.playerPanel.Visible = false;
                this.player.Call = 0;
                this.player.Raise = 0;
                this.player.Power = 0;
                this.player.Type = -1;
                this.playerStatus.Text = "";

                this.last = 0;
                this.globalCall = bb;
                this.globalRaise = 0;
                this.ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
                this.bools.Clear();
                this.globalRounds = 0;
                this.type = 0;
                this.winners = 0;
                this.sorted.Current = 0;
                this.sorted.Power = 0;
                this.potTexBox.Text = "0";

                this.winList.Clear();
                this.ints.Clear();
                this.CheckWinners.Clear();
                for (int os = 0; os < 17; os++)
                {
                    this.Holder[os].Image = null;
                    this.Holder[os].Invalidate();
                    this.Holder[os].Visible = false;
                }

                await Shuffle();
                await Turns();
            }
        }

        async Task AllIn()
        {
            #region All in
            if (Chips <= 0 && !intsadded)
            {
                if (playerStatus.Text.Contains("Raise"))
                {
                    ints.Add(Chips);
                    intsadded = true;
                }
                if (playerStatus.Text.Contains("Call"))
                {
                    ints.Add(Chips);
                    intsadded = true;
                }
            }
            intsadded = false;
            if (botOne.Chips <= 0 && !this.botOne.OutOfChips)
            {
                if (!intsadded)
                {
                    ints.Add(botOne.Chips);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (botTwo.Chips <= 0 && !this.botTwo.OutOfChips)
            {
                if (!intsadded)
                {
                    ints.Add(botTwo.Chips);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (botThree.Chips <= 0 && !this.botThree.OutOfChips)
            {
                if (!intsadded)
                {
                    ints.Add(botThree.Chips);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (botFour.Chips <= 0 && !this.botFour.OutOfChips)
            {
                if (!intsadded)
                {
                    ints.Add(botFour.Chips);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (botFive.Chips <= 0 && !this.botFive.OutOfChips)
            {
                if (!intsadded)
                {
                    ints.Add(botFive.Chips);
                    intsadded = true;
                }
            }
            if (ints.ToArray().Length == maxLeft)
            {
                await Finish(2);
            }
            else
            {
                ints.Clear();
            }
            #endregion

            var abc = bools.Count(x => x == false);

            #region LastManStanding
            if (abc == 1)
            {
                int index = bools.IndexOf(false);
                if (index == 0)
                {
                    Chips += int.Parse(potTexBox.Text);
                    chipsTexBox.Text = Chips.ToString();
                    playerPanel.Visible = true;
                    MessageBox.Show("Player Wins");
                }
                if (index == 1)
                {
                    botOne.Chips += int.Parse(potTexBox.Text);
                    chipsTexBox.Text = botOne.Chips.ToString();
                    botOnePanel.Visible = true;
                    MessageBox.Show("Bot 1 Wins");
                }
                if (index == 2)
                {
                    botTwo.Chips += int.Parse(potTexBox.Text);
                    chipsTexBox.Text = botTwo.Chips.ToString();
                    botTwoPanel.Visible = true;
                    MessageBox.Show("Bot 2 Wins");
                }
                if (index == 3)
                {
                    botThree.Chips += int.Parse(potTexBox.Text);
                    chipsTexBox.Text = botThree.Chips.ToString();
                    botThreePanel.Visible = true;
                    MessageBox.Show("Bot 3 Wins");
                }
                if (index == 4)
                {
                    botFour.Chips += int.Parse(potTexBox.Text);
                    chipsTexBox.Text = botFour.Chips.ToString();
                    botFourPanel.Visible = true;
                    MessageBox.Show("Bot 4 Wins");
                }
                if (index == 5)
                {
                    botFive.Chips += int.Parse(potTexBox.Text);
                    chipsTexBox.Text = botFive.Chips.ToString();
                    botFivePanel.Visible = true;
                    MessageBox.Show("Bot 5 Wins");
                }
                for (int j = 0; j <= 16; j++)
                {
                    Holder[j].Visible = false;
                }
                await Finish(1);
            }
            intsadded = false;
            #endregion

            #region FiveOrLessLeft
            if (abc < 6 && abc > 1 && globalRounds >= End)
            {
                await Finish(2);
            }
            #endregion
        }

        async Task Finish(int n)
        {
            if (n == 2)
            {
                FixWinners();
            }

            this.DisableBots();
            this.EraseBotCall();
            this.EraseBotRaise();
            this.DisableBotPanel();
            this.EraseBotPower();
            this.EraseBotType();
            this.EraseBotStatusText();
            this.UnFoldBots();
            this.EnableBotChips();

            this.ErasePlayerStats();
            this.DisablePlayer();

            this.EraseGameStats();
            
            if (Chips <= 0)
            {
                AddChips f2 = new AddChips();
                f2.ShowDialog();
                if (f2.a != 0)
                {
                    Chips = f2.a;
                    botOne.Chips += f2.a;
                    botTwo.Chips += f2.a;
                    botThree.Chips += f2.a;
                    botFour.Chips += f2.a;
                    botFive.Chips += f2.a;
                    player.OutOfChips = false;
                    this.player.CanPlay = true;
                    raiseButton.Enabled = true;
                    foldButton.Enabled = true;
                    checkButton.Enabled = true;
                    raiseButton.Text = "Raise";
                }
            }

            ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
            for (int os = 0; os < 17; os++)
            {
                Holder[os].Image = null;
                Holder[os].Invalidate();
                Holder[os].Visible = false;
            }
            await Shuffle();
            //await Turns();
        }

        private void EraseBotType()
        {
            for (int i = 0; i < NumberOfBots; i++)
            {
                this.pokerDatabase.TakeBotByIndex(i).Type = -1;
            }
        }

        private void EraseBotPower()
        {
            for (int i = 0; i < NumberOfBots; i++)
            {
                this.pokerDatabase.TakeBotByIndex(i).Power = 0;
            }
        }

        private void DisableBotPanel()
        {
            this.botOnePanel.Visible = false;
            this.botTwoPanel.Visible = false;
            this.botThreePanel.Visible = false;
            this.botFourPanel.Visible = false;
            this.botFivePanel.Visible = false;
        }

        private void EraseBotRaise()
        {
            for (int i = 0; i < NumberOfBots; i++)
            {
                this.pokerDatabase.TakeBotByIndex(i).Raise = 0;
            }
        }

        private void EraseBotCall()
        {
            for (int i = 0; i < NumberOfBots; i++)
            {
                this.pokerDatabase.TakeBotByIndex(i).Call = 0;
            }
        }

        private void EraseBotStatusText()
        {
            for (int i = 0; i < NumberOfBots; i++)
            {
                this.pokerDatabase.TakeBotByIndex(i).Status.Text = "";
            }
        }

        private void UnFoldBots()
        {
            for (int i = 0; i < NumberOfBots; i++)
            {
                this.pokerDatabase.TakeBotByIndex(i).Folded = false;
            }
        }

        private void EnableBotChips()
        {
            for (int i = 0; i < NumberOfBots; i++)
            {
                this.pokerDatabase.TakeBotByIndex(i).OutOfChips = false;
            }
        }

        private void DisableBots()
        {
            for (int i = 0; i < NumberOfBots; i++)
            {
                this.pokerDatabase.TakeBotByIndex(i).CanPlay = false;
            }
        }

        private void DisablePlayer()
        {
            this.player.Folded = false;
            this.player.CanPlay = true;
            this.player.OutOfChips = false;
        }

        private void EraseGameStats()
        {
            this.globalCall = bb;
            this.globalRaise = 0;
            this.globalRounds = 0;
            this.globalRaise = 0;
            this.restart = false;
            this.raising = false;
            this.height = 0;
            this.width = 0;
            this.winners = 0;
            this.Flop = 1;
            this.Turn = 2;
            this.River = 3;
            this.End = 4;
            this.maxLeft = 6;
            this.last = 123;
            this.raisedTurn = 1;
            this.bools.Clear();
            this.CheckWinners.Clear();
            this.ints.Clear();
            this.winList.Clear();
            this.sorted.Current = 0;
            this.sorted.Power = 0;
            this.potTexBox.Text = "0";
            this.t = 60; 
            this.up = 10000000;
            this.turnCount = 0;
            this.foldedPlayers = 5;
            this.type = 0; 
        }

        private void ErasePlayerStats()
        {
            this.playerStatus.Text = "";
            this.player.Power = 0;
            this.player.Type = -1;
            this.player.Raise = 0;
            this.player.Call = 0;
            this.playerPanel.Visible = false;
        }

        void FixWinners()
        {
            this.winList.Clear();
            this.sorted.Current = 0;
            this.sorted.Power = 0;
            string fixedLast = "qwerty";
            if (!this.playerStatus.Text.Contains("Fold"))
            {
                fixedLast = "Player";
                this.Rules(0, 1, "Player", this.player);
            }
            if (!this.botOneStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 1";
                this.Rules(2, 3, "Bot 1", this.botOne);
            }
            if (!this.botTwoStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 2";
                this.Rules(4, 5, "Bot 2", this.botTwo);
            }
            if (!this.botThreeStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 3";
                this.Rules(6, 7, "Bot 3", this.botThree);
            }
            if (!this.botFourStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 4";
                this.Rules(8, 9, "Bot 4", this.botFour);
            }
            if (!this.botFiveStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 5";
                this.Rules(10, 11, "Bot 5", this.botFive);
            }

            this.Winner(this.player.Type, player.Power, "Player", Chips, fixedLast);
            this.Winner(this.botOne.Type, this.botOne.Power, "Bot 1", botOne.Chips, fixedLast);
            this.Winner(this.botTwo.Type, this.botTwo.Power, "Bot 2", botTwo.Chips, fixedLast);
            this.Winner(this.botThree.Type, this.botThree.Power, "Bot 3", botThree.Chips, fixedLast);
            this.Winner(this.botFour.Type, this.botFour.Power, "Bot 4", botFour.Chips, fixedLast);
            this.Winner(this.botFive.Type, this.botFive.Power, "Bot 5", botFive.Chips, fixedLast);
        }

        #region UI
        private async void timer_Tick(object sender, object e)
        {
            if (progressBarTimer.Value <= 0)
            {
                player.OutOfChips = true;
                await Turns();
            }
            if (t > 0)
            {
                t--;
                progressBarTimer.Value = (t / 6) * 100;
            }
        }

        private void Update_Tick(object sender, object e)
        {
            if (Chips <= 0)
            {
                chipsTexBox.Text = "Chips : 0";
            }
            if (botOne.Chips <= 0)
            {
                botOneChips.Text = "Chips : 0";
            }
            if (botTwo.Chips <= 0)
            {
                botTwoChips.Text = "Chips : 0";
            }
            if (botThree.Chips <= 0)
            {
                botThreeChips.Text = "Chips : 0";
            }
            if (botFour.Chips <= 0)
            {
                botFourChips.Text = "Chips : 0";
            }
            if (botFive.Chips <= 0)
            {
                botFiveChips.Text = "Chips : 0";
            }
            chipsTexBox.Text = "Chips : " + Chips.ToString();
            botOneChips.Text = "Chips : " + botOne.Chips.ToString();
            botTwoChips.Text = "Chips : " + botTwo.Chips.ToString();
            botThreeChips.Text = "Chips : " + botThree.Chips.ToString();
            botFourChips.Text = "Chips : " + botFour.Chips.ToString();
            botFiveChips.Text = "Chips : " + botFive.Chips.ToString();
            if (Chips <= 0)
            {
                player.CanPlay = false;
                player.OutOfChips = true;
                callButton.Enabled = false;
                raiseButton.Enabled = false;
                foldButton.Enabled = false;
                checkButton.Enabled = false;
            }
            if (up > 0)
            {
                up--;
            }
            if (Chips >= globalCall)
            {
                callButton.Text = "Call " + globalCall.ToString();
            }
            else
            {
                callButton.Text = "All in";
                raiseButton.Enabled = false;
            }
            if (globalCall > 0)
            {
                checkButton.Enabled = false;
            }
            if (globalCall <= 0)
            {
                checkButton.Enabled = true;
                callButton.Text = "Call";
                callButton.Enabled = false;
            }
            if (Chips <= 0)
            {
                raiseButton.Enabled = false;
            }
            int parsedValue;

            if (this.raiseTexBox.Text != "" && int.TryParse(this.raiseTexBox.Text, out parsedValue))
            {
                if (Chips <= int.Parse(this.raiseTexBox.Text))
                {
                    raiseButton.Text = "All in";
                }
                else
                {
                    raiseButton.Text = "Raise";
                }
            }
            if (Chips < globalCall)
            {
                raiseButton.Enabled = false;
            }
        }

        private async void bFold_Click(object sender, EventArgs e)
        {
            playerStatus.Text = "Fold";
            player.CanPlay = false;
            player.OutOfChips = true;
            await Turns();
        }

        private async void bCheck_Click(object sender, EventArgs e)
        {
            if (globalCall <= 0)
            {
                player.CanPlay = false;
                playerStatus.Text = "Check";
            }
            else
            {
                //playerStatus.Text = "All in " + Chips;

                checkButton.Enabled = false;
            }
            await Turns();
        }

        private async void bCall_Click(object sender, EventArgs e)
        {
            Rules(0, 1, "Player", this.player);
            if (Chips >= globalCall)
            {
                Chips -= globalCall;
                chipsTexBox.Text = "Chips : " + Chips.ToString();
                if (potTexBox.Text != "")
                {
                    potTexBox.Text = (int.Parse(potTexBox.Text) + globalCall).ToString();
                }
                else
                {
                    potTexBox.Text = globalCall.ToString();
                }
                player.CanPlay = false;
                playerStatus.Text = "Call " + globalCall;
                player.Call = globalCall;
            }
            else if (Chips <= globalCall && globalCall > 0)
            {
                potTexBox.Text = (int.Parse(potTexBox.Text) + Chips).ToString();
                playerStatus.Text = "All in " + Chips;
                Chips = 0;
                chipsTexBox.Text = "Chips : " + Chips.ToString();
                player.CanPlay = false;
                foldButton.Enabled = false;
                player.Call = Chips;
            }
            await Turns();
        }

        private async void bRaise_Click(object sender, EventArgs e)
        {
            Rules(0, 1, "Player", this.player);
            int parsedValue;
            if (this.raiseTexBox.Text != "" && int.TryParse(this.raiseTexBox.Text, out parsedValue))
            {
                if (Chips > globalCall)
                {
                    if (globalRaise * 2 > int.Parse(this.raiseTexBox.Text))
                    {
                        this.raiseTexBox.Text = (globalRaise * 2).ToString();
                        MessageBox.Show("You must raise atleast twice as the current raise !");
                        return;
                    }
                    else
                    {
                        if (Chips >= int.Parse(this.raiseTexBox.Text))
                        {
                            globalCall = int.Parse(this.raiseTexBox.Text);
                            globalRaise = int.Parse(this.raiseTexBox.Text);
                            playerStatus.Text = "Raise " + globalCall.ToString();
                            potTexBox.Text = (int.Parse(potTexBox.Text) + globalCall).ToString();
                            callButton.Text = "Call";
                            Chips -= int.Parse(this.raiseTexBox.Text);
                            raising = true;
                            last = 0;
                            player.Raise = Convert.ToInt32(globalRaise);
                        }
                        else
                        {
                            globalCall = Chips;
                            globalRaise = Chips;
                            potTexBox.Text = (int.Parse(potTexBox.Text) + Chips).ToString();
                            playerStatus.Text = "Raise " + globalCall.ToString();
                            Chips = 0;
                            raising = true;
                            last = 0;
                            player.Raise = Convert.ToInt32(globalRaise);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("This is a number only field");
                return;
            }
            player.CanPlay = false;
            await Turns();
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            if (addTexBox.Text == "") { }
            else
            {
                Chips += int.Parse(addTexBox.Text);
                botOne.Chips += int.Parse(addTexBox.Text);
                botTwo.Chips += int.Parse(addTexBox.Text);
                botThree.Chips += int.Parse(addTexBox.Text);
                botFour.Chips += int.Parse(addTexBox.Text);
                botFive.Chips += int.Parse(addTexBox.Text);
            }
            chipsTexBox.Text = "Chips : " + Chips.ToString();
        }

        private void bOptions_Click(object sender, EventArgs e)
        {
            tbBB.Text = bb.ToString();
            tbSB.Text = sb.ToString();
            if (tbBB.Visible == false)
            {
                tbBB.Visible = true;
                tbSB.Visible = true;
                bBB.Visible = true;
                buttonSB.Visible = true;
            }
            else
            {
                tbBB.Visible = false;
                tbSB.Visible = false;
                bBB.Visible = false;
                buttonSB.Visible = false;
            }
        }

        private void bSB_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (tbSB.Text.Contains(",") || tbSB.Text.Contains("."))
            {
                MessageBox.Show("The Small Blind can be only round number !");
                tbSB.Text = sb.ToString();
                return;
            }
            if (!int.TryParse(tbSB.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                tbSB.Text = sb.ToString();
                return;
            }
            if (int.Parse(tbSB.Text) > 100000)
            {
                MessageBox.Show("The maximum of the Small Blind is 100 000 $");
                tbSB.Text = sb.ToString();
            }
            if (int.Parse(tbSB.Text) < 250)
            {
                MessageBox.Show("The minimum of the Small Blind is 250 $");
            }
            if (int.Parse(tbSB.Text) >= 250 && int.Parse(tbSB.Text) <= 100000)
            {
                sb = int.Parse(tbSB.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void bBB_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (tbBB.Text.Contains(",") || tbBB.Text.Contains("."))
            {
                MessageBox.Show("The Big Blind can be only round number !");
                tbBB.Text = bb.ToString();
                return;
            }
            if (!int.TryParse(tbSB.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                tbSB.Text = bb.ToString();
                return;
            }
            if (int.Parse(tbBB.Text) > 200000)
            {
                MessageBox.Show("The maximum of the Big Blind is 200 000");
                tbBB.Text = bb.ToString();
            }
            if (int.Parse(tbBB.Text) < 500)
            {
                MessageBox.Show("The minimum of the Big Blind is 500 $");
            }
            if (int.Parse(tbBB.Text) >= 500 && int.Parse(tbBB.Text) <= 200000)
            {
                bb = int.Parse(tbBB.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void Layout_Change(object sender, LayoutEventArgs e)
        {
            width = this.Width;
            height = this.Height;
        }
        #endregion
    }
}