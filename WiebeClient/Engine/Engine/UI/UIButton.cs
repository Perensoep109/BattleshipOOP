using Engine.Events;
using Engine.Events.EventListeners;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.UI
{
    public class UIButton : UIElementBase, IClickableEvent
    {
        public event EventHandler<MouseStateEventArgs> OnClick;

        public string Text { get; set; }
        public SpriteFont Font { get; set; }
        public Rectangle? Bounds { get; set; }

        public UIButton(int a_width, int a_height, string a_text, SpriteFont a_font, GraphicsDevice a_graphics) : base(a_width, a_height, a_graphics)
        {
            Text = a_text;
            Font = a_font;
            BackColor = Color.White;
        }

        public void BaseOnClick(MouseStateEventArgs a_state)
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
