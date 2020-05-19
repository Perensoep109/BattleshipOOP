using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipOOP.Engine.Networking
{
    enum PacketType
    {

    }

    struct Packet
    {
        public byte[] m_buffer;

        public Packet(int a_bufferSize)
        {
            m_buffer = new byte[a_bufferSize];
        }

        public Packet(byte[] a_buffer)
        {
            m_buffer = a_buffer;
        }
    }
}
