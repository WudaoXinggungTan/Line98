using Zenject;

namespace CompanyCoreScripts.Services.CommandFactoryService.Commands
{
    public interface IBaseCommand
    {
        void SetObjectResolver(DiContainer diContainer);
        void ResolveDependencies();
    }
}
