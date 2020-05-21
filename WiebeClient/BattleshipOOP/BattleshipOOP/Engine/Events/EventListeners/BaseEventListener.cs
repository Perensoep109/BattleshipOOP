using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Engine.Events.EventListeners
{
    /// <summary>
    /// The base event listener is an implementation of the Observer-listener design pattern. 
    /// This design pattern is a system where one event listener observes an event, checks if it should trigger.
    /// X amount of event observers can subscribe (attach) to this event listener, so when the event listener triggers itself. It notifies all the subscribed event listeners accordingly
    /// </summary>
    /// <typeparam name="IObservesEvent">The event to observe</typeparam>
    abstract class BaseEventListener<IObservesEvent> where IObservesEvent : IBaseEvent
    {
        /// <summary>
        /// The singleton instance of this event listener
        /// </summary>
        public static BaseEventListener<IObservesEvent> Instance { get; protected set; }

        /// <summary>
        /// All the subscribed (attached) event listeners
        /// </summary>
        protected List<IObservesEvent> m_listeners = new List<IObservesEvent>();

        /// <summary>
        /// Subscribe an event observer to this event listener
        /// </summary>
        /// <param name="a_observable">The instance of the event observer to attach</param>
        public void Attach(IObservesEvent a_observable)
        {
            if(!m_listeners.Contains(a_observable))
                m_listeners.Add(a_observable);
        }

        /// <summary>
        /// Unsubscribe an event observer from this event listener
        /// </summary>
        /// <param name="a_observable">The instance of the event observer to detach</param>
        public void Detach(IObservesEvent a_observable)
        {
            m_listeners.Remove(a_observable);
        }

        public bool Contains(IObservesEvent a_observable)
        {
            return m_listeners.Contains(a_observable);
        }
    }
}