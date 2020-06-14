using Microsoft.Xna.Framework.Graphics;
using Battleship.Game.Scenes;
using Engine;
using Engine.Graphics;
using Engine.Scenes;
using Engine.Networking;
using System.Net;

namespace Battleship
{
    class BattleshipGame : BaseGame
    {
        private string m_gameID;
        private string m_ip;
        private int m_port;

        public BattleshipGame(string a_ip, int a_port, string a_gameID)
        {
            m_ip = a_ip;
            m_port = a_port;
            m_gameID = a_gameID;
        }

        protected override void BeginRun()
        {
            // Connect to the server
            ServerConnection con = new ServerConnection();
            con.Connect(IPAddress.Parse(m_ip), m_port);

            base.BeginRun();
            SceneSwitcher.AddScene(new MultiplayerGameScene(m_graphics.GraphicsDevice), "GameScene");
            SceneSwitcher.LoadScene("GameScene", con, m_gameID);
            IsMouseVisible = true;
            MouseInput += ((MultiplayerGameScene)SceneSwitcher.GetScene("GameScene")).OnMouseInput;
            KeyboardInput += ((MultiplayerGameScene)SceneSwitcher.GetScene("GameScene")).OnKeyboardInput;
        }

        protected override void LoadContent()
        {
            ResourcePool.LoadResource(new SpriteFontResource(Content.Load<SpriteFont>("Content/Fonts/Font")), "font");
            ResourcePool.LoadResource(new SpriteResource(Content.Load<Texture2D>("Content/Sprites/tile")), "tile");
            ResourcePool.LoadResource(new SpriteResource(Content.Load<Texture2D>("Content/Sprites/line")), "line");
            ResourcePool.LoadResource(new SpriteResource(Content.Load<Texture2D>("Content/Sprites/hit")), "hit");
            ResourcePool.LoadResource(new SpriteResource(Content.Load<Texture2D>("Content/Sprites/miss")), "miss");
            ResourcePool.LoadResource(new SpriteResource(Content.Load<Texture2D>("Content/Sprites/ship")), "ship");
            ResourcePool.LoadResource(new SpriteResource(Content.Load<Texture2D>("Content/Sprites/valid")), "valid");
            ResourcePool.LoadResource(new SpriteResource(Content.Load<Texture2D>("Content/Sprites/invalid")), "invalid");
        }
    }
}
