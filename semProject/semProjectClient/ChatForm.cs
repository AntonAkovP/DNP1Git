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

namespace semProjectClient
{
    public partial class ChatForm : Form
    {

        private NetworkStream ns;

        public ChatForm()
        {
            InitializeComponent();

            messageTB.KeyDown += new KeyEventHandler(messageTB_enterDown);
        }
        private void sendMessage()
        {


        }
        private void messageTB_enterDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Return)
            {
                sendMessage();
            }
        }
        private void sendB_Click(object sender, EventArgs e)
        {
            sendMessage();
        }
    }
}
