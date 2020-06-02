using Battleship.Game.Objects;
using Engine.Networking;
using Engine.Scenes;
using Microsoft.Xna.Framework;
using System;
using System.Net;

namespace Battleship.Game.Scenes
{
    class MultiplayerGameScene : GameScene, INetworkScene
    {
        public bool NetworkResync { get; set; }
        public GameScene NetworkScene { get; set; }

        private ServerConnection m_connection;
        private GameState m_gameState;

        private EnemyShipGrid m_enemyShipGrid;
        private ShipGrid m_shipGrid;

        public void ProcessPacket(object a_sender, Packet a_packet)
        {
            if (a_packet.m_type == PacketType.Ping)
            {
                NetworkResync = true;
                NetworkScene = this;
            }

            if(a_packet.m_type == PacketType.UpdateGameState)
            {
                m_gameState = (GameState)a_packet.m_buffer[Packet.BODY_START_POS];
                NetworkResync = true;
                NetworkScene = this;
            }
        }

        public void Sync()
        {
            for(int i = 0; i < NetworkScene.GameObjects.Count; i++)
            {
                GameObjects[i] = NetworkScene.GameObjects[i];
            }
            m_gameState = ((MultiplayerGameScene)NetworkScene).m_gameState;
            NetworkScene = null;
            NetworkResync = false;
        }

        public override void Update()
        {
            
        }

        public override void Initialize()
        {
            PreferredWindowWidth = 16 * 16 + 1;
            PreferredWindowHeight = 16 * 16 + 17 * 16 + 1;

            m_connection = new ServerConnection();
            m_connection.Connect(IPAddress.Parse("127.0.0.1"), 69);
            m_connection.ReceivedPacket += ProcessPacket;

            GameObjects.Add(m_shipGrid = new ShipGrid(new Vector2(0, 17 * 16), 16, 16, 16, 16));
            GameObjects.Add(m_enemyShipGrid = new EnemyShipGrid(new Vector2(0, 0), 16, 16, 16, 16));

            m_shipGrid.OnCellClick += shipGridOnCellClick;
            m_enemyShipGrid.OnCellClick += enemyShipGridOnCellClick;
        }

        private void shipGridOnCellClick(object sender, ClickedCell e)
        {
            Console.WriteLine("Clicked on ship grid {0}, {1}, Value {2}", e.m_xPos, e.m_yPos, e.m_data);
        }

        private void enemyShipGridOnCellClick(object sender, ClickedCell e)
        {
            Console.WriteLine("Clicked on enemy ship grid {0}, {1}, Value {2}", e.m_xPos, e.m_yPos, e.m_data);
            EnemyShipGrid grid = (EnemyShipGrid)sender;
            grid.SetCell(e.m_xPos, e.m_yPos, 1);
        }
    }
}
