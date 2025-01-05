using System.Collections.Generic;
using System.Linq;
using CodeBase.Constants;
using UnityEngine;

namespace CodeBase.Camera
{
    public class AudioService : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> audioClips = new List<AudioClip>();

        private List<AudioSource> _audioSources = new List<AudioSource>();


        private void Start()
        {
            _audioSources.AddRange(GetComponents<AudioSource>());

            PlaySound(AudioConstants.MainTheme);
        }

        public void PlaySound(string nameOfSound)
        {
            AudioSource audioSource = GetFreeAudioSource();

            foreach (var clip in audioClips.Where(clip => clip.name == nameOfSound))
            {
                if (clip.name == AudioConstants.MainTheme)
                {
                    audioSource.clip = clip;
                    audioSource.loop = true;
                }
                else if (clip.name == AudioConstants.EnemyDeath)
                {
                    audioSource.clip = clip;
                }
                else if (clip.name == AudioConstants.Engine)
                {
                    audioSource.clip = clip;
                    audioSource.loop = true;
                }
                

                audioSource.clip = clip;
                break;
            }

            audioSource.Play();
        }

        private AudioSource GetFreeAudioSource()
        {
            foreach (var audioSource in _audioSources)
            {
                if (!audioSource.isPlaying)
                {
                    return audioSource;
                }
            }

            _audioSources.Add(gameObject.AddComponent<AudioSource>());

            return _audioSources[^1];
        }
    
    }
}