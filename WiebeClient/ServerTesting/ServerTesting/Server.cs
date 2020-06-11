using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerTesting
{
    class Server
    {
        TcpListener m_server;
        TcpClient m_client;

        public Server()
        {
            m_server = new TcpListener(IPAddress.Parse("127.0.0.1"), 69);
            Start();
        }

        public void ClientConnected(IAsyncResult a_result)
        {
            TcpListener server = (TcpListener)a_result.AsyncState;
            m_client = server.EndAcceptTcpClient(a_result);

            Console.WriteLine("SERVER::CONNECTION::ESTABLISHED Client connected, {0}", m_client.Client.RemoteEndPoint.ToString());
            StartReceive();
        }

        public void StartReceive()
        {
            try
            {
                StateObject state = new StateObject();
                state.m_socket = m_client.Client;
                m_client.Client.BeginReceive(state.m_buffer, 0, StateObject.m_bufferSize, 0, ReceiveCallback, state);
            }
            catch (Exception a_e)
            {
                Console.WriteLine(a_e);
            }
        }

        public void ReceiveCallback(IAsyncResult a_result)
        {
            try
            {
                StateObject state = (StateObject)a_result.AsyncState;
                Socket client = state.m_socket;
                int bytesRead = client.EndReceive(a_result);

#if DEBUG
                Console.WriteLine("SERVER::CONNECTION Read {0} bytes from {1}", bytesRead, client.RemoteEndPoint.ToString());
#endif
                if (bytesRead > 0)
                {
                    if (state.m_buffer[1] == 0)
                        Send(new byte[] { 0x8, 0x0, 0x0, 0x0, 0x0, 0x0, 0x1, 0xFF, 0xFF });

                    if (state.m_buffer[1] == 2)
                    {
                        // Send back the hit to the player who shot
                        Send(new byte[] { 0xC, 0x2, 0x0, 0x0, 0x0, 0x0, 0x1, state.m_buffer[6], state.m_buffer[6 + 1], 0x1, 0xFF, 0xFF });

                        // Send back the hit to the hit player
                        Send(new byte[] { 0xC, 0x3, 0x0, 0x0, 0x0, 0x0, 0x1, state.m_buffer[6], state.m_buffer[6 + 1], 0x1, 0xFF, 0xFF });
                    }

                    if(state.m_buffer[1] == 4)
                    {
                        Send(new byte[] { 0xE, 0x4, 0x0, 0x0, 0x0, 0x0, 0x1, state.m_buffer[6 + 0], state.m_buffer[6 + 1], state.m_buffer[6 + 2], state.m_buffer[6 + 3], state.m_buffer[6 + 4], Convert.ToByte(true), 0xFF, 0xFF });
                    }
                }
                m_client.Client.BeginReceive(state.m_buffer, 0, StateObject.m_bufferSize, 0, ReceiveCallback, state);
            }
            catch(SocketException a_e)
            {
                Console.WriteLine("SERVER::CONNECTION::LOST " + m_client.Client.RemoteEndPoint.ToString());
                m_client.Close();
                m_client.Dispose();
                m_client = null;
                m_server.BeginAcceptTcpClient(ClientConnected, m_server);
            }
            catch (Exception a_e)
            {
                Console.WriteLine(a_e);
            }
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

        private void Start()
        {
            m_server.Start();
            m_server.BeginAcceptTcpClient(ClientConnected, m_server);
            Console.WriteLine("SERVER Started server");
        }
    }

    internal class StateObject
    {
        public Socket m_socket;
        public const int m_bufferSize = 32;
        public byte[] m_buffer = new byte[m_bufferSize];
    }
}
