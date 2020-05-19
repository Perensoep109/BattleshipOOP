using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Battleship.Engine;
using Battleship.Engine.Events;
using Battleship.Engine.Graphics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Battleship.MainGame
{
    class TestObject : GameObject, IClickable
    {
        public Rectangle? Bounds { get; set; } = null;

        public TestObject(Vector2 a_pos, SpriteResource a_sprite) : base(a_pos, a_sprite)
        {
            Bounds = Sprite.Area;
        }

        public void OnClick(MouseState a_state)
        {
            Console.WriteLine(a_state.LeftButton);
        }
    }
}
