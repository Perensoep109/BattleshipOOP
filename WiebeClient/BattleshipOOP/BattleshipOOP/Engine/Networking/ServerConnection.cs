using System;
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

        public event EventHandler<Packet> ReceivedPacket;

        private Socket m_client;

        public ServerConnection()
        {
            m_client = new Socket(SocketType.Stream, ProtocolType.Tcp);
        }

        public void Send(Packet a_packet)
        {
            if(m_client.Connected)
                m_client.BeginSend(a_packet.m_buffer, 0, a_packet.m_buffer.Length, 0, SendCallback, m_client);
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
                Console.WriteLine("CLIENT::CONNECTION Read {0} bytes from {1}", bytesRead, client.RemoteEndPoint.ToString());
#endif
                if(bytesRead > 0)
                    ReceivedPacket?.Invoke(this, PacketInterpreter.InterpretPacket(state.m_buffer));
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
            Console.WriteLine("CLIENT::CONNECTION Sent {0} bytes to {1}", bytesSend, client.RemoteEndPoint.ToString());
#endif
        }

        private void ConnectionStateCallback(IAsyncResult a_result)
        {
            try
            {
                m_client.EndConnect(a_result);

                if (m_client.Connected)
                {
                    StateObject state = new StateObject();
                    state.m_socket = m_client;
                    m_client.BeginReceive(state.m_buffer, 0, StateObject.m_bufferSize, 0, ReceiveCallback, state);
                }
            }
            catch(SocketException a_e)
            {
                Console.WriteLine("ERROR::CLIENT::CONNECTION::SOCKET::EXCEPTION {0}", a_e.Message);
            }
            catch(Exception a_e)
            {
                Console.WriteLine("ERROR::CLIENT::CONNECTION::GENERIC::EXCEPTION {0}", a_e.ToString());
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
