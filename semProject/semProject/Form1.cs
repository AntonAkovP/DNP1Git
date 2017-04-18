using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace semProject
{
    public partial class Form1 : Form
    {
        private static readonly object updateLock = new object();
        private UsersDataSet userData;
        private Dictionary<string, NetworkStream> loggedIn;
        private bool listenerB = true;
        public Form1()
        {
            InitializeComponent();

            userData = new UsersDataSet();
            loggedIn = new Dictionary<string, NetworkStream>();

            this.FormClosing += new FormClosingEventHandler(serverFormClosing);
            Task.Run(() => startListening());
        }

        private void startListening()
        {
            byte[] adr = { 127, 0, 0, 1 };
            IPAddress ipAdr = new IPAddress(adr);
            TcpListener newsock = new TcpListener(ipAdr, 12345);
            newsock.Start();
            while (listenerB)
            {
                //Console.WriteLine("Waiting for a client...");
                TcpClient client = newsock.AcceptTcpClient();
                Thread handler = new Thread(handlerM);
                handler.Start(client);

                //Console.WriteLine(client.ToString() + " has connected.");
            }
        }

        private void handlerM(object clientO)
        {
            TcpClient client = clientO as TcpClient;
            if (client == null) return;


            NetworkStream ns = client.GetStream();
            XDocument doc = acceptMessage(ns);
            string user = null;
            try
            {
                while (user==null) { user = LoginReg(doc, ns); doc = acceptMessage(ns); }
                while (listenerB && processMessage(doc, ns, user)) { doc = acceptMessage(ns); }
            }
            catch (System.IO.IOException) {logUserOut(user); return; }

            ns.Close();
            client.Close();
            logUserOut(user);
            return;

        }
        private bool processMessage(XDocument doc, NetworkStream ns, String user)
        {
            switch(doc.Root.Element("type").Value)
            {
                case "loff": logUserOut(user); break;

                case "refresh":break;

                case "startchat":
                    //Task startR = Task.Run(() = startRoom());

                    //await startR;
                    break;
            }

            return true;
        }
        private XElement onlineToXML()
        {
            XElement resultSet = new XElement("users");
            foreach (string user in loggedIn.Keys)
                resultSet.Add(new XElement("user", user));
            return resultSet;
        }
        private XDocument acceptMessage(NetworkStream ns)
        {
            byte[] buff;
            StringBuilder message = new StringBuilder("Message not read");
            int bread;

            while (true)
            {
                //try block moved to handlerM
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


                try { return XDocument.Parse(message.ToString()); }
                catch (System.IO.IOException) { return null; }

            }
        }
        private void updateClients(XDocument doc)
        {
            lock (updateLock)
            {
                byte[] writeBuff = Encoding.UTF8.GetBytes(doc.ToString());
                foreach (string user in loggedIn.Keys)
                {
                    try
                    {
                        loggedIn[user].Write(writeBuff, 0, writeBuff.Length);
                    }
                    catch (ObjectDisposedException) { return; }
                    catch (System.IO.IOException) { if (doc.Root.Element("type").Value.Equals("sd"))continue; logUserOut(user); }
                }
            }
            
        }
        private void logUserIn(string user, NetworkStream client)
        {
            XDocument doc = new XDocument(new XElement("serverUpdate"));
            doc.Root.Add(new XElement("type", "uli"));
            doc.Root.Add(new XElement("user", user));
            updateClients(doc);

            loggedIn.Add(user, client);
            this.Invoke((Action)(()=>onlineList.Items.Add(user)));
        }
        private void logUserOut(string user)
        {
            XDocument doc = new XDocument(new XElement("serverUpdate"));
            doc.Root.Add(new XElement("type", "ulo"));
            doc.Root.Add(new XElement("user", user));
            updateClients(doc);

            try
            {
                loggedIn.Remove(user);
                this.Invoke((Action)(()=>onlineList.Items.Remove(user)));
                Log(user + " logged out/DC'd");
            }
            catch (ArgumentNullException) { }
            catch (InvalidOperationException) { }
            
        }
        /// <summary>
        /// processes login window
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="ns"></param>
        /// <returns>
        /// returns username if login is successful(used to remove from lsit in case of DC) or null if not
        /// </returns>
        private string LoginReg(XDocument doc, NetworkStream ns)
        {
            byte[] writeBuff;

            
            if (doc.Root.Element("type").Value.Equals("log"))
            {
                XDocument responce = new XDocument(new XElement("serverResponce"));
                responce.Root.Add(new XElement("type", "logrep"));

                if (!loggedIn.Keys.Contains(doc.Root.Element("uname").Value))
                {
                    Log(doc.Root.Element("uname").Value + " attempted Login");

                    List<String> resultSet = (from usersData in userData.Users.AsEnumerable()
                                              where usersData.username == doc.Root.Element("uname").Value
                                              select string.Format("{0}", usersData.password)
                                              ).ToList();

                    

                    if (resultSet.Count > 0 && resultSet[0].Equals(doc.Root.Element("pw").Value))
                    {
                        logUserIn(doc.Root.Element("uname").Value, ns);
                        responce.Root.Add(new XElement("rep", true));
                        responce.Root.Add(onlineToXML());
                        writeBuff = Encoding.UTF8.GetBytes(responce.ToString());
                        ns.Write(writeBuff, 0, writeBuff.Length);


                        Log(doc.Root.Element("uname").Value + " sucessfully logged in");


                        return doc.Root.Element("uname").Value; ;
                    }
                    else
                    {
                        responce.Root.Add(new XElement("rep", false));

                        writeBuff = Encoding.UTF8.GetBytes(responce.ToString());
                        ns.Write(writeBuff, 0, writeBuff.Length);

                        Log(doc.Root.Element("uname").Value + " couldn't log in");

                        return null;
                    }

                }
                else
                {
                    responce.Root.Add(new XElement("rep", false));

                    writeBuff = Encoding.UTF8.GetBytes(responce.ToString());
                    ns.Write(writeBuff, 0, writeBuff.Length);

                    Log(doc.Root.Element("uname").Value + " couldn't log in");

                    return null;
                }

            }

            else if (doc.Root.Element("type").Value.Equals("reg"))
            {
                UsersDataSet.UsersRow newUser = userData.Users.NewUsersRow();
                newUser.username = doc.Root.Element("uname").Value;
                newUser.password = doc.Root.Element("pw").Value;

                Log(doc.Root.Element("uname").Value + " attempted registration");
                XElement elem;
                try
                {
                    userData.Users.Rows.Add(newUser);
                    elem = new XElement("rep", true);

                    Log(doc.Root.Element("uname").Value + " sucessfully registered");
                }
                catch (ConstraintException)
                {
                    elem = new XElement("rep", false);

                    Log(doc.Root.Element("uname").Value + " couldn't register");
                }
                XDocument responce = new XDocument(new XElement("serverResponce"));
                responce.Root.Add(elem);
                responce.Root.Add(new XElement("type", "regrep"));

                writeBuff = Encoding.UTF8.GetBytes(responce.ToString());
                ns.Write(writeBuff, 0, writeBuff.Length);
            }
            return null;

        }

        private void serverFormClosing(object sender, FormClosingEventArgs e)
        {
            listenerB = false;
            XDocument doc = new XDocument(new XElement("serverMessage"));
            doc.Root.Add(new XElement("type", "sd"));
            updateClients(doc);
        }
        internal void Log(String message)
        {
            if (logBox.InvokeRequired)
            {
                this.Invoke((Action)(() =>
                    Log(message)
                ));
                return;
            }
            logBox.AppendText(message + Environment.NewLine);
        }

    }



}
