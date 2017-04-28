using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace semProjectClient
{
    /// <summary>
    /// The form for each chat. Contains a list of connected users and an RTB with all the messages. Messages can also be sent
    /// by pressing the return key. I tried almost every suggested solution online about getting images to appear in it
    /// (messing with the Rtf property, pasting stuff) but none of them worked. There wasn't a working example either.
    /// </summary>
    public partial class ChatForm : Form
    {

        private NetworkStream ns;
        private Boolean listening;
        private string user;

        /// <summary>
        /// notifies the server the user accepted the invintation and gets a list of currently online
        /// </summary>
        /// <param name="stream">stream to the chatRoom on the server</param>
        /// <param name="u">the user that creater the chatform</param>
        public ChatForm(NetworkStream stream, string u)
        {
            InitializeComponent();
            ns = stream;
            user = u;
            this.Text = user;

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
        /// <summary>
        /// listens for connects, disconnect and incoming messages
        /// </summary>
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
        /// <summary>
        /// sends the text from the textbox to the server
        /// </summary>
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
        /// <summary>
        /// tells the server the user closed the chat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
