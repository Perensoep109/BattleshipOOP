using Engine;
using Engine.Graphics;
using Engine.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game
{
    class TestGameObject : GameObject, IRenderable
    {
        public SpriteResource Sprite { get; set; }

        public TestGameObject(Vector2 a_pos) : base(a_pos)
        {
            Sprite = ResourcePool.GetSprite("tile");
        }


        public void Draw(SpriteBatch a_sprBatch)
        {
            a_sprBatch.Draw(Sprite.Sprite, m_pos);
        }
    }
}
