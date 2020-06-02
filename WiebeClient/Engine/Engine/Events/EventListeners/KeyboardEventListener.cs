using Engine.Engine.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Events.EventListeners
{
    /// <summary>
    /// The clickable event listener is an eventlistener which registers and manages all game objects which extend the IClickable event
    /// </summary>
    public class KeyboardEventListener : BaseEventListener<IKeyboardEvent>
    {
        /// <summary>
        /// Initialize the singleton event listener
        /// </summary>
        public static void Initialize()
        {
            if (Instance == null)
                Instance = new KeyboardEventListener();
        }

        private KeyboardEventListener()
        {

        }

        public void Update(object a_sender, KeyboardState a_state)
        {
            for(int i = 0; i < m_listeners.Count; i++)
            {
                IKeyboardEvent obj = m_listeners[i];
                obj.BaseOnKeyboard(a_state);
            }  
        }
    }
}
