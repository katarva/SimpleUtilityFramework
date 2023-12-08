namespace Main.Core.Input
{
    /// <summary>
    /// Main input management class.<br/>
    /// Is IService!
    /// </summary>
    public interface IInputManager : IService
    {
        /// <summary>
        /// Concrete instance of this type
        /// </summary>
        object GetInstance { get; }
        /// <summary>
        /// Current proccessing state
        /// </summary>
        IInputState CurrentState { get; }


        /// <summary>
        /// Get concrete state of input manager
        /// </summary>
        T GetInputState<T>() where T : class, IInputState;
        /// <summary>
        /// Change CurrentState to another of this type
        /// </summary>
        void ChangeInput<T>() where T : class, IInputState;
    }
}