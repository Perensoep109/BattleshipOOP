using BattleshipOOP.Engine.Events;
using BattleshipOOP.Engine.Events.EventListeners;
using BattleshipOOP.Engine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipOOP.Engine
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
