using Zenject;

namespace WudaoXinggungTan.CompanyCoreDomain.Scripts.Services.CommandFactoryService
{
    public interface IBaseCommand
    {
        void SetObjectResolver(DiContainer diContainer);
        void ResolveDependencies();
    }
}