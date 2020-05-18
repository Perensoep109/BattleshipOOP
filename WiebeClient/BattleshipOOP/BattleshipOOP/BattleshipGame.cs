using BattleshipOOP.Engine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Input;

namespace BattleshipOOP
{
    class BattleshipGame : Game
    {
        GraphicsDeviceManager m_graphics;
        SpriteBatch m_spriteBatch;
        TestObject m_obj;

        public BattleshipGame()
        {
            m_graphics = new GraphicsDeviceManager(this);
        }

        protected override void BeginRun()
        {
            m_spriteBatch = new SpriteBatch(m_graphics.GraphicsDevice);
            m_obj = new TestObject(new Vector2(0, 0), ResourcePool.GetSprite("love"));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void Draw(GameTime gameTime)
        {
            m_graphics.GraphicsDevice.Clear(Color.Teal);
            m_spriteBatch.Begin();
            m_obj.Draw(m_spriteBatch);
            m_spriteBatch.End();
            base.Draw(gameTime);            
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            ResourcePool.LoadSprite(Texture2D.FromStream(m_graphics.GraphicsDevice, File.OpenRead("love.png")), "love");
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
