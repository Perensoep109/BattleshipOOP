using Battleship.Engine.Events.EventListeners;
using Battleship.Engine.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Engine
{
    class BaseGame : Game
    {
        public event EventHandler<MouseState> MouseInput;

        private MouseState m_lastMouseState = new MouseState();

        protected GraphicsDeviceManager m_graphics;

        protected Scene m_currentScene;
        protected SceneRenderer m_sceneRenderer;

        public BaseGame()
        {
            m_graphics = new GraphicsDeviceManager(this);
        }

        #region Base class overrides
        protected override void Update(GameTime a_gameTime)
        {
            if (m_lastMouseState != Mouse.GetState())
                OnMouseInput(Mouse.GetState());
            base.Update(a_gameTime);
        }

        protected override void Initialize()
        {
            base.Initialize();
            ClickableListener.Initialize();
            Mouse.WindowHandle = Window.Handle;
        }

        protected override void BeginRun()
        {
            m_sceneRenderer = new SceneRenderer(new SpriteBatch(m_graphics.GraphicsDevice));
            m_currentScene = new TestScene();
            MouseInput += ((ClickableListener)ClickableListener.Instance).Update;
        }

        protected override void Draw(GameTime a_gameTime)
        {
            base.Draw(a_gameTime);
            m_graphics.GraphicsDevice.Clear(Color.Teal);
            m_sceneRenderer.Draw(m_currentScene, m_graphics);
        }
        #endregion

        #region Event Throwing
        private void OnMouseInput(MouseState a_state)
        {
            m_lastMouseState = a_state;
            MouseInput?.Invoke(this, a_state);
        }
        #endregion
    }
}
