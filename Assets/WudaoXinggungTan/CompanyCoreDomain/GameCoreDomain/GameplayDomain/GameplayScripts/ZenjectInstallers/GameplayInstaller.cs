using UnityEngine;
using Zenject;
using GameplayScripts._GameplayInitiator;
using GameplayScripts.MVC.UI;
using GameplayScripts.MVC.UI.HUD;

namespace GameplayScripts.ZenjectInstallers
{
    public class GameplayInstaller : MonoInstaller
    {
        #region Dependencies

        [SerializeField] private GameplayUIGroupView gameplayUIGroupView;

        [SerializeField] private HudUICanvasView hudUICanvasView;

        #endregion

        public override void InstallBindings()
        {
            #region Initiator

            Container.Bind<IGameplayInitiator>().To<GameplayInitiator>().AsSingle().NonLazy();

            #endregion

            #region Group UI

            Container.Bind<IGameplayUIGroupController>().To<GameplayUIGroupController>().AsSingle().NonLazy();
            Container.Bind<GameplayUIGroupView>().FromInstance(gameplayUIGroupView).AsSingle().NonLazy();

            #endregion

            #region HUD Canvas

            Container.Bind<IHudUICanvasController>().To<HudUICanvasController>().AsSingle().NonLazy();
            Container.Bind<HudUICanvasView>().FromInstance(hudUICanvasView).AsSingle().NonLazy();
            
            
            #endregion
        }
    }
}