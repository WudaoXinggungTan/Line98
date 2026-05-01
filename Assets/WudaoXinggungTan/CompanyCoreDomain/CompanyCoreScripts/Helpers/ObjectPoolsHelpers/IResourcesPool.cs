namespace CompanyCoreScripts.Helpers.ObjectPoolsHelpers
{
    public interface IResourcesPool<T> : IPool<T> where T : IPoolable
    {
        string AssetPath { get; }
    }
}
