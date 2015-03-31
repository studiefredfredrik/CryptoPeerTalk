
//#define DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;


using System.Net;
using System.Net.Sockets;
using System.IO;

using NATUPNPLib;


namespace CPT_TCP_win
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

#if DEBUGMODE
        DebugTools dbg = new DebugTools();
#endif
        UdpClient udpServer;
        UPnPNATClass upnpnat;
        IStaticPortMappingCollection mappings;
        string recieverIP = "";

        private Cryptography crypto;

        public static string remotePublicKey = "";
        public static bool keySendt = false;
        public static bool encryptedMode = false;

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                crypto = new Cryptography();
                udpServer = new UdpClient(8100);
                Thread thread = new Thread(new ThreadStart(WorkThreadFunction));
                thread.Start();

                Thread threadGetIP = new Thread(new ThreadStart(getIPfunction));
                threadGetIP.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void getIPfunction()
        {
            string ip = Toolbox.getIP_External();
            txtLocalIP.Dispatcher.BeginInvoke((Action)(() => txtLocalIP.Text = ip));
        }

        public static IPAddress GetIPAddress()
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(Environment.MachineName);
            String ss = "";
            foreach (IPAddress address in hostEntry.AddressList)
            {
                //return address;
                if (address.AddressFamily == AddressFamily.InterNetwork)
                    return address;
                ss += "addr: \n" + address.ToString();
                ss += "---------"; 
            }
            MessageBox.Show(ss);
            return null;
        }
        
        public bool forwardPort(int port)
        {
            try
            {
                NATUPNPLib.UPnPNATClass upnpnat = new NATUPNPLib.UPnPNATClass();
                NATUPNPLib.IStaticPortMappingCollection mappings = upnpnat.StaticPortMappingCollection;
                IPAddress ipLocal = GetIPAddress();
#if DEBUGMODE
                DebugTools.append("Mappings count: " + mappings.Count);
                foreach(IStaticPortMapping p in mappings)
                {
                    DebugTools.append("Entry:\n\n\tProtocol: " + p.Protocol.ToString());
                    DebugTools.append("\n\n\tClient: " + p.InternalClient);
                    DebugTools.append("\n\n\tPort: " + p.InternalPort);
                    DebugTools.append("\n\n\tEnabled: " + p.Enabled);
                    DebugTools.append("\n\n\tDescriptor: " + p.Description + "\n");

                }

#endif
                // Check if there is a old mapping or one we can update
                bool isForwarded = false;
                foreach(IStaticPortMapping p in mappings)
                {
                    if (p.InternalPort == port) // Check for port
                    {
                        if (p.InternalClient == ipLocal.ToString())
                        {
                            isForwarded = true; // forwarded ok
                            break;
                        }
                        else p.EditInternalClient(ipLocal.ToString()); // wrong client, forward to right
                    }
                }
                // If not forwarded at all then add mapping
                if (!isForwarded) mappings.Add(port, "TCP", port, ipLocal.ToString(), true, "CPTTCP");

                return true;

                //string ipString = ipLocal.ToString();
                //mappings.Add(port, "TCP", port, ipString, true, "CPTTCP");
                //mappings.Remove(52176, "UDP");
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void removePortRandom()
        {
            try
            {

                NATUPNPLib.UPnPNATClass upnpnat = new NATUPNPLib.UPnPNATClass();
                NATUPNPLib.IStaticPortMappingCollection mappings = upnpnat.StaticPortMappingCollection;

                foreach (IStaticPortMapping p in mappings)
                {
                    if (p.Protocol == "TCP") mappings.Remove(p.ExternalPort, p.Protocol);
                    MessageBox.Show(p.ExternalPort.ToString());
                    break;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void WorkThreadFunction() // listener thread
        {
            try
            {
                while(true)
                {
                     IPEndPoint remoteIPep = new IPEndPoint(IPAddress.Any,1);
                     //udpServer.Receive(ref remoteIPep);

                        string recievedText = "";
                        byte[] b= udpServer.Receive(ref remoteIPep);
                        int k = b.Length;
                        for (int i = 0; i < k; i++)
                            recievedText += Convert.ToChar(b[i]);

                        // update remote ip
                        txtMessages.Dispatcher.BeginInvoke((Action)(() => txtIP.Text = remoteIPep.Address.ToString()));

                        if (recieverIP == "")  // new connection
                        {
                            udpServer.Connect(remoteIPep);
                            recieverIP = remoteIPep.Address.ToString(); // Update the reciever IP if new connection
                            txtIP.Dispatcher.BeginInvoke((Action)(() => txtIP.Text = recieverIP));
                        }

                        if (recievedText.Contains("#holepunch#"))
                        {
                            txtMessages.Dispatcher.BeginInvoke((Action)(() => txtMessages.Text += "\n\t[recieved knock]\n"));
                            txtMessages.Dispatcher.BeginInvoke((Action)(() => txtMessages.ScrollToEnd()));
                            continue;
                        }

                        // Check for public key in the message
                        if (recievedText.Contains("#publicKeyStarts#"))
                        {
                            remotePublicKey = Toolbox.GetStringBetweenStrings(recievedText, "#publicKeyStarts#", "#publicKeyStops#");
                            txtMessages.Dispatcher.BeginInvoke((Action)(() => txtMessages.Text += "\n[recieved key]\n\t"));
                            txtMessages.Dispatcher.BeginInvoke((Action)(() => txtMessages.ScrollToEnd()));
                            if (!keySendt)
                            {
                                // Send key if not sent 
                                string wrappedKey = "#publicKeyStarts#" + crypto.publicKey + "#publicKeyStops#";
                                ASCIIEncoding asen = new ASCIIEncoding();
                                byte[] ba = asen.GetBytes(wrappedKey);
                                udpServer.Send(ba, ba.Length);
                                txtMessages.Dispatcher.BeginInvoke((Action)(() => txtMessages.Text += "\n[sent public key]\n\t"));
                                txtMessages.Dispatcher.BeginInvoke((Action)(() => txtMessages.ScrollToEnd()));
                                keySendt = true;
                            }
                            //MessageBox.Show("Recieved publicKey!\n" + remotePublicKey);
                        }

                        // If there is no public key recieved we dont de-crypt the message 
                        if (remotePublicKey == "" && !recievedText.Contains("#publicKeyStarts#"))
                        {
                            // Invoke. Casting to action makes it act as a delegate, allowing thread safe operations.
                            txtMessages.Dispatcher.BeginInvoke((Action)(() => txtMessages.Text += "\nRemote: [uncrypted]\n\t" + recievedText));
                            txtMessages.Dispatcher.BeginInvoke((Action)(() => txtMessages.ScrollToEnd()));
                        }
                        else if (!recievedText.Contains("#publicKeyStarts#"))
                        {
                            txtMessages.Dispatcher.BeginInvoke((Action)(() => txtMessages.Text += "\nRemote: [crypted]\n\t" + crypto.DecryptMessage(recievedText)));
                            txtMessages.Dispatcher.BeginInvoke((Action)(() => txtMessages.ScrollToEnd()));
                        }
                    


                }

            }
            catch (Exception ex)
            {
                // log errors
                MessageBox.Show(ex.Message);
            }
        }
        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                udpServer.Connect(new IPEndPoint(IPAddress.Parse(txtIP.Text),8100));
                string wrappedKey = "#holepunch#";
                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(wrappedKey);
                udpServer.Send(ba, ba.Length);
                txtMessages.Text += "\n\t[knock sent]\n";
                txtMessages.ScrollToEnd();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            string wrappedKey = "#publicKeyStarts#" + crypto.publicKey + "#publicKeyStops#";
            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] ba = asen.GetBytes(wrappedKey);
            udpServer.Send(ba, ba.Length);
            txtMessages.Text += "\n[sent public key]\n\t";
            txtMessages.ScrollToEnd();
            keySendt = true;
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("\nTCP version for windows\n Current version is: 1.00","CPT");
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (remotePublicKey == "")
                {
                    txtMessages.Text += "\nYou: [uncrypted]\n\t" + txtSend.Text;
                    txtMessages.ScrollToEnd();
                    ASCIIEncoding asen = new ASCIIEncoding();
                    byte[] ba = asen.GetBytes(txtSend.Text);
                    udpServer.Send(ba, ba.Length);
                    //udpServer.SendAsync(ba, ba.Length);
                    txtSend.Text = "";
                }
                else
                {
                    txtMessages.Text += "\nYou: [crypted]\n\t" + txtSend.Text;
                    txtMessages.ScrollToEnd();
                    // Send message
                    //s.Send(Encoding.UTF8.GetBytes(txtSend.Text));
                    string sendmsg = crypto.EncryptMessage(remotePublicKey, txtSend.Text);
                    ASCIIEncoding asen = new ASCIIEncoding();
                    byte[] ba = asen.GetBytes(sendmsg);
                    udpServer.Send(ba, ba.Length);
                    //udpServer.SendAsync(ba, ba.Length);
                    txtSend.Text = "";
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void txtSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Send message
                btnSend_Click(sender,e);
            }
        }

        private void btnUPnP_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(() => forwardPortWithFailsafe(8001));
            thread.Start();
        }

        public void forwardPortWithFailsafe(int port)
        {
            if(!forwardPort(port)) // Try forwarding
            {
                // if forwarding fails then usually router is maxed out
                // remove something to make room for CPT
                removePortRandom(); 
                forwardPort(port); // Then try again
            }
        }
    }
}
