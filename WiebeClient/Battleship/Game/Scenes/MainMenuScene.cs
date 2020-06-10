using Engine.Events.EventListeners;
using Engine.Graphics;
using Engine.Networking;
using Engine.Scenes;
using Engine.UI;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game.Scenes
{
    class MainMenuScene : GameScene
    {
        private class UI : UILayer
        {
            public ServerConnection m_con;

            UIButton btnConnect;

            public UI(GraphicsDevice a_graphicsDevice)
            {
                AddUI(btnConnect = new UIButton(130, 30, "Connect to the server", ResourcePool.GetSpriteFont("font").Font, a_graphicsDevice), 0, 1);
                AddUI(new UILabel(130, 30, "Multiplayer battleship", ResourcePool.GetSpriteFont("font").Font, a_graphicsDevice), 0, 0);

                btnConnect.OnClick += MainMenuUI_OnClick;
            }

            private void MainMenuUI_OnClick(object sender, MouseStateEventArgs e)
            {
                m_con = new ServerConnection();
                m_con = new ServerConnection();
                m_con.Connect(IPAddress.Parse("127.0.0.1"), 69);
            }
        }

        public MainMenuScene(GraphicsDevice a_graphicsDevice) : base(a_graphicsDevice) 
        {
            base.UiLayer = new UI(m_graphics);
        }

        public override void Initialize(params object[] a_initialData)
        {
            
        }

        public override void Update()
        {
            ServerConnection con = ((UI)base.UiLayer).m_con;

            if (con != null && con.Connected)
                SceneSwitcher.LoadScene("GameScene", con);
        }
    }
}
