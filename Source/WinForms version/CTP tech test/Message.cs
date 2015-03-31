using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPeerTalk
{
    public class Message
    {
        public string rawText  = "";
        public string messageText = "";
        public string fromIP = "";
        public string command = "";
        public string remotePublicKey = "";

        public Message(string raw)
        {
            rawText = raw;
            // Get senders ip
            fromIP = Toolbox.GetStringBetweenStrings(rawText, "#sendersIpStarts#", "#sendersIpStops#");

            // Check for public key in the message
            if (rawText.Contains("#publicKeyStarts#"))
            {
                remotePublicKey = Toolbox.GetStringBetweenStrings(rawText, "#publicKeyStarts#", "#publicKeyStops#");
            }

        }

    }
}
