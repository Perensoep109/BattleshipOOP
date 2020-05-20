using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipOOP.Engine.Networking
{
    class PacketInterpreter
    {
        public static Packet InterpretPacket(byte[] a_buffer)
        {
            Packet packet = new Packet(a_buffer, PacketType.Ping);

            return packet;
        }
    }
}
