using Zenject;
using MainMenuScripts._MainMenuInitiator;
using MainMenuScripts.MVC.UI.MainMenu;
using UnityEngine;

namespace MainMenuScripts.ZenjectInstallers
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuUIGroupView mainMenuUIGroupView;
        [SerializeField] private MainMenuUICanvasView mainMenuUICanvasView;
        
        public override void InstallBindings()
        {
            Container.Bind<IMainMenuInitiator>().To<MainMenuInitiator>().AsSingle().NonLazy();
            Container.Bind<IMainMenuUIGroupController>().To<MainMenuUIGroupController>().AsSingle().NonLazy();
            Container.Bind<IMainMenuUICanvasController>().To<MainMenuUICanvasController>().AsSingle().NonLazy();
            Container.Bind<MainMenuUIGroupView>().FromInstance(mainMenuUIGroupView).AsSingle().NonLazy();
            Container.Bind<MainMenuUICanvasView>().FromInstance(mainMenuUICanvasView).AsSingle().NonLazy();
        }
    }
}