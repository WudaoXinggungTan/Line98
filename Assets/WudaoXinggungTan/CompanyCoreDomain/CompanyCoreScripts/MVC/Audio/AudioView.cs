using System.Collections.Generic;
using UnityEngine;
using CompanyCoreScripts.Services.LoggerService.StaticClass;

namespace CompanyCoreScripts.MVC.Audio
{
    public class AudioView : MonoBehaviour
    {
        [SerializeField] private AudioSource masterAudioSource;
        [SerializeField] private AudioSource fxAudioSource;
        [SerializeField] private AudioSource musicAudioSource;

        private Dictionary<AudioChannelType, AudioSource> audioSourceByChannel;

        public void CreateAudioSourceDictionary()
        {
            audioSourceByChannel = new Dictionary<AudioChannelType, AudioSource>
            {
                { AudioChannelType.Master, masterAudioSource },
                { AudioChannelType.Fx, fxAudioSource },
                { AudioChannelType.Music, musicAudioSource }
            };
        }

        public void Play(AudioChannelType channel, AudioClip clip, bool loop)
        {
            if (!audioSourceByChannel.TryGetValue(channel, out var source))
            {
                MyLoggerService.LogError($"No audioChannel of name {channel} found");
                return;
            }

            var isAudioMuted = source.mute || !source.enabled;

            if (isAudioMuted)
            {
                return;
            }

            source.loop = loop;
            if (loop)
            {
                source.clip = clip;
                source.Play();
            }
            else
            {
                source.PlayOneShot(clip);
            }
        }

        public void Stop(AudioChannelType channel)
        {
            if (audioSourceByChannel.TryGetValue(channel, out var source))
            {
                source.Stop();
            }
        }
    }
}