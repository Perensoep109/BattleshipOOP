using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Engine.Events.EventListeners
{
    /// <summary>
    /// The clickable event listener is an eventlistener which registers and manages all game objects which extend the IClickable event
    /// </summary>
    class ClickableListener : BaseEventListener<IClickable>
    {
        /// <summary>
        /// Initialize the singleton event listener
        /// </summary>
        public static void Initialize()
        {
            if (Instance == null)
                Instance = new ClickableListener();
        }

        private ClickableListener()
        {

        }

        public override void Update()
        {
            
        }

        public void Update(object a_sender, MouseState a_state)
        {
            if(a_state.LeftButton == ButtonState.Pressed)
                foreach(IClickable obj in m_listeners)
                    if (obj.Bounds.Value.Contains(new Point(a_state.X, a_state.Y)))
                        obj.OnClick();
        }
    }
}
