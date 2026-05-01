namespace CompanyCoreScripts.Helpers.ObjectPoolsHelpers
{
    public interface IAssetFromBundlePool<T> : IPool<T> where T : IPoolable
    {
        string AssetName { get; }
    }
}
