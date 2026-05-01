using System.Threading;
using UnityEngine;
using Zenject;

namespace CompanyCoreScripts.Services.CommandFactoryService.Commands
{
    public abstract class BaseCommand : IBaseCommand
    {
        protected DiContainer diContainer;

        public void SetObjectResolver(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        public abstract void ResolveDependencies();
    }
}
