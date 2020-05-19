using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Engine.Events
{
    /// <summary>
    /// The IBaseEvent interface is the base interface for every single event which can occur
    /// It is empty, because the point of this interface is to restrict event types in BaseEventListener
    /// </summary>
    interface IBaseEvent
    {
    }
}
