using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleshipOOP.Engine;
using BattleshipOOP.Engine.Events;
using BattleshipOOP.Engine.Graphics;

using Microsoft.Xna.Framework;

namespace BattleshipOOP
{
    class TestObject : GameObject, IClickable
    {
        public Rectangle? Bounds { get; set; } = null;

        public TestObject(Vector2 a_pos, SpriteResource a_sprite) : base(a_pos, a_sprite)
        {
            Bounds = Sprite.Area;
        }

        public void OnClick()
        {
            
        }
    }
}
