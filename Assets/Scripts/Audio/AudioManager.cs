using Scripts.Game;
using UnityEngine;

namespace Scripts.Audio
{
    public class AudioManager : MonoBehaviour,
                                IGameInitListener,
                                IGamePauseListener,
                                IGameResumeListener
    {
        [SerializeField] private SoundLibrary _soundLibrary;
        
        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private AudioSource _soundsAudioSource;

        private float? _currentMusicVolume;
        private float? _currentSoundsVolume;

        public void OnGameInit()
        {
            PlayMusic();
        }

        public void OnGamePaused()
        {
            SetMusicMuteState(true);
        }

        public void OnGameResumed()
        {
            SetMusicMuteState(false);
        }

        public void SetMusicMuteState(bool isMute)
        {
            if (_currentMusicVolume == null)
                _currentMusicVolume = _musicAudioSource.volume;

            if (_currentSoundsVolume == null)
                _currentSoundsVolume = _soundsAudioSource.volume;
            
            if (isMute)
            {
                _currentMusicVolume = _musicAudioSource.volume;
                _currentSoundsVolume = _soundsAudioSource.volume;

                _musicAudioSource.volume = 0f;
                _soundsAudioSource.volume = 0f;
            }
            else
            {
                _musicAudioSource.volume = (float) _currentMusicVolume;
                _soundsAudioSource.volume = (float) _currentSoundsVolume;
            }
        }

        public void PlayClipByType(ClipType clipType)
        {
            var clip = _soundLibrary.AudioClips[clipType];
            PlaySound(clip);
        }

        private void PlayMusic()
        {
            var randomMusic = _soundLibrary.MusicClips[Random.Range(0, _soundLibrary.MusicClips.Count)];
            _musicAudioSource.clip = randomMusic;
            _musicAudioSource.Play();
        }

        private void PlaySound(AudioClip clip)
        {
            if (clip != null)
                _soundsAudioSource.PlayOneShot(clip);
        }
    }
}