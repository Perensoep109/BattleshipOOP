using BaseServer;
using BattleshipServer.GameData;
using ServerTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipServer
{
    class Server : ServerBase
    {
        private Dictionary<uint, Game> m_runningGames;
        private Dictionary<uint, Player> m_players;
        private uint m_lastGameKey = 0;

        public Server()
        {
            OnConnectionEstablished += Server_OnConnectionEstablished;
            OnConnectionLost += Server_OnConnectionLost;
            OnPacketReceived += Server_OnPacketReceived;
            m_runningGames = new Dictionary<uint, Game>();
            m_players = new Dictionary<uint, Player>();
        }

        public override void Initialize(IPAddress a_ip, int a_port)
        {
            base.Initialize(a_ip, a_port);
            m_cmdHandler.AddCommand(new Command("creategame", CreateGame, 0, "Create a game"));
            m_cmdHandler.ExecuteCommand("creategame");
        }

        private void Server_OnPacketReceived(object sender, BaseServer.Events.PacketReceivedEventArgs e)
        {
            if (m_verbose)
            {
                string bytes = "";
                foreach (byte _byte in e.m_packet.m_buffer)
                    bytes += _byte + " ";
                Console.WriteLine(bytes + "\n");
            }

            // Select correct game
            Game game;
            if (m_runningGames.TryGetValue(e.m_packet[2], out game))
            {
                if(e.m_packet[GamePacket.PACKET_TYPE_POS] == 0x6)
                {
                    Player player;
                    if (m_players.TryGetValue(e.m_client.ID, out player))
                        game.ConnectPlayer(player);
                    return;
                }

                game.ProcessPacket(GamePacket.Interpret(e.m_packet));
            }
        }

        private void Server_OnConnectionLost(object sender, BaseServer.Events.ConnectionEventArgs e)
        {

        }

        private void Server_OnConnectionEstablished(object sender, BaseServer.Events.ConnectionEventArgs e)
        {
            m_players.Add(e.m_client.ID, new Player(e.m_client.ID, e.m_client));
        }

        private void CreateGame(string[] a_args)
        {
            m_runningGames.Add(m_lastGameKey, new Game(m_lastGameKey));
            Console.WriteLine("Created a game, ID {0}", m_lastGameKey);
            m_lastGameKey++;
        }
    }
}
