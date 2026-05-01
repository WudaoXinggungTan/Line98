namespace CompanyCoreScripts.Services.SerializersService.Serializer
{
    public interface ISerializerService
    {
        string SerializeJson<T>(T obj);
        T DeserializeJson<T>(string json);
    }
}
