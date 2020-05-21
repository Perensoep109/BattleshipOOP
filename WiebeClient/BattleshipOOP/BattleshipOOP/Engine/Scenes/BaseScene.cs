using Battleship.Engine.Events;
using Battleship.Engine.Events.EventListeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Engine.Scenes
{
    abstract class BaseScene : IDisposable
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
