using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Networking
{
    public class PacketInterpreter
    {
        public static Packet InterpretPacket(byte[] a_buffer)
        {
            Packet packet = new Packet(a_buffer);
            packet.m_type = (PacketType)a_buffer[0];
            packet.m_gameID = BitConverter.ToUInt32(a_buffer, 1);
            return packet;
        }
    }
}
