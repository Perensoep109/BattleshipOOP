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
    class GameObject : IDisposable
    {
        public Vector2 Pos { get; protected set; }
        public SpriteResource Sprite { get; protected set; }

        public GameObject(Vector2 a_pos, SpriteResource a_sprite)
        {
            Pos = a_pos;
            Sprite = a_sprite;
        }

        public void Draw(SpriteBatch a_sprBatch)
        {
            a_sprBatch.Draw(Sprite.Sprite, Pos, Color.White);
        }

        public void Dispose()
        {
            if (this is IClickable)
                ClickableListener.Instance.Detach((IClickable)this);
        }
    }
}
