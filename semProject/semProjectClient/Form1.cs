using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace semProjectClient
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void OnFocus(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.ForeColor == Color.Gray)
            {
                tb.Text = String.Empty;
                tb.ForeColor = Color.Black;
            }
        }

        private void usernameTB_LostFocus(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text.Equals(String.Empty))
            {
                tb.Text = "Username";
                tb.ForeColor = Color.Gray;
            }
        }
        private void passwordTB_LostFocus(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text.Equals(String.Empty))
            {
                tb.Text = "Password";
                tb.ForeColor = Color.Gray;
            }
        }

        private void loginB_Click(object sender, EventArgs e)
        {

        }

        private void registerB_Click(object sender, EventArgs e)
        {

        }
    }
}
