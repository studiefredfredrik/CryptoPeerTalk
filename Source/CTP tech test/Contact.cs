using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPeerTalk
{
    [Serializable]
    public partial class Contact
    {
        public string Name = "";
        public string IP = "";
        public Contact(string name, string ip)
        {
            Name = name;
            IP = ip;
        }


        // To allow combo box to use it properly
        public override string ToString()
        {
            return Name + "/" + IP;
        }
    }
}
