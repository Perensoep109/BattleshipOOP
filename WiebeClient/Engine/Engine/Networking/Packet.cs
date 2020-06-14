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
        ShipPlacementLength = 5,
        GameConnect = 6,
        AssignPlayerID = 7
    }

    public class Packet
    {
        public byte m_bufferLength;
        public byte[] m_buffer = null;
        public PacketType m_type = PacketType.Ping;
        public uint m_gameID = 0;

        public const uint BODY_START_POS = 7;

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

        public static Packet CreateShootPackage(uint a_gameID, int a_xPos, int a_yPos, byte a_playerID, byte a_toPlayerID)
        {
            byte[] gameID = BitConverter.GetBytes(a_gameID);
            return new Packet(new byte[] { 0xD, 0x2, gameID[0], gameID[1], gameID[2], gameID[3], a_playerID, a_toPlayerID, (byte)a_xPos, (byte)a_yPos, 0x0, 0xFF, 0xFF});
        }

        public static Packet CreateShipPlacementPackage(uint a_gameID, int a_xPos, int a_yPos, int a_xDir, int a_yDir, int a_length, byte a_playerID)
        {
            byte[] gameID = BitConverter.GetBytes(a_gameID);
            return new Packet(new byte[] { 0xF, 0x4, gameID[0], gameID[1], gameID[2], gameID[3], a_playerID, (byte)a_xPos, (byte)a_yPos, (byte)a_xDir, (byte)a_yDir, (byte)a_length, Convert.ToByte(false), 0xFF, 0xFF });
        }
        public static Packet CreateGameConnect(uint a_gameID)
        {
            byte[] gameID = BitConverter.GetBytes(a_gameID);
            return new Packet(new byte[] { 0xA, (byte)PacketType.GameConnect, gameID[0], gameID[1], gameID[2], gameID[3], 0x0, 0xFF, 0xFF });
        }
    }
}
