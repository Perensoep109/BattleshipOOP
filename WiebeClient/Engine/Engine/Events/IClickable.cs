using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Events
{
    /// <summary>
    /// IClickable is an public interface which manages clicking on an object.
    /// The attached EventListener is ClickableEventListener
    /// </summary>
    public interface IClickable : IBaseEvent
    {
        /// <summary>
        /// The bounds of the clickable area
        /// </summary>
        Rectangle? Bounds { get; set; }

        void BaseOnClick(MouseState a_state);

        event EventHandler<MouseState> OnClick;
    }
}
