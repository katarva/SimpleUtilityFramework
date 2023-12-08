namespace Main.Core
{
    /// <summary>
    /// Implement this to mark service as suitable for putting in Update loop
    /// </summary>
    public interface IUpdatableService : IService
    {
        void Tick();
    }
}