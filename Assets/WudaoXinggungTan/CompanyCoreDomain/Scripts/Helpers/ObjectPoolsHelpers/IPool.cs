namespace WudaoXinggungTan.CompanyCoreDomain.Scripts.Helpers.ObjectPoolsHelpers
{
    public interface IPool<T> where T : IPoolable
    {
        void InitPool();
        T Spawn();
    }
}