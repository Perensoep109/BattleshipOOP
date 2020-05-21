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
    class UIButton : UIElementBase, IClickable
    {
        public event EventHandler<MouseState> OnClick;

        public string Text { get; set; }
        public SpriteFont Font { get; set; }
        public Rectangle? Bounds { get; set; }

        public UIButton(int a_width, int a_height, string a_text, SpriteFont a_font, GraphicsDevice a_graphics) : base(a_graphics)
        {
            Width = a_width;
            Height = a_height;
            Text = a_text;
            Font = a_font;
            BackColor = Color.White;
        }

        public void BaseOnClick(MouseState a_state)
        {
            OnClick?.Invoke(this, a_state);
        }

        public override void Draw(SpriteBatch a_sprb)
        {
            a_sprb.Draw(m_backTexture, new Rectangle(GridX * Width, GridY * Height, Width, Height), Color.White);
            a_sprb.DrawString(Font, Text, new Vector2(GridX * Width, GridY * Height), Color.Black);
        }
    }
}
