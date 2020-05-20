using Battleship.MainGame;
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
using BattleshipOOP.Engine.Networking;
using BattleshipOOP.Engine.Rendering;
using BattleshipOOP.Engine;
using BattleshipOOP.Engine.UI;
using Battleship.Engine.Scenes;
using BattleshipOOP.Engine.Scenes;

namespace Battleship.Engine
{
    class BaseGame : Game
    {
        public event EventHandler<MouseState> MouseInput;

        private MouseState m_lastMouseState = new MouseState();

        protected GraphicsDeviceManager m_graphics;

        protected BaseScene m_currentScene;
        protected GameSceneRenderer m_sceneRenderer;
        protected UISceneRenderer m_uiSceneRenderer;

        public BaseGame()
        {
            m_graphics = new GraphicsDeviceManager(this);
        }

        #region Base class overrides
        protected override void Update(GameTime a_gameTime)
        {
            if (m_lastMouseState.LeftButton != Mouse.GetState().LeftButton)
                OnMouseInput(Mouse.GetState());
            if(m_currentScene != null)
            {
                // Check for network synchronization before updating
                if (m_currentScene is INetworkScene)
                {
                    INetworkScene scene = ((INetworkScene)m_currentScene);

                    if (scene.NetworkResync)
                        scene.Sync();
                }
                if(m_currentScene is GameScene)
                    ((GameScene)m_currentScene).Update();
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
            m_uiSceneRenderer = new UISceneRenderer(new SpriteBatch(m_graphics.GraphicsDevice));
            MouseInput += ((ClickableListener)ClickableListener.Instance).Update;
        }

        protected override void Draw(GameTime a_gameTime)
        {
            base.Draw(a_gameTime);
            m_graphics.GraphicsDevice.Clear(Color.Teal);
            if (m_currentScene != null)
            {
                if (m_currentScene is GameScene)
                    m_sceneRenderer.Draw((GameScene)m_currentScene, m_graphics);
                if (m_currentScene is UIScene)
                    m_uiSceneRenderer.Draw((UIScene)m_currentScene, m_graphics);
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
