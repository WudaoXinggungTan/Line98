using System;

namespace CompanyCoreScripts.Helpers.ObjectPoolsHelpers
{
    public interface IPoolable
    {
        void OnCreated();
        Action Despawn { set; }
        void OnSpawned();
        void OnDespawned();
    }
}
