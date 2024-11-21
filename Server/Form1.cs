using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Xml.Linq;
using Server.Entities;

namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        
            TcpChannel chnl = new TcpChannel(1234);
            ChannelServices.RegisterChannel(chnl, false);

            // User
            RemotingConfiguration.RegisterWellKnownServiceType(
            typeof(User), "user", WellKnownObjectMode.
            SingleCall);

            // Product
            RemotingConfiguration.RegisterWellKnownServiceType(
            typeof(Product), "product", WellKnownObjectMode.
            SingleCall);

            // Category
            RemotingConfiguration.RegisterWellKnownServiceType(
            typeof(Category), "category", WellKnownObjectMode.
            SingleCall);

            // Role
            RemotingConfiguration.RegisterWellKnownServiceType(
            typeof(Role), "role", WellKnownObjectMode.
            SingleCall);

            // Cart
            RemotingConfiguration.RegisterWellKnownServiceType(
            typeof(Cart), "cart", WellKnownObjectMode.
            SingleCall);
            progressBar1.Value = 100;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
