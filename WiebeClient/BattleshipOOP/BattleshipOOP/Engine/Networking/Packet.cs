using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipOOP.Engine.Networking
{
    enum PacketType : byte
    {
        Ping = 0
    }

    class Packet
    {
        public byte[] m_buffer = null;
        public PacketType m_type = PacketType.Ping;
        public uint m_gameID = 0;

        public Packet(byte[] a_buffer)
        {
            m_buffer = a_buffer;
        }
    }
}
