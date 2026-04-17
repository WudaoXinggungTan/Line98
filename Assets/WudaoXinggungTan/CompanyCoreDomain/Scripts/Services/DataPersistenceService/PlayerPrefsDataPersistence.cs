using System;
using UnityEngine;
using WudaoXinggungTan.CompanyCoreDomain.Scripts.Utils;
using WudaoXinggungTan.CompanyCoreDomain.Scripts.Services.LoggerService;
using WudaoXinggungTan.CompanyCoreDomain.Scripts.Services.SerializersService.Serializer;

namespace WudaoXinggungTan.CompanyCoreDomain.Scripts.Services.DataPersistenceService
{
    public class PlayerPrefsDataPersistence : IDataPersistence
    {
        private readonly ISerializerService _serializer;

        public PlayerPrefsDataPersistence(ISerializerService serializer)
        {
            _serializer = serializer;
        }

        public void Save<T>(string id, T data)
        {
            try
            {
                var json = _serializer.SerializeJson(data);
                var encrypted = EncryptionUtils.Encrypt(json);
                PlayerPrefs.SetString(id, encrypted);
                PlayerPrefs.Save();
            }
            catch (Exception e)
            {
                MyLogService.LogError($"Tried to save {id}, but exception was thrown: {e}");
            }
        }

        public T Load<T>(string id, T defaultValue = default)
        {
            try
            {
                if (!PlayerPrefs.HasKey(id))
                    return defaultValue;

                var encrypted = PlayerPrefs.GetString(id);
                var json = EncryptionUtils.Decrypt(encrypted);
                return _serializer.DeserializeJson<T>(json);
            }
            catch (Exception e)
            {
                MyLogService.LogError($"Tried to load {id}, but exception was thrown: {e}");
                throw;
            }
        }
    }
}