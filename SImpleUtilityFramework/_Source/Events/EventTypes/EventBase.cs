using System.Collections.Generic;
using System;

namespace Main.Events
{
    /// <summary>
    /// Base realization for any event by IEvent interface
    /// </summary>
    public abstract class EventBase : IEvent
    {
        private List<Action<IEventEmitter, EventArgsBase>> _listeners;
        private const int MAX_LISTENER_COUNT = 100;


        public EventBase()
        {
            _listeners = new List<Action<IEventEmitter, EventArgsBase>>(MAX_LISTENER_COUNT);
        }
        ~EventBase()
        {
            _listeners.Clear();
            _listeners = null;
        }
        public void Listen(Action<IEventEmitter, EventArgsBase> callback)
        {
            _listeners.Add(callback);
        }
        public void StopListen(Action<IEventEmitter, EventArgsBase> callback)
        {
            _listeners.Remove(callback);
        }
        public void Fire(IEventEmitter obj = null, EventArgsBase args = null)
        {
            for (int i = 0; i < _listeners.Count; i++) _listeners[i].Invoke(obj, args);
        }
    }
}
