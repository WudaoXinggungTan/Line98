using UnityEngine;
using WudaoXinggungTan.CompanyCoreDomain.Scripts.Helpers.SerializableDictionaryHelpers;

namespace WudaoXinggungTan.CompanyCoreDomain.Scripts.Services.AudioService
{
    public abstract class AudioClipsScriptableObject : ScriptableObject
    {
        public SerializableDictionary<AudioClipType, AudioClip> AudioClips;
    }
}