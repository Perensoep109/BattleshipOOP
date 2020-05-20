using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipOOP.Engine.Graphics
{
    class SpriteFontResource : BaseResource
    {
        public SpriteFont Font { get; set; }

        public SpriteFontResource(SpriteFont a_font)
        {
            Font = a_font;
        }
    }
}
