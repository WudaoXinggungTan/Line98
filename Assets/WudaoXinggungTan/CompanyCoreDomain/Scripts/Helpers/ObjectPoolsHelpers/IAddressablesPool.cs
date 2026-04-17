namespace WudaoXinggungTan.CompanyCoreDomain.Scripts.Helpers.ObjectPoolsHelpers
{
    public interface IAddressablesPool<T> : IPoolAsync<T> where T : IPoolable
    {
        string AssetAdress { get; }
    }
}
