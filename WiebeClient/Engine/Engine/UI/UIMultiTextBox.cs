using Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Engine.UI
{
    public class UIMultiTextBox : UIElementBase
    {
        public List<string> Text;
        public SpriteFont Font { get; set; }
        int DisplayLines = 3;
        int ScrollPos = 0;
        int LineHeight = 10;

        public UIMultiTextBox(int a_width, int a_height, SpriteFont a_font, GraphicsDevice a_graphicsDevice) : base(a_width, a_height, a_graphicsDevice)
        {
            Font = a_font;
            Text = new List<string>();
            BackColor = Color.White;
        }

        public override void Draw(SpriteBatch a_sprb)
        {
            a_sprb.Draw(m_backTexture, new Rectangle(GridX * Width, GridY * Height, Width, Height), Color.White);
            for(int i = 0; i < DisplayLines; i++)
            {
                if(ScrollPos + i < Text.Count)
                    a_sprb.DrawString(Font, Text[ScrollPos + i], new Vector2(GridX * Width, GridY * Height + i * LineHeight), Color.Black);
            }
        }
    }
}