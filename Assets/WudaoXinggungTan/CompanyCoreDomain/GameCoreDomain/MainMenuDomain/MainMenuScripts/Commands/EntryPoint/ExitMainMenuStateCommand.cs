using System.Threading;
using CompanyCoreScripts.Services.CommandFactoryService.Commands;
using CompanyCoreScripts.Utils;
using UnityEngine;

namespace MainMenuScripts.Commands.EntryPoint
{
    public class ExitMainMenuStateCommand : BaseCommand, ICommandVoid
    {
        public override void ResolveDependencies()
        {
            
        }

        public void Execute()
        {
            // Dispose classes that need dispose
        }
    }
}
