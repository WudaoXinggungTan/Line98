using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using CompanyCoreScripts.Services.LoggerService;
using CompanyCoreScripts.Services.LoggerService.StaticClass;
using CompanyCoreScripts.Services.PlayerPrefDataPersistenceService;

namespace CompanyCoreScripts.MVC.Audio
{
    public class AudioController : IAudioController
    {
        #region Dependency

        private const string MUTED_KEY = "Audio.Muted";

        private AudioView audioView;
        private readonly IPlayerPrefDataPersistenceService persistenceService;

        #endregion

        #region Depedencies

        private readonly List<AudioClipsScriptableObject> audioClipsScriptableObjects = new();

        #endregion

        #region Properties

        public bool IsMuted { get; private set; }

        #endregion

        #region Constructor

        public AudioController(AudioView audioView, IPlayerPrefDataPersistenceService persistenceService)
        {
            this.audioView = audioView;
            this.persistenceService = persistenceService;
            this.audioView.CreateAudioSourceDictionary();

            // Mute is remembered between sessions
            IsMuted = persistenceService.Load(MUTED_KEY, false);
            this.audioView.SetMuted(IsMuted);
        }

        #endregion

        #region Private Methods

        private bool TryPlayAudioClip(AudioClipType audioClipType, AudioChannelType audioChannel, AudioPlayType audioPlayType, out AudioClip audioClip)
        {
            if (!TryGetAudioClip(audioClipType, out audioClip))
            {
                return false;
            }

            bool isLoop = audioPlayType == AudioPlayType.Loop;
            audioView.Play(audioChannel, audioClip, isLoop);

            MyLoggerService.LogTopic($"Played Audio {audioClipType} for channel {audioChannel}", LogTopicType.Audio);
            return true;
        }

        private bool TryGetAudioClip(AudioClipType audioClipType, out AudioClip audioClip)
        {
            foreach (var audioClipsScriptableObject in audioClipsScriptableObjects)
            {
                if (audioClipsScriptableObject.AudioClips.TryGetValue(audioClipType, out audioClip))
                {
                    return true;
                }
            }

            MyLoggerService.LogError($"No clip of name {audioClipType} found");
            audioClip = null;
            return false;
        }

        #endregion

        #region Public Methods

        public void AddAudioClips(AudioClipsScriptableObject audioClipsScriptableObject)
        {
            if (!audioClipsScriptableObjects.Contains(audioClipsScriptableObject))
            {
                audioClipsScriptableObjects.Add(audioClipsScriptableObject);
            }
        }

        public void RemoveAudioClips(AudioClipsScriptableObject audioClipsScriptableObject)
        {
            audioClipsScriptableObjects.Remove(audioClipsScriptableObject);
        }

        public void PlayAudio(AudioClipType audioClipType, AudioChannelType audioChannel, AudioPlayType audioPlayType = AudioPlayType.OneShot)
        {
            TryPlayAudioClip(audioClipType, audioChannel, audioPlayType, out _);
        }

        public async Awaitable PlayAudioAsync(AudioClipType audioClipType, AudioChannelType audioChannel, CancellationTokenSource cancellationTokenSource, AudioPlayType audioPlayType = AudioPlayType.OneShot)
        {
            if (TryPlayAudioClip(audioClipType, audioChannel, audioPlayType, out var audioClip))
            {
                await Awaitable.WaitForSecondsAsync(audioClip.length, cancellationTokenSource.Token);
            }
        }

        public void SetMuted(bool isMuted)
        {
            IsMuted = isMuted;
            audioView.SetMuted(isMuted);
            persistenceService.Save(MUTED_KEY, isMuted);
            MyLoggerService.LogTopic($"Audio muted: {isMuted}", LogTopicType.Audio);
        }

        public void StopAllAudio()
        {
            MyLoggerService.LogTopic("Stop all audio", LogTopicType.Audio);

            foreach (AudioChannelType channel in System.Enum.GetValues(typeof(AudioChannelType)))
            {
                audioView.Stop(channel);
            }
        }

        #endregion
    }
}