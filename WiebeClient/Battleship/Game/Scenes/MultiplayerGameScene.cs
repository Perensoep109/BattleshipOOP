using Battleship.Game.Objects;
using Engine.Events.EventListeners;
using Engine.Graphics;
using Engine.Networking;
using Engine.Scenes;
using Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Battleship.Game.Scenes
{
    class MultiplayerGameScene : GameScene, INetworkScene
    {
        private class UI : UILayer
        {
            public UILabel m_label;

            public UI(GraphicsDevice a_graphics)
            {
                AddUI(m_label = new UILabel(50, 30, "Ships to place", ResourcePool.GetSpriteFont("font").Font, a_graphics), 6, 10);
            }
        }

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
                m_enemyShipGrid.SetCell(a_packet[Packet.BODY_START_POS], a_packet[Packet.BODY_START_POS + 1], a_packet[Packet.BODY_START_POS + 2]);
                NetworkResync = true;
                NetworkScene = this;
            }

            if (a_packet.m_type == PacketType.ShipPlacement)
            {
                if (Convert.ToBoolean(a_packet[Packet.BODY_START_POS + 5]))
                {
                    m_shipGrid.CreateShip();
                    NetworkResync = true;
                    NetworkScene = this;
                }
            }
        }

        public void Sync()
        {
            for(int i = 0; i < NetworkScene.GameObjects.Count; i++)
                GameObjects[i] = NetworkScene.GameObjects[i];

            m_gameState = ((MultiplayerGameScene)NetworkScene).m_gameState;
            m_shipGrid = ((MultiplayerGameScene)NetworkScene).m_shipGrid;
            m_enemyShipGrid = ((MultiplayerGameScene)NetworkScene).m_enemyShipGrid;
            NetworkScene = null;
            NetworkResync = false;
        }

        public override void Update()
        {
        }

        public override void Initialize(params object[] a_initialData)
        {
            PreferredWindowWidth = 25 * 16 + 1;
            PreferredWindowHeight = 35 * 16 + 1;
            UiLayer = new UI(m_graphics);

            m_connection = (ServerConnection)a_initialData[0];
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
                if (m_shipGrid.ShipPreview.m_validPos)
                    m_connection.Send(Packet.CreateShipPlacementPackage((byte)e.m_xPos, (byte)e.m_yPos, 1, 0, 5));
        }

        private void EnemyShipGridOnCellClick(object sender, Cell e)
        {
            if(m_gameState == GameState.Shoot)
                m_connection.Send(Packet.CreateShootPackage(e.m_xPos, e.m_yPos));  // Shoot
        }

        public void OnMouseInput(object sender, MouseStateEventArgs state)
        {
            if (m_gameState == GameState.ShipPlacement)
                m_shipGrid.UpdateShipPreview(state.m_newState.X, state.m_newState.Y, (state.m_newState.ScrollWheelValue - state.m_oldState.ScrollWheelValue) / 120);
        }

        public void OnKeyboardInput(object sender, KeyboardStateEventArgs state)
        {
            if (state.m_newState.IsKeyDown(Keys.Space) && m_gameState != GameState.Shoot)
            {
                m_gameState = GameState.Shoot;
                m_shipGrid.UpdateShipPreview(-16, -16, 0);
            }
        }
    }
}
