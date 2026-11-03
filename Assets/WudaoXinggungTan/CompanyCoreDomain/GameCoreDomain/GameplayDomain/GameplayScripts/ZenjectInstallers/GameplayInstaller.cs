using UnityEngine;
using Zenject;
using CompanyCoreScripts.Helpers.ObjectPoolsHelpers;
using GameplayScripts._GameplayInitiator;
using GameplayScripts.MVC.Gameplay.Ball;
using GameplayScripts.MVC.Gameplay.Board;
using GameplayScripts.MVC.UI;
using GameplayScripts.MVC.UI.HUD;
using GameplayScripts.MVC.UI.Settings;
using GameplayScripts.Services.ScoreService;

namespace GameplayScripts.ZenjectInstallers
{
    public class GameplayInstaller : MonoInstaller
    {
        #region Dependencies

        [SerializeField] private GameplayUIGroupView gameplayUIGroupView;

        [SerializeField] private HudUICanvasView hudUICanvasView;

        [SerializeField] private SettingsUICanvasView settingsUICanvasView;

        [SerializeField] private BoardView boardView;

        [SerializeField] private BallView ballPrefab;

        #endregion

        public override void InstallBindings()
        {
            #region Services

            Container.Bind<IScoreService>().To<ScoreService>().AsSingle();

            #endregion

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

            #region Settings Canvas

            Container.Bind<ISettingsUICanvasController>().To<SettingsUICanvasController>().AsSingle().NonLazy();
            Container.Bind<SettingsUICanvasView>().FromInstance(settingsUICanvasView).AsSingle().NonLazy();

            #endregion

            #region Board

            Container.Bind<IBoardController>().To<BoardController>().AsSingle().NonLazy();
            Container.Bind<BoardView>().FromInstance(boardView).AsSingle().NonLazy();

            #endregion

            #region Ball

            Container.Bind<IBallController>().To<BallController>().AsSingle().NonLazy();
            Container.Bind<BallPool>().AsSingle().WithArguments(new PoolData(27, 9), ballPrefab);

            #endregion
        }
    }
}
