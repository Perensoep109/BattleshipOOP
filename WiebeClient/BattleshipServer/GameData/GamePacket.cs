using BaseServer.Networking;
using ServerTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipServer.GameData
{
    class GamePacket : Packet
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

        public PacketType m_type;
        public static readonly uint BODY_START_POS = 7;
        public static readonly uint PACKET_TYPE_POS = 1;
        public static readonly uint PLAYER_ID_POS = 6;

        public GamePacket(byte[] a_buffer) : base(a_buffer)
        {
            m_type = PacketType.Ping;
        }

        public static GamePacket Interpret(Packet a_packet)
        {
            GamePacket packet = new GamePacket(a_packet.m_buffer);
            packet.m_type = (PacketType)a_packet[PACKET_TYPE_POS];
            return packet;
        }

        public static GamePacket CreateStateUpdate(uint a_gameID, byte a_playerID, GameState a_state)
        {
            byte[] gameID = BitConverter.GetBytes(a_gameID);
            return new GamePacket(new byte[] { 0xA, 0x1, gameID[0], gameID[1], gameID[2], gameID[3], 0x0, (byte)a_state, 0xFF, 0xFF });
        }
        public static GamePacket AssignPlayerID(uint a_gameID, byte a_playerID)
        {
            byte[] gameID = BitConverter.GetBytes(a_gameID);
            return new GamePacket(new byte[] { 0xA, 0x7, gameID[0], gameID[1], gameID[2], gameID[3], 0x0, a_playerID, 0xFF, 0xFF });
        }

        public static GamePacket ValidateShipPlacement(uint a_gameID)
        {
            byte[] gameID = BitConverter.GetBytes(a_gameID);
            return new GamePacket(new byte[] { 0xA, (byte)PacketType.ShipPlacement, gameID[0], gameID[1], gameID[2], gameID[3], 0x0, Convert.ToByte(true), 0xFF, 0xFF });
        }

        public static GamePacket SetShipLength(uint a_gameID, byte a_length)
        {
            byte[] gameID = BitConverter.GetBytes(a_gameID);
            return new GamePacket(new byte[] { 0xA, (byte)PacketType.ShipPlacementLength, gameID[0], gameID[1], gameID[2], gameID[3], 0x0, a_length, 0xFF, 0xFF });
        }

        public static GamePacket Shoot(uint a_gameID, byte a_x, byte a_y, bool a_hit, byte a_playerID, byte a_toPlayerID)
        {
            byte[] gameID = BitConverter.GetBytes(a_gameID);
            return new GamePacket(new byte[] { 0xD, (byte)PacketType.Shoot, gameID[0], gameID[1], gameID[2], gameID[3], a_playerID, a_toPlayerID, a_x, a_y, Convert.ToByte(a_hit), 0xFF, 0xFF });
        }

        public static GamePacket Hit(uint a_gameID, byte a_x, byte a_y, bool a_hit, byte a_playerID, byte a_fromPlayerID)
        {
            byte[] gameID = BitConverter.GetBytes(a_gameID);
            return new GamePacket(new byte[] { 0xD, (byte)PacketType.Hit, gameID[0], gameID[1], gameID[2], gameID[3], a_playerID, a_fromPlayerID, a_x, a_y, Convert.ToByte(a_hit), 0xFF, 0xFF });
        }
    }
}
