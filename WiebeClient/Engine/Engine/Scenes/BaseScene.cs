using Engine.Events;
using Engine.Events.EventListeners;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Scenes
{
    abstract public class BaseScene : IDisposable
    {
        public bool Initialized { get; protected set; }
        public int PreferredWindowWidth { get; set; } = 640;
        public int PreferredWindowHeight { get; set; } = 480;

        protected GraphicsDevice m_graphics;

        public abstract void OnSwitchTo(params object[] a_data);
        public abstract void OnSwitchFrom();
        public abstract void Initialize(params object[] a_initialData);

        public BaseScene(GraphicsDevice a_device)
        {
            m_graphics = a_device;
        }

        public void Dispose()
        {
            OnSwitchFrom();
        }
    }
}
