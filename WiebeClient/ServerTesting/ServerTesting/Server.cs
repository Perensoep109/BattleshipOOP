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
        bool m_verbose;

        private int m_length = 2;

        const int BODY_START_POS = 6;

        public Server(string a_ip, int a_port, bool a_verbose)
        {
            m_verbose = a_verbose;
            m_server = new TcpListener(IPAddress.Parse(a_ip), a_port);
            Start();
        }

        private void ClientConnected(IAsyncResult a_result)
        {
            try
            {
                TcpListener server = (TcpListener)a_result.AsyncState;
                m_client = server.EndAcceptTcpClient(a_result);

                Console.WriteLine("SERVER::CONNECTION::ESTABLISHED Client connected, {0}", m_client.Client.RemoteEndPoint.ToString());
                StartReceive();
            }
            catch(SocketException a_e)
            {
                Console.WriteLine("ERROR::SERVER::CONNECTION::ESTABLISHING " + a_e);
                ConnectClients();
            }
        }

        private void StartReceive()
        {
            try
            {
                StateObject state = new StateObject();
                state.m_socket = m_client.Client;
                m_client.Client.BeginReceive(state.m_buffer, 0, StateObject.m_bufferSize, 0, ReceiveCallback, state);
            }
            catch (Exception a_e)
            {
                Console.WriteLine("ERROR::SERVER::RECEIVE Could not receive data from client " + a_e.ToString());
            }
        }

        private async void ReceiveCallback(IAsyncResult a_result)
        {
            try
            {
                StateObject state = (StateObject)a_result.AsyncState;
                Socket client = state.m_socket;
                int bytesRead = client.EndReceive(a_result);

                Console.WriteLine();
                Console.WriteLine("SERVER::CONNECTION Read {0} bytes from {1}", bytesRead, client.RemoteEndPoint.ToString());
                if (m_verbose)
                    Console.WriteLine("BYTES::READ {0} from {1}", BitConverter.ToString(state.m_buffer).Replace('-', ' '), m_client.Client.RemoteEndPoint.ToString());

                if (bytesRead > 0)
                {
                    if (state.m_buffer[1] == 0)
                        Send(new byte[] { 0x7, 0x0, 0x0, 0x0, 0x0, 0x0, 0xFF, 0xFF });

                    if (state.m_buffer[1] == 1)
                    {
                        Send(new byte[] { 0x8, 0x1, 0x0, 0x0, 0x0, 0x0, 0x1, 0xFF, 0xFF });
                        Send(new byte[] { 0x8, 0x5, 0x0, 0x0, 0x0, 0x0, 0x2, 0xFF, 0xFF });
                    }

                    if (state.m_buffer[1] == 2)
                    {
                        // Send back the hit to the player who shot
                        Send(new byte[] { 0xC, 0x2, 0x0, 0x0, 0x0, 0x0, state.m_buffer[BODY_START_POS], state.m_buffer[BODY_START_POS + 1], state.m_buffer[BODY_START_POS + 2], 0x1, 0xFF, 0xFF });

                        // Send back the hit to the hit player
                        Send(new byte[] { 0xC, 0x3, 0x0, 0x0, 0x0, 0x0, state.m_buffer[BODY_START_POS], state.m_buffer[BODY_START_POS + 1], state.m_buffer[BODY_START_POS + 2], 0x1, 0xFF, 0xFF });

                        _ = Task.Run(() =>
                        {
                            // Switch whose players turn it is
                            Thread.Sleep(100);
                            Send(new byte[] { 0x8, 0x1, 0x0, 0x0, 0x0, 0x0, 0x3, 0xFF, 0xFF });
                            Thread.Sleep(3000);
                            Send(new byte[] { 0x8, 0x1, 0x0, 0x0, 0x0, 0x0, 0x2, 0xFF, 0xFF });
                        });
                    }

                    if (state.m_buffer[1] == 4)
                    {
                        Send(new byte[] { 0xE, 0x4, 0x0, 0x0, 0x0, 0x0, state.m_buffer[BODY_START_POS], state.m_buffer[BODY_START_POS + 1], state.m_buffer[BODY_START_POS + 2], state.m_buffer[BODY_START_POS + 3], state.m_buffer[BODY_START_POS + 4], state.m_buffer[BODY_START_POS + 5], Convert.ToByte(true), 0xFF, 0xFF });
                        if (m_length != 5)
                            Send(new byte[] { 0x8, 0x5, 0x0, 0x0, 0x0, 0x0, (byte)++m_length, 0xFF, 0xFF });
                        else
                            Send(new byte[] { 0x8, 0x1, 0x0, 0x0, 0x0, 0x0, 0x2, 0xFF, 0xFF });
                    }
                }
                m_client.Client.BeginReceive(state.m_buffer, 0, StateObject.m_bufferSize, 0, ReceiveCallback, state);
            }
            catch(SocketException a_e)
            {
                Console.WriteLine("SERVER::CONNECTION::LOST " + m_client.Client.RemoteEndPoint.ToString());
                ConnectClients();
            }
            catch (Exception a_e)
            {
                Console.WriteLine(a_e);
            }
        }

        private void Send(byte[] a_packet)
        {
            m_client.Client.BeginSend(a_packet, 0, a_packet.Length, 0, SendCallback, m_client.Client);
            if (m_verbose)
                Console.WriteLine("BYTES::SENT {0} to {1}", BitConverter.ToString(a_packet).Replace('-', ' '), m_client.Client.RemoteEndPoint.ToString());
        }

        private void SendCallback(IAsyncResult a_result)
        {
            Socket client = (Socket)a_result.AsyncState;

            int bytesSend = client.EndSend(a_result);
            Console.WriteLine("SERVER::CONNECTION Sent {0} bytes to {1}", bytesSend, client.RemoteEndPoint.ToString());
        }

        private void ConnectClients()
        {
            if (m_client != null)
            {
                m_length = 2;
                m_client.Close();
                m_client.Dispose();
                m_client = null;
            }
            Console.WriteLine("SERVER::CONNECTION Waiting for client connection attempt");
            m_server.BeginAcceptTcpClient(ClientConnected, m_server);
        }

        private void Start()
        {
            m_server.Start();
            Console.WriteLine("SERVER Started server on {0}, verbose logging: {1}, type 'help' to show commands", m_server.LocalEndpoint.ToString(), m_verbose);
            ConnectClients();
        }

        public void ToggleVerbose()
        {
            m_verbose = !m_verbose;
            Console.WriteLine("SERVER Toggled verbose mode, current mode: {0}", m_verbose);
        }
    }

    internal class StateObject
    {
        public Socket m_socket;
        public const int m_bufferSize = 32;
        public byte[] m_buffer = new byte[m_bufferSize];
    }
}
