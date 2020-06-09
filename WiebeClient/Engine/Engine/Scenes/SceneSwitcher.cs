using Engine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Scenes
{
    public class SceneSwitcher
    {
        public bool Initialized { get; private set; }
        public static SceneSwitcher Instance { get; private set; }
        public Dictionary<string, BaseScene> Scenes { get; private set; }
        public string CurrentSceneKey { get; private set; }
        public BaseScene CurrentScene { get; private set; }

        private GraphicsDeviceManager m_windowHost;

        static SceneSwitcher()
        {
            Instance = new SceneSwitcher();
        }
        private SceneSwitcher()
        {
            Scenes = new Dictionary<string, BaseScene>();
        }

        public static void Initialize(GraphicsDeviceManager a_graphics)
        {
            Instance.m_windowHost = a_graphics;
            Instance.Initialized = true;
        }

        public static void AddScene(BaseScene a_scene, string a_name)
        {
            if (!Instance.Initialized)
                throw new Exception("Singleton instance not initialized");

            if(!Instance.Scenes.ContainsKey(a_name))
                Instance.Scenes.Add(a_name, a_scene);
        }

        public static void DeleteScene(string a_name)
        {
            if (!Instance.Initialized)
                throw new Exception("Singleton instance not initialized");
            Instance.Scenes.Remove(a_name);
        }

        public static BaseScene GetScene(string a_name)
        {
            if (!Instance.Initialized)
                throw new Exception("Singleton instance not initialized");
            BaseScene scene;
            if (!Instance.Scenes.TryGetValue(a_name, out scene))
                throw new KeyNotFoundException("A value with key " + a_name + " was not found in the  list of scenes");
            return scene;
        }

        public static void LoadScene(string a_name, params object[] a_data)
        {
            if (!Instance.Initialized)
                throw new Exception("Singleton instance not initialized");

            if (Instance.CurrentScene != null)
                Instance.CurrentScene.OnSwitchFrom();
            try
            {
                BaseScene newScene = GetScene(a_name);
                newScene.OnSwitchTo(a_data);
                Instance.m_windowHost.PreferredBackBufferWidth = newScene.PreferredWindowWidth;
                Instance.m_windowHost.PreferredBackBufferHeight = newScene.PreferredWindowHeight;
                Instance.m_windowHost.ApplyChanges();
                Instance.CurrentScene = newScene;
            }
            catch (KeyNotFoundException a_e)
            {
                Console.WriteLine("ERROR::SCENE::SWITCHER " + a_e);
            }
        }
    }
}
