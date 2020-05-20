using Battleship.Engine;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipOOP.Engine.UI
{
    class UIScene : BaseScene
    {
        /// <summary>
        /// All gameobjects in this scene
        /// </summary>
        public List<UIComponent> UIComponents { get; private set; }

        protected GraphicsDevice m_graphics;

        public UIScene(GraphicsDevice a_graphics)
        {
            UIComponents = new List<UIComponent>();
            m_graphics = a_graphics;
        }
    }
}
