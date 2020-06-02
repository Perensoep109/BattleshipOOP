using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Networking
{
    public enum PacketType : byte
    {
        Ping = 0,
        UpdateGameState = 1
    }

    public class Packet
    {
        public byte[] m_buffer = null;
        public PacketType m_type = PacketType.Ping;
        public uint m_gameID = 0;

        public const uint BODY_START_POS = 6;

        public Packet(byte[] a_buffer)
        {
            m_buffer = a_buffer;
        }
    }
}
