using Battleship.Engine.Events;
using Battleship.Engine.Events.EventListeners;
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
    abstract class UIComponent : IClickable, IDisposable
    {
        public Rectangle? Bounds { get; set; }
        public Vector2 Pos { get; set; }
        public Color BackColor { get { return BackColor; } set => SetBackColor(value); }
        public event EventHandler<MouseState> OnClick;

        protected GraphicsDevice m_graphicsDevice;
        protected Texture2D m_backTexture;

        public UIComponent(GraphicsDevice a_graphics)
        {
            m_graphicsDevice = a_graphics;
        }

        public void SetBackColor(Color a_value)
        {
            m_backTexture = new Texture2D(m_graphicsDevice, 1, 1);
            m_backTexture.SetData(new Color[] { a_value });
        }

        public void Dispose()
        {
            ClickableListener.Instance.Detach(this);
        }

        public void BaseOnClick(MouseState a_state)
        {
            OnClick?.Invoke(this, a_state);
        }

        public abstract void Draw(SpriteBatch a_spriteBatch);
    }
}
