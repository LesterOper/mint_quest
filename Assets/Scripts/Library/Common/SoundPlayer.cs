using System;
using System.Collections.Generic;
using System.Linq;
using App;
using UnityEngine;

namespace Swimmer.Library.Common
{
    public class SoundPlayer : MonoBehaviour
    {
        public const string PrefName = "SoundPlayer_enabled";

        [SerializeField]
        private AudioClip[] _clips;

        private bool _enabled;
        private List<AudioSource> _sources = new List<AudioSource>();

        private void Awake()
        {
            _enabled = PlayerPrefs.GetInt(PrefName, 1) == 1;
            for (int i = 0; i < 15; i++)
                _sources.Add(gameObject.AddComponent(typeof(AudioSource)) as AudioSource);
        }

        public AudioSource PlaySound(string soundName, AudioMixerController.MasterMixerGroup masterMixerGroup = AudioMixerController.MasterMixerGroup.SFX , float volume = 0.3f)
        {
            if (_enabled)
                try
                {
                    var source = GetSource();

                    if (source.isPlaying) source.Stop();
                    source.loop = true;
                    source.clip = _clips.FirstOrDefault(clip => clip.name.Equals(soundName));
                    source.outputAudioMixerGroup = GetComponent<AudioMixerController>().GetMixerGroup(masterMixerGroup);
                    source.volume = PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME);
                    source.spatialBlend = 0;
                    source.time = 0;
                    source.Play();
                    OnSourceUsed(source);
                    return source;
                }
                catch (Exception e)
                {
                    Debug.LogWarning(e.Message);
                   
                }
            return null;
        }
        
        public AudioSource PlaySound(AudioSource sourceCurr, string sourceName, AudioMixerController.MasterMixerGroup masterMixerGroup = AudioMixerController.MasterMixerGroup.SFX , float volume = 0.3f)
        {
            if (_enabled)
                try
                {
                    var audioSource = _sources.FirstOrDefault(obj => sourceCurr == obj);
                    if (_sources.FirstOrDefault(obj => sourceCurr == obj) != null && audioSource.isPlaying)  
                    {
                        //audioSource.Stop();
                        return audioSource;
                    }

                    var source = GetSource();
                    //if (source.isPlaying) source.Stop();
                    source.loop = true;
                    source.clip = _clips.FirstOrDefault(clip => clip.name.Equals(sourceName));
                    source.outputAudioMixerGroup = GetComponent<AudioMixerController>().GetMixerGroup(masterMixerGroup);
                    source.volume = PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME);
                    source.spatialBlend = 0;
                    source.time = 0;
                    
                    source.Play();
                    OnSourceUsed(source);
                    return source;
                }
                catch (Exception e)
                {
                    Debug.LogWarning(e.Message);
                   
                }
            return null;
        }
        public AudioSource PlaySoundClick(string soundName, AudioMixerController.MasterMixerGroup masterMixerGroup = AudioMixerController.MasterMixerGroup.SFX , float volume = 0.3f)
        {
            if (_enabled)
                try
                {
                    var source = GetSource();

                    if (source.isPlaying) source.Stop();
                    source.loop = false;
                    source.clip = _clips.FirstOrDefault(clip => clip.name.Equals(soundName));
                    source.outputAudioMixerGroup = GetComponent<AudioMixerController>().GetMixerGroup(masterMixerGroup);
                    source.volume = PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME);
                    source.spatialBlend = 0;
                    source.time = 0;
                    source.PlayOneShot(source.clip);
                    OnSourceUsed(source);
                    return source;
                }
                catch (Exception e)
                {
                    Debug.LogWarning(e.Message);
                   
                }
            return null;
        }
        
        public AudioSource PlaySoundClick(AudioSource sourceCurr, string soundName, AudioMixerController.MasterMixerGroup masterMixerGroup = AudioMixerController.MasterMixerGroup.SFX , float volume = 0.3f)
        {
            if (_enabled)
                try
                {
                    var audio = _sources.FirstOrDefault(obj => obj == sourceCurr);
                    if (audio != null && audio.isPlaying) return audio;
                    var source = GetSource();

                    if (source.isPlaying) source.Stop();
                    source.loop = false;
                    source.clip = _clips.FirstOrDefault(clip => clip.name.Equals(soundName));
                    source.outputAudioMixerGroup = GetComponent<AudioMixerController>().GetMixerGroup(masterMixerGroup);
                    source.volume = PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME);
                    source.spatialBlend = 0;
                    source.time = 0;
                    source.Play();
                    OnSourceUsed(source);
                    return source;
                }
                catch (Exception e)
                {
                    Debug.LogWarning(e.Message);
                   
                }
            return null;
        }

        private AudioSource GetSource()
        {
            foreach (var source in _sources)
                if (!source.isPlaying)
                    if(!source.loop)
                        return source;
            return _sources.Last();
        }

        public void UpdateEachSourceVolume(float volume, AudioMixerController.MasterMixerGroup sfx)
        {
            _sources.ForEach(obj => obj.volume = volume);
        }
        
        private void OnSourceUsed(AudioSource source)
        {
            if (_sources[0] != source)
            {
                _sources.Remove(source);
                _sources.Insert(0, source);
            }
        }

        public void SetEnabled(bool enabled)
        {
            _enabled = enabled;
            PlayerPrefs.SetInt(PrefName, enabled ? 1 : 0);
            PlayerPrefs.Save();

            if (!enabled)
                StopPlayingSounds();
        }

        public bool IsEnabled()
        {
            return _enabled;
        }

        public void StopPlayingSounds() {
            foreach (var source in _sources)
            {
                if (source.isPlaying)
                    source.Stop();
            }
        }

        public void StopPlayingSounds(string nameClip)
        {
            foreach (var source in _sources)
            {
                if (source.isPlaying && source.clip.name == nameClip) 
                    source.Stop();
            }
        }

        public void StopPlayingSounds(AudioSource sound)
        {
            if (sound != null)
            {
                sound.Stop();
            }
        }

        public bool IsTargetSourcePlaying(AudioSource source)
        {
            return source == null ? false : source.isPlaying;
        }

        public void StartAudioSourceInParticularPosition(AudioSource audioSource, float seconds)
        {
            audioSource.time = seconds;
        }
    }
}
