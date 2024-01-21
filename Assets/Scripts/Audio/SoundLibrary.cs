using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Audio
{
    [CreateAssetMenu(fileName = "SoundLibrary", menuName = "Configs/SoundLibrary")]
    public class SoundLibrary : ScriptableObject
    {
        public List<AudioClip> MusicClips = new();
        public List<AudioData> AudioClips = new();
    }

    [Serializable]
    public class AudioData
    {
        public ClipType ClipType;
        public AudioClip AudioClip;
    }
}