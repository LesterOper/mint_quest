using System;
using System.Collections.Generic;
using System.Linq;
using App;
using DG.Tweening;
using Swimmer.Library.Common;
using UnityEngine;

namespace Library.Common
{
    [DisallowMultipleComponent]
    public class MusicPlayer : MonoBehaviour
    {
        private AudioMixerController _audioMixerController;

        public enum Channel
        {
            Music = 0,
            Ambient = 1
        }

        public const string PrefName = "MusicPlayer_enabled";
        private readonly Dictionary<Channel, ChannelData> _chanelDatas = new Dictionary<Channel, ChannelData>();

        private readonly Dictionary<Channel, AudioSource> _sources = new Dictionary<Channel, AudioSource>();

        [SerializeField] private AudioClip[] _audioClips;

        private bool _enabled;

        private void Awake()
        {
            if (GetComponent<Context>().Redundant)
                return;

            _enabled = PlayerPrefs.GetInt(PrefName, 1) == 1;

            foreach (var channel in Enum.GetValues(typeof(Channel)).Cast<Channel>())
                _sources[channel] = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        }

        public void MusicChangeSpeed(float speed, float duration)
        {
            foreach (var var in _sources)
            {
                if (duration > 0)
                {
                    DOTween.To(() => var.Value.pitch, x => var.Value.pitch = x,
                        speed, duration);
                }
                else
                {
                    var.Value.pitch = speed;
                }
            }
        }

        private void Start()
        {
            _audioMixerController = GetComponent<AudioMixerController>();
            foreach (AudioSource source in _sources.Values)
            {
                source.outputAudioMixerGroup = _audioMixerController._masterMusicGroup;
            }
        }

        public void PlayMusic(string musicName, Channel channel, float volume = 0.25f, bool withStop = true)
        {
            var chanelData = new ChannelData
            {
                MusicName = musicName,
                Volume = volume
            };
        
            Debug.Log("[MP] PlayMusic : " + musicName + " ; chanel=" + channel + " ; ");

            if (_enabled &&
                (!_chanelDatas.ContainsKey(channel) ||
                 !musicName.Equals(_chanelDatas[channel].MusicName) ||
                 Math.Abs(_chanelDatas[channel].Volume - volume) > 0.0001f))
                PlayMusic(channel, chanelData, withStop);

            _chanelDatas[channel] = chanelData;
        }


        private void PlayMusic(Channel channel, ChannelData channelData, bool withStop = false)
        {
            Debug.Log("[MP] PlayMusic 2 : " + channelData.MusicName + "; chanel=" + channel + " ; ");

            try
            {
                if (withStop)
                    StopChannel(channel);
                _sources[channel].clip =
                    _audioClips.FirstOrDefault(audioClip => audioClip.name.Equals(channelData.MusicName));
                _sources[channel].loop = true;


#if UNITY_ANDROID
                _sources[channel].volume = Mathf.Clamp01(channelData.Volume);
#endif
#if UNITY_IOS
                    _sources[channel].volume = Mathf.Clamp01(channelData.Volume);
#endif
                _sources[channel].Play();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        public float GetTrackTime(Channel channel)
        {
            return _sources[channel].time;
        }

        public void SetTrackTime(Channel channel, float time)
        {
            _sources[channel].time = time;
        }


        public void StopMusic()
        {
            foreach (var source in _sources.Values)
                if (source.isPlaying)
                    source.Stop();
        }

        public void StopChannel(Channel channel)
        {
            if (_sources[channel].isPlaying)
                SmoothStop(_sources[channel], channel);
        }

        private void SmoothStop(AudioSource audioSource, Channel channel)
        {
            DOTween.To(() => audioSource.volume, x => audioSource.volume = x, 0, .8f).onComplete += delegate
            {
                audioSource.Stop();
                _chanelDatas.Remove(channel);
            };
        }
        public void ChangeChannelVolume(Channel channel, float volume)
        {
            if (_sources[channel].isPlaying)
                SmoothChangeVolumeChannel(_sources[channel], channel, volume);
        }

        private void SmoothChangeVolumeChannel(AudioSource audioSource, Channel channel, float volume)
        {
            DOTween.To(() => audioSource.volume, x => audioSource.volume = x, volume, .8f).onComplete += delegate
                {
                    _chanelDatas[channel].Volume = volume;
                };
        }

        public void SetEnabled(bool musicEnabled)
        {
            _enabled = musicEnabled;

            PlayerPrefs.SetInt(PrefName, musicEnabled ? 1 : 0);
            PlayerPrefs.Save();
            if (musicEnabled)
                foreach (var channel in Enum.GetValues(typeof(Channel)).Cast<Channel>())
                    if (_chanelDatas.ContainsKey(channel))
                        PlayMusic(channel, _chanelDatas[channel]);
                    else
                        StopMusic();
        }

        public bool IsEnabled()
        {
            return _enabled;
        }

        private class ChannelData
        {
            public string MusicName { get; set; }

            public float Volume { get; set; }
        }
    }
}