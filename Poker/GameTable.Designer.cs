namespace Poker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    partial class GameTable
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components;
        private Button foldButton;
        private Button checkButton;
        private Button callButton;
        private Button raiseButton;
        private ProgressBar progressBarTimer;
        private TextBox chipsTexBox;
        private Button addButton;
        private TextBox addTexBox;
        private TextBox botFiveChips;
        private TextBox botFourChips;
        private TextBox botThreeChips;
        private TextBox botTwoChips;
        private TextBox botOneChips;
        private TextBox potTexBox;
        private Button optionsButton;
        private Button bigBlindButton;
        private TextBox smallBlindTexBox;
        private Button smallBlindButton;
        private TextBox bigBlindTexBox;
        private Label botFiveStatus;
        private Label botFourStatus;
        private Label botThreeStatus;
        private Label botOneStatus;
        private Label playerStatus;
        private Label botTwoStatus;
        private Label globalLabel;
        private TextBox raiseTexBox;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.foldButton = new Button();
            this.checkButton = new Button();
            this.callButton = new Button();
            this.raiseButton = new Button();
            this.progressBarTimer = new ProgressBar();
            this.chipsTexBox = new TextBox();
            this.addButton = new Button();
            this.addTexBox = new TextBox();
            this.botFiveChips = new TextBox();
            this.botFourChips = new TextBox();
            this.botThreeChips = new TextBox();
            this.botTwoChips = new TextBox();
            this.botOneChips = new TextBox();
            this.potTexBox = new TextBox();
            this.optionsButton = new Button();
            this.bigBlindButton = new Button();
            this.smallBlindTexBox = new TextBox();
            this.smallBlindButton = new Button();
            this.bigBlindTexBox = new TextBox();
            this.botFiveStatus = new Label();
            this.botFourStatus = new Label();
            this.botThreeStatus = new Label();
            this.botOneStatus = new Label();
            this.playerStatus = new Label();
            this.botTwoStatus = new Label();
            this.globalLabel = new Label();
            this.raiseTexBox = new TextBox();
            this.SuspendLayout();

            this.foldButton.Anchor = AnchorStyles.Bottom;
            this.foldButton.Font = new Font("Microsoft Sans Serif", 17F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.foldButton.Location = new Point(335, 660);
            this.foldButton.Name = "Fold button";
            this.foldButton.Size = new Size(130, 62);
            this.foldButton.TabIndex = 0;
            this.foldButton.Text = "Fold";
            this.foldButton.UseVisualStyleBackColor = true;
            this.foldButton.Click += new EventHandler(this.FoldButton_Click);

            this.checkButton.Anchor = AnchorStyles.Bottom;
            this.checkButton.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.checkButton.Location = new Point(494, 660);
            this.checkButton.Name = "Check button";
            this.checkButton.Size = new Size(134, 62);
            this.checkButton.TabIndex = 2;
            this.checkButton.Text = "Check";
            this.checkButton.UseVisualStyleBackColor = true;
            this.checkButton.Click += new EventHandler(this.CheckButton_Click);

            this.callButton.Anchor = AnchorStyles.Bottom;
            this.callButton.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.callButton.Location = new Point(667, 661);
            this.callButton.Name = "Call button";
            this.callButton.Size = new Size(126, 62);
            this.callButton.TabIndex = 3;
            this.callButton.Text = "Call";
            this.callButton.UseVisualStyleBackColor = true;
            this.callButton.Click += new EventHandler(this.CallButton_Click);

            this.raiseButton.Anchor = AnchorStyles.Bottom;
            this.raiseButton.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.raiseButton.Location = new Point(835, 661);
            this.raiseButton.Name = "Raise button";
            this.raiseButton.Size = new Size(124, 62);
            this.raiseButton.TabIndex = 4;
            this.raiseButton.Text = "Raise";
            this.raiseButton.UseVisualStyleBackColor = true;
            this.raiseButton.Click += new EventHandler(this.RaiseButton_Click);

            this.progressBarTimer.Anchor = AnchorStyles.Bottom;
            this.progressBarTimer.BackColor = SystemColors.Control;
            this.progressBarTimer.Location = new Point(335, 631);
            this.progressBarTimer.Maximum = 1000;
            this.progressBarTimer.Name = "Progress bar timer";
            this.progressBarTimer.Size = new Size(667, 23);
            this.progressBarTimer.TabIndex = 5;
            this.progressBarTimer.Value = 1000;

            this.chipsTexBox.Anchor = AnchorStyles.Bottom;
            this.chipsTexBox.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.chipsTexBox.Location = new Point(755, 553);
            this.chipsTexBox.Name = "Chips texbox";
            this.chipsTexBox.Size = new Size(163, 23);
            this.chipsTexBox.TabIndex = 6;
            this.chipsTexBox.Text = "Chips : 0";

            this.addButton.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.addButton.Location = new Point(12, 697);
            this.addButton.Name = "Add button";
            this.addButton.Size = new Size(75, 25);
            this.addButton.TabIndex = 7;
            this.addButton.Text = "AddChips";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new EventHandler(this.AddButton_Click);

            this.addTexBox.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.addTexBox.Location = new Point(93, 700);
            this.addTexBox.Name = "Add texbox";
            this.addTexBox.Size = new Size(125, 20);
            this.addTexBox.TabIndex = 8;

            this.botFiveChips.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.botFiveChips.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.botFiveChips.Location = new Point(1012, 553);
            this.botFiveChips.Name = "tbBotChips5";
            this.botFiveChips.Size = new Size(152, 23);
            this.botFiveChips.TabIndex = 9;
            this.botFiveChips.Text = "Chips : 0";

            this.botFourChips.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.botFourChips.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.botFourChips.Location = new Point(970, 81);
            this.botFourChips.Name = "tbBotChips4";
            this.botFourChips.Size = new Size(123, 23);
            this.botFourChips.TabIndex = 10;
            this.botFourChips.Text = "Chips : 0";

            this.botThreeChips.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.botThreeChips.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.botThreeChips.Location = new Point(755, 81);
            this.botThreeChips.Name = "tbBotChips3";
            this.botThreeChips.Size = new Size(125, 23);
            this.botThreeChips.TabIndex = 11;
            this.botThreeChips.Text = "Chips : 0";

            this.botTwoChips.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.botTwoChips.Location = new Point(276, 81);
            this.botTwoChips.Name = "tbBotChips2";
            this.botTwoChips.Size = new Size(133, 23);
            this.botTwoChips.TabIndex = 12;
            this.botTwoChips.Text = "Chips : 0";

            this.botOneChips.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.botOneChips.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.botOneChips.Location = new Point(181, 553);
            this.botOneChips.Name = "tbBotChips1";
            this.botOneChips.Size = new Size(142, 23);
            this.botOneChips.TabIndex = 13;
            this.botOneChips.Text = "Chips : 0";

            this.potTexBox.Anchor = AnchorStyles.None;
            this.potTexBox.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.potTexBox.Location = new Point(606, 212);
            this.potTexBox.Name = "tbPot";
            this.potTexBox.Size = new Size(125, 23);
            this.potTexBox.TabIndex = 14;
            this.potTexBox.Text = "0";

            this.optionsButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.optionsButton.Location = new Point(12, 12);
            this.optionsButton.Name = "bOptions";
            this.optionsButton.Size = new Size(75, 36);
            this.optionsButton.TabIndex = 15;
            this.optionsButton.Text = "BB/SB";
            this.optionsButton.UseVisualStyleBackColor = true;
            this.optionsButton.Click += new System.EventHandler(this.OptionsButton_Click);

            this.bigBlindButton.Location = new Point(12, 254);
            this.bigBlindButton.Name = "bBB";
            this.bigBlindButton.Size = new Size(75, 23);
            this.bigBlindButton.TabIndex = 16;
            this.bigBlindButton.Text = "Big Blind";
            this.bigBlindButton.UseVisualStyleBackColor = true;
            this.bigBlindButton.Click += new System.EventHandler(this.BigBlindButton_Click);

            this.smallBlindTexBox.Location = new Point(12, 228);
            this.smallBlindTexBox.Name = "tbSB";
            this.smallBlindTexBox.Size = new Size(75, 20);
            this.smallBlindTexBox.TabIndex = 17;
            this.smallBlindTexBox.Text = "250";

            this.smallBlindButton.Location = new Point(12, 199);
            this.smallBlindButton.Name = "bSB";
            this.smallBlindButton.Size = new Size(75, 23);
            this.smallBlindButton.TabIndex = 18;
            this.smallBlindButton.Text = "Small Blind";
            this.smallBlindButton.UseVisualStyleBackColor = true;
            this.smallBlindButton.Click += new System.EventHandler(this.SmallBlindButton_Click);

            this.bigBlindTexBox.Location = new Point(12, 283);
            this.bigBlindTexBox.Name = "tbBB";
            this.bigBlindTexBox.Size = new Size(75, 20);
            this.bigBlindTexBox.TabIndex = 19;
            this.bigBlindTexBox.Text = "500";

            this.botFiveStatus.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.botFiveStatus.Location = new Point(1012, 579);
            this.botFiveStatus.Name = "b5Status";
            this.botFiveStatus.Size = new Size(152, 32);
            this.botFiveStatus.TabIndex = 26;

            this.botFourStatus.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.botFourStatus.Location = new Point(970, 107);
            this.botFourStatus.Name = "b4Status";
            this.botFourStatus.Size = new Size(123, 32);
            this.botFourStatus.TabIndex = 27;

            this.botThreeStatus.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.botThreeStatus.Location = new Point(755, 107);
            this.botThreeStatus.Name = "b3Status";
            this.botThreeStatus.Size = new Size(125, 32);
            this.botThreeStatus.TabIndex = 28;

            this.botOneStatus.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.botOneStatus.Location = new Point(181, 579);
            this.botOneStatus.Name = "b1Status";
            this.botOneStatus.Size = new Size(142, 32);
            this.botOneStatus.TabIndex = 29;

            this.playerStatus.Anchor = AnchorStyles.Bottom;
            this.playerStatus.Location = new Point(755, 579);
            this.playerStatus.Name = "pStatus";
            this.playerStatus.Size = new Size(163, 32);
            this.playerStatus.TabIndex = 30;

            this.botTwoStatus.Location = new Point(276, 107);
            this.botTwoStatus.Name = "b2Status";
            this.botTwoStatus.Size = new Size(133, 32);
            this.botTwoStatus.TabIndex = 31;

            this.globalLabel.Anchor = AnchorStyles.None;
            this.globalLabel.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.globalLabel.Location = new Point(654, 188);
            this.globalLabel.Name = "label1";
            this.globalLabel.Size = new Size(31, 21);
            this.globalLabel.TabIndex = 0;
            this.globalLabel.Text = "Pot";

            this.raiseTexBox.Anchor = AnchorStyles.Bottom;
            this.raiseTexBox.Location = new Point(965, 703);
            this.raiseTexBox.Name = "tbRaise";
            this.raiseTexBox.Size = new Size(108, 20);
            this.raiseTexBox.TabIndex = 0;

            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackgroundImage = global::Poker.Properties.Resources.poker_table___Copy;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.ClientSize = new Size(1350, 729);
            this.Controls.Add(this.raiseTexBox);
            this.Controls.Add(this.globalLabel);
            this.Controls.Add(this.botTwoStatus);
            this.Controls.Add(this.playerStatus);
            this.Controls.Add(this.botOneStatus);
            this.Controls.Add(this.botThreeStatus);
            this.Controls.Add(this.botFourStatus);
            this.Controls.Add(this.botFiveStatus);
            this.Controls.Add(this.bigBlindTexBox);
            this.Controls.Add(this.smallBlindButton);
            this.Controls.Add(this.smallBlindTexBox);
            this.Controls.Add(this.bigBlindButton);
            this.Controls.Add(this.optionsButton);
            this.Controls.Add(this.potTexBox);
            this.Controls.Add(this.botOneChips);
            this.Controls.Add(this.botTwoChips);
            this.Controls.Add(this.botThreeChips);
            this.Controls.Add(this.botFourChips);
            this.Controls.Add(this.botFiveChips);
            this.Controls.Add(this.addTexBox);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.chipsTexBox);
            this.Controls.Add(this.progressBarTimer);
            this.Controls.Add(this.raiseButton);
            this.Controls.Add(this.callButton);
            this.Controls.Add(this.checkButton);
            this.Controls.Add(this.foldButton);
            this.DoubleBuffered = true;
            this.Name = "Poker Table";
            this.Text = "GLS Texas Poker";
            this.Layout += new LayoutEventHandler(this.Layout_Change);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

