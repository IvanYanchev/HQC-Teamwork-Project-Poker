namespace Poker
{
    using System.ComponentModel;
    using System.Windows.Forms;

    partial class GameTable
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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

        #region Windows Form Designer generated code

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
            this.bBB = new Button();
            this.tbSB = new TextBox();
            this.buttonSB = new Button();
            this.tbBB = new TextBox();
            this.botFiveStatus = new Label();
            this.botFourStatus = new Label();
            this.botThreeStatus = new Label();
            this.botOneStatus = new Label();
            this.playerStatus = new Label();
            this.botTwoStatus = new Label();
            this.globalLabel = new Label();
            this.raiseTexBox = new TextBox();
            this.SuspendLayout();
            // 
            // bFold
            // 
            this.foldButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.foldButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.foldButton.Location = new System.Drawing.Point(335, 660);
            this.foldButton.Name = "bFold";
            this.foldButton.Size = new System.Drawing.Size(130, 62);
            this.foldButton.TabIndex = 0;
            this.foldButton.Text = "Fold";
            this.foldButton.UseVisualStyleBackColor = true;
            this.foldButton.Click += new System.EventHandler(this.FoldButton_Click);
            // 
            // bCheck
            // 
            this.checkButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.checkButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkButton.Location = new System.Drawing.Point(494, 660);
            this.checkButton.Name = "bCheck";
            this.checkButton.Size = new System.Drawing.Size(134, 62);
            this.checkButton.TabIndex = 2;
            this.checkButton.Text = "Check";
            this.checkButton.UseVisualStyleBackColor = true;
            this.checkButton.Click += new System.EventHandler(this.CheckButton_Click);
            // 
            // bCall
            // 
            this.callButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.callButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.callButton.Location = new System.Drawing.Point(667, 661);
            this.callButton.Name = "bCall";
            this.callButton.Size = new System.Drawing.Size(126, 62);
            this.callButton.TabIndex = 3;
            this.callButton.Text = "Call";
            this.callButton.UseVisualStyleBackColor = true;
            this.callButton.Click += new System.EventHandler(this.CallButton_Click);
            // 
            // bRaise
            // 
            this.raiseButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.raiseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.raiseButton.Location = new System.Drawing.Point(835, 661);
            this.raiseButton.Name = "bRaise";
            this.raiseButton.Size = new System.Drawing.Size(124, 62);
            this.raiseButton.TabIndex = 4;
            this.raiseButton.Text = "Raise";
            this.raiseButton.UseVisualStyleBackColor = true;
            this.raiseButton.Click += new System.EventHandler(this.RaiseButton_Click);
            // 
            // pbTimer
            // 
            this.progressBarTimer.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.progressBarTimer.BackColor = System.Drawing.SystemColors.Control;
            this.progressBarTimer.Location = new System.Drawing.Point(335, 631);
            this.progressBarTimer.Maximum = 1000;
            this.progressBarTimer.Name = "pbTimer";
            this.progressBarTimer.Size = new System.Drawing.Size(667, 23);
            this.progressBarTimer.TabIndex = 5;
            this.progressBarTimer.Value = 1000;
            // 
            // tbChips
            // 
            this.chipsTexBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.chipsTexBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chipsTexBox.Location = new System.Drawing.Point(755, 553);
            this.chipsTexBox.Name = "tbChips";
            this.chipsTexBox.Size = new System.Drawing.Size(163, 23);
            this.chipsTexBox.TabIndex = 6;
            this.chipsTexBox.Text = "Chips : 0";
            // 
            // bAdd
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addButton.Location = new System.Drawing.Point(12, 697);
            this.addButton.Name = "bAdd";
            this.addButton.Size = new System.Drawing.Size(75, 25);
            this.addButton.TabIndex = 7;
            this.addButton.Text = "AddChips";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // tbAdd
            // 
            this.addTexBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addTexBox.Location = new System.Drawing.Point(93, 700);
            this.addTexBox.Name = "tbAdd";
            this.addTexBox.Size = new System.Drawing.Size(125, 20);
            this.addTexBox.TabIndex = 8;
            // 
            // tbBotChips5
            // 
            this.botFiveChips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.botFiveChips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.botFiveChips.Location = new System.Drawing.Point(1012, 553);
            this.botFiveChips.Name = "tbBotChips5";
            this.botFiveChips.Size = new System.Drawing.Size(152, 23);
            this.botFiveChips.TabIndex = 9;
            this.botFiveChips.Text = "Chips : 0";
            // 
            // tbBotChips4
            // 
            this.botFourChips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.botFourChips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.botFourChips.Location = new System.Drawing.Point(970, 81);
            this.botFourChips.Name = "tbBotChips4";
            this.botFourChips.Size = new System.Drawing.Size(123, 23);
            this.botFourChips.TabIndex = 10;
            this.botFourChips.Text = "Chips : 0";
            // 
            // tbBotChips3
            // 
            this.botThreeChips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.botThreeChips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.botThreeChips.Location = new System.Drawing.Point(755, 81);
            this.botThreeChips.Name = "tbBotChips3";
            this.botThreeChips.Size = new System.Drawing.Size(125, 23);
            this.botThreeChips.TabIndex = 11;
            this.botThreeChips.Text = "Chips : 0";
            // 
            // tbBotChips2
            // 
            this.botTwoChips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.botTwoChips.Location = new System.Drawing.Point(276, 81);
            this.botTwoChips.Name = "tbBotChips2";
            this.botTwoChips.Size = new System.Drawing.Size(133, 23);
            this.botTwoChips.TabIndex = 12;
            this.botTwoChips.Text = "Chips : 0";
            // 
            // tbBotChips1
            // 
            this.botOneChips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.botOneChips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.botOneChips.Location = new System.Drawing.Point(181, 553);
            this.botOneChips.Name = "tbBotChips1";
            this.botOneChips.Size = new System.Drawing.Size(142, 23);
            this.botOneChips.TabIndex = 13;
            this.botOneChips.Text = "Chips : 0";
            // 
            // tbPot
            // 
            this.potTexBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.potTexBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.potTexBox.Location = new System.Drawing.Point(606, 212);
            this.potTexBox.Name = "tbPot";
            this.potTexBox.Size = new System.Drawing.Size(125, 23);
            this.potTexBox.TabIndex = 14;
            this.potTexBox.Text = "0";
            // 
            // bOptions
            // 
            this.optionsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.optionsButton.Location = new System.Drawing.Point(12, 12);
            this.optionsButton.Name = "bOptions";
            this.optionsButton.Size = new System.Drawing.Size(75, 36);
            this.optionsButton.TabIndex = 15;
            this.optionsButton.Text = "BB/SB";
            this.optionsButton.UseVisualStyleBackColor = true;
            this.optionsButton.Click += new System.EventHandler(this.OptionsButton_Click);
            // 
            // bBB
            // 
            this.bBB.Location = new System.Drawing.Point(12, 254);
            this.bBB.Name = "bBB";
            this.bBB.Size = new System.Drawing.Size(75, 23);
            this.bBB.TabIndex = 16;
            this.bBB.Text = "Big Blind";
            this.bBB.UseVisualStyleBackColor = true;
            this.bBB.Click += new System.EventHandler(this.bBB_Click);
            // 
            // tbSB
            // 
            this.tbSB.Location = new System.Drawing.Point(12, 228);
            this.tbSB.Name = "tbSB";
            this.tbSB.Size = new System.Drawing.Size(75, 20);
            this.tbSB.TabIndex = 17;
            this.tbSB.Text = "250";
            // 
            // bSB
            // 
            this.buttonSB.Location = new System.Drawing.Point(12, 199);
            this.buttonSB.Name = "bSB";
            this.buttonSB.Size = new System.Drawing.Size(75, 23);
            this.buttonSB.TabIndex = 18;
            this.buttonSB.Text = "Small Blind";
            this.buttonSB.UseVisualStyleBackColor = true;
            this.buttonSB.Click += new System.EventHandler(this.bSB_Click);
            // 
            // tbBB
            // 
            this.tbBB.Location = new System.Drawing.Point(12, 283);
            this.tbBB.Name = "tbBB";
            this.tbBB.Size = new System.Drawing.Size(75, 20);
            this.tbBB.TabIndex = 19;
            this.tbBB.Text = "500";
            // 
            // b5Status
            // 
            this.botFiveStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.botFiveStatus.Location = new System.Drawing.Point(1012, 579);
            this.botFiveStatus.Name = "b5Status";
            this.botFiveStatus.Size = new System.Drawing.Size(152, 32);
            this.botFiveStatus.TabIndex = 26;
            // 
            // b4Status
            // 
            this.botFourStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.botFourStatus.Location = new System.Drawing.Point(970, 107);
            this.botFourStatus.Name = "b4Status";
            this.botFourStatus.Size = new System.Drawing.Size(123, 32);
            this.botFourStatus.TabIndex = 27;
            // 
            // b3Status
            // 
            this.botThreeStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.botThreeStatus.Location = new System.Drawing.Point(755, 107);
            this.botThreeStatus.Name = "b3Status";
            this.botThreeStatus.Size = new System.Drawing.Size(125, 32);
            this.botThreeStatus.TabIndex = 28;
            // 
            // b1Status
            // 
            this.botOneStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.botOneStatus.Location = new System.Drawing.Point(181, 579);
            this.botOneStatus.Name = "b1Status";
            this.botOneStatus.Size = new System.Drawing.Size(142, 32);
            this.botOneStatus.TabIndex = 29;
            // 
            // pStatus
            // 
            this.playerStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playerStatus.Location = new System.Drawing.Point(755, 579);
            this.playerStatus.Name = "pStatus";
            this.playerStatus.Size = new System.Drawing.Size(163, 32);
            this.playerStatus.TabIndex = 30;
            // 
            // b2Status
            // 
            this.botTwoStatus.Location = new System.Drawing.Point(276, 107);
            this.botTwoStatus.Name = "b2Status";
            this.botTwoStatus.Size = new System.Drawing.Size(133, 32);
            this.botTwoStatus.TabIndex = 31;
            // 
            // label1
            // 
            this.globalLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.globalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.globalLabel.Location = new System.Drawing.Point(654, 188);
            this.globalLabel.Name = "label1";
            this.globalLabel.Size = new System.Drawing.Size(31, 21);
            this.globalLabel.TabIndex = 0;
            this.globalLabel.Text = "Pot";
            // 
            // tbRaise
            // 
            this.raiseTexBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.raiseTexBox.Location = new System.Drawing.Point(965, 703);
            this.raiseTexBox.Name = "tbRaise";
            this.raiseTexBox.Size = new System.Drawing.Size(108, 20);
            this.raiseTexBox.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Poker.Properties.Resources.poker_table___Copy;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.raiseTexBox);
            this.Controls.Add(this.globalLabel);
            this.Controls.Add(this.botTwoStatus);
            this.Controls.Add(this.playerStatus);
            this.Controls.Add(this.botOneStatus);
            this.Controls.Add(this.botThreeStatus);
            this.Controls.Add(this.botFourStatus);
            this.Controls.Add(this.botFiveStatus);
            this.Controls.Add(this.tbBB);
            this.Controls.Add(this.buttonSB);
            this.Controls.Add(this.tbSB);
            this.Controls.Add(this.bBB);
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
            this.Name = "Form1";
            this.Text = "GLS Texas Poker";
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.Layout_Change);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private Button bBB;
        private TextBox tbSB;
        private Button buttonSB;
        private TextBox tbBB;
        private Label botFiveStatus;
        private Label botFourStatus;
        private Label botThreeStatus;
        private Label botOneStatus;
        private Label playerStatus;
        private Label botTwoStatus;
        private Label globalLabel;
        private TextBox raiseTexBox;
    }
}

