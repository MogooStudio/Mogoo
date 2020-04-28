using System.Collections;
using System.Collections.Generic;
using Mogoo.Base;
using Mogoo.Mono;
using Mogoo.Res;
using UnityEngine;
using UnityEngine.Events;

namespace Mogoo.Music
{
    public class MusicManager : Singleton<MusicManager>
    {
        // music
        private AudioSource _musicAudioSource = null;
        private float _musicVolume = 1;

        // sound
        private GameObject _soundGameObject = null;
        private float _soundVolume = 1;
        private readonly List<AudioSource> _soundList = new List<AudioSource>();

        public MusicManager()
        {
            MonoManager.Instance.AddUpdateListener(Update);
        }

        private void Update()
        {
            for (var i = _soundList.Count - 1; i >= 0; i--)
            {
                if (!_soundList[i].isPlaying)
                {
                    _soundList[i].Stop();
                    _soundList.RemoveAt(i);
                    GameObject.Destroy(_soundList[i]);
                }
            }
        }

        public void PlayMusic(string name)
        {
            if (_musicAudioSource == null)
            {
                GameObject gameObject = new GameObject("Music");
                _musicAudioSource = gameObject.AddComponent<AudioSource>();
            }

            ResManager.Instance.LoadAsync<AudioClip>(name, (audioClip) =>
            {
                _musicAudioSource.clip = audioClip;
                _musicAudioSource.loop = true;
                _musicAudioSource.volume = _musicVolume;
                _musicAudioSource.Play();
            });
        }

        public void PauseMusic()
        {
            if (_musicAudioSource != null)
            {
                _musicAudioSource.Pause();
            }
        }

        public void StopMusic()
        {
            if (_musicAudioSource != null)
            {
                _musicAudioSource.Stop();
            }
        }

        public void SetMusicVolume(float value)
        {
            _musicVolume = value;
            if (_musicAudioSource != null)
            {
                _musicAudioSource.volume = value;
            }
        }

        public void PlaySound(string name, bool isLoop, UnityAction<AudioSource> callback = null)
        {
            if (_soundGameObject == null)
            {
                _soundGameObject = new GameObject("Sound");
            }

            ResManager.Instance.LoadAsync<AudioClip>(name, (audioClip) =>
            {
                AudioSource audioSource = _soundGameObject.AddComponent<AudioSource>();
                audioSource.clip = audioClip;
                audioSource.loop = isLoop;
                audioSource.volume = _soundVolume;
                audioSource.Play();
                _soundList.Add(audioSource);
                callback?.Invoke(audioSource);
            });
        }

        public void SetSoundVolume(float value)
        {
            _soundVolume = value;
            for (var i = 0; i < _soundList.Count; i++)
            {
                _soundList[i].volume = value;
            }
        }

        public void StopSound(AudioSource audioSource)
        {
            if (_soundList.Contains(audioSource))
            {
                audioSource.Stop();
                _soundList.Remove(audioSource);
                GameObject.Destroy(audioSource);
            }
        }
    }
}


