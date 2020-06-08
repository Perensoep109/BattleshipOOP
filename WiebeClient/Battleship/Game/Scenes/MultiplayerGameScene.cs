using Battleship.Game.Objects;
using Battleship.Game.UI;
using Engine;
using Engine.Networking;
using Engine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        public MultiplayerGameScene(GraphicsDevice a_graphics) : base(a_graphics)
        {
        }

        public void ProcessPacket(object a_sender, Packet a_packet)
        {
            if (a_packet.m_type == PacketType.Ping)
            {
                NetworkResync = true;
                NetworkScene = this;
            }

            if(a_packet.m_type == PacketType.UpdateGameState)
            {
                m_gameState = (GameState)a_packet[Packet.BODY_START_POS];
                NetworkResync = true;
                NetworkScene = this;
            }

            if(a_packet.m_type == PacketType.Hit)
            {
                m_shipGrid.CheckHit(a_packet[Packet.BODY_START_POS], a_packet[Packet.BODY_START_POS + 1]);
                NetworkResync = true;
                NetworkScene = this;
            }

            if (a_packet.m_type == PacketType.Shoot)
            {
                //m_shipGrid.CheckHit(a_packet[Packet.BODY_START_POS + 1], a_packet[Packet.BODY_START_POS + 2]);
                m_enemyShipGrid.SetCell(a_packet[Packet.BODY_START_POS], a_packet[Packet.BODY_START_POS + 1], a_packet[Packet.BODY_START_POS + 2]);
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
            m_shipGrid = ((MultiplayerGameScene)NetworkScene).m_shipGrid;
            m_enemyShipGrid = ((MultiplayerGameScene)NetworkScene).m_enemyShipGrid;
            NetworkScene = null;
            NetworkResync = false;
        }

        public override void Update()
        {
        }

        public override void Initialize()
        {
            PreferredWindowWidth = 25 * 16 + 1;
            PreferredWindowHeight = 35 * 16 + 1;
            UI = new MultiplayerGameUI(m_graphics);

            m_connection = new ServerConnection();
            m_connection.Connect(IPAddress.Parse("127.0.0.1"), 69);
            m_connection.ReceivedPacket += ProcessPacket;

            GameObjects.Add(m_shipGrid = new ShipGrid(          new Vector2(16, 19 * 16),   16, 16, 16));
            GameObjects.Add(m_enemyShipGrid = new EnemyShipGrid(new Vector2(16, 16),        16, 16, 16));

            m_gameState = GameState.ShipPlacement;

            m_shipGrid.OnCellClick += ShipGridOnCellClick;
            m_enemyShipGrid.OnCellClick += EnemyShipGridOnCellClick;
        }

        private void ShipGridOnCellClick(object sender, Cell e)
        {
            if (m_gameState == GameState.ShipPlacement)
            {
                m_shipGrid.CreateShip();
                m_shipGrid.UpdateShipPreview(-16, -16, 0);
            }
        }

        private void EnemyShipGridOnCellClick(object sender, Cell e)
        {
            EnemyShipGrid grid = (EnemyShipGrid)sender;
            if(m_gameState == GameState.Shoot)
                m_connection.Send(new Packet(new byte[] { 0x2, 0x0, 0x0, 0x0, 0x0, 0x1, (byte)e.m_xPos, (byte)e.m_yPos, 0x0}));  // Shoot
        }

        public void OnMouseInput(object sender, MouseStateEventArgs state)
        {
            if (m_gameState == GameState.ShipPlacement)
                m_shipGrid.UpdateShipPreview(state.m_newState.X, state.m_newState.Y, (state.m_newState.ScrollWheelValue - state.m_oldState.ScrollWheelValue) / 120);
        }

        public void OnKeyboardInput(object sender, KeyboardState state)
        {
            if (state.IsKeyDown(Keys.Space) && m_gameState != GameState.Shoot)
            {
                m_gameState = GameState.Shoot;
                m_shipGrid.UpdateShipPreview(-16, -16, 0);
            }
        }
    }
}
