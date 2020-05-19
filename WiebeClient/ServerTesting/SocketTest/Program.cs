using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketTest
{
    class Program
    {
        internal class StateObject
        {
            public Socket m_socket;
            public const int m_bufferSize = 32;
            public byte[] m_buffer = new byte[m_bufferSize];
        }

        static Socket m_client;

        static void Main(string[] args)
        {
            Start();
        }

        static void Start()
        {
            m_client = new Socket(SocketType.Stream, ProtocolType.Tcp);
            m_client.BeginConnect("127.0.0.1", 69, ConnectionStateCallback, m_client);

            while (true) { }
        }

        static void ConnectionStateCallback(IAsyncResult a_result)
        {
            Socket client = (Socket)a_result.AsyncState;
            client.EndConnect(a_result);

            Console.WriteLine("Client connected to {0}",
                client.RemoteEndPoint.ToString());
        }
    }
}

/*
 * using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipOOP.Engine.Networking
{
    class ServerConnection : IDisposable
    {
        internal class StateObject
        {
            public Socket m_socket;
            public const int m_bufferSize = 32;
            public byte[] m_buffer = new byte[m_bufferSize];
        }

        public event EventHandler<byte[]> ReceivedPacket;

        Socket m_client;

        public ServerConnection()
        {
            m_client = new Socket(SocketType.Stream, ProtocolType.Tcp);
        }

        public void Send(byte[] a_packet)
        {
            m_client.BeginSend(a_packet, 0, a_packet.Length, 0, SendCallback, m_client);
        }

        public void Connect(IPAddress a_to, int a_port)
        {
            try
            {
                m_client.BeginConnect(a_to, a_port, ConnectionStateCallback, null);
            }
            catch (Exception a_e)
            {
                Console.WriteLine(a_e);
            }
        }

        #region Callback functions
        private void ReceiveCallback(IAsyncResult a_result)
        {
            try
            {
                StateObject state = (StateObject)a_result.AsyncState;
                Socket client = state.m_socket;
                int bytesRead = client.EndReceive(a_result);

#if DEBUG
                Console.WriteLine("SERVER::CONNECTION Read {0} bytes from {1}", bytesRead, client.RemoteEndPoint.ToString());
#endif
                if(bytesRead > 0)
                    ReceivedPacket?.Invoke(this, state.m_buffer);
                m_client.BeginReceive(state.m_buffer, 0, StateObject.m_bufferSize, 0, ReceiveCallback, state);
            }
            catch (Exception a_e)
            {
                Console.WriteLine(a_e);
            }
        }

        private void SendCallback(IAsyncResult a_result)
        {
            Socket client = (Socket)a_result.AsyncState;

            int bytesSend = client.EndSend(a_result);
#if DEBUG
            Console.WriteLine("SERVER::CONNECTION Sent {0} bytes to {1}", client.RemoteEndPoint.ToString());
#endif
        }

        private void ConnectionStateCallback(IAsyncResult a_result)
        {
            m_client.EndConnect(a_result);

            if (m_client.Connected)
            {
                Console.WriteLine("Server connection established! {0}", a_result);
                StateObject state = new StateObject();
                state.m_socket = m_client;
                m_client.BeginReceive(state.m_buffer, 0, StateObject.m_bufferSize, 0, ReceiveCallback, state);
            }
        }
        #endregion

        public void Dispose()
        {
            m_client.Close();
            m_client.Dispose();
        }
    }
}

 */
