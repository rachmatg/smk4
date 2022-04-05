using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        SimpleTcpServer server;

        private void Form1_Load(object sender, EventArgs e)
        {
            server = new SimpleTcpServer();
            server.Delimiter = 0x13;
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += Server_DataReceived;
        }

        private void Server_DataReceived(object sender, SimpleTCP.Message e)
        {
            textStatus.Invoke((MethodInvoker)delegate ()
            {
                textStatus.Text += e.MessageString;
                e.ReplyLine(string.Format("You Said: {0}", e.MessageString));
            });
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            textStatus.Text += "Server Starting...";
            System.Net.IPAddress ip = System.Net.IPAddress.Parse(textHost.Text);
            server.Start(ip, Convert.ToInt32(textPort.Text));
        }
            
        private void btnStop_Click(object sender, EventArgs e)
        {
            if(server.IsStarted)
            { 
                server.Stop();
                textStatus.Clear();
            }
        }

        private void textStatus_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
