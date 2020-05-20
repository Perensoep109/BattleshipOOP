using BattleshipOOP.Engine;
using BattleshipOOP.Engine.Objects;
using BattleshipOOP.Engine.Rendering;
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
    class GameSceneRenderer : BaseSceneRenderer
    {
        public GameSceneRenderer(SpriteBatch a_spriteBatch) : base(a_spriteBatch)
        {
            
        }

        public override void Draw(BaseScene a_scene, GraphicsDeviceManager a_graphics)
        {
            GameScene scene = (GameScene)a_scene;
            m_spriteBatch.Begin();
            scene.GameObjects.ForEach(obj => {
                if (obj is IRenderable)
                    ((IRenderable)obj).Draw(m_spriteBatch);
            });
            m_spriteBatch.End();
        }
    }
}
