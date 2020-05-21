using Battleship.Engine;
using Battleship.Engine.Events;
using Battleship.Engine.Events.EventListeners;
using Battleship.Engine.Scenes;
using BattleshipOOP.Engine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipOOP.Engine.Scenes
{
    abstract class GameScene : BaseScene
    {
        /// <summary>
        /// All gameobjects in this scene
        /// </summary>
        public List<GameObject> GameObjects { get; private set; }
        public UILayer UI { get; set; }

        public GameScene()
        {
            GameObjects = new List<GameObject>();
        }

        public abstract void Update();

        /// <summary>
        /// Add a game object to the scene
        /// Then register the new game object to the event listeners
        /// </summary>
        /// <param name="a_object">The new GameObject</param>
        public void AddGameObject(GameObject a_object)
        {
            if (a_object is IClickable)
                ClickableListener.Instance.Attach((IClickable)a_object);
            GameObjects.Add(a_object);
        }

        /// <summary>
        /// Destroy a specific game object in this scene
        /// </summary>
        /// <param name="a_object">The GameObject to remove</param>
        public void DestroyGameObject(GameObject a_object)
        {
            if (a_object is IClickable)
                ClickableListener.Instance.Detach((IClickable)a_object);
        }

        public override void OnSwitchTo()
        {
            GameObjects.ForEach(
            obj =>
            {
                if (obj is IClickable)
                    if (!ClickableListener.Instance.Contains((IClickable)obj))
                        ClickableListener.Instance.Attach((IClickable)obj);
            });
            if (UI != null)
                UI.Activate();
        }

        public override void OnSwitchFrom()
        {
            GameObjects.ForEach(
            obj =>
            {
                if (obj is IClickable)
                    ClickableListener.Instance.Detach((IClickable)obj);
            });

            if (UI != null)
                UI.Deactivate();
        }
    }
}
