using BattleshipOOP.Engine.Events;
using BattleshipOOP.Engine.Events.EventObservers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipOOP.Engine
{
    class Scene
    {
        public List<GameObject> GameObjects { get; private set; }

        public Scene()
        {
            GameObjects = new List<GameObject>();
        }

        public void AddGameObject(GameObject a_object)
        {
            if (a_object is IClickable) 
                ClickableListener.Instance.Attach((IClickable)a_object);
            GameObjects.Add(a_object);
        }

        public void DestroyGameObject(GameObject a_object)
        {
            if (a_object is IClickable)
                ClickableListener.Instance.Detach((IClickable)a_object);
        }
    }
}
