namespace Poker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    partial class AddChips
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components;
        private Label labelOne;
        private Button buttonOne;
        private Button buttonTwo;
        private TextBox textBoxOne;

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
            this.labelOne = new Label();
            this.buttonOne = new Button();
            this.buttonTwo = new Button();
            this.textBoxOne = new TextBox();
            this.SuspendLayout();

            this.labelOne.Font = new Font("Microsoft Sans Serif", 12.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.labelOne.Location = new Point(48, 49);
            this.labelOne.Name = "label1";
            this.labelOne.Size = new Size(176, 23);
            this.labelOne.TabIndex = 0;
            this.labelOne.Text = "You ran out of chips !";
            this.labelOne.TextAlign = ContentAlignment.MiddleCenter;

            this.buttonOne.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.buttonOne.Location = new Point(12, 226);
            this.buttonOne.Name = "button1";
            this.buttonOne.Size = new Size(75, 23);
            this.buttonOne.TabIndex = 1;
            this.buttonOne.Text = "Add Chips";
            this.buttonOne.UseVisualStyleBackColor = true;
            this.buttonOne.Click += new EventHandler(this.ButtonOne_Click);

            this.buttonTwo.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.buttonTwo.Location = new Point(197, 226);
            this.buttonTwo.Name = "button2";
            this.buttonTwo.Size = new Size(75, 23);
            this.buttonTwo.TabIndex = 2;
            this.buttonTwo.Text = "Exit";
            this.buttonTwo.UseVisualStyleBackColor = true;
            this.buttonTwo.Click += new EventHandler(this.ButtonTwo_Click);

            this.textBoxOne.Location = new Point(91, 229);
            this.textBoxOne.Name = "textBox1";
            this.textBoxOne.Size = new Size(100, 20);
            this.textBoxOne.TabIndex = 3;

            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(284, 261);
            this.Controls.Add(this.textBoxOne);
            this.Controls.Add(this.buttonTwo);
            this.Controls.Add(this.buttonOne);
            this.Controls.Add(this.labelOne);
            this.Name = "AddChips";
            this.Text = "You Ran Out Of Chips";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}