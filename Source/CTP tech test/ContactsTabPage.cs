using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoPeerTalk
{
    class ContactsTabPage : TabPage
    {
        public ContactsTabPage()
        {
            // Creates a tab page based on the contact list user control
            ContactsList cs = new ContactsList();
            cs.Dock = DockStyle.Fill;
            this.Controls.Add(cs);
            this.Text = "Contacts";
        }
        public override string ToString()
        {
            // The override is used when checking to see if a tab is a 
            // contacts list tab
            return "ContactsTabPage";
        }
    }
}
