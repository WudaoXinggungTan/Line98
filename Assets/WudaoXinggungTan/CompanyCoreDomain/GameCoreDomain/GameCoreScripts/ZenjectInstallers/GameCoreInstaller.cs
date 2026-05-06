using UnityEngine;
using Zenject;
using GameCoreScripts.MVC.LoadingScreen;
using GameCoreScripts.Services.GameStateService;
using GameCoreScripts._GameCoreInitiator;
using GameCoreScripts._MainMenuState;
using GameCoreScripts._MainMenuEnterData;
using GameCoreScripts._GameplayEnterData;
using GameCoreScripts._GameplayState;

namespace GameCoreScripts.ZenjectInstallers
{
    public class GameCoreInstaller : MonoInstaller
    {
        #region Dependencies

        [SerializeField] private LoadingScreenView loadingScreenView;

        #endregion
        
        public override void InstallBindings()
        {
            Container.Bind<IGameCoreInitiator>().To<GameCoreInitiator>().AsSingle().NonLazy();
            Container.Bind<IGameStateService>().To<GameStateService>().AsSingle().NonLazy();
            
            Container.BindFactory<MainMenuInitiatorEnterData, MainMenuState, MainMenuStateFactory>().AsSingle().NonLazy();
            Container.BindFactory<GameplayInitiatorEnterData, GameplayState, GameplayStateFactory>().AsSingle().NonLazy();
            
            Container.Bind<ILoadingScreenController>().To<LoadingScreenController>().AsSingle().NonLazy();
            Container.Bind<LoadingScreenView>().FromInstance(loadingScreenView).AsSingle().NonLazy();            
            
        }
    }
}
