using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// IO and serializing
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace CryptoPeerTalk
{
    class ContactSerializer
    {
        public string PROGRAMPATH = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public string ContactsPATH = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\contacts.ctl";

        FileStream output;
        FileStream input;
        BinaryFormatter reader = new BinaryFormatter();
        BinaryFormatter formatter = new BinaryFormatter();

        public void saveContacts(List<Contact> contacts)
        {
            try
            {
                // override if exists
                if (File.Exists(ContactsPATH))
                    File.Delete(ContactsPATH);
                output = new FileStream(ContactsPATH, FileMode.OpenOrCreate, FileAccess.Write);
                formatter.Serialize(output, contacts);
            }
            catch (Exception)
            {
            }
        }

        public List<Contact> openContacts()
        {
            if (File.Exists(ContactsPATH))
            {
                input = new FileStream(ContactsPATH, FileMode.Open, FileAccess.Read);
                List<Contact> Contactlist = (List<Contact>)reader.Deserialize(input);
                return Contactlist;
            }
            else
            {
                return new List<Contact>();
            }
        }
    }
}
