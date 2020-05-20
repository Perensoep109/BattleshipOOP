using Battleship.Engine.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipOOP.Engine.UI
{
    class UIButton : UIComponent
    {
        public string Text { get; set; }
        public SpriteFont Font { get; set; }

        public UIButton(Vector2 a_position, int a_width, int a_height, string a_text, SpriteFont a_font, GraphicsDevice a_graphics) : base(a_graphics)
        {
            Bounds = new Rectangle((int)a_position.X, (int)a_position.Y, a_width, a_height);
            Pos = a_position;
            Text = a_text;
            BackColor = Color.White;
            Font = a_font;
        }

        public override void Draw(SpriteBatch a_spriteBatch)
        {
            a_spriteBatch.Draw(m_backTexture, new Rectangle((int)Pos.X, (int)Pos.Y, Bounds.Value.Width, Bounds.Value.Height), Color.White);
            a_spriteBatch.DrawString(Font, Text, Pos + new Vector2(5, 5), Color.Black);
        }
    }
}
