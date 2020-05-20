using Battleship.Engine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Battleship.Engine;
using BattleshipOOP.Game.Scenes;
using BattleshipOOP.Engine.Graphics;

namespace Battleship.MainGame
{
    class BattleshipGame : BaseGame
    {
        protected override void BeginRun()
        {
            base.BeginRun();
            m_currentScene = new MainMenuScene(m_graphics.GraphicsDevice);
        }

        protected override void LoadContent()
        {
            ResourcePool.LoadResource(new SpriteFontResource(Content.Load<SpriteFont>("Content/Fonts/Font")), "font");
        }
    }
}
