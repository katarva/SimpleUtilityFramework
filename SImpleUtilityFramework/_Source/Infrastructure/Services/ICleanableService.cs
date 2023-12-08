namespace Main.Core
{
    /// <summary>
    /// Implement this to clean service after use in ServiceLocator
    /// </summary>
    public interface ICleanableService : IService
    {
        void Clean();
    }
}