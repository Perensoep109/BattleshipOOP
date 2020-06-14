using Engine.Events.EventListeners;
using Engine.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using Engine.Networking;
using Engine.Scenes;

namespace Engine
{
    public class BaseGame : Game
    {
        public event EventHandler<MouseStateEventArgs> MouseInput;
        public event EventHandler<KeyboardStateEventArgs> KeyboardInput;

        private MouseState m_lastMouseState = new MouseState();
        private KeyboardState m_lastKeyboardState = new KeyboardState();

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
            // Check for event updates
            MouseState newMousestate = Mouse.GetState();

            if (m_lastMouseState != newMousestate)
                OnMouseInput(new MouseStateEventArgs(newMousestate, m_lastMouseState));
            if (m_lastKeyboardState != Keyboard.GetState())
                OnKeyInput(new KeyboardStateEventArgs(Keyboard.GetState(), m_lastKeyboardState));

            // Update the current scene
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
            SceneSwitcher.Initialize(m_graphics);
            SceneSwitcher.Stop();
            ClickableEventListener.Initialize();
            KeyboardEventListener.Initialize();
            Mouse.WindowHandle = Window.Handle;
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            SceneSwitcher.LoadScene("null");
        }

        protected override void BeginRun()
        {
            m_sceneRenderer = new GameSceneRenderer(new SpriteBatch(m_graphics.GraphicsDevice));
            m_uiSceneRenderer = new UIRenderer();
            MouseInput += ((ClickableEventListener)ClickableEventListener.Instance).Update;
            KeyboardInput += ((KeyboardEventListener)KeyboardEventListener.Instance).Update;
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
        private void OnMouseInput(MouseStateEventArgs a_state)
        {
            m_lastMouseState = a_state.m_newState;
            MouseInput?.Invoke(this, a_state);
        }

        private void OnKeyInput(KeyboardStateEventArgs a_state)
        {
            m_lastKeyboardState = a_state.m_newState;
            KeyboardInput?.Invoke(this, a_state);
        }
        #endregion
    }
}
