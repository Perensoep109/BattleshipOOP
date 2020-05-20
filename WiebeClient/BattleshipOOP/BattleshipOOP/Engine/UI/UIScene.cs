using Battleship.Engine;
using Battleship.Engine.Events.EventListeners;
using Battleship.Engine.Scenes;
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

        public void AddElement(UIComponent a_component)
        {
            ClickableListener.Instance.Attach(a_component);
            UIComponents.Add(a_component);
        }

        protected override void OnSwitchTo()
        {
            throw new NotImplementedException();
        }

        protected override void OnSwitchFrom()
        {
            throw new NotImplementedException();
        }
    }
}
