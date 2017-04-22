using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Diagnostics;

namespace semProject
{
    /// <summary>
    /// Basically there were 2 ways to implement the whole chat thing. First was to have messages each be handled centrally
    /// using an ID for the chat room and have those stored somewhere. The other way was just create seperate little server
    /// for each chat room, whcih is what this class is.
    /// </summary>
    class chatRoom
    {
        private readonly object updateLock = new object();
        private Dictionary<string, NetworkStream> loggedIn;
        private TcpListener chatsock;
        private Boolean chat;
        private Stopwatch timeLive;

        public chatRoom(List<NetworkStream> users, string inviter)
        {
            chat = true;
            loggedIn = new Dictionary<string, NetworkStream>();
            byte[] adr = { 127, 0, 0, 1 };
            IPAddress ipAdr = new IPAddress(adr);
            chatsock = new TcpListener(ipAdr, 0);
            chatsock.Start();

            XDocument doc = new XDocument(new XElement("chatInvite"));

            doc.Root.Add(new XElement("type", "inv"));
            doc.Root.Add(new XElement("invtr", inviter));
            doc.Root.Add(new XElement("ip", ((IPEndPoint)chatsock.LocalEndpoint).Address.ToString()));
            doc.Root.Add(new XElement("p", ((IPEndPoint)chatsock.LocalEndpoint).Port.ToString()));

            foreach (NetworkStream ns in users)
                Task.Run(() => clinetHandler(ns, doc));
            timeLive = new Stopwatch();
            timeLive.Start();
        }

        private void clinetHandler(NetworkStream toInv, XDocument doc1)
        {
            byte[] writebuff = Encoding.UTF8.GetBytes(doc1.ToString());
            toInv.Write(writebuff, 0, writebuff.Length);

            TcpClient client = chatsock.AcceptTcpClient();

            NetworkStream ns = client.GetStream();

            XDocument doc = acceptMessage(ns);

            string user;
            

            if(bool.Parse(doc.Root.Element("acc").Value))
            {
                user = doc.Root.Element("user").Value;

                logUserIn(user, ns);

                XDocument toSend = new XDocument(new XElement("chatMessage"));
                toSend.Root.Add(onlineToXML());

                writebuff = Encoding.UTF8.GetBytes(toSend.ToString());
                ns.Write(writebuff, 0, writebuff.Length);

                
            }
            else
            {
                XDocument toSend = new XDocument(new XElement("chatMessage"));
                toSend.Root.Add(new XElement("user", doc.Root.Element("user").Value));
                toSend.Root.Add(new XElement("type", "dec"));

                updateClients(toSend);
                return;
            }

            Boolean connected = true;
            while (chat&&connected)
            {
                if (!loggedIn.Keys.Contains(user)) break;
                try { doc = acceptMessage(ns); } catch (System.IO.IOException) { break; }
                if (doc == null) break;
                switch (doc.Root.Element("type").Value)
                {
                    case "m":
                        updateClients(doc, ref connected);
                        break;
                    case "dc":
                        connected = false;
                        logUserOut(doc.Root.Element("user").Value);
                        break;
                }

                if (loggedIn.Count == 0 && timeLive.ElapsedMilliseconds > 60000)
                {
                    chat = false;
                    return;
                }
            }

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
                catch (System.Xml.XmlException) { return null; }


            }
        }
        private XElement onlineToXML()
        {
            XElement resultSet = new XElement("users");
            foreach (string user in loggedIn.Keys)
                resultSet.Add(new XElement("user", user));
            return resultSet;
        }
        private void updateClients(XDocument doc)
        {
            List<String> toLogout = new List<String>();
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
                    catch (System.IO.IOException) { if (doc.Root.Element("type").Value.Equals("sd")) continue; toLogout.Add(user); }
                }
                foreach (string user in toLogout)
                    logUserOut(user);
            }

        }
        private void updateClients(XDocument doc, ref Boolean con)
        {
            List<String> toLogout = new List<String>();
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
                    catch (System.IO.IOException) { con = false; toLogout.Add(user); }
                }
                foreach (string user in toLogout)
                    logUserOut(user);
            }

        }
        private void logUserIn(string user, NetworkStream client)
        {
            XDocument doc = new XDocument(new XElement("serverUpdate"));
            doc.Root.Add(new XElement("type", "uli"));
            doc.Root.Add(new XElement("user", user));
            updateClients(doc);

            loggedIn.Add(user, client);
        }
        private void logUserOut(string user)
        { 
            try
            {
                loggedIn.Remove(user);
            }
            catch (ArgumentNullException) { }
            catch (InvalidOperationException) { }

            if(loggedIn.Count == 0 && timeLive.ElapsedMilliseconds > 60000)
            {
                chat = false;
                return;
            }
            
            XDocument doc = new XDocument(new XElement("serverUpdate"));
            doc.Root.Add(new XElement("type", "ulo"));
            doc.Root.Add(new XElement("user", user));
            updateClients(doc);
        }
    }
}
