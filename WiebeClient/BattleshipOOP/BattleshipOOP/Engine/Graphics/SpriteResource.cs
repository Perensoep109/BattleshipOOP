using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace BattleshipOOP.Engine.Graphics
{
    class SpriteResource
    {
        public Texture2D Sprite { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Rectangle Area { get; private set; }

        public SpriteResource(Texture2D a_sprite)
        {
            Sprite = a_sprite;
            Width = Sprite.Width;
            Height = Sprite.Height;
            Area = new Rectangle(0, 0, Width, Height);
        }
    }
}
