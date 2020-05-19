using Battleship.Engine.Events;
using Battleship.Engine.Events.EventListeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Engine
{
    /// <summary>
    /// A scene is a collection of game objects
    /// </summary>
    class Scene
    {
        /// <summary>
        /// All gameobjects in this scene
        /// </summary>
        public List<GameObject> GameObjects { get; private set; }

        public Scene()
        {
            GameObjects = new List<GameObject>();
        }

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
    }
}
