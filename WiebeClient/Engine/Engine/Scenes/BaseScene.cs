using Engine.Events;
using Engine.Events.EventListeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Scenes
{
    abstract public class BaseScene : IDisposable
    {
        public bool Initialized { get; protected set; }

        public abstract void OnSwitchTo();
        public abstract void OnSwitchFrom();

        public abstract void Initialize();

        public void Dispose()
        {
            OnSwitchFrom();
        }
    }
}
