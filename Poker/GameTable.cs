namespace Poker
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Poker.Core;
    using Poker.Interfaces;
    using Poker.Models;

    public partial class GameTable : Form
    {
        #region Private Variables
        private IPokerDatabase pokerDatabase;
        private IBotEraser botEraser;

        private IPlayer botOne;
        private IPlayer botTwo;
        private IPlayer botThree;
        private IPlayer botFour;
        private IPlayer botFive;
        private readonly IPlayer player;
        private const string DefaultPlayerName = "Player";
        private const string DefaultChipsName = "Chips";

        private ProgressBar progressBar;
        private Panel playerPanel;

        private int globalCall;
        private int foldedPlayers;
        private int globalChips;
        private double globalType;
        private int globalRounds;
        private int globalRaise;

        private bool intsAdded;
        private bool changed;
        private bool isRestartRequested;
        private bool isRaisingActivated;

        private int globalHeight;
        private int globalWidth;
        private int winnersCount = 0;
        private int Flop = 1;
        private int Turn = 2;
        private int River = 3;
        private int End = 4;
        private int maxPlayersLeft;
        private int raisedTurn = 1;
        private int time = 60;
        private int bigBlind;
        private int smallBlind;
        private int maxChipsAmount;
        private int turnCount = 0;

        private readonly List<bool?> disabledPlayers = new List<bool?>();
        private readonly List<PokerType> winList = new List<PokerType>();
        private readonly List<string> CheckWinners = new List<string>();
        private readonly List<int> ints = new List<int>();

        private readonly int[] reserveArray = new int[17];
        private readonly Image[] Deck = new Image[PokerGameConstants.NumberOfCards];
        private readonly PictureBox[] Holder = new PictureBox[PokerGameConstants.NumberOfCards];
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
            this.globalCall = PokerGameConstants.InitialCall;
            this.bigBlind = PokerGameConstants.BigBlindValue;
            this.smallBlind = PokerGameConstants.SmallBlindValue;
            this.maxChipsAmount = PokerGameConstants.MaximalChipsAmount;
            this.globalChips = PokerGameConstants.DefaultStartingChips;
            this.foldedPlayers = PokerGameConstants.InitialFoldedPlayers;
            this.isRestartRequested = PokerGameConstants.RestartRequestedDefault;
            this.isRaisingActivated = PokerGameConstants.RaisingActivatedDefault;
            this.maxPlayersLeft = PokerGameConstants.MaximalPlayers;
            this.globalRounds = 0;
            this.globalRaise = 0;

            this.InitializeComponent();

            this.progressBar = new ProgressBar();
            this.pokerDatabase = new PokerDatabase();
            this.botEraser = new BotEraser();
            this.InitializeBots();
            this.InitializeChipTexBoxText();

            this.player = new Player(DefaultPlayerName);
            this.playerPanel = new Panel();
            this.player.Status = this.playerStatus;
            this.player.OutOfChips = true;
            this.player.CardOne = 0;
            this.player.CardTwo = 1;

            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.Updates.Start();

            this.globalWidth = this.Width;
            this.globalHeight = this.Height;

            this.Shuffle();

            this.potTextBox.Enabled = false;
            this.chipsTexBox.Enabled = false;

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
            this.raiseTexBox.Text = (this.bigBlind * 2).ToString();
        }

        private async Task Shuffle()
        {
            this.disabledPlayers.Add(this.player.OutOfChips);
            this.disabledPlayers.Add(this.botOne.OutOfChips);
            for (int botNumber = 0; botNumber < PokerGameConstants.NumberOfBots; botNumber++)
            {
                this.disabledPlayers.Add(this.pokerDatabase.TakeBotByIndex(botNumber).OutOfChips);
            }

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
            List<Tuple<int, int>> coordinates = new List<Tuple<int, int>>();
            coordinates.Add(new Tuple<int, int>(15, 420));
            coordinates.Add(new Tuple<int, int>(75, 65));
            coordinates.Add(new Tuple<int, int>(590, 25));
            coordinates.Add(new Tuple<int, int>(1115, 65));
            coordinates.Add(new Tuple<int, int>(1160, 420));

            for (int i = ImgLocation.Length; i > 0; i--)
            {
                int j = RandomGenerator.Next(i);
                var k = this.ImgLocation[j];
                this.ImgLocation[j] = this.ImgLocation[i - 1];
                this.ImgLocation[i - 1] = k;
            }

            for (int i = 0; i < 17; i++)
            {
                this.Deck[i] = Image.FromFile(ImgLocation[i]);
                var charsToRemove = new string[] { "Assets\\Cards\\", ".png" };
                foreach (var c in charsToRemove)
                {
                    this.ImgLocation[i] = this.ImgLocation[i].Replace(c, string.Empty);
                }

                this.reserveArray[i] = int.Parse(ImgLocation[i]) - 1;
                this.Holder[i] = new PictureBox();
                this.Holder[i].SizeMode = PictureBoxSizeMode.StretchImage;
                this.Holder[i].Height = 130;
                this.Holder[i].Width = 80;
                this.Controls.Add(this.Holder[i]);
                this.Holder[i].Name = "pb" + i.ToString();
                await Task.Delay(200);
                #region Throwing Cards
                if (i < 2)
                {
                    if (this.Holder[0].Tag != null)
                    {
                        this.Holder[1].Tag = reserveArray[1];
                    }

                    this.Holder[0].Tag = this.reserveArray[0];
                    this.Holder[i].Image = this.Deck[i];
                    this.Holder[i].Anchor = (AnchorStyles.Bottom);
                    this.Holder[i].Dock = DockStyle.Top;
                    this.Holder[i].Location = new Point(horizontal, vertical);
                    horizontal += this.Holder[i].Width;
                    this.Controls.Add(this.playerPanel);
                    this.playerPanel.Location = new Point(this.Holder[0].Left - 10, this.Holder[0].Top - 10);
                    this.playerPanel.BackColor = Color.DarkBlue;
                    this.playerPanel.Height = 150;
                    this.playerPanel.Width = 180;
                    this.playerPanel.Visible = false;
                }
                for (int botNumber = 0; botNumber < PokerGameConstants.NumberOfBots; botNumber++)
                {
                    IPlayer currentBot = this.pokerDatabase.TakeBotByIndex(botNumber);
                    if (currentBot.Chips > 0)
                    {
                        this.foldedPlayers--;
                        if (i == currentBot.CardOne || i == currentBot.CardTwo)
                        {
                            if (this.Holder[currentBot.CardOne].Tag != null)
                            {
                                this.Holder[currentBot.CardTwo].Tag = this.reserveArray[currentBot.CardTwo];
                            }
                            this.Holder[currentBot.CardOne].Tag = this.reserveArray[currentBot.CardOne];
                            if (!check)
                            {
                                horizontal = coordinates[botNumber].Item1;
                                vertical = coordinates[botNumber].Item2;
                            }
                            check = true;
                            this.Holder[i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
                            this.Holder[i].Image = backImage;
                            this.Holder[i].Image = this.Deck[i];
                            this.Holder[i].Location = new Point(horizontal, vertical);
                            horizontal += this.Holder[i].Width;
                            this.Holder[i].Visible = true;
                            this.Controls.Add(currentBot.Panel);
                            currentBot.Panel.Location = new Point(this.Holder[currentBot.CardOne].Left - 10, this.Holder[currentBot.CardOne].Top - 10);
                            currentBot.Panel.BackColor = Color.DarkBlue;
                            currentBot.Panel.Height = 150;
                            currentBot.Panel.Width = 180;
                            currentBot.Panel.Visible = false;
                            if (i == currentBot.CardTwo)
                            {
                                check = false;
                            }
                        }
                    }

                }

                if (i >= 12)
                {
                    this.Holder[12].Tag = this.reserveArray[12];
                    if (i > 12) this.Holder[13].Tag = this.reserveArray[13];
                    if (i > 13) this.Holder[14].Tag = this.reserveArray[14];
                    if (i > 14) this.Holder[15].Tag = this.reserveArray[15];
                    if (i > 15)
                    {
                        this.Holder[16].Tag = this.reserveArray[16];

                    }
                    if (!check)
                    {
                        horizontal = 410;
                        vertical = 265;
                    }
                    check = true;
                    if (this.Holder[i] != null)
                    {
                        this.Holder[i].Anchor = AnchorStyles.None;
                        this.Holder[i].Image = backImage;
                        this.Holder[i].Image = this.Deck[i];
                        this.Holder[i].Location = new Point(horizontal, vertical);
                        horizontal += 110;
                    }
                }
                #endregion
                for (int botNumber = 0; botNumber < PokerGameConstants.NumberOfBots; botNumber++)
                {
                    IPlayer currentBot = this.pokerDatabase.TakeBotByIndex(botNumber);
                    if (currentBot.Chips <= 0)
                    {
                        currentBot.OutOfChips = true;
                        this.Holder[currentBot.CardOne].Visible = false;
                        this.Holder[currentBot.CardTwo].Visible = false;
                    }
                    else
                    {
                        currentBot.OutOfChips = false;
                        if (i == currentBot.CardTwo && this.Holder[currentBot.CardTwo] != null)
                        {
                            this.Holder[currentBot.CardOne].Visible = true;
                            this.Holder[currentBot.CardTwo].Visible = true;
                        }
                    }
                }

                if (i == 16)
                {
                    if (!this.isRestartRequested)
                    {
                        this.MaximizeBox = true;
                        this.MinimizeBox = true;
                    }
                    this.timer.Start();
                }
            }
            if (this.foldedPlayers == PokerGameConstants.InitialFoldedPlayers)
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
                this.foldedPlayers = PokerGameConstants.InitialFoldedPlayers;
            }
            this.raiseButton.Enabled = true;
            this.callButton.Enabled = true;
            this.raiseButton.Enabled = true;
            this.raiseButton.Enabled = true;
            this.foldButton.Enabled = true;
        }

        private async Task Turns()
        {
            if (!this.player.OutOfChips && this.player.CanPlay)
            {
                this.player.FixCall(ref this.globalCall, ref this.globalRaise, 1, this.globalRounds, ref this.callButton);
                MessageBox.Show("Player's Turn");
                this.progressBarTimer.Visible = true;
                this.progressBarTimer.Value = 1000;
                this.time = 60;
                this.maxChipsAmount = PokerGameConstants.MaximalChipsAmount;
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
                        this.maxPlayersLeft--;
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

                for (int botNumber = 1; botNumber <= PokerGameConstants.NumberOfBots; botNumber++)
                {
                    IPlayer currentBot = this.pokerDatabase.TakeBotByIndex(botNumber - 1);
                    if (!currentBot.OutOfChips && currentBot.CanPlay)
                    {
                        currentBot.FixCall(ref this.globalCall, ref this.globalRaise, 1, this.globalRounds, ref this.callButton);
                        currentBot.FixCall(ref this.globalCall, ref this.globalRaise, 2, this.globalRounds, ref this.callButton);

                        int name = 0;
                        this.Rules(currentBot);
                        MessageBox.Show(string.Format("Bot {0}'s Turn", currentBot.Name));
                        ActionManager.AI(currentBot, this.globalCall, this.potTextBox, ref this.globalRaise, ref this.isRaisingActivated, ref this.globalRounds, name);

                        this.turnCount++;
                        currentBot.CanPlay = false;
                        this.pokerDatabase.TakeBotByIndex(botNumber).CanPlay = true;
                    }
                    if (currentBot.OutOfChips && !currentBot.Folded)
                    {
                        this.disabledPlayers.RemoveAt(botNumber);
                        this.disabledPlayers.Insert(botNumber, null);
                        this.maxPlayersLeft--;
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
                        this.maxPlayersLeft--;
                        this.player.Folded = true;
                    }
                }
                await this.AllIn();
                if (!this.isRestartRequested)
                {
                    await this.Turns();
                }
                this.isRestartRequested = false;
            }
        }

        private void Rules(IPlayer currentPlayer)
        {
            if (currentPlayer.CardOne == 0 && currentPlayer.CardTwo == 1)
            {
            }
            if (!currentPlayer.OutOfChips || currentPlayer.CardOne == 0 && currentPlayer.CardTwo == 1 && this.playerStatus.Text.Contains("Fold") == false)
            {
                #region Variables
                bool done = false;
                bool vf = false;
                int[] StraightOne = new int[5];
                int[] StraightTwo = new int[7];
                StraightTwo[0] = reserveArray[currentPlayer.CardOne];
                StraightTwo[1] = reserveArray[currentPlayer.CardTwo];
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
                for (int i = 0; i < 16; i++)
                {
                    if (reserveArray[i] == int.Parse(Holder[currentPlayer.CardOne].Tag.ToString()) && 
                        reserveArray[i + 1] == int.Parse(Holder[currentPlayer.CardTwo].Tag.ToString()))
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

        private void Winner(double current, double power, string currentText, int chips, string lastly)
        {
            if (lastly == " ")
            {
                lastly = "Bot 5";
            }

            for (int j = 0; j <= 16; j++)
            {
                //await Task.Delay(5);
                if (this.Holder[j].Visible)
                    this.Holder[j].Image = this.Deck[j];
            }

            if (current == this.sorted.Current && power == this.sorted.Power)
            {
                winnersCount++;
                this.CheckWinners.Add(currentText);
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
            if (currentText == lastly)
            {
                if (this.winnersCount > 1)
                {
                    if (this.CheckWinners.Contains(DefaultPlayerName))
                    {
                        this.globalChips += int.Parse(this.potTextBox.Text) / this.winnersCount;
                        this.chipsTexBox.Text = this.globalChips.ToString();
                        this.playerPanel.Visible = true;

                    }
                    for (int botNumber = 0; botNumber < PokerGameConstants.NumberOfBots; botNumber++)
                    {
                        IPlayer currentBot = this.pokerDatabase.TakeBotByIndex(botNumber);
                        if (this.CheckWinners.Contains(currentBot.Name))
                        {
                            currentBot.Chips += int.Parse(this.potTextBox.Text) / this.winnersCount;
                            currentBot.ChipsTextBox.Text = currentBot.Chips.ToString();
                            currentBot.Panel.Visible = true;
                        }
                    }
                    //await this.Finish(1);
                }
                if (winnersCount == 1)
                {
                    if (CheckWinners.Contains(DefaultPlayerName))
                    {
                        globalChips += int.Parse(potTextBox.Text);
                        //await Finish(1);
                        //this.playerPanel.Visible = true;
                    }
                    for (int botNumber = 0; botNumber < PokerGameConstants.NumberOfBots; botNumber++)
                    {
                        IPlayer currentBot = this.pokerDatabase.TakeBotByIndex(botNumber);
                        if (this.CheckWinners.Contains(currentBot.Name))
                        {
                            currentBot.Chips += int.Parse(this.potTextBox.Text);
                            //currentBot.Panel.Visible = true;
                            //await Finish(1);
                        }
                    }
                }
            }
        }

        private async Task CheckRaise(int currentTurn, int raiseTurn)
        {
            if (this.isRaisingActivated)
            {
                this.turnCount = 0;
                this.isRaisingActivated = false;
                this.raisedTurn = currentTurn;
                this.changed = true;
            }
            else
            {
                if (this.turnCount >= this.maxPlayersLeft - 1 || !this.changed && this.turnCount == this.maxPlayersLeft)
                {
                    if (currentTurn == raisedTurn - 1 || !this.changed && this.turnCount == this.maxPlayersLeft || raisedTurn == 0 && currentTurn == 5)
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
                        this.botEraser.EraseBotCall(this.pokerDatabase);
                        this.botEraser.EraseBotRaise(this.pokerDatabase);
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
                        this.botEraser.EraseBotCall(this.pokerDatabase);
                        this.botEraser.EraseBotRaise(this.pokerDatabase);
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
                        this.botEraser.EraseBotRaise(this.pokerDatabase);
                        this.botEraser.EraseBotCall(this.pokerDatabase);
                    }
                }
            }
            if (this.globalRounds == this.End && this.maxPlayersLeft == 6)
            {
                string fixedLast = "qwerty";
                if (!this.playerStatus.Text.Contains("Fold"))
                {
                    fixedLast = DefaultPlayerName;
                    this.Rules(this.player);
                }
                for (int botNumber = 0; botNumber < this.pokerDatabase.BotsOnTable.Count(); botNumber++)
                {
                    IPlayer currentBot = this.pokerDatabase.TakeBotByIndex(botNumber);
                    if (!currentBot.Status.Text.Contains("Fold"))
                    {
                        fixedLast = currentBot.Name;
                        this.Rules(currentBot);
                    }
                }

                this.Winner(this.player.Type, this.player.Power, DefaultPlayerName, this.globalChips, fixedLast);
                this.Winner(this.botOne.Type, this.botOne.Power, this.botOne.Name, this.botOne.Chips, fixedLast);
                this.Winner(this.botTwo.Type, this.botTwo.Power, this.botTwo.Name, this.botTwo.Chips, fixedLast);
                this.Winner(this.botThree.Type, this.botThree.Power, this.botThree.Name, this.botThree.Chips, fixedLast);
                this.Winner(this.botFour.Type, this.botFour.Power, this.botFour.Name, this.botFour.Chips, fixedLast);
                this.Winner(this.botFive.Type, this.botFive.Power, this.botFive.Name, this.botFive.Chips, fixedLast);
                this.isRestartRequested = true;
                this.player.CanPlay = true;
                this.player.OutOfChips = false;

                this.botEraser.EnableBotChips(this.pokerDatabase);

                if (this.globalChips <= 0)
                {
                    AddChips f2 = new AddChips();
                    f2.ShowDialog();
                    if (f2.a != 0)
                    {
                        this.globalChips = f2.a;
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

                this.botEraser.DisableBotPanel(this.pokerDatabase);
                this.botEraser.EraseBotCall(this.pokerDatabase);
                this.botEraser.EraseBotRaise(this.pokerDatabase);
                this.botEraser.EraseBotPower(this.pokerDatabase);
                this.botEraser.EraseBotType(this.pokerDatabase);

                this.playerPanel.Visible = false;
                this.player.Call = 0;
                this.player.Raise = 0;
                this.player.Power = 0;
                this.player.Type = -1;
                this.playerStatus.Text = "";

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
            if (this.globalChips <= 0 && !this.intsAdded)
            {
                if (this.playerStatus.Text.Contains("Raise"))
                {
                    this.ints.Add(globalChips);
                    this.intsAdded = true;
                }
                if (this.playerStatus.Text.Contains("Call"))
                {
                    this.ints.Add(globalChips);
                    this.intsAdded = true;
                }
            }
            this.intsAdded = false;
            for (int i = 0; i < PokerGameConstants.NumberOfBots; i++)
            {
                IPlayer currentBot = this.pokerDatabase.TakeBotByIndex(i);
                if (currentBot.Chips <= 0 && !currentBot.OutOfChips)
                {
                    if (!this.intsAdded)
                    {
                        this.ints.Add(currentBot.Chips);
                    }
                    this.intsAdded = false;
                }
            }
            if (this.ints.ToArray().Length == this.maxPlayersLeft)
            {
                await this.Finish(2);
            }
            else
            {
                this.ints.Clear();
            }
            #endregion

            var numberOfDisabledPlayers = disabledPlayers.Count(x => x == false);

            #region LastManStanding
            if (numberOfDisabledPlayers == 1)
            {
                int index = this.disabledPlayers.IndexOf(false);
                if (index == 0)
                {
                    this.globalChips += int.Parse(potTextBox.Text);
                    this.chipsTexBox.Text = this.globalChips.ToString();
                    this.playerPanel.Visible = true;
                    MessageBox.Show("Player Wins");
                }
                if (index == 1)
                {
                    this.botOne.Chips += int.Parse(this.potTextBox.Text);
                    this.chipsTexBox.Text = this.botOne.Chips.ToString();
                    this.botOne.Panel.Visible = true;
                    MessageBox.Show("Bot 1 Wins");
                }
                if (index == 2)
                {
                    this.botTwo.Chips += int.Parse(this.potTextBox.Text);
                    this.chipsTexBox.Text = this.botTwo.Chips.ToString();
                    this.botTwo.Panel.Visible = true;
                    MessageBox.Show("Bot 2 Wins");
                }
                if (index == 3)
                {
                    this.botThree.Chips += int.Parse(this.potTextBox.Text);
                    this.chipsTexBox.Text = this.botThree.Chips.ToString();
                    this.botThree.Panel.Visible = true;
                    MessageBox.Show("Bot 3 Wins");
                }
                if (index == 4)
                {
                    this.botFour.Chips += int.Parse(this.potTextBox.Text);
                    this.chipsTexBox.Text = this.botFour.Chips.ToString();
                    this.botFour.Panel.Visible = true;
                    MessageBox.Show("Bot 4 Wins");
                }
                if (index == 5)
                {
                    this.botFive.Chips += int.Parse(this.potTextBox.Text);
                    this.chipsTexBox.Text = this.botFive.Chips.ToString();
                    this.botFive.Panel.Visible = true;
                    MessageBox.Show("Bot 5 Wins");
                }
                for (int j = 0; j <= 16; j++)
                {
                    this.Holder[j].Visible = false;
                }
                await this.Finish(1);
            }
            this.intsAdded = false;
            #endregion

            #region FiveOrLessLeft
            if (numberOfDisabledPlayers < 6 && numberOfDisabledPlayers > 1 && this.globalRounds >= this.End)
            {
                await this.Finish(2);
            }
            #endregion
        }

        private void InitializeChipTexBoxText()
        {
            this.chipsTexBox.Text = "Chips : " + this.globalChips.ToString();
            this.botOne.Panel.Text = "Chips : " + this.botOne.Chips.ToString();
            this.botTwo.Panel.Text = "Chips : " + this.botTwo.Chips.ToString();
            this.botThree.Panel.Text = "Chips : " + this.botThree.Chips.ToString();
            this.botFour.Panel.Text = "Chips : " + this.botFour.Chips.ToString();
            this.botFive.Panel.Text = "Chips : " + this.botFive.Chips.ToString();
        }

        public virtual void InitializeBots()
        {
            this.botOne = new Player("Bot 1");
            this.botOne.CardOne = PokerGameConstants.BotOneCardOne;
            this.botOne.CardTwo = PokerGameConstants.BotOneCardTwo;

            this.botTwo = new Player("Bot 2");
            this.botTwo.CardOne = PokerGameConstants.BotTwoCardOne;
            this.botTwo.CardTwo = PokerGameConstants.BotTwoCardTwo;

            this.botThree = new Player("Bot 3");
            this.botThree.CardOne = PokerGameConstants.BotThreeCardOne;
            this.botThree.CardTwo = PokerGameConstants.BotThreeCardTwo;

            this.botFour = new Player("Bot 4");
            this.botFour.CardOne = PokerGameConstants.BotFourCardOne;
            this.botFour.CardTwo = PokerGameConstants.BotFourCardTwo;

            this.botFive = new Player("Bot 5");
            this.botFive.CardOne = PokerGameConstants.BotFiveCardOne;
            this.botFive.CardTwo = PokerGameConstants.BotFiveCardTwo;

            this.botOne.Status = this.botOneStatus;
            this.botTwo.Status = this.botTwoStatus;
            this.botThree.Status = this.botThreeStatus;
            this.botFour.Status = this.botFourStatus;
            this.botFive.Status = this.botFiveStatus;

            this.botOne.ChipsTextBox = this.botOneChips;
            this.botTwo.ChipsTextBox = this.botTwoChips;
            this.botThree.ChipsTextBox = this.botThreeChips;
            this.botFour.ChipsTextBox = this.botFourChips;
            this.botFive.ChipsTextBox = this.botFiveChips;

            this.botOne.ChipsTextBox.Enabled = false;
            this.botTwo.ChipsTextBox.Enabled = false;
            this.botThree.ChipsTextBox.Enabled = false;
            this.botFour.ChipsTextBox.Enabled = false;
            this.botFive.ChipsTextBox.Enabled = false;

            this.pokerDatabase.AddBot(this.botOne);
            this.pokerDatabase.AddBot(this.botTwo);
            this.pokerDatabase.AddBot(this.botThree);
            this.pokerDatabase.AddBot(this.botFour);
            this.pokerDatabase.AddBot(this.botFive);
        }

        private async Task Finish(int n)
        {
            if (n == 2)
            {
                FixWinners();
            }

            this.botEraser.DisableBots(this.pokerDatabase);
            this.botEraser.EraseBotCall(this.pokerDatabase);
            this.botEraser.EraseBotRaise(this.pokerDatabase);
            this.botEraser.DisableBotPanel(this.pokerDatabase);
            this.botEraser.EraseBotPower(this.pokerDatabase);
            this.botEraser.EraseBotType(this.pokerDatabase);
            this.botEraser.EraseBotStatusText(this.pokerDatabase);
            this.botEraser.UnFoldBots(this.pokerDatabase);
            this.botEraser.EnableBotChips(this.pokerDatabase);

            this.ErasePlayerStats();
            this.DisablePlayer();

            this.EraseGameStats();

            if (this.globalChips <= 0)
            {
                AddChips f2 = new AddChips();
                f2.ShowDialog();
                if (f2.a != 0)
                {
                    this.globalChips = f2.a;
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

            this.ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
            for (int os = 0; os < 17; os++)
            {
                this.Holder[os].Image = null;
                this.Holder[os].Invalidate();
                this.Holder[os].Visible = false;
            }
            await this.Shuffle();
            await this.Turns();
        }

        public void DisablePlayer()
        {
            this.player.Folded = false;
            this.player.CanPlay = true;
            this.player.OutOfChips = false;
        }

        public void EraseGameStats()
        {
            this.globalCall = bigBlind;
            this.globalRaise = 0;
            this.globalRounds = 0;
            this.globalRaise = 0;
            this.isRestartRequested = false;
            this.isRaisingActivated = false;
            this.globalHeight = 0;
            this.globalWidth = 0;
            this.winnersCount = 0;
            this.Flop = 1;
            this.Turn = 2;
            this.River = 3;
            this.End = 4;
            this.maxPlayersLeft = 6;
            this.raisedTurn = 1;
            this.disabledPlayers.Clear();
            this.CheckWinners.Clear();
            this.ints.Clear();
            this.winList.Clear();
            this.sorted.Current = 0;
            this.sorted.Power = 0;
            this.potTextBox.Text = "0";
            this.time = 60;
            this.maxChipsAmount = 10000000;
            this.turnCount = 0;
            this.foldedPlayers = 5;
            this.globalType = 0;
        }

        public void ErasePlayerStats()
        {
            this.playerStatus.Text = "";
            this.player.Power = 0;
            this.player.Type = -1;
            this.player.Raise = 0;
            this.player.Call = 0;
            this.playerPanel.Visible = false;
        }

        public void FixWinners()
        {
            this.winList.Clear();
            this.sorted.Current = 0;
            this.sorted.Power = 0;
            string fixedLast = "qwerty";
            if (!this.playerStatus.Text.Contains("Fold"))
            {
                fixedLast = DefaultPlayerName;
                this.Rules(this.player);
            }
            for (int i = 0; i < PokerGameConstants.NumberOfBots; i++)
            {
                IPlayer currentBot = this.pokerDatabase.TakeBotByIndex(i);
                if (!currentBot.Status.Text.Contains("Fold"))
                {
                    fixedLast = currentBot.Name;
                    this.Rules(currentBot);
                }
            }

            this.Winner(this.player.Type, player.Power, DefaultPlayerName, globalChips, fixedLast);
            this.Winner(this.botOne.Type, this.botOne.Power, "Bot 1", botOne.Chips, fixedLast);
            this.Winner(this.botTwo.Type, this.botTwo.Power, "Bot 2", botTwo.Chips, fixedLast);
            this.Winner(this.botThree.Type, this.botThree.Power, "Bot 3", botThree.Chips, fixedLast);
            this.Winner(this.botFour.Type, this.botFour.Power, "Bot 4", botFour.Chips, fixedLast);
            this.Winner(this.botFive.Type, this.botFive.Power, "Bot 5", botFive.Chips, fixedLast);
        }

        #region UI
        private async void Timer_Tick(object sender, object e)
        {
            if (this.progressBarTimer.Value <= 0)
            {
                this.player.OutOfChips = true;
                await Turns();
            }
            if (this.time > 0)
            {
                this.time--;
                this.progressBarTimer.Value = (this.time / 6) * 100;
            }
        }

        private void Update_Tick(object sender, object e)
        {
            if (this.globalChips <= 0)
            {
                this.chipsTexBox.Text = "Chips : 0";
            }
            for (int i = 0; i < PokerGameConstants.NumberOfBots; i++)
            {
                IPlayer currentBot = this.pokerDatabase.TakeBotByIndex(i);
                if (currentBot.Chips <= 0)
                {
                    currentBot.ChipsTextBox.Text = "Chips : 0";
                }
            }

            this.InitializeChipTexBoxText();
            if (this.globalChips <= 0)
            {
                this.player.CanPlay = false;
                this.player.OutOfChips = true;
                this.callButton.Enabled = false;
                this.raiseButton.Enabled = false;
                this.foldButton.Enabled = false;
                this.checkButton.Enabled = false;
            }
            if (this.maxChipsAmount > 0)
            {
                this.maxChipsAmount--;
            }
            if (this.globalChips >= this.globalCall)
            {
                this.callButton.Text = "Call " + this.globalCall.ToString();
            }
            else
            {
                this.callButton.Text = "All in";
                this.raiseButton.Enabled = false;
            }
            if (this.globalCall > 0)
            {
                this.checkButton.Enabled = false;
            }
            if (this.globalCall <= 0)
            {
                this.checkButton.Enabled = true;
                this.callButton.Text = "Call";
                this.callButton.Enabled = false;
            }
            if (this.globalChips <= 0)
            {
                this.raiseButton.Enabled = false;
            }
            int parsedValue;

            if (this.raiseTexBox.Text != "" && int.TryParse(this.raiseTexBox.Text, out parsedValue))
            {
                if (this.globalChips <= int.Parse(this.raiseTexBox.Text))
                {
                    this.raiseButton.Text = "All in";
                }
                else
                {
                    this.raiseButton.Text = "Raise";
                }
            }
            if (this.globalChips < this.globalCall)
            {
                this.raiseButton.Enabled = false;
            }
        }

        private async void FoldButton_Click(object sender, EventArgs e)
        {
            this.playerStatus.Text = "Fold";
            this.player.CanPlay = false;
            this.player.OutOfChips = true;
            await Turns();
        }

        private async void CheckButton_Click(object sender, EventArgs e)
        {
            if (this.globalCall <= 0)
            {
                this.player.CanPlay = false;
                this.playerStatus.Text = "Check";
            }
            else
            {
                this.playerStatus.Text = "All in " + this.globalChips;
                this.checkButton.Enabled = false;
            }
            await Turns();
        }

        private async void CallButton_Click(object sender, EventArgs e)
        {
            this.Rules(this.player);
            if (this.globalChips >= this.globalCall)
            {
                this.globalChips -= this.globalCall;
                this.chipsTexBox.Text = "Chips : " + this.globalChips.ToString();
                if (this.potTextBox.Text != "")
                {
                    this.potTextBox.Text = (int.Parse(this.potTextBox.Text) + this.globalCall).ToString();
                }
                else
                {
                    this.potTextBox.Text = this.globalCall.ToString();
                }
                this.player.CanPlay = false;
                this.playerStatus.Text = "Call " + this.globalCall;
                this.player.Call = this.globalCall;
            }
            else if (this.globalChips <= this.globalCall && this.globalCall > 0)
            {
                this.potTextBox.Text = (int.Parse(this.potTextBox.Text) + this.globalChips).ToString();
                this.playerStatus.Text = "All in " + this.globalChips;
                this.globalChips = 0;
                this.chipsTexBox.Text = "Chips : " + this.globalChips.ToString();
                this.player.CanPlay = false;
                this.foldButton.Enabled = false;
                this.player.Call = this.globalChips;
            }
            await Turns();
        }

        private async void RaiseButton_Click(object sender, EventArgs e)
        {
            this.Rules(this.player);
            int parsedValue;
            if (this.raiseTexBox.Text != "" && int.TryParse(this.raiseTexBox.Text, out parsedValue))
            {
                if (this.globalChips > this.globalCall)
                {
                    if (this.globalRaise * 2 > int.Parse(this.raiseTexBox.Text))
                    {
                        this.raiseTexBox.Text = (this.globalRaise * 2).ToString();
                        MessageBox.Show("You must raise atleast twice as the current raise !");
                        return;
                    }
                    else
                    {
                        if (this.globalChips >= int.Parse(this.raiseTexBox.Text))
                        {
                            this.globalCall = int.Parse(this.raiseTexBox.Text);
                            this.globalRaise = int.Parse(this.raiseTexBox.Text);
                            this.playerStatus.Text = "Raise " + this.globalCall.ToString();
                            this.potTextBox.Text = (int.Parse(this.potTextBox.Text) + this.globalCall).ToString();
                            this.callButton.Text = "Call";
                            this.globalChips -= int.Parse(this.raiseTexBox.Text);
                            this.isRaisingActivated = true;
                            this.player.Raise = Convert.ToInt32(this.globalRaise);
                        }
                        else
                        {
                            this.globalCall = this.globalChips;
                            this.globalRaise = this.globalChips;
                            this.potTextBox.Text = (int.Parse(this.potTextBox.Text) + this.globalChips).ToString();
                            this.playerStatus.Text = "Raise " + this.globalCall.ToString();
                            this.globalChips = 0;
                            this.isRaisingActivated = true;
                            this.player.Raise = Convert.ToInt32(this.globalRaise);
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
            if (this.addTexBox.Text == "") { }
            else
            {
                this.globalChips += int.Parse(this.addTexBox.Text);
                this.botOne.Chips += int.Parse(this.addTexBox.Text);
                this.botTwo.Chips += int.Parse(this.addTexBox.Text);
                this.botThree.Chips += int.Parse(this.addTexBox.Text);
                this.botFour.Chips += int.Parse(this.addTexBox.Text);
                this.botFive.Chips += int.Parse(this.addTexBox.Text);
            }
            this.chipsTexBox.Text = "Chips : " + this.globalChips.ToString();
        }

        private void OptionsButton_Click(object sender, EventArgs e)
        {
            this.bigBlindTexBox.Text = this.bigBlind.ToString();
            this.smallBlindTexBox.Text = this.smallBlind.ToString();
            if (this.bigBlindTexBox.Visible == false)
            {
                this.bigBlindTexBox.Visible = true;
                this.smallBlindTexBox.Visible = true;
                this.bigBlindButton.Visible = true;
                this.smallBlindButton.Visible = true;
            }
            else
            {
                this.bigBlindTexBox.Visible = false;
                this.smallBlindTexBox.Visible = false;
                this.bigBlindButton.Visible = false;
                this.smallBlindButton.Visible = false;
            }
        }

        private void SmallBlindButton_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (this.smallBlindTexBox.Text.Contains(",") || this.smallBlindTexBox.Text.Contains("."))
            {
                MessageBox.Show("The Small Blind can be only round number !");
                this.smallBlindTexBox.Text = this.smallBlind.ToString();
                return;
            }
            if (!int.TryParse(this.smallBlindTexBox.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                this.smallBlindTexBox.Text = this.smallBlind.ToString();
                return;
            }
            if (int.Parse(smallBlindTexBox.Text) > 100000)
            {
                MessageBox.Show("The maximum of the Small Blind is 100 000 $");
                this.smallBlindTexBox.Text = this.smallBlind.ToString();
            }
            if (int.Parse(this.smallBlindTexBox.Text) < PokerGameConstants.SmallBlindValue)
            {
                MessageBox.Show("The minimum of the Small Blind is 250 $");
            }
            if (int.Parse(this.smallBlindTexBox.Text) >= PokerGameConstants.SmallBlindValue && int.Parse(this.smallBlindTexBox.Text) <= 100000)
            {
                this.smallBlind = int.Parse(this.smallBlindTexBox.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void BigBlindButton_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (this.bigBlindTexBox.Text.Contains(",") || this.bigBlindTexBox.Text.Contains("."))
            {
                MessageBox.Show("The Big Blind can be only round number !");
                this.bigBlindTexBox.Text = this.bigBlind.ToString();
                return;
            }
            if (!int.TryParse(this.smallBlindTexBox.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                this.smallBlindTexBox.Text = this.bigBlind.ToString();
                return;
            }
            if (int.Parse(this.bigBlindTexBox.Text) > 200000)
            {
                MessageBox.Show("The maximum of the Big Blind is 200 000");
                this.bigBlindTexBox.Text = this.bigBlind.ToString();
            }
            if (int.Parse(this.bigBlindTexBox.Text) < PokerGameConstants.BigBlindValue)
            {
                MessageBox.Show("The minimum of the Big Blind is 500 $");
            }
            if (int.Parse(this.bigBlindTexBox.Text) >= PokerGameConstants.BigBlindValue && int.Parse(this.bigBlindTexBox.Text) <= 200000)
            {
                this.bigBlind = int.Parse(this.bigBlindTexBox.Text);
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