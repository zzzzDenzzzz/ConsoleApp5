using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        TcpListener? tcpListener;
        void BtnStart_Click(object sender, EventArgs e)
        {
            try
            {
                tcpListener = new(IPAddress.Parse(txtIp.Text), Convert.ToInt32(txtPort.Text));
                tcpListener.Start();

                Thread thread = new(new ThreadStart(
                    async () =>
                    {
                        while (true)
                        {
                            using TcpClient tcpClient = tcpListener.AcceptTcpClient();
                            StreamReader reader = new(tcpClient.GetStream(), Encoding.Unicode);
                            string? s = await reader.ReadLineAsync();
                            if (s != null)
                            {
                                listBoxMessage.Items.Add(s);
                                if (s.ToUpper() == "EXIT")
                                {
                                    tcpListener.Stop();
                                    Close();
                                }
                            }
                        }
                    }))
                {
                    IsBackground = true
                };
                thread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (tcpListener != null)
            {
                tcpListener.Stop();
            }
        }
    }
}