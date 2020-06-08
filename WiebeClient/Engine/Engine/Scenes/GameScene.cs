using Engine;
using Engine.Events;
using Engine.Events.EventListeners;
using Engine.Scenes;
using Engine.UI;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Scenes
{
    abstract public class GameScene : BaseScene
    {
        /// <summary>
        /// All gameobjects in this scene
        /// </summary>
        public List<GameObject> GameObjects { get; private set; }
        public UILayer UI { get; set; }

        public GameScene(GraphicsDevice a_graphics) : base(a_graphics)
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
            if (a_object is IClickableEvent)
                ClickableEventListener.Instance.Attach((IClickableEvent)a_object);
            GameObjects.Add(a_object);
        }

        /// <summary>
        /// Destroy a specific game object in this scene
        /// </summary>
        /// <param name="a_object">The GameObject to remove</param>
        public void DestroyGameObject(GameObject a_object)
        {
            if (a_object is IClickableEvent)
                ClickableEventListener.Instance.Detach((IClickableEvent)a_object);
        }

        public override void OnSwitchTo()
        {
            if (!Initialized)
                Initialize();

            GameObjects.ForEach(
            obj =>
            {
                if (obj is IClickableEvent)
                    if (!ClickableEventListener.Instance.Contains((IClickableEvent)obj))
                        ClickableEventListener.Instance.Attach((IClickableEvent)obj);
            });
            if (UI != null)
                UI.Activate();
        }

        public override void OnSwitchFrom()
        {
            GameObjects.ForEach(
            obj =>
            {
                if (obj is IClickableEvent)
                    ClickableEventListener.Instance.Detach((IClickableEvent)obj);
            });

            if (UI != null)
                UI.Deactivate();
        }
    }
}
