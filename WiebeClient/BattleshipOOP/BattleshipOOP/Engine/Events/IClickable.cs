using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Engine.Events
{
    interface IClickable : IBaseEvent
    {
        Rectangle? Bounds { get; set; }
        void OnClick();
    }
}
