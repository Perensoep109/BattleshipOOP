using Engine.Events;
using Engine.Events.EventListeners;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.UI
{
    abstract public class UIElementBase : IDisposable
    {
        public int GridX { get; set; }
        public int GridY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color BackColor { get { return m_backColor; } set => SetBackColor(value); }

        private Color m_backColor = Color.White;

        protected Texture2D m_backTexture;
        private GraphicsDevice m_graphicsDevice;

        public UIElementBase(int a_width, int a_height, GraphicsDevice a_graphicsDevice)
        {
            m_graphicsDevice = a_graphicsDevice;
            Width = a_width;
            Height = a_height;
        }

        public void SetBackColor(Color a_value)
        {
            m_backTexture = new Texture2D(m_graphicsDevice, 1, 1);
            m_backTexture.SetData(new Color[] { a_value });
        }

        public void Dispose()
        {
            if (this is IClickableEvent)
                ClickableEventListener.Instance.Detach((IClickableEvent)this);
        }

        public abstract void Draw(SpriteBatch a_sprb);
    }
}
