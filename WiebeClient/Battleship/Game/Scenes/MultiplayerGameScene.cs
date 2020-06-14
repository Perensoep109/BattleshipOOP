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
            public UILabel m_playerID;

            public UI(GraphicsDevice a_graphics)
            {
                AddUI(m_label = new UILabel(50, 0, "", ResourcePool.GetSpriteFont("font").Font, a_graphics), 0, 0);
                //AddUI(m_playerID = new UILabel(50, 0, "", ResourcePool.GetSpriteFont("font").Font, a_graphics), 2, 0);
            }
        }

        public bool NetworkResync { get; set; }
        public GameScene NetworkScene { get; set; }

        private ServerConnection m_connection;
        private GameState m_gameState;
        private uint m_gameID;
        private byte m_playerID;

        private EnemyShipGrid m_enemyShipGrid;
        private ShipGrid m_shipGrid;

        public MultiplayerGameScene(GraphicsDevice a_graphics) : base(a_graphics)
        {
        }

        public void ProcessPacket(object a_sender, Packet a_packet)
        {
            if (a_packet.m_type == PacketType.UpdateGameState)
            {
                if (m_gameState == GameState.ShipPlacement)
                    m_shipGrid.UpdateShipPreview(-16, -16, 0);

                m_gameState = (GameState)a_packet[Packet.BODY_START_POS];
                ((UI)UiLayer).m_label.Text = m_gameState.ToString();
                NetworkResync = true;
                NetworkScene = this;
            }

            if(a_packet.m_type == PacketType.AssignPlayerID)
            {
                m_playerID = a_packet[Packet.BODY_START_POS];
                NetworkResync = true;
                NetworkScene = this;
            }

            if (a_packet.m_type == PacketType.ShipPlacement)
            {
                if (Convert.ToBoolean(a_packet[Packet.BODY_START_POS]))
                {
                    m_shipGrid.CreateShip();
                    NetworkResync = true;
                    NetworkScene = this;
                }
            }

            if (a_packet.m_type == PacketType.ShipPlacementLength)
            {
                m_shipGrid.ShipPreview.m_length = a_packet[Packet.BODY_START_POS];
                NetworkResync = true;
                NetworkScene = this;
            }

            if (a_packet.m_type == PacketType.Hit)
            {
                m_shipGrid.CheckHit(a_packet[Packet.BODY_START_POS + 1], a_packet[Packet.BODY_START_POS + 2]);
                NetworkResync = true;
                NetworkScene = this;
            }

            if (a_packet.m_type == PacketType.Shoot)
            {
                m_enemyShipGrid.SetCell(a_packet[Packet.BODY_START_POS + 1], a_packet[Packet.BODY_START_POS + 2], (Convert.ToBoolean(a_packet[Packet.BODY_START_POS + 3]) == true ? 1 : 2));
                NetworkResync = true;
                NetworkScene = this;
            }
        }

        public void Sync()
        {
            for(int i = 0; i < NetworkScene.GameObjects.Count; i++)
                GameObjects[i] = NetworkScene.GameObjects[i];

            m_gameState = ((MultiplayerGameScene)NetworkScene).m_gameState;
            m_shipGrid = ((MultiplayerGameScene)NetworkScene).m_shipGrid;
            m_enemyShipGrid = ((MultiplayerGameScene)NetworkScene).m_enemyShipGrid;
            m_gameID = ((MultiplayerGameScene)NetworkScene).m_gameID;
            m_playerID = ((MultiplayerGameScene)NetworkScene).m_playerID;
            UiLayer = ((MultiplayerGameScene)NetworkScene).UiLayer;
            NetworkScene = null;
            NetworkResync = false;
        }

        public override void Update()
        {
        }

        public override void Initialize(params object[] a_initialData)
        {
            PreferredWindowWidth = 18 * 16 + 1;
            PreferredWindowHeight = 36 * 16 + 1;
            UiLayer = new UI(m_graphics);

            m_connection = (ServerConnection)a_initialData[0];
            m_connection.ReceivedPacket += ProcessPacket;
            m_gameID = Convert.ToByte(a_initialData[1]);

            GameObjects.Add(m_shipGrid = new ShipGrid(          new Vector2(16, 20 * 16),   16, 16, 16));
            GameObjects.Add(m_enemyShipGrid = new EnemyShipGrid(new Vector2(16, 32),        16, 16, 16));

            m_shipGrid.OnCellClick += ShipGridOnCellClick;
            m_enemyShipGrid.OnCellClick += EnemyShipGridOnCellClick;
            m_connection.Send(Packet.CreateGameConnect(m_gameID));
        }

        public override void OnSwitchFrom()
        {
            m_connection.Close();
        }

        private void ShipGridOnCellClick(object sender, Cell e)
        {
            if (m_gameState == GameState.ShipPlacement)
                if (m_shipGrid.ShipPreview.m_validPos)
                    m_connection.Send(Packet.CreateShipPlacementPackage(m_gameID, (byte)e.m_xPos, (byte)e.m_yPos, (byte)m_shipGrid.ShipPreview.m_dir.X, (byte)m_shipGrid.ShipPreview.m_dir.Y, (byte)m_shipGrid.ShipPreview.m_length, m_playerID));
        }

        private void EnemyShipGridOnCellClick(object sender, Cell e)
        {
            if(m_gameState == GameState.YourTurn)
                m_connection.Send(Packet.CreateShootPackage(m_gameID, e.m_xPos, e.m_yPos, m_playerID, (byte)(m_playerID == 1 ? 2 : 1)));  // Shoot
        }

        public void OnMouseInput(object sender, MouseStateEventArgs state)
        {
            if (m_gameState == GameState.ShipPlacement)
                m_shipGrid.UpdateShipPreview(state.m_newState.X, state.m_newState.Y, (state.m_newState.ScrollWheelValue - state.m_oldState.ScrollWheelValue) / 120);
        }
        
        public void OnKeyboardInput(object sender, KeyboardStateEventArgs state)
        {
            if (state.m_newState.IsKeyDown(Keys.Space) && m_gameState != GameState.YourTurn)
            {
                m_gameState = GameState.YourTurn;
                m_shipGrid.UpdateShipPreview(-16, -16, 0);
            }
        }
    }
}
