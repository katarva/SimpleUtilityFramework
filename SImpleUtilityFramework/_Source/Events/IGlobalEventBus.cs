using Main.Core;

namespace Main.Events
{
    /// <summary>
    /// Event bus that can be taken and used as global service from ServiceLocator
    /// </summary>
    public interface IGlobalEventBus : IEventBus, IService
    {
        
    }
}