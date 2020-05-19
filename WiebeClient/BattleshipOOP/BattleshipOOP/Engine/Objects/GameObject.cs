using Battleship.Engine.Events;
using Battleship.Engine.Events.EventListeners;
using Battleship.Engine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Engine
{
    /// <summary>
    /// A GameObject is the base class for every game object.
    /// </summary>
    class GameObject : IDisposable
    {
        /// <summary>
        /// The position of this object
        /// </summary>
        public Vector2 Pos { get; protected set; }
        /// <summary>
        /// The sprite of this object
        /// </summary>
        public SpriteResource Sprite { get; protected set; }

        public GameObject(Vector2 a_pos, SpriteResource a_sprite)
        {
            Pos = a_pos;
            Sprite = a_sprite;
        }

        /// <summary>
        /// Draw this object
        /// </summary>
        /// <param name="a_sprBatch">The spritebatch to use for drawing this object (sprite)</param>
        public void Draw(SpriteBatch a_sprBatch)
        {
            a_sprBatch.Draw(Sprite.Sprite, Pos, Color.White);
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
