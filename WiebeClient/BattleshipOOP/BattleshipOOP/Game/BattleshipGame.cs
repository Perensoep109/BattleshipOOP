using Battleship.Engine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Battleship.Engine;
using BattleshipOOP.Game.Scenes;
using BattleshipOOP.Engine.Graphics;
using BattleshipOOP.Engine.Scenes;

namespace Battleship.MainGame
{
    class BattleshipGame : BaseGame
    {
        protected override void BeginRun()
        {
            base.BeginRun();
            SceneSwitcher.AddScene(new MainMenuScene(m_graphics.GraphicsDevice), "MainMenu");
            SceneSwitcher.AddScene(new MultiplayerScene(), "GameScene");
            SceneSwitcher.LoadScene("MainMenu");
        }

        protected override void LoadContent()
        {
            ResourcePool.LoadResource(new SpriteFontResource(Content.Load<SpriteFont>("Content/Fonts/Font")), "font");
        }
    }
}
