using Engine.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Scenes
{
    public class SceneSwitcher
    {
        public static SceneSwitcher Instance { get; private set; }
        public Dictionary<string, BaseScene> Scenes { get; private set; }
        public string CurrentSceneKey { get; private set; }
        public BaseScene CurrentScene { get; private set; }

        static SceneSwitcher()
        {
            Instance = new SceneSwitcher();
        }
        private SceneSwitcher()
        {
            Scenes = new Dictionary<string, BaseScene>();
        }

        public static void AddScene(BaseScene a_scene, string a_name)
        {
            if(!Instance.Scenes.ContainsKey(a_name))
                Instance.Scenes.Add(a_name, a_scene);
        }

        public static void DeleteScene(string a_name)
        {
            Instance.Scenes.Remove(a_name);
        }

        public static BaseScene GetScene(string a_name)
        {
            BaseScene scene;
            if (!Instance.Scenes.TryGetValue(a_name, out scene))
                throw new KeyNotFoundException("A value with key " + a_name + " was not found in the  list of scenes");
            return scene;
        }

        public static void LoadScene(string a_name)
        {
            if (Instance.CurrentScene != null)
                Instance.CurrentScene.OnSwitchFrom();
            try
            {
                BaseScene newScene = GetScene(a_name);
                newScene.OnSwitchTo();
                Instance.CurrentScene = newScene;
            }
            catch (KeyNotFoundException a_e)
            {
                Console.WriteLine(a_e);
            }
        }
    }
}
