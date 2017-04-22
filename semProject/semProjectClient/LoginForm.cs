using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace semProjectClient
{
    public partial class LoginForm : Form
    {
        private TcpClient client;
        private NetworkStream ns = null;
        private static Boolean accept = true;
        
        public LoginForm()
        {
            InitializeComponent();

            usernameTB.GotFocus += new EventHandler(OnFocus);
            passwordTB.GotFocus += new EventHandler(OnFocus);

            usernameTB.LostFocus += new EventHandler(usernameTB_LostFocus);
            passwordTB.LostFocus += new EventHandler(passwordTB_LostFocus);

            Task.Run(()=>connectToServer());
        }

        private void connectToServer()
        {

            try
            {
                client = new TcpClient("127.0.0.1", 12345);
                ns = client.GetStream();
            }
            catch (SocketException)
            {
                if (MessageBox.Show("Failed to connect to the server. Press \"Yes\" to try again or \"No\" to exit.", "Error connecting to the server", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    connectToServer();
                    return;
                }
                else this.Invoke((Action)(() => Close()));
            }

            interceptMessages();
        }
        private void interceptMessages()
        {
            XDocument doc;
            while (accept)
            {
                doc = acceptMessage(ns);
                if (doc == null) break;
                switch(doc.Root.Element("type").Value)
                {
                    case "regrep":
                        if (Boolean.Parse(doc.Root.Element("rep").Value))
                            MessageBox.Show("Successfully registered.","Registration", MessageBoxButtons.OK);
                        else
                            MessageBox.Show("Registration failed", "Registration", MessageBoxButtons.OK);
                        break;
                    case "logrep":
                        if (Boolean.Parse(doc.Root.Element("rep").Value))
                        {
                            accept = false;
                            List<String> NewOnline = new List<String>();
                            foreach (XElement user in doc.Root.Element("users").Elements("user"))
                                NewOnline.Add(user.Value);
                            this.Invoke((Action)(() => new MainForm(NewOnline, ns, this).Show()));
                            this.Invoke((Action)(() => this.Hide()));
                        }
                        else
                            MessageBox.Show("Couldn't log in", "Log in", MessageBoxButtons.OK);
                        break;
                }
            }

        }

        internal static XDocument acceptMessage(NetworkStream ns)
        {
            byte[] buff;
            StringBuilder message = new StringBuilder("Message not read");
            int bread;

            while (true)
            {
                try
                {
                    if (ns.CanRead)
                    {
                        buff = new byte[1024];
                        message = new StringBuilder();

                        do
                        {
                            bread = ns.Read(buff, 0, buff.Length);

                            message.AppendFormat("{0}", Encoding.UTF8.GetString(buff, 0, bread));

                        }
                        while (ns.DataAvailable);
                    }


                    return XDocument.Parse(message.ToString());


                }
                catch (System.IO.IOException) { return null; }

            }
        }
        private void OnFocus(object sender, System.EventArgs e)
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
            XDocument req = new XDocument(new XElement("clientMessage"));
            req.Root.Add(new XElement("type", "log"));
            req.Root.Add(new XElement("uname", usernameTB.Text));
            req.Root.Add(new XElement("pw", passwordTB.Text));

            byte[] writeBuff = Encoding.UTF8.GetBytes(req.ToString());
            ns.Write(writeBuff, 0, writeBuff.Length);
        }

        private void registerB_Click(object sender, EventArgs e)
        {
            XDocument req = new XDocument(new XElement("clientMessage"));
            req.Root.Add(new XElement("type", "reg"));
            req.Root.Add(new XElement("uname", usernameTB.Text));
            req.Root.Add(new XElement("pw", passwordTB.Text));

            byte[] writeBuff = Encoding.UTF8.GetBytes(req.ToString());
            ns.Write(writeBuff, 0, writeBuff.Length);
        }

        private void TB_TextChanged(object sender, EventArgs e)
        {
            if (!usernameTB.Text.Equals("Username") && !passwordTB.Text.Equals("Password")&& 
                !usernameTB.Text.Equals(String.Empty) && !passwordTB.Text.Equals(String.Empty))
            {
                loginB.Enabled = true; registerB.Enabled = true;
            }
            else
            {
                loginB.Enabled = false; registerB.Enabled = false;
            }
        }


    }
}
