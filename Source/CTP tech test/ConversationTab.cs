using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Send and recieve data
using System.Net;
using System.Net.Sockets;

namespace CryptoPeerTalk
{
    public partial class ConversationTab : UserControl
    {
        public static UdpClient udp;
        public static IPEndPoint udp_ep;
        public int localPort = 2280;
        public List<Message> messages = new List<Message>();
        public string remoteIP = "";
        public string localIP = "";
        public string remotePublicKey = "";
        Cryptography crypto;
        public string localPublicKey = "";
        public string localPrivateKey = "";

        public static bool keySendt = false;
        public static bool encryptedMode = false;
        public static int listenPort = 2280;

        public ConversationTab(UdpClient up)
        {
            InitializeComponent();
            try
            {
                // init the crypto object
                crypto = new Cryptography();
                localPrivateKey = crypto.privateKey;
                localPublicKey = crypto.publicKey;

                // define timer
                //timer1.Enabled = false;
                //timer1.Interval = 1000;

                richTextBox2.AppendText("CPT Public Beta 2\n" +
                "-----------------------------------------------------------\n");
                richTextBox2.AppendText("Debug console running\n");
                //Create endpoint
                //udp_ep = new IPEndPoint(IPAddress.Any, listenPort);
                richTextBox2.AppendText("Endpoint created\n");

                //Initialize UdpClient
                udp = up;
                richTextBox2.AppendText("UdpClient initialized\n");
                richTextBox2.AppendText("Encryption status: off\n");

                //timer1.Enabled = true;
                richTextBox2.AppendText("Now listening...\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public ConversationTab(UdpClient up, string remoteIp)
        {
            InitializeComponent();
            try
            {
                // The window was created from external ip
                remoteIP = remoteIp;


                // init the crypto object
                crypto = new Cryptography();
                localPrivateKey = crypto.privateKey;
                localPublicKey = crypto.publicKey;

                // define timer
                //timer1.Enabled = false;
                //timer1.Interval = 1000;

                richTextBox2.AppendText("CPT Public Beta 2\n" +
                "-----------------------------------------------------------\n");
                richTextBox2.AppendText("Debug console running\n");
                //Create endpoint
                //udp_ep = new IPEndPoint(IPAddress.Any, listenPort);
                richTextBox2.AppendText("Endpoint created\n");

                //Initialize UdpClient
                udp = up;
                richTextBox2.AppendText("UdpClient initialized\n");
                richTextBox2.AppendText("Encryption status: off\n");

                //timer1.Enabled = true;
                richTextBox2.AppendText("Now listening...\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void sendMessage(string message)
        {
            try
            {
                // encrypt
                string encrypted = crypto.EncryptMessage(remotePublicKey, message);
                // send
                udpSendString(encrypted, remoteIP, Convert.ToString(localPort));
                // scroll to caret
                richTextBox1.ScrollToCaret();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "User error occured");
            }
        }
        private void udpSendString(string text, string ip, string port)
        {
            try
            {
                string sendmsg = textBox1.Text;
                byte[] bSend = Encoding.ASCII.GetBytes(sendmsg);

                //Send the data
                udp.Send(bSend, bSend.Length, ip, Convert.ToInt32(port));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void sendPublicKey()
        {
            try
            {
                string wrapped = "#publicKeyStarts#" + localPublicKey + "#publicKeyStops#";
                byte[] bSend = Encoding.ASCII.GetBytes(wrapped);

                //Send the data
                udp.Send(bSend, bSend.Length, remoteIP, localPort);
                keySendt = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "User error occured");
            }
        }

        private void cboIP_DropDown(object sender, EventArgs e)
        {
            cboIP.Items.Clear();
            foreach(Contact c in StaticContactList.ListOfContacts)
            {
                cboIP.Items.Add(c);
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (remoteIP == "")
            {
                if (cboIP.SelectedItem == null)
                {
                    remoteIP = cboIP.Text;
                    this.Parent.Text = remoteIP;
                }
            }
            // Add a new conversation if you press connect
            if (remoteIP != "")
            {
                cboIP.Enabled = false;
                try
                {
                    sendPublicKey();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "User error occured");
                }
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if(remoteIP == "")
            {
                if(cboIP.SelectedItem == null)
                {
                    remoteIP = cboIP.Text;
                }
            }
            if (remoteIP != "")
            {
                cboIP.Enabled = false;
                if (remotePublicKey == "") // No remote key, send unencrypted is only option
                {
                    //Create byte[] of data to send
                    string sendmsg = textBox1.Text;
                    byte[] bSend = Encoding.ASCII.GetBytes(sendmsg);

                    //Send the data
                    udp.Send(bSend, bSend.Length, remoteIP, localPort);
                    richTextBox1.AppendText("(unsafe)You: " + sendmsg + "\n");
                }
                else // Has remote key, send encrypted
                {
                    string sendmsg = crypto.EncryptMessage(remotePublicKey, textBox1.Text);
                    byte[] bSend = Encoding.ASCII.GetBytes(sendmsg);
                    udp.Send(bSend, bSend.Length, remoteIP, localPort);
                    richTextBox1.AppendText("You: " + textBox1.Text + "\n");

                }
            }
        }



        public void IncomingData(string sResponse)
        {
            if (remotePublicKey == "")
            {
                // Check for public key in the message
                if (sResponse.Contains("#publicKeyStarts#"))
                {
                    // Recieved publicKey!
                    remotePublicKey = Toolbox.GetStringBetweenStrings(sResponse, "#publicKeyStarts#", "#publicKeyStops#");
                    // Write log
                    richTextBox2.AppendText("-----------------------------------------------------------\n" +
                    "Public key recieved!\nNow communicating in RSA 2048bit\n" +
                    "Encryption status: on\n" +
                    "-----------------------------------------------------------\n");
                    richTextBox1.ScrollToCaret();

                    // Send one back if not done allready
                    if (!keySendt)
                    {
                        sendPublicKey();
                    }
                    cboIP.Text = remoteIP;
                    cboIP.Enabled = false;
                }
                else
                {
                    richTextBox1.AppendText("(unsafe)Remote:" + sResponse + "\n");
                }
            }
            if(remotePublicKey != "" && !sResponse.Contains("#publicKeyStarts#"))
            {
                richTextBox1.AppendText("Remote:" + crypto.DecryptMessage(sResponse) + "\n");
            }
            // scroll to caret
            richTextBox1.ScrollToCaret();
            
            
        }
        public override string ToString()
        {
            return remoteIP;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // check for enter key press
            try
            {
                if (e.KeyChar == (char)13)
                {
                    // Then Enter key was pressed
                    btnSend_Click(new object(), new EventArgs());
                    textBox1.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboIP_DropDownClosed(object sender, EventArgs e)
        {
            if (cboIP.SelectedItem != null)
            {
                remoteIP = ((Contact)cboIP.SelectedItem).IP;
            }
        }

        private void ConversationTab_Load(object sender, EventArgs e)
        {

        }
    }
}
