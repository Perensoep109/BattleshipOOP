using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipOOP.Engine.UI
{
    class UILabel : UIElementBase
    {
        public string Text { get; set; }
        public SpriteFont Font { get; set; }

        public UILabel(int a_width, int a_height, string a_text, SpriteFont a_font, GraphicsDevice a_graphicsDevice) : base(a_width, a_height, a_graphicsDevice)
        {
            Font = a_font;
            Text = a_text;
        }

        public override void Draw(SpriteBatch a_sprb)
        {
            if(m_backTexture != null)
                a_sprb.Draw(m_backTexture, new Rectangle(GridX * Width, GridY * Height, Width, Height), Color.White);
            a_sprb.DrawString(Font, Text, new Vector2(GridX * Width, GridY * Height), Color.Black);
        }
    }
}
