namespace CompanyCoreScripts.Services.PlayerPrefDataPersistenceService
{
    public interface IPlayerPrefDataPersistenceService
    {
        void Save<T>(string id, T data);
        T Load<T>(string id, T defaultValue = default);
    }
}
