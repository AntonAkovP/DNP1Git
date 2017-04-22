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
    public partial class ChatForm : Form
    {

        private NetworkStream ns;
        private Boolean listening;
        private string user;
        public ChatForm(NetworkStream stream, string u)
        {
            InitializeComponent();

            ns = stream;
            user = u;

            XDocument doc = new XDocument(new XElement("acceptMessage"));
            doc.Root.Add(new XElement("acc", true));
            doc.Root.Add(new XElement("user", user));

            byte[] buff = Encoding.UTF8.GetBytes(doc.ToString());
            ns.Write(buff, 0, buff.Length);

            XDocument online = LoginForm.acceptMessage(ns);

            foreach (XElement val in online.Root.Element("users").Elements("user"))
                onlineBox.Items.Add(val.Value);

            messageTB.KeyDown += new KeyEventHandler(messageTB_enterDown);
            this.FormClosing += new FormClosingEventHandler(chatFormClosing);

            Task.Run(() => messageListener());
        }
        private void messageListener()
        {
            listening = true;
            XDocument doc = LoginForm.acceptMessage(ns);
            while(listening)
            {
                string text = null;
                switch (doc.Root.Element("type").Value)
                {
                    case "uli":
                        this.Invoke((Action)(() => onlineBox.Items.Add(doc.Root.Element("user").Value)));
                        text = 
                            '['+ DateTime.Now.ToString("HH:mm:ss") + "] "+
                            doc.Root.Element("user").Value + 
                            " connected" + Environment.NewLine;
                        this.Invoke((Action)(() => chatRTB.AppendText(text)));
                        break;
                    case "ulo":
                        this.Invoke((Action)(() => onlineBox.Items.Remove(doc.Root.Element("user").Value)));

                        text =
                            '[' + DateTime.Now.ToString("HH:mm:ss") + "] " +
                            doc.Root.Element("user").Value +
                            " disconnected" + Environment.NewLine;
                        this.Invoke((Action)(() => chatRTB.AppendText(text)));

                        break;
                    case "m":
                        text =
                            '[' + DateTime.Now.ToString("HH:mm:ss") + "] " +
                            doc.Root.Element("user").Value + ": " +
                            doc.Root.Element("text").Value + Environment.NewLine;
                        this.Invoke((Action)(() => chatRTB.AppendText(text)));
                        break;
                }
                doc = LoginForm.acceptMessage(ns);
            }
        }
        private void sendMessage()
        {
            if (messageTB.Text.Equals(String.Empty))return ;
            XDocument message = new XDocument(new XElement("chatMessage"));
            message.Root.Add(new XElement("type", "m"));
            message.Root.Add(new XElement("text", messageTB.Text));
            message.Root.Add(new XElement("user", user));

            byte[] writebuff = Encoding.UTF8.GetBytes(message.ToString());
            ns.Write(writebuff, 0, writebuff.Length);

            this.Invoke((Action)(() => messageTB.Clear()));
        }
        private void messageTB_enterDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Return)
            {
                e.SuppressKeyPress=true;
                sendMessage();
            }
        }
        private void sendB_Click(object sender, EventArgs e)
        {
            sendMessage();
        }
        private void chatFormClosing(object sender, FormClosingEventArgs e)
        {
            listening = false;

            XDocument doc = new XDocument(new XElement("chatMessage"));
            doc.Root.Add(new XElement("type", "dc"));
            doc.Root.Add(new XElement("user", user));

            byte[] buff = Encoding.UTF8.GetBytes(doc.ToString());
            ns.Write(buff, 0, buff.Length);
        }
    }
}
