using Zenject;
using GameCoreScripts._GameCoreInitiator;
using GameCoreScripts._MainMenuState;
using GameCoreScripts._MainMenuEnterData;
using GameCoreScripts._GameplayEnterData;
using GameCoreScripts._GameplayState;

namespace GameCoreScripts.ZenjectInstallers
{
    public class GameCoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameCoreInitiator>().To<GameCoreSceneInitiator>().AsSingle().NonLazy();
            
            Container.Bind<MainMenuInitiatorEnterData>().AsSingle().NonLazy();
            Container.Bind<GameplayInitiatorEnterData>().AsSingle().NonLazy();
            Container.BindFactory<MainMenuInitiatorEnterData, MainMenuState, MainMenuStateFactory>().AsSingle().NonLazy();
            Container.BindFactory<GameplayInitiatorEnterData, GameplayState, GameplayStateFactory>().AsSingle().NonLazy();
        }
    }
}
