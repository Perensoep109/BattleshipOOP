using Battleship.Engine.Events;
using Battleship.Engine.Events.EventListeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Engine.Scenes
{
    abstract class BaseScene
    {
        protected abstract void OnSwitchTo();
        protected abstract void OnSwitchFrom();
    }
}
