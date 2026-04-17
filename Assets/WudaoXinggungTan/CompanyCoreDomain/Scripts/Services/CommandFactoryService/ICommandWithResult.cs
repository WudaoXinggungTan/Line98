namespace WudaoXinggungTan.CompanyCoreDomain.Scripts.Services.CommandFactoryService
{
    public interface ICommandWithResult<TReturn> : IBaseCommand
    {
        TReturn Execute();
    }
}