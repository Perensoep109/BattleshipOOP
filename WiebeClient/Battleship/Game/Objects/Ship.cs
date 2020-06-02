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

namespace Battleship.Game.Objects
{
    class Ship : GameObject, IRenderable
    {
        public Ship(Vector2 a_pos) : base(a_pos)
        {
        }

        public SpriteResource Sprite { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Draw(SpriteBatch a_sprBatch)
        {
            
        }
    }
}
