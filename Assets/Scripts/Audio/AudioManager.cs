using Scripts.Game;
using UnityEngine;

namespace Scripts.Audio
{
    public class AudioManager : MonoBehaviour,
                                IGameInitListener
    {
        [SerializeField] private SoundLibrary _soundLibrary;
        
        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private AudioSource _soundsAudioSource;

        private float? _currentMusicVolume;

        public void OnGameInit()
        {
            PlayMusic();
        }

        public void PlayMusic()
        {
            var randomMusic = _soundLibrary.MusicClips[Random.Range(0, _soundLibrary.MusicClips.Count)];
            _musicAudioSource.clip = randomMusic;
            _musicAudioSource.Play();
        }

        public void SetMusicMuteState(bool state)
        {
            if (_currentMusicVolume == null)
                _currentMusicVolume = _musicAudioSource.volume;
            
            if (state)
            {
                _musicAudioSource.volume = (float) _currentMusicVolume;
            }
            else
            {
                _currentMusicVolume = _musicAudioSource.volume;
                _musicAudioSource.volume = 0f;
            }
        }

        public void PlayClipByType(ClipType clipType)
        {
            var clip = _soundLibrary.AudioClips[clipType];
            PlaySound(clip);
        }

        public void PlaySound(AudioClip clip)
        {
            if (clip != null)
                _soundsAudioSource.PlayOneShot(clip);
        }
    }
}