using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipOOP.Engine.Events
{
    interface IClickable
    {
        Rectangle? Bounds { get; set; }
        void OnClick();
    }
}
