using Engine.Events;
using Engine.Events.EventListeners;
using Engine.Graphics;
using Engine.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    /// <summary>
    /// A GameObject is the base public class for every game object.
    /// </summary>
    abstract public class GameObject : IDisposable
    {
        public Vector2 m_pos;

        public GameObject(Vector2 a_pos)
        {
            m_pos = a_pos;
        }

        /// <summary>
        /// Delete this object
        /// Unsubscribe from all the event listeners that use this object
        /// </summary>
        public void Dispose()
        {
            if (this is IClickable)
                ClickableListener.Instance.Detach((IClickable)this);
        }
    }
}
