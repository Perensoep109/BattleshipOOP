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

        public UIButton(Vector2 a_position, string a_text, GraphicsDevice a_graphics) : base(a_graphics)
        {
            Pos = a_position;
            Text = a_text;
        }

        public override void Draw(SpriteBatch a_spriteBatch)
        {
            a_spriteBatch.Draw(m_backTexture, Pos, Bounds);
            a_spriteBatch.DrawString(Font, Text, Pos + new Vector2(5, 5), Color.Black);
        }
    }
}
