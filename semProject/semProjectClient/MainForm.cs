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
        private bool listen;

        public MainForm(List<String> userList, NetworkStream nets, LoginForm parent)
        {
            InitializeComponent();

            this.Text = parent.Controls.Find("usernameTB", false)[0].Text;
            onlineList.Items.Clear();
            foreach (var e in userList)
                onlineList.Items.Add(e);

            ns = nets;
            openChats = new List<Form>();
            loginF = parent;
            listen = true;

            this.FormClosing += new FormClosingEventHandler(mainFormClosing);

            Task.Run(() => inviteListener());
        }

        private void inviteListener()
        {

            XDocument doc = LoginForm.acceptMessage(ns);
            while (listen)
            {
                if (doc == null) { break; }
                switch (doc.Root.Element("type").Value)
                {
                    case "uli":
                        this.Invoke((Action)(() => onlineList.Items.Add(doc.Root.Element("user").Value)));
                        break;
                    case "ulo":
                        this.Invoke((Action)(() => onlineList.Items.Remove(doc.Root.Element("user").Value)));
                        break;
                    case "inv":
                        if (doc.Root.Element("invtr").Value.Equals(this.Text))
                        {
                            this.Invoke((Action)(() => new ChatForm(new TcpClient(
                                doc.Root.Element("ip").Value, int.Parse(doc.Root.Element("p").Value)
                                ).GetStream(), this.Text).Show()));
                        }
                        else
                        {
                            if (DialogResult.Yes == MessageBox.Show(
                                doc.Root.Element("invtr").Value + "  invited you to join a chat room. Connect and open a new chat window?",
                                doc.Root.Element("invtr").Value + "  invited you to join a chat.",
                                MessageBoxButtons.YesNo
                                ))
                            {
                                this.Invoke((Action)(() => new ChatForm(new TcpClient(
                                doc.Root.Element("ip").Value, int.Parse(doc.Root.Element("p").Value)
                                ).GetStream(), this.Text).Show()));
                            }
                            else
                            {
                                XDocument resp = new XDocument(new XElement("replyResp"));
                                resp.Root.Add(new XElement("acc"), false);
                                resp.Root.Add(new XElement("user", this.Text));

                                byte[] decbuff = Encoding.UTF8.GetBytes(resp.ToString());

                                new TcpClient(
                                doc.Root.Element("ip").Value, int.Parse(doc.Root.Element("p").Value)
                                ).GetStream().Write(decbuff, 0, decbuff.Length);
                            }
                        }
                        break;
                    case "sd":
                        MessageBox.Show("Server has shut down, client will now exit.", "Server shut down", MessageBoxButtons.OK);
                        this.Invoke((Action)(() => this.Close()));
                        return;
                }
                doc = LoginForm.acceptMessage(ns);
            }
        }

        private void mainFormClosing(object sender, FormClosingEventArgs e)
        {
            listen = false;
            foreach (Form chat in openChats)
            {
                try { chat.Close(); } catch (ObjectDisposedException) { }
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

        private void inviteButton_Click(object sender, EventArgs e)
        {
            var selected = onlineList.SelectedItems.Cast<string>();

            if (selected.Count()==0 ||
                (selected.Count() == 1 && selected.Contains(this.Text))
                ) return;
            XDocument doc = new XDocument(new XElement("clinetMessage"));
            doc.Root.Add(new XElement("type", "inv"));
            doc.Root.Add(new XElement("users"));

            
            foreach (string user in selected)
                doc.Root.Element("users").Add(new XElement("user", user));

            if (!selected.Contains(this.Text)) doc.Root.Element("users").Add(new XElement("user", this.Text));

            byte[] writebuff = Encoding.UTF8.GetBytes(doc.ToString());
            try
            {
                ns.Write(writebuff, 0, writebuff.Length);
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Disconnected form server, client will now exit.", "Server shut down", MessageBoxButtons.OK);
                this.Invoke((Action)(() => this.Close()));
                return;
            }
        }
    }
}
