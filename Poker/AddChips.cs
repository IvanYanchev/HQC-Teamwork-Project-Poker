namespace Poker
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class AddChips : Form
    {
        internal int a = 0;

        public AddChips()
        {
            FontFamily fontFamily = new FontFamily("Arial");
            this.InitializeComponent();
            this.ControlBox = false;
            this.labelOne.BorderStyle = BorderStyle.FixedSingle;
        }

        public void ButtonOne_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (int.Parse(this.textBoxOne.Text) > 100000000)
            {
                MessageBox.Show("The maximium chips you can add is 100000000");
                return;
            }
            if (!int.TryParse(this.textBoxOne.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                return;
            }
            else if (int.TryParse(this.textBoxOne.Text, out parsedValue) && int.Parse(this.textBoxOne.Text) <= 100000000)
            {
                a = int.Parse(textBoxOne.Text);
                this.Close();
            }
        }

        private void ButtonTwo_Click(object sender, EventArgs e)
        {
            var message = "Are you sure?";
            var title = "Quit";
            var result = MessageBox.Show(
            message,title,
            MessageBoxButtons.YesNo, 
            MessageBoxIcon.Question);
            switch (result)
            {
                case DialogResult.No:
                    break;
                case DialogResult.Yes:
                    Application.Exit();
                    break;
            }
        }
    }
}
