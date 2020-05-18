using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipOOP.Engine.Events.EventListeners
{
    abstract class BaseEventListener<IObservesEvent> where IObservesEvent : IBaseEvent
    {
        public static BaseEventListener<IObservesEvent> Instance { get; protected set; }

        protected BaseEventListener()
        {

        }

        protected List<IObservesEvent> m_listeners = new List<IObservesEvent>();

        public void Attach(IObservesEvent a_observable)
        {
            m_listeners.Add(a_observable);
        }

        public void Detach(IObservesEvent a_observable)
        {
            m_listeners.Remove(a_observable);
        }

        public abstract void Update();
    }
}