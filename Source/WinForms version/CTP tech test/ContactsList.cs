using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoPeerTalk
{
    public partial class ContactsList : UserControl
    {
        
        public ContactsList()
        {
            InitializeComponent();
            foreach(Contact c in StaticContactList.ListOfContacts)
            {
                cboContacts.Items.Add(c);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            StaticContactList.ListOfContacts.Add(new Contact(txtContactName.Text, txtContactIP.Text));
            updateCbo();
        }
        private void updateCbo()
        {
            cboContacts.Text = "";
            cboContacts.Items.Clear();
            foreach (Contact c in StaticContactList.ListOfContacts)
            {
                cboContacts.Items.Add(c);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            StaticContactList.ListOfContacts.Remove((Contact)cboContacts.SelectedItem);
            updateCbo();
        }

        private void ContactsList_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Saves the contact list to file in program folder
            ContactSerializer s = new ContactSerializer();
            s.saveContacts(StaticContactList.ListOfContacts);
        }
    }
}
