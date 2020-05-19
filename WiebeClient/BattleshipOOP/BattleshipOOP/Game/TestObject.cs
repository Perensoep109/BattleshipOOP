using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Battleship.Engine;
using Battleship.Engine.Events;
using Battleship.Engine.Graphics;
using BattleshipOOP.Engine.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Battleship.MainGame
{
    class TestObject : GameObject, IClickable, IRenderable
    {
        public Rectangle? Bounds { get; set; } = null;
        public SpriteResource Sprite { get; set; }

        public TestObject(Vector2 a_pos, SpriteResource a_sprite) : base(a_pos)
        {
            Sprite = a_sprite;
            Bounds = Sprite.Area;
        }

        public void OnClick(MouseState a_state)
        {
            Console.WriteLine(a_state.LeftButton);
        }

        public void Draw(SpriteBatch a_sprBatch)
        {
            a_sprBatch.Draw(Sprite.Sprite, Pos);
        }
    }
}
