namespace CompanyCoreScripts.Services.CommandFactoryService.Commands
{
    public interface ICommandWithResult<TReturn> : IBaseCommand
    {
        TReturn Execute();
    }
}
