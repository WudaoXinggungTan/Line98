using Zenject;
using GameplayScripts._GameplayInitiator;

namespace GameplayScripts.ZenjectInstallers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameplayInitiator>().To<GameplayInitiator>().AsSingle().NonLazy();
        }
    }
}