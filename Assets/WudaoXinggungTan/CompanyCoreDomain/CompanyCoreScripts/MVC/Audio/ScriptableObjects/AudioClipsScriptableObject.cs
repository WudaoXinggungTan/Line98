using UnityEngine;
using CompanyCoreScripts.Helpers.SerializableDictionaryHelpers;

namespace CompanyCoreScripts.MVC.Audio
{
    [CreateAssetMenu(fileName = "AudioClipsScriptableObject", menuName = "Scriptable Objects/AudioClipsScriptableObject")]
    public class AudioClipsScriptableObject : ScriptableObject
    {
        public SerializableDictionary<AudioClipType, AudioClip> AudioClips;
    }
}
