using System;

namespace Main.Core
{
    /// <summary>
    /// Interface for modules. They provide reactivity for Game class by responding to global events
    /// </summary>
    public interface IGameModule
    {
        void Init();
        void Subscribe();
        void Unsubscribe();
        void Dispose();
        /// <summary>
        /// Needed for searching by predicate through collection of modules
        /// </summary>
        bool IsDesired(Func<IGameModule, bool> predicate);
    }
}