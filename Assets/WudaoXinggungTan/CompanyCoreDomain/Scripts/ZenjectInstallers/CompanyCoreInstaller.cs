using UnityEngine;
using Zenject;
using WudaoXinggungTan.CompanyCoreDomain.Plugins.InputSystem;
using WudaoXinggungTan.CompanyCoreDomain.Scripts.Services.LoggerService;
using WudaoXinggungTan.CompanyCoreDomain.Scripts.Services.SceneService;

namespace WudaoXinggungTan.CompanyCoreDomain.Scripts.ZenjectInstallers
{
    public class CompanyCoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<InputSystem_Actions>().AsSingle().NonLazy();
            Container.BindInterfacesTo<CustomLogger>().AsSingle().NonLazy();
        }
    }
}