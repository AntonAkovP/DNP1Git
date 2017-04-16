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
        private UsersDataSet userData;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            userData = new UsersDataSet();
            Task.Run(()=>startListening());
        }

        private void startListening()
        {
            byte[] adr = { 127, 0, 0, 1 };
            IPAddress ipAdr = new IPAddress(adr);
            TcpListener newsock = new TcpListener(ipAdr, 12345);
            newsock.Start();
            while (true)
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

            try
            {
                while (!LoginReg(doc, ns)) { doc = acceptMessage(ns); }
            }
            catch (Exception) { }

            
        }
        private XDocument acceptMessage(NetworkStream ns)
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

        private Boolean LoginReg(XDocument doc, NetworkStream ns)
        {
            byte[] writeBuff;

            
            if (doc.Root.Element("type").Value.Equals("log"))
            {
                Log(doc.Root.Element("uname").Value + "attempted Login");

                List<String> resultSet = (from usersData in userData.Users.AsEnumerable()
                                          where usersData.username == doc.Root.Element("uname").Value
                                          select string.Format("{0}", usersData.password)
                                          ).ToList();

                XDocument responce = new XDocument(new XElement("serverResponce"));
                responce.Root.Add(new XElement("type", "logrep"));
                XElement elem;

                if (resultSet.Count>0 && resultSet[0].Equals(doc.Root.Element("pw").Value))
                {
                    elem = new XElement("rep", true);
                    responce.Root.Add(elem);

                    writeBuff = Encoding.UTF8.GetBytes(responce.ToString());
                    ns.Write(writeBuff, 0, writeBuff.Length);


                    Log(doc.Root.Element("uname").Value + "sucessfully logged in");

                    return true;
                }
                else
                {
                    elem = new XElement("rep", false);
                    responce.Root.Add(elem);

                    writeBuff = Encoding.UTF8.GetBytes(responce.ToString());
                    ns.Write(writeBuff, 0, writeBuff.Length);

                    Log(doc.Root.Element("uname").Value + "couldn't logged in");

                    return false;
                }



            }

            else if (doc.Root.Element("type").Value.Equals("reg"))
            {
                UsersDataSet.UsersRow newUser = userData.Users.NewUsersRow();
                newUser.username = doc.Root.Element("uname").Value;
                newUser.password = doc.Root.Element("pw").Value;

                Log(doc.Root.Element("uname").Value + "attempted registration");
                XElement elem;
                try
                {
                    userData.Users.Rows.Add(newUser);
                    elem = new XElement("rep", true);

                    Log(doc.Root.Element("uname").Value + "sucessfully registered");
                }
                catch (ConstraintException)
                {
                    elem = new XElement("rep", false);

                    Log(doc.Root.Element("uname").Value + "couldn't register");
                }
                XDocument responce = new XDocument(new XElement("serverResponce"));
                responce.Root.Add(elem);
                responce.Root.Add(new XElement("type", "regrep"));

                writeBuff = Encoding.UTF8.GetBytes(responce.ToString());
                ns.Write(writeBuff, 0, writeBuff.Length);
            }
            return false;

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
