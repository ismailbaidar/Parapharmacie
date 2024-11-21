using Middleware.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        public IUserRPC _userRPC;
        public Form1()
        {
            InitializeComponent();
            this.Resize += new EventHandler(Form1_Resize);
            CenterPanel();
            TcpChannel chnl = new TcpChannel();
            ChannelServices.RegisterChannel(chnl, false);
            _userRPC = (IUserRPC)Activator.GetObject(typeof(IUserRPC), "tcp://localhost:1234/user");
            ShowLoginPanel();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            CenterPanel();
        }

        private void CenterPanel()
        {

            loginPanel.Left = (this.ClientSize.Width - loginPanel.Width) / 2;
            loginPanel.Top = (this.ClientSize.Height - loginPanel.Height) / 2;
            registerPanel.Left = (this.ClientSize.Width - registerPanel.Width) / 2;
            registerPanel.Top = (this.ClientSize.Height - registerPanel.Height) / 2;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool result = _userRPC.Register(registerEmail.Text, registerPassword.Text, registerName.Text, 1);
            MessageBox.Show(result.ToString());

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowLoginPanel();

        }

        public void ShowLoginPanel()
        {
            registerPanel.Hide();
            loginPanel.Show();
            
            
        }

        public void ShowRegisterPanel()
        {
            loginPanel.Hide();
            registerPanel.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowRegisterPanel();
        }
    }
}
