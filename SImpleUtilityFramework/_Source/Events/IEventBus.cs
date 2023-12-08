namespace Main.Events
{
    /// <summary>
    /// Basic interface for all Event Bus objects
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// Get or create event object of type IEvent
        /// </summary>
        public T GetEvent<T>() where T : class, IEvent, new();
        /// <summary>
        /// Emit event of type IEvent with optional IEventEmiiter object and EventArgsBase arguments 
        /// </summary>
        /// <returns>True if event is successfully emitted, false if not</returns>
        public bool Emit<T>(IEventEmitter obj = null, EventArgsBase args = null) where T : class, IEvent;
    }
}