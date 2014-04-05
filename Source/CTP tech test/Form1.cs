using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Send and recieve data
using System.Net;
using System.Net.Sockets;



namespace CryptoPeerTalk
{

    public partial class CPT : Form
    {
        //CTP_tech_test
        public static UdpClient udp;
        public static IPEndPoint udp_ep;
        public static Cryptography crypto;

        public static string remotePublicKey = "";
        public static bool keySendt = false;
        public static bool encryptedMode = false;
        public static int listenPort = 2280;

        public bool formHasFocus = true;

        public CPT()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Create endpoint
            udp_ep = new IPEndPoint(IPAddress.Any, listenPort);

            //Initialize UdpClient
            udp = new UdpClient(udp_ep);

            // Add a tab at startup for users to stare at
            newConversationToolStripMenuItem_Click(new object(), new EventArgs());

            // define timer
            timer1.Enabled = true;
            timer1.Interval = 1000;

            // Opens the list of contacts
            ContactSerializer s = new ContactSerializer();
            StaticContactList.ListOfContacts = s.openContacts();
        }


        public void UDP_IncomingData(IAsyncResult ar)
        {
                //Get the data from the response
                byte[] bResp = udp.EndReceive(ar, ref udp_ep);

                //Convert the data to a string
                string sResponse = Encoding.UTF8.GetString(bResp);

                // Route message to the right conversation
                bool existInTab = false;
                foreach(ConversationTabPage c in tabControl1.TabPages)
                {
                    if (c.convTab.remoteIP == udp_ep.Address.ToString())
                    {
                        //c.convTab.IncomingData(sResponse);
                        // a little invoke
                        c.convTab.Invoke((Action)(() => c.convTab.IncomingData(sResponse)));
                        existInTab = true;
                    }
                }
                if (!existInTab)
                {
                    ConversationTabPage convpage = new ConversationTabPage(udp ,udp_ep.Address.ToString());
                    tabControl1.TabPages.Add(convpage);
                    tabControl1.SelectedTab = convpage;
                    //convpage.convTab.IncomingData(sResponse);
                    //txt.Invoke((Action)(() => txt.ScrollToCaret()));
                    // need a little invoke
                    convpage.convTab.Invoke((Action)(() => convpage.convTab.IncomingData(sResponse)));
                    convpage.convTab.Invoke((Action)(() => convpage.Text = udp_ep.Address.ToString())); 
                }

                // dont give a fuck just make the alert for anything
                if(sResponse != "")
                {
                    if(!formHasFocus)
                    {
                        //FlashWindowBar.FlashWindowEx(this);
                        this.Invoke((Action)(() => FlashWindowBar.FlashWindowEx(this)));
                    }
                }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //Begin waiting for a response asynchronously
                // We want to pass a richTextBox, not just the standard object so we add a Tuple
                // tuple not really needed
                udp.BeginReceive(new AsyncCallback(UDP_IncomingData), Tuple.Create(udp_ep, remotePublicKey, crypto));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void FileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HowTo help = new HowTo();
            help.ShowDialog();
        }

        private void contactsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Only add contab if it's not open
            bool hasContab = false;
            foreach(TabPage t in tabControl1.TabPages)
            {
                if(t.ToString() == "ContactsTabPage")
                {
                    hasContab = true;
                }
            }
            if (!hasContab)
            {
                ContactsTabPage contab = new ContactsTabPage();
                tabControl1.TabPages.Add(contab);
                tabControl1.SelectedTab = contab;
            }
        }

        private void newConversationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConversationTabPage convtab = new ConversationTabPage(udp);
            tabControl1.TabPages.Add(convtab);
            tabControl1.SelectedTab = convtab;
        }

        private void closeConversationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Dont close tabs that dont exist :p
            if(tabControl1.TabCount != 0)
            {
            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            }
        }


        private void showExternalIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowExternalIP s = new ShowExternalIP();
            s.ShowDialog();
        }
        private void tryUPnPForwardingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Trying UPnP, this might take a few seconds to complete...\n"
                + "The implementation is Alpha (shitty) and will cause the program to hang for a few seconds", "Dont panic, it's cool");
            if (Toolbox.UPnP_Add_CTP())
            {
                MessageBox.Show("Added UPnP mapping\n");
            }
            else
            {
                MessageBox.Show("UPnP mapping failed, sorry\n");
            }
        }

        // Form focus checks
        private void CPT_Deactivate(object sender, EventArgs e)
        {
            formHasFocus = false;
        }

        private void CPT_Activated(object sender, EventArgs e)
        {
            formHasFocus = true;
        }
    }
}
