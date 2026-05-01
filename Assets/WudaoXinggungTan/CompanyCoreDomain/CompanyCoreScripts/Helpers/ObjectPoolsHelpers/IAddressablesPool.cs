namespace CompanyCoreScripts.Helpers.ObjectPoolsHelpers
{
    public interface IAddressablesPool<T> : IPoolAsync<T> where T : IPoolable
    {
        string AssetAdress { get; }
    }
}
