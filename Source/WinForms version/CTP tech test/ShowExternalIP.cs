using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoPeerTalk
{
    public partial class ShowExternalIP : Form
    {
        private BackgroundWorker bw = new BackgroundWorker();
        public ShowExternalIP()
        {
            InitializeComponent();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
        }

        private void ShowExternalIP_Load(object sender, EventArgs e)
        {
            bw.RunWorkerAsync();
            pictureBox1.Image = Image.FromFile("dog_running-186.gif");
        }
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            string ip = Toolbox.getMyExternalIP();
            e.Result = ip;
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            textBox1.Invoke((Action)(() => textBox1.Text = (string)e.Result));
        }
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }
    }
}
