namespace Poker
{
    using System.ComponentModel;

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
            this.foldButton = new System.Windows.Forms.Button();
            this.checkButton = new System.Windows.Forms.Button();
            this.callButton = new System.Windows.Forms.Button();
            this.raiseButton = new System.Windows.Forms.Button();
            this.pbTimer = new System.Windows.Forms.ProgressBar();
            this.tableChips = new System.Windows.Forms.TextBox();
            this.bAdd = new System.Windows.Forms.Button();
            this.tbAdd = new System.Windows.Forms.TextBox();
            this.botFiveChips = new System.Windows.Forms.TextBox();
            this.botFourChips = new System.Windows.Forms.TextBox();
            this.botThreeChips = new System.Windows.Forms.TextBox();
            this.botTwoChips = new System.Windows.Forms.TextBox();
            this.botOneChips = new System.Windows.Forms.TextBox();
            this.tablePot = new System.Windows.Forms.TextBox();
            this.bOptions = new System.Windows.Forms.Button();
            this.bBB = new System.Windows.Forms.Button();
            this.tbSB = new System.Windows.Forms.TextBox();
            this.bSB = new System.Windows.Forms.Button();
            this.tbBB = new System.Windows.Forms.TextBox();
            this.botFiveStatus = new System.Windows.Forms.Label();
            this.botFourStatus = new System.Windows.Forms.Label();
            this.botThreeStatus = new System.Windows.Forms.Label();
            this.botOneStatus = new System.Windows.Forms.Label();
            this.playerStatus = new System.Windows.Forms.Label();
            this.botTwoStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbRaise = new System.Windows.Forms.TextBox();
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
            this.foldButton.Click += new System.EventHandler(this.bFold_Click);
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
            this.checkButton.Click += new System.EventHandler(this.bCheck_Click);
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
            this.callButton.Click += new System.EventHandler(this.bCall_Click);
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
            this.raiseButton.Click += new System.EventHandler(this.bRaise_Click);
            // 
            // pbTimer
            // 
            this.pbTimer.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pbTimer.BackColor = System.Drawing.SystemColors.Control;
            this.pbTimer.Location = new System.Drawing.Point(335, 631);
            this.pbTimer.Maximum = 1000;
            this.pbTimer.Name = "pbTimer";
            this.pbTimer.Size = new System.Drawing.Size(667, 23);
            this.pbTimer.TabIndex = 5;
            this.pbTimer.Value = 1000;
            // 
            // tbChips
            // 
            this.tableChips.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tableChips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tableChips.Location = new System.Drawing.Point(755, 553);
            this.tableChips.Name = "tbChips";
            this.tableChips.Size = new System.Drawing.Size(163, 23);
            this.tableChips.TabIndex = 6;
            this.tableChips.Text = "Chips : 0";
            // 
            // bAdd
            // 
            this.bAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bAdd.Location = new System.Drawing.Point(12, 697);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(75, 25);
            this.bAdd.TabIndex = 7;
            this.bAdd.Text = "AddChips";
            this.bAdd.UseVisualStyleBackColor = true;
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // tbAdd
            // 
            this.tbAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbAdd.Location = new System.Drawing.Point(93, 700);
            this.tbAdd.Name = "tbAdd";
            this.tbAdd.Size = new System.Drawing.Size(125, 20);
            this.tbAdd.TabIndex = 8;
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
            this.tablePot.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tablePot.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tablePot.Location = new System.Drawing.Point(606, 212);
            this.tablePot.Name = "tbPot";
            this.tablePot.Size = new System.Drawing.Size(125, 23);
            this.tablePot.TabIndex = 14;
            this.tablePot.Text = "0";
            // 
            // bOptions
            // 
            this.bOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bOptions.Location = new System.Drawing.Point(12, 12);
            this.bOptions.Name = "bOptions";
            this.bOptions.Size = new System.Drawing.Size(75, 36);
            this.bOptions.TabIndex = 15;
            this.bOptions.Text = "BB/SB";
            this.bOptions.UseVisualStyleBackColor = true;
            this.bOptions.Click += new System.EventHandler(this.bOptions_Click);
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
            this.bSB.Location = new System.Drawing.Point(12, 199);
            this.bSB.Name = "bSB";
            this.bSB.Size = new System.Drawing.Size(75, 23);
            this.bSB.TabIndex = 18;
            this.bSB.Text = "Small Blind";
            this.bSB.UseVisualStyleBackColor = true;
            this.bSB.Click += new System.EventHandler(this.bSB_Click);
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
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(654, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pot";
            // 
            // tbRaise
            // 
            this.tbRaise.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tbRaise.Location = new System.Drawing.Point(965, 703);
            this.tbRaise.Name = "tbRaise";
            this.tbRaise.Size = new System.Drawing.Size(108, 20);
            this.tbRaise.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Poker.Properties.Resources.poker_table___Copy;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.tbRaise);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.botTwoStatus);
            this.Controls.Add(this.playerStatus);
            this.Controls.Add(this.botOneStatus);
            this.Controls.Add(this.botThreeStatus);
            this.Controls.Add(this.botFourStatus);
            this.Controls.Add(this.botFiveStatus);
            this.Controls.Add(this.tbBB);
            this.Controls.Add(this.bSB);
            this.Controls.Add(this.tbSB);
            this.Controls.Add(this.bBB);
            this.Controls.Add(this.bOptions);
            this.Controls.Add(this.tablePot);
            this.Controls.Add(this.botOneChips);
            this.Controls.Add(this.botTwoChips);
            this.Controls.Add(this.botThreeChips);
            this.Controls.Add(this.botFourChips);
            this.Controls.Add(this.botFiveChips);
            this.Controls.Add(this.tbAdd);
            this.Controls.Add(this.bAdd);
            this.Controls.Add(this.tableChips);
            this.Controls.Add(this.pbTimer);
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

        private System.Windows.Forms.Button foldButton;
        private System.Windows.Forms.Button checkButton;
        private System.Windows.Forms.Button callButton;
        private System.Windows.Forms.Button raiseButton;
        private System.Windows.Forms.ProgressBar pbTimer;
        private System.Windows.Forms.TextBox tableChips;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.TextBox tbAdd;
        private System.Windows.Forms.TextBox botFiveChips;
        private System.Windows.Forms.TextBox botFourChips;
        private System.Windows.Forms.TextBox botThreeChips;
        private System.Windows.Forms.TextBox botTwoChips;
        private System.Windows.Forms.TextBox botOneChips;
        private System.Windows.Forms.TextBox tablePot;
        private System.Windows.Forms.Button bOptions;
        private System.Windows.Forms.Button bBB;
        private System.Windows.Forms.TextBox tbSB;
        private System.Windows.Forms.Button bSB;
        private System.Windows.Forms.TextBox tbBB;
        private System.Windows.Forms.Label botFiveStatus;
        private System.Windows.Forms.Label botFourStatus;
        private System.Windows.Forms.Label botThreeStatus;
        private System.Windows.Forms.Label botOneStatus;
        private System.Windows.Forms.Label playerStatus;
        private System.Windows.Forms.Label botTwoStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tableRaise;
    }
}

