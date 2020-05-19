using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Engine.Events
{
    /// <summary>
    /// IClickable is an interface which manages clicking on an object.
    /// The attached EventListener is ClickableEventListener
    /// </summary>
    interface IClickable : IBaseEvent
    {
        /// <summary>
        /// The bounds of the clickable area
        /// </summary>
        Rectangle? Bounds { get; set; }
        /// <summary>
        /// The method to execute when this object is clicked
        /// </summary>
        void OnClick(MouseState a_state);
    }
}
