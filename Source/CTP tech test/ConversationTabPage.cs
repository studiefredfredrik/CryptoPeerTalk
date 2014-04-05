using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Send and recieve data
using System.Net;
using System.Net.Sockets;

namespace CryptoPeerTalk
{
    public class ConversationTabPage : TabPage
    {
        public ConversationTab convTab;
        public ConversationTabPage(UdpClient up)
        {
            // Creates a tab page based on the conversationTab user control
            convTab = new ConversationTab(up);
            convTab.Dock = DockStyle.Fill;
            this.Controls.Add(convTab);
            this.Text = "Conversation";
        }
        public ConversationTabPage(UdpClient up, string ip)
        {
            // Creates a tab page based on the conversationTab user control
            convTab = new ConversationTab(up, ip);
            convTab.Dock = DockStyle.Fill;
            this.Controls.Add(convTab);
            this.Text = ip;
        }
        public override string ToString()
        {
            return convTab.ToString();
        }
    }
}
