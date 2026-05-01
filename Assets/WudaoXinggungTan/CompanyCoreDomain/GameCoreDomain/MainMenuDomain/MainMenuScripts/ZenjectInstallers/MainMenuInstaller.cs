using Zenject;
using MainMenuScripts._MainMenuInitiator;
using MainMenuScripts.MVC.UI.MainMenu;
using UnityEngine;

namespace MainMenuScripts.ZenjectInstallers
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuUIGroupView mainMenuUIGroupView;
        
        public override void InstallBindings()
        {
            Container.Bind<IMainMenuInitiator>().To<MainMenuInitiator>().AsSingle().NonLazy();
            Container.Bind<IMainMenuUIController>().To<MainMenuUIController>().AsSingle().NonLazy();
            Container.Bind<MainMenuUIGroupView>().FromInstance(mainMenuUIGroupView).AsSingle().NonLazy();
        }
    }
}