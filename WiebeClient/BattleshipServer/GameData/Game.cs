using BaseServer.Events;
using BaseServer.Networking;
using BattleshipServer;
using BattleshipServer.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerTesting
{
    enum GameState
    {
        Invalid = 0,
        ShipPlacement = 1,
        YourTurn = 2,
        OtherPlayerTurn = 3,
        GameWon = 4,
        GameLost = 5
    }

    class Game
    {
        private Dictionary<byte, Player> m_players;
        private int m_maxPlayerCount = 2;
        private uint m_gameID;
        public bool m_running;

        private byte[] m_shipPlacementPattern = new byte[] { 2, 3, 3, 4, 5, 0};

        public Game(uint a_gameID)
        {
            m_gameID = a_gameID;
            m_players = new Dictionary<byte, Player>();
        }

        public void ProcessPacket(GamePacket a_packet)
        {
            if(m_running)
            {
                // Place a ship
                if (a_packet.m_type == GamePacket.PacketType.ShipPlacement)
                {
                    Player player;
                    if (m_players.TryGetValue(a_packet[GamePacket.PLAYER_ID_POS], out player))
                    {
                        if(player.m_ships.Count + 1 < m_shipPlacementPattern.Length)
                        {
                            player.CreateShip(new byte[] { a_packet[GamePacket.BODY_START_POS], a_packet[GamePacket.BODY_START_POS + 1], a_packet[GamePacket.BODY_START_POS + 2], a_packet[GamePacket.BODY_START_POS + 3], a_packet[GamePacket.BODY_START_POS + 4] });
                            player.m_client.Send(GamePacket.ValidateShipPlacement(m_gameID));
                            player.m_client.Send(GamePacket.SetShipLength(m_gameID, m_shipPlacementPattern[player.m_ships.Count]));
                        }
                    }

                    // Once all ships are placed, move to a new game state
                    if(GetPlayer(1)?.m_ships.Count == m_shipPlacementPattern.Length - 1 && GetPlayer(2)?.m_ships.Count == m_shipPlacementPattern.Length - 1)
                    {
                        GetPlayer(1)?.m_client.Send(GamePacket.CreateStateUpdate(m_gameID, 0, GameState.YourTurn));
                        GetPlayer(2)?.m_client.Send(GamePacket.CreateStateUpdate(m_gameID, 0, GameState.OtherPlayerTurn));
                    }
                }

                // Shoot
                if(a_packet.m_type == GamePacket.PacketType.Shoot)
                {
                    byte fromPlayerID = a_packet[GamePacket.PLAYER_ID_POS];
                    byte toPlayerID = a_packet[GamePacket.BODY_START_POS];
                    Player fromPlayer = GetPlayer(fromPlayerID);
                    Player toPlayer = GetPlayer(toPlayerID);

                    // Shoot and hit player
                    bool hit = toPlayer.Hit(a_packet[GamePacket.BODY_START_POS + 1], a_packet[GamePacket.BODY_START_POS + 2]);
                    fromPlayer.m_client.Send(GamePacket.Shoot(m_gameID, a_packet[GamePacket.BODY_START_POS + 1], a_packet[GamePacket.BODY_START_POS + 2], hit, fromPlayerID, toPlayerID));
                    toPlayer.m_client.Send(GamePacket.Hit(m_gameID, a_packet[GamePacket.BODY_START_POS + 1], a_packet[GamePacket.BODY_START_POS + 2], hit, toPlayerID, fromPlayerID));
                    Thread.Sleep(10);

                    // Check win condition
                    if(toPlayer.CheckDestroyed())
                    {
                        fromPlayer.m_client.Send(GamePacket.CreateStateUpdate(m_gameID, fromPlayerID, GameState.GameWon));
                        toPlayer.m_client.Send(GamePacket.CreateStateUpdate(m_gameID, toPlayerID, GameState.GameLost));
                        return;
                    }

                    fromPlayer.m_client.Send(GamePacket.CreateStateUpdate(m_gameID, fromPlayerID, GameState.OtherPlayerTurn));
                    toPlayer.m_client.Send(GamePacket.CreateStateUpdate(m_gameID, toPlayerID, GameState.YourTurn));
                }
            }
        }

        public bool ConnectPlayer(Player a_player)
        {
            if(m_players.Count < m_maxPlayerCount)
            {
                m_players.Add((byte)(m_players.Count + 1), a_player);
                a_player.m_client.Send(GamePacket.AssignPlayerID(m_gameID, (byte)m_players.Count));
                Console.WriteLine("GAME A player with ID {0} connected to game {1}", a_player.ID, m_gameID);

                if (m_players.Count == m_maxPlayerCount)
                    Start();
                    
                return true;
            }

            return false;
        }

        public void Start()
        {
            Console.WriteLine("GAME Game {0} started", m_gameID);
            m_running = true;
            foreach (KeyValuePair<byte, Player> pair in m_players)
            {
                pair.Value.m_client.Send(GamePacket.CreateStateUpdate(m_gameID, pair.Key, GameState.ShipPlacement));
                Thread.Sleep(10);
                pair.Value.m_client.Send(GamePacket.SetShipLength(m_gameID, m_shipPlacementPattern[pair.Value.m_ships.Count]));
            }
        }

        private Player GetPlayer(byte a_index)  
        {
            Player player;
            m_players.TryGetValue(a_index, out player);
            return player;
        }
    }
}