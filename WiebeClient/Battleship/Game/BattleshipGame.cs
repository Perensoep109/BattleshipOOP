using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Battleship.Game.Scenes;
using Engine;
using Engine.Graphics;
using Engine.Scenes;
using Battleship.Game;
using Engine.Networking;
using System.Net;

namespace Battleship
{
    class BattleshipGame : BaseGame
    {
        private string m_gameID;

        public BattleshipGame(string a_gameID)
        {
            m_gameID = a_gameID;
        }

        protected override void BeginRun()
        {
            // Connect to the server
            ServerConnection con = new ServerConnection();
            con.Connect(IPAddress.Parse("127.0.0.1"), 69);

            base.BeginRun();
            SceneSwitcher.AddScene(new MultiplayerGameScene(m_graphics.GraphicsDevice), "GameScene");
            SceneSwitcher.LoadScene("GameScene", con);
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
