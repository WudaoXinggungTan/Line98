using System;

namespace WudaoXinggungTan.CompanyCoreDomain.Scripts.Helpers.ObjectPoolsHelpers
{
    public interface IPoolable
    {
        void OnCreated();
        Action Despawn { set; }
        void OnSpawned();
        void OnDespawned();
    }
}