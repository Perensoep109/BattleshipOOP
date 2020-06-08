using Engine.Events;
using Engine.Events.EventListeners;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Engine.Events
{
    public interface IKeyboardEvent : IBaseEvent
    {
        event EventHandler<KeyboardStateEventArgs> OnKeyInput;
        void BaseOnKeyboard(KeyboardStateEventArgs a_state);
    }
}
