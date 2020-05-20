using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipOOP.Engine.Networking
{
    enum PacketType
    {
        Ping = 0
    }

    struct Packet
    {
        public byte[] m_buffer;
        public PacketType m_type;

        public Packet(byte[] a_buffer, PacketType a_type)
        {
            m_buffer = a_buffer;
            m_type = a_type;
        }
    }
}
