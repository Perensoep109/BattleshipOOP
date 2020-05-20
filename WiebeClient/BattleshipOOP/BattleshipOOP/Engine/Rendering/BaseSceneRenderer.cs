using Battleship.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipOOP.Engine.Rendering
{
    abstract class BaseSceneRenderer
    {
        /// <summary>
        /// The 2D spritebatch to use for rendering
        /// </summary>
        protected SpriteBatch m_spriteBatch;

        public BaseSceneRenderer(SpriteBatch a_spriteBatch)
        {
            m_spriteBatch = a_spriteBatch;
        }

        public abstract void Draw(BaseScene a_scene, GraphicsDeviceManager a_graphics);
    }
}
