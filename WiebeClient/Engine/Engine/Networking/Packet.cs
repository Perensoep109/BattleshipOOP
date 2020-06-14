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
        UpdateGameState = 1,
        Shoot = 2,
        Hit = 3,
        ShipPlacement = 4,
        ShipPlacementLength = 5
    }

    public class Packet
    {
        public byte m_bufferLength;
        public byte[] m_buffer = null;
        public PacketType m_type = PacketType.Ping;
        public uint m_gameID = 0;

        public const uint BODY_START_POS = 6;

        public Packet(byte[] a_buffer)
        {
            m_buffer = a_buffer;
        }

        /// <summary>
        /// Overload the [] operator of this instance
        /// </summary>
        /// <param name="a_index">The index of the byte to return</param>
        /// <returns>The buffer</returns>
        public byte this[uint a_index]
        {
            get => m_buffer[a_index];
        }

        public static Packet CreateShootPackage(int a_xPos, int a_yPos)
        {
            return new Packet(new byte[] { 0xC, 0x2, 0x0, 0x0, 0x0, 0x0, 0x1, (byte)a_xPos, (byte)a_yPos, 0x0, 0xFF, 0xFF});
        }

        public static Packet CreateShipPlacementPackage(int a_xPos, int a_yPos, int a_xDir, int a_yDir, int a_length)
        {
            return new Packet(new byte[] { 0xF, 0x4, 0x0, 0x0, 0x0, 0x0, 0x1, (byte)a_xPos, (byte)a_yPos, (byte)a_xDir, (byte)a_yDir, (byte)a_length, Convert.ToByte(false), 0xFF, 0xFF });
        }
    }
}
