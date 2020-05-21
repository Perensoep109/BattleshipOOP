using Engine.Scenes;
using Engine;
using Engine.Objects;
using Engine.Rendering;
using Engine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Rendering
{
    /// <summary>
    /// The scene renderer is a system to render a specific scene to the screen (main framebuffer primarily)
    /// </summary>
    public class GameSceneRenderer : BaseSceneRenderer
    {
        UIRenderer m_uiRenderer;

        public GameSceneRenderer(SpriteBatch a_spriteBatch) : base(a_spriteBatch)
        {
            m_uiRenderer = new UIRenderer();
        }

        public override void Draw(BaseScene a_scene, GraphicsDeviceManager a_graphics)
        {
            GameScene scene = (GameScene)a_scene;
            m_spriteBatch.Begin();
            scene.GameObjects.ForEach(obj => {
                if (obj is IRenderable)
                    ((IRenderable)obj).Draw(m_spriteBatch);
            });
            if (scene.UI != null)
                m_uiRenderer.Draw(scene.UI, m_spriteBatch);
            m_spriteBatch.End();
        }
    }
}
