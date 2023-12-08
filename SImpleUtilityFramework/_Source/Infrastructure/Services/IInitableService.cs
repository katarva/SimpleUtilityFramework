namespace Main.Core
{
    /// <summary>
    /// Implement this to initialize service after it's creation in ServiceLocator
    /// </summary>
    public interface IInitableService : IService
    {
        void Init();
    }
}