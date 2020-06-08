using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Events.EventListeners
{
    public class MouseStateEventArgs : EventArgs
    {
        public MouseState m_newState;
        public MouseState m_oldState;

        public MouseStateEventArgs(MouseState a_newState, MouseState a_oldState)
        {
            m_newState = a_newState;
            m_oldState = a_oldState;
        }
    }

    /// <summary>
    /// The clickable event listener is an eventlistener which registers and manages all game objects which extend the IClickable event
    /// </summary>
    public class ClickableEventListener : BaseEventListener<IClickableEvent>
    {
        /// <summary>
        /// Initialize the singleton event listener
        /// </summary>
        public static void Initialize()
        {
            if (Instance == null)
                Instance = new ClickableEventListener();
        }

        private ClickableEventListener()
        {

        }

        public void Update(object a_sender, MouseStateEventArgs a_state)
        {
            if (a_state.m_newState.LeftButton == ButtonState.Pressed && a_state.m_oldState.LeftButton != ButtonState.Pressed)
            {
                for(int i = 0; i < m_listeners.Count; i++)
                {
                    IClickableEvent obj = m_listeners[i];
                    if (obj.Bounds.Value.Contains(new Point(a_state.m_newState.X, a_state.m_newState.Y)))
                        obj.BaseOnClick(a_state);
                }  
            }
        }
    }
}
