using Engine.Events.EventListeners;
using Engine.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Networking;
using Engine.Rendering;
using Engine;
using Engine.UI;
using Engine.Scenes;
using Engine.Scenes;

namespace Engine
{
    public class BaseGame : Game
    {
        public event EventHandler<MouseState> MouseInput;

        private MouseState m_lastMouseState = new MouseState();

        protected GraphicsDeviceManager m_graphics;

        protected GameSceneRenderer m_sceneRenderer;
        protected UIRenderer m_uiSceneRenderer;

        public BaseGame()
        {
            m_graphics = new GraphicsDeviceManager(this);
        }

        #region Base public class overrides
        protected override void Update(GameTime a_gameTime)
        {
            if (m_lastMouseState.LeftButton != Mouse.GetState().LeftButton)
                OnMouseInput(Mouse.GetState());
            BaseScene çurScene = SceneSwitcher.Instance.CurrentScene;
            if (çurScene != null)
            {
                // Check for network synchronization before updating
                if (çurScene is INetworkScene)
                {
                    INetworkScene scene = ((INetworkScene)SceneSwitcher.Instance.CurrentScene);

                    if (scene.NetworkResync)
                        scene.Sync();
                }
                if(çurScene is GameScene)
                    ((GameScene)çurScene).Update();
            }
            
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
            m_sceneRenderer = new GameSceneRenderer(new SpriteBatch(m_graphics.GraphicsDevice));
            m_uiSceneRenderer = new UIRenderer();
            MouseInput += ((ClickableListener)ClickableListener.Instance).Update;
        }

        protected override void Draw(GameTime a_gameTime)
        {
            base.Draw(a_gameTime);
            m_graphics.GraphicsDevice.Clear(Color.Teal);
            BaseScene çurScene = SceneSwitcher.Instance.CurrentScene;
            if (çurScene != null)
            {
                if (çurScene is GameScene)
                    m_sceneRenderer.Draw((GameScene)çurScene, m_graphics);
            }
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
