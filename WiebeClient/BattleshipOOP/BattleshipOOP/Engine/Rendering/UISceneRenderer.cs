using Battleship.Engine;
using Battleship.Engine.Scenes;
using BattleshipOOP.Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipOOP.Engine.Rendering
{
    class UISceneRenderer : BaseSceneRenderer
    {
        public UISceneRenderer(SpriteBatch a_spriteBatch) : base(a_spriteBatch)
        {

        }

        public override void Draw(BaseScene a_scene, GraphicsDeviceManager a_graphics)
        {
            m_spriteBatch.Begin();
            ((UIScene)a_scene).UIComponents.ForEach(component => {
                component.Draw(m_spriteBatch);
            });
            m_spriteBatch.End();
        }
    }
}
