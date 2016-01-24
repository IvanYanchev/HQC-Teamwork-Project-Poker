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
        #region Private Variables
        private IPokerDatabase pokerDatabase = new PokerDatabase();

        private readonly IBot botOne = new Bot("Bot 1");
        private readonly IBot botTwo = new Bot("Bot 2");
        private readonly IBot botThree = new Bot("Bot 3");
        private readonly IBot botFour = new Bot("Bot 4");
        private readonly IBot botFive = new Bot("Bot 5");
        private IPlayer player = new Human();

        private const int NumberOfBots = 5;
        private const int NumberOfCards = 52;

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

        private ProgressBar progressBar = new ProgressBar();
        private Panel playerPanel = new Panel();
        private Panel botOnePanel = new Panel();
        private Panel botTwoPanel = new Panel();
        private Panel botThreePanel = new Panel();
        private Panel botFourPanel = new Panel();
        private Panel botFivePanel = new Panel();

        private int Nm;
        private int globalCall = 500;
        private int foldedPlayers = 5;
        private int Chips = 10000;
        private double globalType;
        private int globalRounds = 0;
        private int globalRaise = 0;

        private bool intsadded;
        private bool changed;
        private bool restart = false;
        private bool raising = false;

        private int globalHeight;
        private int globalWidth;
        private int winnersCount = 0;
        private int Flop = 1;
        private int Turn = 2;
        private int River = 3;
        private int End = 4;
        private int maxLeft = 6;
        private int last = 123;
        private int raisedTurn = 1;
        private int t = 60;
        private int i;
        private int bigBlind = 500;
        private int smallBlind = 250;
        private int maxChipsAmount = 10000000;
        private int turnCount = 0;

        private readonly List<bool?> disabledPlayers = new List<bool?>();
        private readonly List<PokerType> winList = new List<PokerType>();
        private readonly List<string> CheckWinners = new List<string>();
        private readonly List<int> ints = new List<int>();

        private readonly int[] reserveArray = new int[17];
        private readonly Image[] Deck = new Image[NumberOfCards];
        private readonly PictureBox[] Holder = new PictureBox[NumberOfCards];
        private readonly Timer timer = new Timer();
        private readonly Timer Updates = new Timer();
        #endregion

        private PokerType sorted;
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

        public GameTable()
        {
            this.globalCall = this.bigBlind;

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

            this.globalWidth = this.Width;
            this.globalHeight = this.Height;

            Shuffle();

            this.potTextBox.Enabled = false;
            this.chipsTexBox.Enabled = false;

            this.botOneChips.Enabled = false;
            this.botTwoChips.Enabled = false;
            this.botThreeChips.Enabled = false;
            this.botFourChips.Enabled = false;
            this.botFiveChips.Enabled = false;

            this.chipsTexBox.Text = "Chips : " + this.Chips.ToString();
            this.botOneChips.Text = "Chips : " + this.botOne.Chips.ToString();
            this.botTwoChips.Text = "Chips : " + this.botTwo.Chips.ToString();
            this.botThreeChips.Text = "Chips : " + this.botThree.Chips.ToString();
            this.botFourChips.Text = "Chips : " + this.botFour.Chips.ToString();
            this.botFiveChips.Text = "Chips : " + this.botFive.Chips.ToString();

            this.timer.Interval = (1 * 1 * 1000);
            this.timer.Tick += Timer_Tick;
            this.Updates.Interval = (1 * 1 * 100);

            this.Updates.Tick += Update_Tick;
            this.bigBlindTexBox.Visible = true;
            this.smallBlindTexBox.Visible = true;
            this.bigBlindButton.Visible = true;
            this.smallBlindButton.Visible = true;
            this.bigBlindTexBox.Visible = true;
            this.smallBlindTexBox.Visible = true;
            this.bigBlindButton.Visible = true;
            this.smallBlindButton.Visible = true;
            this.bigBlindTexBox.Visible = false;
            this.smallBlindTexBox.Visible = false;
            this.bigBlindButton.Visible = false;
            this.smallBlindButton.Visible = false;
            this.raiseTexBox.Text = (bigBlind * 2).ToString();
        }

        private async Task Shuffle()
        {
            this.disabledPlayers.Add(player.OutOfChips);
            this.disabledPlayers.Add(botOne.OutOfChips);
            this.disabledPlayers.Add(botTwo.OutOfChips);
            this.disabledPlayers.Add(botThree.OutOfChips);
            this.disabledPlayers.Add(botFour.OutOfChips);
            this.disabledPlayers.Add(botFive.OutOfChips);

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

        private async Task Turns()
        {
            if (!this.player.OutOfChips && this.player.CanPlay)
            {
                this.player.FixCall(ref this.globalCall, ref this.globalRaise, 1, this.globalRounds, ref this.callButton);
                //MessageBox.Show("Player's Turn");
                this.progressBarTimer.Visible = true;
                this.progressBarTimer.Value = 1000;
                this.t = 60;
                this.maxChipsAmount = 10000000;
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
                await this.AllIn();
                if (this.player.OutOfChips && !this.player.Folded)
                {
                    if (this.callButton.Text.Contains("All in") == false || this.raiseButton.Text.Contains("All in") == false)
                    {
                        this.disabledPlayers.RemoveAt(0);
                        this.disabledPlayers.Insert(0, null);
                        this.maxLeft--;
                        this.player.Folded = true;
                    }
                }

                await this.CheckRaise(0, 0);
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

                        int name = 0;
                        int cardOne = this.GetCardOne(botNumber - 1);
                        int cardTwo = this.GetCardTwo(botNumber - 1);
                        this.Rules(cardOne, cardTwo, currentBot.Name, currentBot);
                        MessageBox.Show(string.Format("Bot {0}'s Turn", currentBot.Name));
                        ActionManager.AI(currentBot, this.globalCall, this.potTextBox, ref this.globalRaise, ref this.raising, ref this.globalRounds, name);

                        this.turnCount++;
                        this.last = botNumber;
                        currentBot.CanPlay = false;
                        this.pokerDatabase.TakeBotByIndex(botNumber).CanPlay = true;
                    }
                    if (currentBot.OutOfChips && !currentBot.Folded)
                    {
                        this.disabledPlayers.RemoveAt(botNumber);
                        this.disabledPlayers.Insert(botNumber, null);
                        this.maxLeft--;
                        currentBot.Folded = true;
                    }
                    if (currentBot.OutOfChips || !currentBot.CanPlay)
                    {
                        await this.CheckRaise(botNumber, botNumber);
                        this.pokerDatabase.TakeBotByIndex(botNumber).CanPlay = true;
                    }
                }

                if (this.player.OutOfChips && !this.player.Folded)
                {
                    if (this.callButton.Text.Contains("All in") == false || this.raiseButton.Text.Contains("All in") == false)
                    {
                        this.disabledPlayers.RemoveAt(0);
                        this.disabledPlayers.Insert(0, null);
                        this.maxLeft--;
                        this.player.Folded = true;
                    }
                }
                await this.AllIn();
                if (!this.restart)
                {
                    await this.Turns();
                }
                this.restart = false;
            }
        }

        private int GetCardOne(int botNumber)
        {
            switch (botNumber)
            {
                case 0: return BotOneCardOne;
                case 1: return BotTwoCardOne;
                case 2: return BotThreeCardOne;
                case 3: return BotFourCardOne;
                case 4: return BotFiveCardOne;
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
                case 0: return BotOneCardTwo;
                case 1: return BotTwoCardTwo;
                case 2: return BotThreeCardTwo;
                case 3: return BotFourCardTwo;
                case 4: return BotFiveCardTwo;
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

                        CardCombinations.rFullHouse(currentPlayer, ref this.globalType, ref done, this.winList, this.reserveArray, ref this.sorted, StraightTwo);

                        CardCombinations.rFourOfAKind(currentPlayer, this.winList, this.reserveArray, ref this.sorted, StraightTwo);

                        CardCombinations.rStraightFlush(currentPlayer, this.winList, this.reserveArray, ref this.sorted, st1, st2, st3, st4);

                        CardCombinations.rHighCard(currentPlayer, i, this.winList, this.reserveArray, ref this.sorted);
                    }
                }
            }
        }

        private void Winner(double current, double Power, string currentText, int chips, string lastly)
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
                    winnersCount++;
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
                if (winnersCount > 1)
                {
                    if (CheckWinners.Contains("Player"))
                    {
                        Chips += int.Parse(potTextBox.Text) / winnersCount;
                        chipsTexBox.Text = Chips.ToString();
                        //pPanel.Visible = true;

                    }
                    if (CheckWinners.Contains("Bot 1"))
                    {
                        botOne.Chips += int.Parse(potTextBox.Text) / winnersCount;
                        botOneChips.Text = botOne.Chips.ToString();
                        //b1Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 2"))
                    {
                        botTwo.Chips += int.Parse(potTextBox.Text) / winnersCount;
                        botTwoChips.Text = botTwo.Chips.ToString();
                        //b2Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 3"))
                    {
                        botThree.Chips += int.Parse(potTextBox.Text) / winnersCount;
                        botThreeChips.Text = botThree.Chips.ToString();
                        //b3Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 4"))
                    {
                        botFour.Chips += int.Parse(potTextBox.Text) / winnersCount;
                        botFourChips.Text = botFour.Chips.ToString();
                        //b4Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 5"))
                    {
                        botFive.Chips += int.Parse(potTextBox.Text) / winnersCount;
                        botFiveChips.Text = botFive.Chips.ToString();
                        //b5Panel.Visible = true;
                    }
                    //await Finish(1);
                }
                if (winnersCount == 1)
                {
                    if (CheckWinners.Contains("Player"))
                    {
                        Chips += int.Parse(potTextBox.Text);
                        //await Finish(1);
                        //pPanel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 1"))
                    {
                        botOne.Chips += int.Parse(potTextBox.Text);
                        //await Finish(1);
                        //b1Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 2"))
                    {
                        botTwo.Chips += int.Parse(potTextBox.Text);
                        //await Finish(1);
                        //b2Panel.Visible = true;

                    }
                    if (CheckWinners.Contains("Bot 3"))
                    {
                        botThree.Chips += int.Parse(potTextBox.Text);
                        //await Finish(1);
                        //b3Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 4"))
                    {
                        botFour.Chips += int.Parse(potTextBox.Text);
                        //await Finish(1);
                        //b4Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 5"))
                    {
                        botFive.Chips += int.Parse(potTextBox.Text);
                        //await Finish(1);
                        //b5Panel.Visible = true;
                    }
                }
            }
        }

        private async Task CheckRaise(int currentTurn, int raiseTurn)
        {
            if (this.raising)
            {
                this.turnCount = 0;
                this.raising = false;
                this.raisedTurn = currentTurn;
                this.changed = true;
            }
            else
            {
                if (this.turnCount >= this.maxLeft - 1 || !this.changed && this.turnCount == this.maxLeft)
                {
                    if (currentTurn == raisedTurn - 1 || !this.changed && this.turnCount == this.maxLeft || raisedTurn == 0 && currentTurn == 5)
                    {
                        this.changed = false;
                        this.turnCount = 0;
                        this.globalRaise = 0;
                        this.globalCall = 0;
                        this.raisedTurn = 123;
                        this.globalRounds++;
                        if (!this.player.OutOfChips)
                            this.playerStatus.Text = "";
                        if (!this.botOne.OutOfChips)
                            this.botOneStatus.Text = "";
                        if (!this.botTwo.OutOfChips)
                            this.botTwoStatus.Text = "";
                        if (!this.botThree.OutOfChips)
                            this.botThreeStatus.Text = "";
                        if (!this.botFour.OutOfChips)
                            this.botFourStatus.Text = "";
                        if (!this.botFive.OutOfChips)
                            this.botFiveStatus.Text = "";
                    }
                }
            }
            if (this.globalRounds == this.Flop)
            {
                for (int j = 12; j <= 14; j++)
                {
                    if (this.Holder[j].Image != this.Deck[j])
                    {
                        this.Holder[j].Image = this.Deck[j];
                        this.player.Call = 0;
                        this.player.Raise = 0;
                        this.EraseBotCall();
                        this.EraseBotRaise();
                    }
                }
            }
            if (this.globalRounds == this.Turn)
            {
                for (int j = 14; j <= 15; j++)
                {
                    if (this.Holder[j].Image != this.Deck[j])
                    {
                        this.Holder[j].Image = this.Deck[j];
                        this.player.Call = 0;
                        this.player.Raise = 0;
                        this.EraseBotCall();
                        this.EraseBotRaise();
                    }
                }
            }
            if (this.globalRounds == this.River)
            {
                for (int j = 15; j <= 16; j++)
                {
                    if (this.Holder[j].Image != this.Deck[j])
                    {
                        this.Holder[j].Image = this.Deck[j];
                        this.player.Call = 0;
                        this.player.Raise = 0;
                        this.EraseBotRaise();
                        this.EraseBotCall();
                    }
                }
            }
            if (this.globalRounds == this.End && this.maxLeft == 6)
            {
                string fixedLast = "qwerty";
                if (!this.playerStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Player";
                    this.Rules(0, 1, "Player", this.player);
                }
                for (int botNumber = 0; botNumber < this.pokerDatabase.BotsOnTable.Count(); botNumber++)
                {
                    if (!this.pokerDatabase.TakeBotByIndex(i).Status.Text.Contains("Fold"))
                    {
                        fixedLast = this.pokerDatabase.TakeBotByIndex(i).Name;
                        int cardOne = this.GetCardOne(botNumber);
                        int cardTwo = this.GetCardTwo(botNumber);
                        this.Rules(cardOne, cardTwo, this.pokerDatabase.TakeBotByIndex(i).Name, this.pokerDatabase.TakeBotByIndex(i)); 
                    }
                }

                this.Winner(this.player.Type, this.player.Power, "Player", this.Chips, fixedLast);
                this.Winner(this.botOne.Type, this.botOne.Power, "Bot 1", this.botOne.Chips, fixedLast);
                this.Winner(this.botTwo.Type, this.botTwo.Power, "Bot 2", this.botTwo.Chips, fixedLast);
                this.Winner(this.botThree.Type, this.botThree.Power, "Bot 3", this.botThree.Chips, fixedLast);
                this.Winner(this.botFour.Type, this.botFour.Power, "Bot 4", this.botFour.Chips, fixedLast);
                this.Winner(this.botFive.Type, this.botFive.Power, "Bot 5", this.botFive.Chips, fixedLast);
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
                this.globalCall = bigBlind;
                this.globalRaise = 0;
                this.ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
                this.disabledPlayers.Clear();
                this.globalRounds = 0;
                this.globalType = 0;
                this.winnersCount = 0;
                this.sorted.Current = 0;
                this.sorted.Power = 0;
                this.potTextBox.Text = "0";

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

        private async Task AllIn()
        {
            #region All in
            if (this.Chips <= 0 && !this.intsadded)
            {
                if (this.playerStatus.Text.Contains("Raise"))
                {
                    this.ints.Add(Chips);
                    this.intsadded = true;
                }
                if (this.playerStatus.Text.Contains("Call"))
                {
                    this.ints.Add(Chips);
                    this.intsadded = true;
                }
            }
            this.intsadded = false;
            for (int i = 0; i < NumberOfBots; i++)
            {
                IBot currentBot = this.pokerDatabase.TakeBotByIndex(i);
                if (currentBot.Chips <= 0 && !currentBot.OutOfChips)
                {
                    if (!this.intsadded)
                    {
                        this.ints.Add(currentBot.Chips);
                    }
                    this.intsadded = false;
                }
            }
            if (this.ints.ToArray().Length == this.maxLeft)
            {
                await this.Finish(2);
            }
            else
            {
                this.ints.Clear();
            }
            #endregion

            var abc = disabledPlayers.Count(x => x == false);

            #region LastManStanding
            if (abc == 1)
            {
                int index = this.disabledPlayers.IndexOf(false);
                if (index == 0)
                {
                    this.Chips += int.Parse(potTextBox.Text);
                    this.chipsTexBox.Text = this.Chips.ToString();
                    this.playerPanel.Visible = true;
                    MessageBox.Show("Player Wins");
                }
                if (index == 1)
                {
                    this.botOne.Chips += int.Parse(this.potTextBox.Text);
                    this.chipsTexBox.Text = this.botOne.Chips.ToString();
                    this.botOnePanel.Visible = true;
                    MessageBox.Show("Bot 1 Wins");
                }
                if (index == 2)
                {
                    this.botTwo.Chips += int.Parse(this.potTextBox.Text);
                    this.chipsTexBox.Text = this.botTwo.Chips.ToString();
                    this.botTwoPanel.Visible = true;
                    MessageBox.Show("Bot 2 Wins");
                }
                if (index == 3)
                {
                    this.botThree.Chips += int.Parse(this.potTextBox.Text);
                    this.chipsTexBox.Text = this.botThree.Chips.ToString();
                    this.botThreePanel.Visible = true;
                    MessageBox.Show("Bot 3 Wins");
                }
                if (index == 4)
                {
                    this.botFour.Chips += int.Parse(this.potTextBox.Text);
                    this.chipsTexBox.Text = this.botFour.Chips.ToString();
                    this.botFourPanel.Visible = true;
                    MessageBox.Show("Bot 4 Wins");
                }
                if (index == 5)
                {
                    this.botFive.Chips += int.Parse(this.potTextBox.Text);
                    this.chipsTexBox.Text = this.botFive.Chips.ToString();
                    this.botFivePanel.Visible = true;
                    MessageBox.Show("Bot 5 Wins");
                }
                for (int j = 0; j <= 16; j++)
                {
                    this.Holder[j].Visible = false;
                }
                await this.Finish(1);
            }
            this.intsadded = false;
            #endregion

            #region FiveOrLessLeft
            if (abc < 6 && abc > 1 && this.globalRounds >= this.End)
            {
                await this.Finish(2);
            }
            #endregion
        }

        private async Task Finish(int n)
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
            this.globalCall = bigBlind;
            this.globalRaise = 0;
            this.globalRounds = 0;
            this.globalRaise = 0;
            this.restart = false;
            this.raising = false;
            this.globalHeight = 0;
            this.globalWidth = 0;
            this.winnersCount = 0;
            this.Flop = 1;
            this.Turn = 2;
            this.River = 3;
            this.End = 4;
            this.maxLeft = 6;
            this.last = 123;
            this.raisedTurn = 1;
            this.disabledPlayers.Clear();
            this.CheckWinners.Clear();
            this.ints.Clear();
            this.winList.Clear();
            this.sorted.Current = 0;
            this.sorted.Power = 0;
            this.potTextBox.Text = "0";
            this.t = 60; 
            this.maxChipsAmount = 10000000;
            this.turnCount = 0;
            this.foldedPlayers = 5;
            this.globalType = 0; 
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

        private void FixWinners()
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
        private async void Timer_Tick(object sender, object e)
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
            if (maxChipsAmount > 0)
            {
                maxChipsAmount--;
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

        private async void FoldButton_Click(object sender, EventArgs e)
        {
            playerStatus.Text = "Fold";
            player.CanPlay = false;
            player.OutOfChips = true;
            await Turns();
        }

        private async void CheckButton_Click(object sender, EventArgs e)
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

        private async void CallButton_Click(object sender, EventArgs e)
        {
            Rules(0, 1, "Player", this.player);
            if (Chips >= globalCall)
            {
                Chips -= globalCall;
                chipsTexBox.Text = "Chips : " + Chips.ToString();
                if (potTextBox.Text != "")
                {
                    potTextBox.Text = (int.Parse(potTextBox.Text) + globalCall).ToString();
                }
                else
                {
                    potTextBox.Text = globalCall.ToString();
                }
                player.CanPlay = false;
                playerStatus.Text = "Call " + globalCall;
                player.Call = globalCall;
            }
            else if (Chips <= globalCall && globalCall > 0)
            {
                potTextBox.Text = (int.Parse(potTextBox.Text) + Chips).ToString();
                playerStatus.Text = "All in " + Chips;
                Chips = 0;
                chipsTexBox.Text = "Chips : " + Chips.ToString();
                player.CanPlay = false;
                foldButton.Enabled = false;
                player.Call = Chips;
            }
            await Turns();
        }

        private async void RaiseButton_Click(object sender, EventArgs e)
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
                            potTextBox.Text = (int.Parse(potTextBox.Text) + globalCall).ToString();
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
                            potTextBox.Text = (int.Parse(potTextBox.Text) + Chips).ToString();
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

        private void AddButton_Click(object sender, EventArgs e)
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

        private void OptionsButton_Click(object sender, EventArgs e)
        {
            bigBlindTexBox.Text = bigBlind.ToString();
            smallBlindTexBox.Text = smallBlind.ToString();
            if (bigBlindTexBox.Visible == false)
            {
                bigBlindTexBox.Visible = true;
                smallBlindTexBox.Visible = true;
                bigBlindButton.Visible = true;
                smallBlindButton.Visible = true;
            }
            else
            {
                bigBlindTexBox.Visible = false;
                smallBlindTexBox.Visible = false;
                bigBlindButton.Visible = false;
                smallBlindButton.Visible = false;
            }
        }

        private void SmallBlindButton_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (smallBlindTexBox.Text.Contains(",") || smallBlindTexBox.Text.Contains("."))
            {
                MessageBox.Show("The Small Blind can be only round number !");
                smallBlindTexBox.Text = smallBlind.ToString();
                return;
            }
            if (!int.TryParse(smallBlindTexBox.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                smallBlindTexBox.Text = smallBlind.ToString();
                return;
            }
            if (int.Parse(smallBlindTexBox.Text) > 100000)
            {
                MessageBox.Show("The maximum of the Small Blind is 100 000 $");
                smallBlindTexBox.Text = smallBlind.ToString();
            }
            if (int.Parse(smallBlindTexBox.Text) < 250)
            {
                MessageBox.Show("The minimum of the Small Blind is 250 $");
            }
            if (int.Parse(smallBlindTexBox.Text) >= 250 && int.Parse(smallBlindTexBox.Text) <= 100000)
            {
                smallBlind = int.Parse(smallBlindTexBox.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void BigBlindButton_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (bigBlindTexBox.Text.Contains(",") || bigBlindTexBox.Text.Contains("."))
            {
                MessageBox.Show("The Big Blind can be only round number !");
                bigBlindTexBox.Text = bigBlind.ToString();
                return;
            }
            if (!int.TryParse(smallBlindTexBox.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                smallBlindTexBox.Text = bigBlind.ToString();
                return;
            }
            if (int.Parse(bigBlindTexBox.Text) > 200000)
            {
                MessageBox.Show("The maximum of the Big Blind is 200 000");
                bigBlindTexBox.Text = bigBlind.ToString();
            }
            if (int.Parse(bigBlindTexBox.Text) < 500)
            {
                MessageBox.Show("The minimum of the Big Blind is 500 $");
            }
            if (int.Parse(bigBlindTexBox.Text) >= 500 && int.Parse(bigBlindTexBox.Text) <= 200000)
            {
                bigBlind = int.Parse(bigBlindTexBox.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void Layout_Change(object sender, LayoutEventArgs e)
        {
            this.globalWidth = this.Width;
            this.globalHeight = this.Height;
        }
        #endregion
    }
}