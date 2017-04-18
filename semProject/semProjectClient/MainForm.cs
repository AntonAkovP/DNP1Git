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
    public partial class MainForm : Form
    {
        private List<Form> openChats;
        private NetworkStream ns;
        private LoginForm loginF;

        public MainForm(List<String> userList, NetworkStream nets, LoginForm parent)
        {
            InitializeComponent();

            this.Text = parent.Controls.Find("usernameTB",false)[0].Text;
            onlineList.Items.Clear();
            foreach (var e in userList)
                onlineList.Items.Add(e);

            ns = nets;
            openChats = new List<Form>();
            loginF = parent;

            this.FormClosing += new FormClosingEventHandler(mainFormClosing);

            Task.Run(() => inviteListener());
        }
        
        private void inviteListener()
        {

            XDocument doc = LoginForm.acceptMessage(ns);
            while (true)
            {
                if (doc == null) break;
                switch (doc.Root.Element("type").Value)
                {
                    case "uli":
                        this.Invoke((Action)(() => onlineList.Items.Add(doc.Root.Element("user").Value)));
                        break;
                    case "ulo":
                        this.Invoke((Action)(() => onlineList.Items.Remove(doc.Root.Element("user").Value)));
                        break;
                    case "inv": break;
                    case "sd":
                        MessageBox.Show("Server has shut down, client will now exit.","Server shut down",MessageBoxButtons.OK);
                        this.Invoke((Action)(() => this.Close()));
                        return;
                }
                doc = LoginForm.acceptMessage(ns);
            }
        }

        private void mainFormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Form chat in openChats)
            {
                chat.Close();
            }
            try
            {
                XDocument doc = new XDocument(new XElement("logoffMessage"));
                doc.Root.Add(new XElement("type", "loff"));
                byte[] writebuff = Encoding.UTF8.GetBytes(doc.ToString());
                ns.Write(writebuff, 0, writebuff.Length);

                ns.Close();
            }
            catch (System.IO.IOException) { }
            catch (SocketException) { }

            loginF.Close();
        }

    }
}
