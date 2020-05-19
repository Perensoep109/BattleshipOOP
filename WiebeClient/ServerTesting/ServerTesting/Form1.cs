using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerTesting
{
    public partial class Form1 : Form
    {
        TcpListener m_server;
        TcpClient m_client;

        public Form1()
        {
            InitializeComponent();
            m_server = new TcpListener(IPAddress.Parse("127.0.0.1"), 69);
            m_server.Start();
            m_server.BeginAcceptTcpClient(ClientConnected, m_server);
        }

        public void ClientConnected(IAsyncResult a_result)
        {
            TcpListener server = (TcpListener)a_result.AsyncState;
            m_client = server.EndAcceptTcpClient(a_result);

            Console.WriteLine("Server connected to {0}",
                m_client.Client.RemoteEndPoint.ToString());
        }

        private void btnYeetPackage_Click(object sender, EventArgs e)
        {
            byte[] buffer = new byte[4] { 0x8, 0x1, 0x5, 0x4 };
            Send(buffer);
        }

        public void Send(byte[] a_packet)
        {
            m_client.Client.BeginSend(a_packet, 0, a_packet.Length, 0, SendCallback, m_client.Client);
        }

        private void SendCallback(IAsyncResult a_result)
        {
            Socket client = (Socket)a_result.AsyncState;

            int bytesSend = client.EndSend(a_result);
#if DEBUG
            Console.WriteLine("SERVER::CONNECTION Sent {0} bytes to {1}", bytesSend, client.RemoteEndPoint.ToString());
#endif
        }
    }
}
