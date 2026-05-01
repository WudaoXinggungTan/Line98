namespace CompanyCoreScripts.Helpers.ObjectPoolsHelpers
{
    public interface IPool<T> where T : IPoolable
    {
        void InitPool();
        T Spawn();
    }
}
