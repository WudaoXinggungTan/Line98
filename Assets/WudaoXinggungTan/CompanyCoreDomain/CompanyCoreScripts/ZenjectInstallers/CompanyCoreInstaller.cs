using UnityEngine;
using CompanyCoreScripts.MVC.LoadingScreen;
using CompanyCoreScripts.MVC.WorldCamera;
using CompanyCoreScripts.Services.AddressablesLoader;
using CompanyCoreScripts.Services.AssetBundleLoaderService;
using CompanyCoreScripts.MVC.Audio;
using CompanyCoreScripts.MVC.UICamera;
using CompanyCoreScripts.Services.BroadcastService;
using CompanyCoreScripts.Services.CommandFactoryService.Factory;
using CompanyCoreScripts.Services.GameStateService;
using CompanyCoreScripts.Services.LoggerService;
using CompanyCoreScripts.Services.LoggerService.StaticClass;
using CompanyCoreScripts.Services.LoggerService.Interface;
using CompanyCoreScripts.Services.PlayerPrefDataPersistenceService;
using CompanyCoreScripts.Services.ResourcesLoaderService;
using CompanyCoreScripts.Services.SceneInitiatorsService;
using CompanyCoreScripts.Services.SceneLoaderService;
using CompanyCoreScripts.Services.SerializersService.Serializer;
using CompanyCoreScripts.Services.UpdateService;
using Zenject;
using ProjectPlugins.InputSystem;

namespace CompanyCoreScripts.ZenjectInstallers
{
    public class CompanyCoreInstaller : MonoInstaller
    {
        #region References

        [SerializeField] private UpdateSubscriptionService updateSubscriptionService;

        [SerializeField] private AudioView audioView;
        [SerializeField] private LoadingScreenView loadingScreenView;
        [SerializeField] private WorldCameraView worldCameraView;
        [SerializeField] private UICameraView uiCameraView;

        #endregion

        public override void InstallBindings()
        {
            #region Systems

            Container.Bind<InputSystem_Actions>().AsSingle().NonLazy();

            #endregion

            #region MVC that control the loading screen, world camera and audio

            Container.Bind<IAudioController>().To<AudioController>().AsSingle().NonLazy();
            Container.Bind<AudioView>().FromInstance(audioView).AsSingle().NonLazy();


            Container.Bind<ILoadingScreenController>().To<LoadingScreenController>().AsSingle().NonLazy();
            Container.Bind<LoadingScreenView>().FromInstance(loadingScreenView).AsSingle().NonLazy();

            Container.Bind<IWorldCameraController>().To<WorldCameraController>().AsSingle().NonLazy();
            Container.Bind<WorldCameraView>().FromInstance(worldCameraView).AsSingle().NonLazy();
            
            Container.Bind<IUICameraController>().To<UICameraController>().AsSingle().NonLazy();
            Container.Bind<UICameraView>().FromInstance(uiCameraView).AsSingle().NonLazy();

            // Copying and pasting the Container.BindInterfacesTo<CommandFactory>().AsSingle().NonLazy() statement into the installer for every sub-container.
            Container.Bind<ICommandFactory>().To<CommandFactory>().AsSingle().CopyIntoAllSubContainers().NonLazy();

            #endregion

            #region Audio, Assets, Resources, PlayerPref related Services

            Container.Bind<IAddressablesLoaderService>().To<AddressablesLoaderService>().AsSingle().NonLazy();
            Container.Bind<IAssetBundleLoaderService>().To<AssetBundleLoaderService>().AsSingle().NonLazy();
            Container.Bind<IResourcesLoaderService>().To<ResourcesLoaderService>().AsSingle().NonLazy();
            Container.Bind<IPlayerPrefDataPersistenceService>().To<PlayerPrefDataPersistenceService>().AsSingle().NonLazy();

            #endregion

            #region I'm not even sure these Services are needed or not

            Container.Bind<IBroadcastService>().To<BroadcastService>().AsSingle().NonLazy();
            Container.Bind<ISerializerService>().To<SerializerService>().AsSingle().NonLazy();

            #endregion

            #region Less important Services But Still used a lot in the project

            Container.Bind<ICustomLogger>().To<CustomLogger1>().AsSingle().NonLazy();
            Container.Bind<IUpdateSubscriptionService>().To<UpdateSubscriptionService>().FromInstance(updateSubscriptionService).AsSingle().NonLazy();

            #endregion

            #region Important Services (Without these, game won't run)

            Container.Bind<ISceneInitiatorsService>().To<SceneInitiatorsService>().AsSingle().NonLazy();
            Container.Bind<ISceneLoaderService>().To<SceneLoaderService>().AsSingle();
            Container.Bind<IGameStateService>().To<GameStateService>().AsSingle().NonLazy();

            #endregion
        }
    }
}