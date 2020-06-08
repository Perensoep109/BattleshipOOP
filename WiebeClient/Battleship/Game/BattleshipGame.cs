using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Battleship.Game.Scenes;
using Engine;
using Engine.Graphics;
using Engine.Scenes;

namespace Battleship
{
    class BattleshipGame : BaseGame
    {
        protected override void BeginRun()
        {
            base.BeginRun();
            SceneSwitcher.AddScene(new MainMenuScene(m_graphics.GraphicsDevice), "MainMenu");
            SceneSwitcher.AddScene(new MultiplayerGameScene(m_graphics.GraphicsDevice), "GameScene");
            SceneSwitcher.LoadScene("MainMenu");
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
