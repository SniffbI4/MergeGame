using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Scripts.Audio
{
    [CreateAssetMenu(fileName = "SoundLibrary", menuName = "Configs/SoundLibrary")]
    public class SoundLibrary : SerializedScriptableObject
    {
        public List<AudioClip> MusicClips = new();
        public Dictionary<ClipType, AudioClip> AudioClips = new();
    }
}