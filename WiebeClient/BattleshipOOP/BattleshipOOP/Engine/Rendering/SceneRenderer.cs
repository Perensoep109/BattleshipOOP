using BattleshipOOP.Engine.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Engine.Rendering
{
    /// <summary>
    /// The scene renderer is a system to render a specific scene to the screen (main framebuffer primarily)
    /// </summary>
    class SceneRenderer
    {
        /// <summary>
        /// The 2D spritebatch to use for rendering
        /// </summary>
        private SpriteBatch m_spriteBatch;
        
        public SceneRenderer(SpriteBatch a_spriteBatch)
        {
            m_spriteBatch = a_spriteBatch;
        }

        /// <summary>
        /// Draw a scene to the main framebuffer
        /// </summary>
        /// <param name="a_scene">The scene to draw</param>
        /// <param name="a_graphics">The graphics device to use for drawing</param>
        public void Draw(Scene a_scene, GraphicsDeviceManager a_graphics)
        {
            m_spriteBatch.Begin();
            a_scene.GameObjects.ForEach(obj => { 
                if (obj is IRenderable) 
                    ((IRenderable)obj).Draw(m_spriteBatch);
            });
            m_spriteBatch.End();
        }
    }
}
