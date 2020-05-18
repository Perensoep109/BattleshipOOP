using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Engine.Rendering
{
    class SceneRenderer
    {
        private SpriteBatch m_spriteBatch;
        
        public SceneRenderer(SpriteBatch a_spriteBatch)
        {
            m_spriteBatch = a_spriteBatch;
        }

        public void Draw(Scene a_scene, GraphicsDeviceManager a_graphics)
        {
            m_spriteBatch.Begin();
            a_scene.GameObjects.ForEach(obj => { obj.Draw(m_spriteBatch); });
            m_spriteBatch.End();
        }
    }
}
