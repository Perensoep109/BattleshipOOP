using Microsoft.Xna.Framework;
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
        Rectangle? Bounds { get; set; }
        void OnClick();
    }
}
