using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        TcpClient? tcpClient;
        async void BtnSend_Click(object sender, EventArgs e)
        {
            try
            {
                IPEndPoint iPEndPoint = new(IPAddress.Parse(txtIp.Text), Convert.ToInt32(txtPort.Text));
                tcpClient = new();
                await tcpClient.ConnectAsync(iPEndPoint);
                NetworkStream nstream = tcpClient.GetStream();

                byte[] message = Encoding.Unicode.GetBytes(txtMessage.Text);
                nstream.Write(message, 0, message.Length);
                tcpClient.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (tcpClient != null)
            {
                tcpClient.Close();
            }
        }
    }
}