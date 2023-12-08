using System;

namespace Main.Core.Input
{
    /// <summary>
    /// Input state interface for processising input in any IInputManager
    /// </summary>
    public interface IInputState
    {
        void Init();
        void Dispose();
        /// <summary>
        /// Cast IInputState object to T object of same interface
        /// </summary>
        T GetInputObject<T>() where T : class, IInputState;
        void Tick(float delta);
    }
}