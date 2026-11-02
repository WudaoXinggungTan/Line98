using Zenject;
using CompanyCoreScripts.Helpers.ObjectPoolsHelpers;

namespace GameplayScripts.MVC.Gameplay.Ball
{
    public class BallPool : PrefabsPool<BallView>
    {
        protected override string ParentGameObjectName => "BallPool";

        public BallPool(PoolData poolData, DiContainer diContainer, BallView prefab) : base(poolData, diContainer, prefab)
        {
        }
    }
}
