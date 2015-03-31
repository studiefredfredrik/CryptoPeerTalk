using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// UPNP port forwarding
using NATUPNPLib;

// Get IP
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Documents;


namespace CPT_TCP_win
{
    public static class Toolbox
    {
        public static string GetStringBetweenStrings(string total, string startsAfter, string stopsBefore)
        {
            //string str = "super exemple of string key : text I want to keep - end of my string";
            int startIndex = total.IndexOf(startsAfter) + startsAfter.Length;
            int endIndex = total.IndexOf(stopsBefore);
            string newString = total.Substring(startIndex, endIndex - startIndex);

            return newString;
        }

        public static bool UPnP_Add(string protocol, string port, string localIP, string optionalServerName)
        {
            try
            {
                // To solve error: "Use the applicable interface instead." do:
                // Find the reference to NATUPNPLib in the solution explorer, 
                // select it and in the Properties tab change "Embed Interop Types" to FALSE and then rebuild.


                //NATUPNPLib.UPnPNAT upnpnat = new NATUPNPLib.UPnPNAT();
                //NATUPNPLib.IStaticPortMappingCollection mappings = upnpnat.StaticPortMappingCollection;
                UPnPNATClass upnpnat = new UPnPNATClass();
                IStaticPortMappingCollection mappings = upnpnat.StaticPortMappingCollection;

                //foreach(NATUPNPLib.IStaticPortMappingCollection m in mappings)
                //{
                //    MessageBox.Show(m.Count.ToString());
                //}

                if (optionalServerName == "") optionalServerName = "Local Web Server";

                if (protocol == "tcp")
                {
                    // Here's an example of opening up TCP Port 80 to forward to a specific Computer on the Private Network
                    mappings.Add(Convert.ToInt32(port), "TCP", Convert.ToInt32(port), localIP, true, optionalServerName);
                }
                if (protocol == "udp")
                {
                    // Here's an example of forwarding the UDP traffic of Internet Port 80 to Port 8080 on a Computer on the Private Network
                    mappings.Add(Convert.ToInt32(port), "UDP", Convert.ToInt32(port), localIP, true, optionalServerName);
                    //mappings.Add(51000, "UDP", 51000, "46.246.23.179", true, optionalServerName);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("The full error message was not helpful:\n" + ex.Message, "No supported UPnP device found");
                return false;
            }
        }
        public static bool UPnP_Add_Dynamic(string protocol, string port, string remoteHost, string localIP, string optionalServerName,
            int leaseTime)
        {
            try
            {
                // Throws a not implemented
                UPnPNATClass upnpnat = new UPnPNATClass();
                IDynamicPortMappingCollection mappings = upnpnat.DynamicPortMappingCollection;

                if (optionalServerName == "") optionalServerName = "Local Web Server";

                if (protocol == "tcp")
                {
                    // Here's an example of opening up TCP Port 80 to forward to a specific Computer on the Private Network
                    mappings.Add(remoteHost, Convert.ToInt32(port), protocol, Convert.ToInt32(port), localIP, true, optionalServerName, leaseTime);

                }
                if (protocol == "udp")
                {
                    // Here's an example of forwarding the UDP traffic of Internet Port 80 to Port 8080 on a Computer on the Private Network
                    mappings.Add(remoteHost, Convert.ToInt32(port), protocol, Convert.ToInt32(port), localIP, true, optionalServerName, leaseTime);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("The full error message was not helpful:\n" + ex.Message, "No supported UPnP device found");
                return false;
            }
        }
        public static bool UPnP_Add_CTP()
        {
            // Note: UPnP is not needed as UDP packets will by default flow
            // to the same remote port as the local port they left

            // Adds CTP to the firewall with standard options
            return UPnP_Add("udp", "2280", getIP_Local(), "CTP");
        }
        public static string getIP_External()
        {
            string externalIP = new WebClient().DownloadString("http://icanhazip.com");
            return externalIP;
        }

        public static string getIP_Local()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }

        public static bool IsNetworkAvailable()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }

    }
}
