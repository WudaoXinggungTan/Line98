using Zenject;
using UnityEngine;
using MainMenuScripts._MainMenuInitiator;
using MainMenuScripts.MVC.UI;
using MainMenuScripts.MVC.UI.MainCanvas;
using MainMenuScripts.MVC.UI.SettingsCanvas;

namespace MainMenuScripts.ZenjectInstallers
{
    public class MainMenuInstaller : MonoInstaller
    {
        #region Dependencies

        [SerializeField] private MainMenuUIGroupView mainMenuUIGroupView;

        [SerializeField] private MainUICanvasView mainUICanvasView;
        [SerializeField] private SettingsUICanvasView settingsUICanvasView;

        #endregion
        
        public override void InstallBindings()
        {
            #region Initiator

            Container.Bind<IMainMenuInitiator>().To<MainMenuInitiator>().AsSingle().NonLazy();

            #endregion

            #region Group UI

            Container.Bind<IMainMenuUIGroupController>().To<MainMenuUIGroupController>().AsSingle().NonLazy();
            Container.Bind<MainMenuUIGroupView>().FromInstance(mainMenuUIGroupView).AsSingle().NonLazy();

            #endregion

            #region Main Canvas

            Container.Bind<IMainUICanvasController>().To<MainUICanvasController>().AsSingle().NonLazy();
            Container.Bind<MainUICanvasView>().FromInstance(mainUICanvasView).AsSingle().NonLazy();

            #endregion

            #region Settings Canvas

            Container.Bind<ISettingsUICanvasController>().To<SettingsUICanvasController>().AsSingle().NonLazy();
            Container.Bind<SettingsUICanvasView>().FromInstance(settingsUICanvasView).AsSingle().NonLazy();

            #endregion
        }
    }
}