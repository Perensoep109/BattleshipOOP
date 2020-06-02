using Engine.Events;
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
        event EventHandler<KeyboardState> OnKeyInput;
        void BaseOnKeyboard(KeyboardState a_state);
    }
}
