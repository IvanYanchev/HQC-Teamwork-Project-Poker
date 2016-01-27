namespace Poker
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Poker.Core;
    using Poker.Interfaces;
    using Poker.Models;

    public partial class GameTable : Form
    {
        #region Private Variables
        private IPokerDatabase pokerDatabase;
        private IPlayer player;

        private Panel playerPanel;

        private int GlobalCall;
        private int foldedPlayers;
        private int GlobalChips;
        private double GlobalType;
        private int GlobalRounds;
        private int GlobalRaise;

        private bool intsAdded;
        private bool changed;
        private bool IsRestartRequested;
        private bool IsRaisingActivated;

        private int globalHeight;
        private int globalWidth;
        private int winnersCount;
        private int maxPlayersLeft;
        private int raisedTurn = 1;
        private int time;
        private int bigBlind;
        private int smallBlind;
        private int maxChipsAmount;
        private int turnCount = 0;

        private readonly bool?[] disabledPlayers = new bool?[PokerGameConstants.MaximalPlayers];
        private readonly List<PokerType> winList = new List<PokerType>();
        private readonly List<string> CheckWinners = new List<string>();
        private readonly List<int> ints = new List<int>();

        private readonly int[] reserveArray = new int[PokerGameConstants.CardsOnTable];
        private readonly Image[] Deck = new Image[PokerGameConstants.NumberOfCards];
        private readonly PictureBox[] Holder = new PictureBox[PokerGameConstants.NumberOfCards];
        private readonly Timer timer = new Timer();
        private readonly Timer Updates = new Timer();
        #endregion

        private PokerType sorted;
        private string[] ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);

        public GameTable(IActionManager actionManager, IBotEraser botEraser, ICombinationDatabase combinationsDatabase)
        {
            this.ActionManager = actionManager;
            this.ActionManager.GameTable = this;
            this.BotEraser = botEraser;
            this.CombinationsDatabase = combinationsDatabase;

            this.GlobalCall = PokerGameConstants.InitialCall;
            this.bigBlind = PokerGameConstants.BigBlindValue;
            this.smallBlind = PokerGameConstants.SmallBlindValue;
            this.maxChipsAmount = PokerGameConstants.MaximalChipsAmount;
            this.GlobalChips = PokerGameConstants.DefaultStartingChips;
            this.foldedPlayers = PokerGameConstants.InitialFoldedPlayers;
            this.IsRestartRequested = PokerGameConstants.RestartRequestedDefault;
            this.IsRaisingActivated = PokerGameConstants.RaisingActivatedDefault;
            this.maxPlayersLeft = PokerGameConstants.MaximalPlayers;
            this.GlobalRounds = PokerGameConstants.InitialRounds;
            this.GlobalRaise = PokerGameConstants.InitialRaise;
            this.time = PokerGameConstants.InitialTime;
            this.winnersCount = PokerGameConstants.InitialWinners;

            this.pokerDatabase = new PokerDatabase();

            this.InitializeComponent();
            this.InitializeBots();
            this.InitializeChipTexBoxText();
            this.InitializePlayer();

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
            this.raiseTexBox.Text = (this.bigBlind * 2).ToString();
        }

        public IActionManager ActionManager { get; private set; }

        public IBotEraser BotEraser { get; private set; }

        public ICombinationDatabase CombinationsDatabase { get; private set; }

        private async Task Shuffle()
        {
            this.disabledPlayers[0] = this.player.OutOfChips;
            for (int botIndex = 0; botIndex < PokerGameConstants.NumberOfBots; botIndex++)
            {
                this.disabledPlayers[botIndex + 1] = this.pokerDatabase.TakeBotByIndex(botIndex).OutOfChips;
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

            for (int i = 0; i < PokerGameConstants.CardsOnTable; i++)
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

                    // this.Holder[i].Dock = DockStyle.Top;
                    this.Holder[i].Location = new Point(horizontal, vertical);
                    horizontal += this.Holder[i].Width;
                    this.Controls.Add(this.playerPanel);
                    this.playerPanel.Location = new Point(this.Holder[0].Left - 10, this.Holder[0].Top - 10);
                    this.playerPanel.BackColor = Color.DarkBlue;
                    this.playerPanel.Height = 150;
                    this.playerPanel.Width = 180;
                    this.playerPanel.Visible = false;
                }
                for (int botIndex = 0; botIndex < PokerGameConstants.NumberOfBots; botIndex++)
                {
                    IPlayer currentBot = this.pokerDatabase.TakeBotByIndex(botIndex);
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
                                horizontal = coordinates[botIndex].Item1;
                                vertical = coordinates[botIndex].Item2;
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
                for (int botIndex = 0; botIndex < PokerGameConstants.NumberOfBots; botIndex++)
                {
                    IPlayer currentBot = this.pokerDatabase.TakeBotByIndex(botIndex);
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
                    if (!this.IsRestartRequested)
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
                this.player.FixCall(ref this.GlobalCall, ref this.GlobalRaise, 1, this.GlobalRounds, ref this.callButton);
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
                this.player.FixCall(ref this.GlobalCall, ref this.GlobalRaise, 1, this.GlobalRounds, ref this.callButton);
            }

            if (this.player.OutOfChips || !this.player.CanPlay)
            {
                await this.AllIn();
                if (this.player.OutOfChips && !this.player.Folded)
                {
                    if (this.callButton.Text.Contains("All in") == false || this.raiseButton.Text.Contains("All in") == false)
                    {
                        this.disabledPlayers[0] = null;
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

                for (int botIndex = 0; botIndex < PokerGameConstants.NumberOfBots; botIndex++)
                {
                    IPlayer currentBot = this.pokerDatabase.TakeBotByIndex(botIndex);
                    currentBot.CanPlay = true;
                    if (!currentBot.OutOfChips && currentBot.CanPlay)
                    {
                        currentBot.FixCall(ref this.GlobalCall, ref this.GlobalRaise, 1, this.GlobalRounds, ref this.callButton);
                        currentBot.FixCall(ref this.GlobalCall, ref this.GlobalRaise, 2, this.GlobalRounds, ref this.callButton);

                        int name = 0;
                        this.Rules(currentBot);
                        MessageBox.Show(string.Format("{0}'s Turn", currentBot.Name));
                        this.ActionManager.AI(currentBot, this.GlobalCall, this.potTextBox, ref this.GlobalRaise, ref this.IsRaisingActivated, ref this.GlobalRounds, name);

                        this.turnCount++;
                        currentBot.CanPlay = false;
                    }
                    if (currentBot.OutOfChips && !currentBot.Folded)
                    {
                        this.disabledPlayers[botIndex + 1] = null;
                        this.maxPlayersLeft--;
                        currentBot.Folded = true;
                    }
                    if (currentBot.OutOfChips || !currentBot.CanPlay)
                    {
                        await this.CheckRaise(botIndex, botIndex);
                        this.pokerDatabase.TakeBotByIndex(botIndex).CanPlay = true;
                    }
                }

                if (this.player.OutOfChips && !this.player.Folded)
                {
                    if (this.callButton.Text.Contains("All in") == false || this.raiseButton.Text.Contains("All in") == false)
                    {
                        this.disabledPlayers[0] = null;
                        this.maxPlayersLeft--;
                        this.player.Folded = true;
                    }
                }
                await this.AllIn();
                if (!this.IsRestartRequested)
                {
                    await this.Turns();
                }
                this.IsRestartRequested = false;
            }
        }

        private void Rules(IPlayer currentPlayer)
        {
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
                    if (this.reserveArray[i] == int.Parse(this.Holder[currentPlayer.CardOne].Tag.ToString()) &&
                        this.reserveArray[i + 1] == int.Parse(this.Holder[currentPlayer.CardTwo].Tag.ToString()))
                    {
                        this.CombinationsDatabase.rPairFromHand(currentPlayer, i, this.winList, this.reserveArray, ref this.sorted);

                        this.CombinationsDatabase.rPairTwoPair(currentPlayer, i, this.winList, this.reserveArray, ref this.sorted);

                        this.CombinationsDatabase.rTwoPair(currentPlayer, i, this.winList, this.reserveArray, ref this.sorted);

                        this.CombinationsDatabase.rThreeOfAKind(currentPlayer, this.winList, this.reserveArray, ref this.sorted, StraightTwo);

                        this.CombinationsDatabase.rStraight(currentPlayer, this.winList, this.reserveArray, ref this.sorted, StraightTwo);

                        this.CombinationsDatabase.rFlush(currentPlayer, i, ref vf, this.winList, this.reserveArray, ref this.sorted, StraightOne);

                        this.CombinationsDatabase.rFullHouse(currentPlayer, ref this.GlobalType, ref done, this.winList, this.reserveArray, ref this.sorted, StraightTwo);

                        this.CombinationsDatabase.rFourOfAKind(currentPlayer, this.winList, this.reserveArray, ref this.sorted, StraightTwo);

                        this.CombinationsDatabase.rStraightFlush(currentPlayer, this.winList, this.reserveArray, ref this.sorted, st1, st2, st3, st4);

                        this.CombinationsDatabase.rHighCard(currentPlayer, i, this.winList, this.reserveArray, ref this.sorted);
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
                    if (this.CheckWinners.Contains(PokerGameConstants.DefaultPlayerName))
                    {
                        this.GlobalChips += int.Parse(this.potTextBox.Text) / this.winnersCount;
                        this.chipsTexBox.Text = this.GlobalChips.ToString();
                        this.playerPanel.Visible = true;

                    }

                    for (int botIndex = 0; botIndex < PokerGameConstants.NumberOfBots; botIndex++)
                    {
                        IPlayer currentBot = this.pokerDatabase.TakeBotByIndex(botIndex);
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
                    if (CheckWinners.Contains(PokerGameConstants.DefaultPlayerName))
                    {
                        GlobalChips += int.Parse(potTextBox.Text);

                        //await Finish(1);

                        // this.playerPanel.Visible = true;
                    }

                    for (int botIndex = 0; botIndex < PokerGameConstants.NumberOfBots; botIndex++)
                    {
                        IPlayer currentBot = this.pokerDatabase.TakeBotByIndex(botIndex);
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
            if (this.IsRaisingActivated)
            {
                this.turnCount = 0;
                this.IsRaisingActivated = false;
                this.raisedTurn = currentTurn;
                this.changed = true;
            }
            else
            {
                if (this.turnCount >= this.maxPlayersLeft - 1 || !this.changed && this.turnCount == this.maxPlayersLeft)
                {
                    if (currentTurn == raisedTurn - 1 || !this.changed && this.turnCount == this.maxPlayersLeft || this.raisedTurn == 0 && currentTurn == 5)
                    {
                        this.changed = false;
                        this.turnCount = 0;
                        this.GlobalRaise = 0;
                        this.GlobalCall = 0;
                        this.raisedTurn = 123;
                        this.GlobalRounds++;
                        if (!this.player.OutOfChips)
                        {
                            this.playerStatus.Text = "";
                        }
                            
                        for (int botIndex = 0; botIndex < PokerGameConstants.NumberOfBots; botIndex++)
                        {
                            IPlayer currentBot = this.pokerDatabase.TakeBotByIndex(botIndex);
                            if (!currentBot.OutOfChips)
                            {
                                currentBot.Status.Text = "";
                            }
                        }
                    }
                }
            }

            if (this.GlobalRounds == (int)PokerEnum.Flop)
            {
                for (int j = 12; j <= 14; j++)
                {
                    if (this.Holder[j].Image != this.Deck[j])
                    {
                        this.Holder[j].Image = this.Deck[j];
                        this.player.Call = 0;
                        this.player.Raise = 0;
                        this.BotEraser.EraseBotCall(this.pokerDatabase);
                        this.BotEraser.EraseBotRaise(this.pokerDatabase);
                    }
                }
            }

            if (this.GlobalRounds == (int)PokerEnum.Turn)
            {
                for (int j = 14; j <= 15; j++)
                {
                    if (this.Holder[j].Image != this.Deck[j])
                    {
                        this.Holder[j].Image = this.Deck[j];
                        this.player.Call = 0;
                        this.player.Raise = 0;
                        this.BotEraser.EraseBotCall(this.pokerDatabase);
                        this.BotEraser.EraseBotRaise(this.pokerDatabase);
                    }
                }
            }

            if (this.GlobalRounds == (int)PokerEnum.River)
            {
                for (int j = 15; j <= 16; j++)
                {
                    if (this.Holder[j].Image != this.Deck[j])
                    {
                        this.Holder[j].Image = this.Deck[j];
                        this.player.Call = 0;
                        this.player.Raise = 0;
                        this.BotEraser.EraseBotRaise(this.pokerDatabase);
                        this.BotEraser.EraseBotCall(this.pokerDatabase);
                    }
                }
            }

            if (this.GlobalRounds == (int)PokerEnum.End && this.maxPlayersLeft == 6)
            {
                string fixedLast = "qwerty";
                if (!this.playerStatus.Text.Contains("Fold"))
                {
                    fixedLast = PokerGameConstants.DefaultPlayerName;
                    this.Rules(this.player);
                }

                for (int botIndex = 0; botIndex < this.pokerDatabase.BotsOnTable.Count(); botIndex++)
                {
                    IPlayer currentBot = this.pokerDatabase.TakeBotByIndex(botIndex);
                    if (!currentBot.Status.Text.Contains("Fold"))
                    {
                        fixedLast = currentBot.Name;
                        this.Rules(currentBot);
                    }
                }

                this.Winner(this.player.Type, this.player.Power, PokerGameConstants.DefaultPlayerName, this.GlobalChips, fixedLast);
                for (int i = 0; i < PokerGameConstants.NumberOfBots; i++)
                {
                    IPlayer currentBot = this.pokerDatabase.TakeBotByIndex(i);
                    this.Winner(currentBot.Type, currentBot.Power, currentBot.Name, currentBot.Chips, fixedLast);
                }

                this.IsRestartRequested = true;
                this.player.CanPlay = true;
                this.player.OutOfChips = false;

                this.BotEraser.EnableBotChips(this.pokerDatabase);

                if (this.GlobalChips <= 0)
                {
                    AddChips f2 = new AddChips();
                    f2.ShowDialog();
                    if (f2.a != 0)
                    {
                        this.GlobalChips = f2.a;
                        for (int botIndex = 0; botIndex < PokerGameConstants.NumberOfBots; botIndex++)
                        {
                            IPlayer currentBot = this.pokerDatabase.TakeBotByIndex(botIndex);
                            currentBot.Chips += f2.a;
                        }

                        this.player.OutOfChips = false;
                        this.player.CanPlay = true;
                        this.raiseButton.Enabled = true;
                        this.foldButton.Enabled = true;
                        this.checkButton.Enabled = true;
                        this.raiseButton.Text = "Raise";
                    }
                }

                this.BotEraser.DisableBotPanel(this.pokerDatabase);
                this.BotEraser.EraseBotCall(this.pokerDatabase);
                this.BotEraser.EraseBotRaise(this.pokerDatabase);
                this.BotEraser.EraseBotPower(this.pokerDatabase);
                this.BotEraser.EraseBotType(this.pokerDatabase);

                this.playerPanel.Visible = false;
                this.player.Call = 0;
                this.player.Raise = 0;
                this.player.Power = 0;
                this.player.Type = -1;
                this.playerStatus.Text = "";

                this.GlobalCall = bigBlind;
                this.GlobalRaise = 0;
                this.ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < PokerGameConstants.MaximalPlayers; i++)
                {
                    this.disabledPlayers[i] = null;
                }

                this.GlobalRounds = 0;
                this.GlobalType = 0;
                this.winnersCount = 0;
                this.sorted.Current = 0;
                this.sorted.Power = 0;
                this.potTextBox.Text = "0";

                this.winList.Clear();
                this.ints.Clear();
                this.CheckWinners.Clear();
                for (int os = 0; os < PokerGameConstants.CardsOnTable; os++)
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
            if (this.GlobalChips <= 0 && !this.intsAdded)
            {
                if (this.playerStatus.Text.Contains("Raise"))
                {
                    this.ints.Add(GlobalChips);
                    this.intsAdded = true;
                }
                if (this.playerStatus.Text.Contains("Call"))
                {
                    this.ints.Add(GlobalChips);
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
                int index = -1;
                for (int playerIndex = 0; playerIndex < PokerGameConstants.MaximalPlayers; playerIndex++)
                {
                    if (this.disabledPlayers[playerIndex] == false)
                    {
                        index = playerIndex;
                        break;
                    }
                }

                if (index == 0)
                {
                    this.GlobalChips += int.Parse(this.potTextBox.Text);
                    this.chipsTexBox.Text = this.GlobalChips.ToString();
                    this.playerPanel.Visible = true;
                    MessageBox.Show("Player Wins");
                }
                for (int botIndex = 0; botIndex < PokerGameConstants.NumberOfBots; botIndex++)
                {
                    if (index == botIndex + 1)
                    {
                        IPlayer currentBot = this.pokerDatabase.TakeBotByIndex(botIndex);
                        currentBot.Chips += int.Parse(this.potTextBox.Text);
                        this.chipsTexBox.Text = currentBot.Chips.ToString();
                        currentBot.Panel.Visible = true;
                        MessageBox.Show("{0} Wins", currentBot.Name);
                    }
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
            if (numberOfDisabledPlayers < 6 && numberOfDisabledPlayers > 1 && this.GlobalRounds >= (int)PokerEnum.End)
            {
                await this.Finish(2);
            }
            #endregion
        }

        private void InitializeChipTexBoxText()
        {
            this.chipsTexBox.Text = "Chips : " + this.GlobalChips.ToString();
            for (int botIndex = 0; botIndex < PokerGameConstants.NumberOfBots; botIndex++)
            {
                IPlayer currentPlayer = this.pokerDatabase.TakeBotByIndex(botIndex);
                currentPlayer.ChipsTextBox.Text = "Chips : " + currentPlayer.Chips.ToString();
            }
        }

        public virtual void InitializeBots()
        {
            IPlayer botOne = new Player("Bot 1");
            botOne.CardOne = PokerGameConstants.BotOneCardOne;
            botOne.CardTwo = PokerGameConstants.BotOneCardTwo;

            IPlayer botTwo = new Player("Bot 2");
            botTwo.CardOne = PokerGameConstants.BotTwoCardOne;
            botTwo.CardTwo = PokerGameConstants.BotTwoCardTwo;
            
            IPlayer botThree = new Player("Bot 3");
            botThree.CardOne = PokerGameConstants.BotThreeCardOne;
            botThree.CardTwo = PokerGameConstants.BotThreeCardTwo;
            
            IPlayer botFour = new Player("Bot 4");
            botFour.CardOne = PokerGameConstants.BotFourCardOne;
            botFour.CardTwo = PokerGameConstants.BotFourCardTwo;
            
            IPlayer botFive = new Player("Bot 5");
            botFive.CardOne = PokerGameConstants.BotFiveCardOne;
            botFive.CardTwo = PokerGameConstants.BotFiveCardTwo;
            
            botOne.Status = this.botOneStatus;
            botTwo.Status = this.botTwoStatus;
            botThree.Status = this.botThreeStatus;
            botFour.Status = this.botFourStatus;
            botFive.Status = this.botFiveStatus;

            botOne.ChipsTextBox = this.botOneChips;
            botTwo.ChipsTextBox = this.botTwoChips;
            botThree.ChipsTextBox = this.botThreeChips;
            botFour.ChipsTextBox = this.botFourChips;
            botFive.ChipsTextBox = this.botFiveChips;

            botOne.ChipsTextBox.Enabled = false;
            botTwo.ChipsTextBox.Enabled = false;
            botThree.ChipsTextBox.Enabled = false;
            botFour.ChipsTextBox.Enabled = false;
            botFive.ChipsTextBox.Enabled = false;

            this.pokerDatabase.AddBot(botOne);
            this.pokerDatabase.AddBot(botTwo);
            this.pokerDatabase.AddBot(botThree);
            this.pokerDatabase.AddBot(botFour);
            this.pokerDatabase.AddBot(botFive);
        }

        public void InitializePlayer()
        {
            this.player = new Player(PokerGameConstants.DefaultPlayerName);
            this.playerPanel = new Panel();
            this.player.Status = this.playerStatus;
            this.player.CardOne = 0;
            this.player.CardTwo = 1;
        }

        private async Task Finish(int n)
        {
            if (n == 2)
            {
                FixWinners();
            }

            this.BotEraser.DisableBots(this.pokerDatabase);
            this.BotEraser.EraseBotCall(this.pokerDatabase);
            this.BotEraser.EraseBotRaise(this.pokerDatabase);
            this.BotEraser.DisableBotPanel(this.pokerDatabase);
            this.BotEraser.EraseBotPower(this.pokerDatabase);
            this.BotEraser.EraseBotType(this.pokerDatabase);
            this.BotEraser.EraseBotStatusText(this.pokerDatabase);
            this.BotEraser.UnFoldBots(this.pokerDatabase);
            this.BotEraser.EnableBotChips(this.pokerDatabase);

            this.ErasePlayerStats();
            this.EnablePlayer();

            this.EraseGameStats();

            if (this.GlobalChips <= 0)
            {
                AddChips f2 = new AddChips();
                f2.ShowDialog();
                if (f2.a != 0)
                {
                    this.GlobalChips = f2.a;
                    for (int botIndex = 0; botIndex < PokerGameConstants.NumberOfBots; botIndex++)
                    {
                        this.pokerDatabase.TakeBotByIndex(botIndex).Chips += f2.a;
                    }
                    this.player.OutOfChips = false;
                    this.player.CanPlay = true;
                    this.raiseButton.Enabled = true;
                    this.foldButton.Enabled = true;
                    this.checkButton.Enabled = true;
                    this.raiseButton.Text = "Raise";
                }
            }

            this.ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
            for (int os = 0; os < PokerGameConstants.CardsOnTable; os++)
            {
                this.Holder[os].Image = null;
                this.Holder[os].Invalidate();
                this.Holder[os].Visible = false;
            }
            await this.Shuffle();
            await this.Turns();
        }

        public void EnablePlayer()
        {
            this.player.Folded = false;
            this.player.CanPlay = true;
            this.player.OutOfChips = false;
        }

        public void EraseGameStats()
        {
            this.GlobalCall = bigBlind;
            this.GlobalRaise = PokerGameConstants.InitialRaise;
            this.GlobalRounds = PokerGameConstants.InitialRounds;
            this.IsRestartRequested = false;
            this.IsRaisingActivated = false;
            this.globalHeight = 0;
            this.globalWidth = 0;
            this.winnersCount = 0;
            this.maxPlayersLeft = PokerGameConstants.MaximalPlayers;
            this.raisedTurn = 1;
            for (int i = 0; i < PokerGameConstants.MaximalPlayers; i++)
            {
                this.disabledPlayers[i] = null;
            }

            this.CheckWinners.Clear();
            this.ints.Clear();
            this.winList.Clear();
            this.sorted.Current = 0;
            this.sorted.Power = 0;
            this.potTextBox.Text = "0";
            this.time = 60;
            this.maxChipsAmount = PokerGameConstants.MaximalChipsAmount;
            this.turnCount = 0;
            this.foldedPlayers = PokerGameConstants.InitialFoldedPlayers;
            this.GlobalType = 0;
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
                fixedLast = PokerGameConstants.DefaultPlayerName;
                this.Rules(this.player);
            }
            for (int botIndex = 0; botIndex < PokerGameConstants.NumberOfBots; botIndex++)
            {
                IPlayer currentBot = this.pokerDatabase.TakeBotByIndex(botIndex);
                if (!currentBot.Status.Text.Contains("Fold"))
                {
                    fixedLast = currentBot.Name;
                    this.Rules(currentBot);
                }
            }

            this.Winner(this.player.Type, player.Power, PokerGameConstants.DefaultPlayerName, GlobalChips, fixedLast);
            for (int botIndex = 0; botIndex < PokerGameConstants.NumberOfBots; botIndex++)
            {
                IPlayer currentBot = this.pokerDatabase.TakeBotByIndex(botIndex);
                this.Winner(currentBot.Type, currentBot.Power, currentBot.Name, currentBot.Chips, fixedLast);
            }
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
            if (this.GlobalChips <= 0)
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
            if (this.GlobalChips <= 0)
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
            if (this.GlobalChips >= this.GlobalCall)
            {
                this.callButton.Text = "Call " + this.GlobalCall.ToString();
            }
            else
            {
                this.callButton.Text = "All in";
                this.raiseButton.Enabled = false;
            }
            if (this.GlobalCall > 0)
            {
                this.checkButton.Enabled = false;
            }
            if (this.GlobalCall <= 0)
            {
                this.checkButton.Enabled = true;
                this.callButton.Text = "Call";
                this.callButton.Enabled = false;
            }
            if (this.GlobalChips <= 0)
            {
                this.raiseButton.Enabled = false;
            }
            int parsedValue;

            if (this.raiseTexBox.Text != "" && int.TryParse(this.raiseTexBox.Text, out parsedValue))
            {
                if (this.GlobalChips <= int.Parse(this.raiseTexBox.Text))
                {
                    this.raiseButton.Text = "All in";
                }
                else
                {
                    this.raiseButton.Text = "Raise";
                }
            }
            if (this.GlobalChips < this.GlobalCall)
            {
                this.raiseButton.Enabled = false;
            }
        }

        private async void FoldButton_Click(object sender, EventArgs e)
        {
            this.playerStatus.Text = "Fold";
            this.player.CanPlay = false;
            this.player.OutOfChips = true;
            await this.Turns();
        }

        private async void CheckButton_Click(object sender, EventArgs e)
        {
            if (this.GlobalCall <= 0)
            {
                this.player.CanPlay = false;
                this.playerStatus.Text = "Check";
            }
            else
            {
                this.playerStatus.Text = "All in " + this.GlobalChips;
                this.checkButton.Enabled = false;
            }
            await this.Turns();
        }

        private async void CallButton_Click(object sender, EventArgs e)
        {
            this.Rules(this.player);
            if (this.GlobalChips >= this.GlobalCall)
            {
                this.GlobalChips -= this.GlobalCall;
                this.chipsTexBox.Text = "Chips : " + this.GlobalChips.ToString();
                if (this.potTextBox.Text != "")
                {
                    this.potTextBox.Text = (int.Parse(this.potTextBox.Text) + this.GlobalCall).ToString();
                }
                else
                {
                    this.potTextBox.Text = this.GlobalCall.ToString();
                }
                this.player.CanPlay = false;
                this.playerStatus.Text = "Call " + this.GlobalCall;
                this.player.Call = this.GlobalCall;
            }
            else if (this.GlobalChips <= this.GlobalCall && this.GlobalCall > 0)
            {
                this.potTextBox.Text = (int.Parse(this.potTextBox.Text) + this.GlobalChips).ToString();
                this.playerStatus.Text = "All in " + this.GlobalChips;
                this.GlobalChips = 0;
                this.chipsTexBox.Text = "Chips : " + this.GlobalChips.ToString();
                this.player.CanPlay = false;
                this.foldButton.Enabled = false;
                this.player.Call = this.GlobalChips;
            }
            await this.Turns();
        }

        private async void RaiseButton_Click(object sender, EventArgs e)
        {
            this.Rules(this.player);
            int parsedValue;
            if (this.raiseTexBox.Text != "" && int.TryParse(this.raiseTexBox.Text, out parsedValue))
            {
                if (this.GlobalChips > this.GlobalCall)
                {
                    if (this.GlobalRaise * 2 > int.Parse(this.raiseTexBox.Text))
                    {
                        this.raiseTexBox.Text = (this.GlobalRaise * 2).ToString();
                        MessageBox.Show("You must raise atleast twice as the current raise !");
                        return;
                    }
                    else
                    {
                        if (this.GlobalChips >= int.Parse(this.raiseTexBox.Text))
                        {
                            this.GlobalCall = int.Parse(this.raiseTexBox.Text);
                            this.GlobalRaise = int.Parse(this.raiseTexBox.Text);
                            this.playerStatus.Text = "Raise " + this.GlobalCall.ToString();
                            this.potTextBox.Text = (int.Parse(this.potTextBox.Text) + this.GlobalCall).ToString();
                            this.callButton.Text = "Call";
                            this.GlobalChips -= int.Parse(this.raiseTexBox.Text);
                            this.IsRaisingActivated = true;
                            this.player.Raise = Convert.ToInt32(this.GlobalRaise);
                        }
                        else
                        {
                            this.GlobalCall = this.GlobalChips;
                            this.GlobalRaise = this.GlobalChips;
                            this.potTextBox.Text = (int.Parse(this.potTextBox.Text) + this.GlobalChips).ToString();
                            this.playerStatus.Text = "Raise " + this.GlobalCall.ToString();
                            this.GlobalChips = 0;
                            this.IsRaisingActivated = true;
                            this.player.Raise = Convert.ToInt32(this.GlobalRaise);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("This is a number only field");
                return;
            }
            this.player.CanPlay = false;
            await this.Turns();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (this.addTexBox.Text == "") { }
            else
            {
                this.GlobalChips += int.Parse(this.addTexBox.Text);
                for (int botIndex = 0; botIndex < PokerGameConstants.NumberOfBots; botIndex++)
                {
                    this.pokerDatabase.TakeBotByIndex(botIndex).Chips += int.Parse(this.addTexBox.Text);
                }
            }
            this.chipsTexBox.Text = "Chips : " + this.GlobalChips.ToString();
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

            if (int.Parse(smallBlindTexBox.Text) > PokerGameConstants.SmallBlindMaximum)
            {
                MessageBox.Show("The maximum of the Small Blind is 100 000 $");
                this.smallBlindTexBox.Text = this.smallBlind.ToString();
            }

            if (int.Parse(this.smallBlindTexBox.Text) < PokerGameConstants.SmallBlindValue)
            {
                MessageBox.Show("The minimum of the Small Blind is 250 $");
            }

            if (int.Parse(this.smallBlindTexBox.Text) >= PokerGameConstants.SmallBlindValue &&
                int.Parse(this.smallBlindTexBox.Text) <= PokerGameConstants.SmallBlindMaximum)
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

            if (int.Parse(this.bigBlindTexBox.Text) > PokerGameConstants.BigBlindMaxmum)
            {
                MessageBox.Show("The maximum of the Big Blind is 200 000");
                this.bigBlindTexBox.Text = this.bigBlind.ToString();
            }

            if (int.Parse(this.bigBlindTexBox.Text) < PokerGameConstants.BigBlindValue)
            {
                MessageBox.Show("The minimum of the Big Blind is 500 $");
            }

            if (int.Parse(this.bigBlindTexBox.Text) >= PokerGameConstants.BigBlindValue && 
                int.Parse(this.bigBlindTexBox.Text) <= PokerGameConstants.BigBlindMaxmum)
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