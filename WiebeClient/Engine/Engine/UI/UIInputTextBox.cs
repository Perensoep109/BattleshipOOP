using Engine.Engine.Events;
using Engine.Events;
using Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Engine.UI
{
    public class UIInputTextBox : UIElementBase, IKeyboardEvent, IClickableEvent
    {
        public event EventHandler<KeyboardState> OnKeyInput;
        public event EventHandler<MouseState> OnClick;

        public string Text { get; set; } = "";
        public Rectangle? Bounds { get; set; }
        public bool Selected { get; private set; }
        public SpriteFont Font { get; private set; }

        public UIInputTextBox(int a_width, int a_height, SpriteFont a_font, GraphicsDevice a_graphicsDevice) : base(a_width, a_height, a_graphicsDevice)
        {
            Font = a_font;
            BackColor = Color.Gray;
        }

        public override void Draw(SpriteBatch a_sprb)
        {
            a_sprb.Draw(m_backTexture, new Rectangle(GridX * Width, GridY * Height, Width, Height), Color.White);
            a_sprb.DrawString(Font, Text, new Vector2(GridX * Width + 5, GridY * Height + 5), Color.Black);
        }

        public void BaseOnClick(MouseState a_state)
        {
            Selected = !Selected;
        }

        public void BaseOnKeyboard(KeyboardState a_state)
        {
            if(Selected)
            {
                if (a_state.GetPressedKeys().Length == 0)
                    return;

                char ch = (char)a_state.GetPressedKeys()[0];
                if (ch >= 33 && ch <= 126)
                    Text += ch;

                if (a_state.IsKeyDown(Keys.Back))
                    if(Text.Length > 0)
                        Text = Text.Remove(Text.Length - 1);
            }
        }
    }
}
