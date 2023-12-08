using System;

namespace Main.Events
{
    /// <summary>
    /// Basic interface for events used by IEventBus
    /// </summary>
    public interface IEvent
    {
        void Listen(Action<IEventEmitter, EventArgsBase> callback);
        void StopListen(Action<IEventEmitter, EventArgsBase> callback);
        void Fire(IEventEmitter obj = null, EventArgsBase args = null);
    }
}
