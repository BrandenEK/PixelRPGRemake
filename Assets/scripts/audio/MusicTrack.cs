using UnityEngine;

namespace PixelRPG.Audio
{
    [CreateAssetMenu(fileName = "New music", menuName = "Music Track")]
    public class MusicTrack : ScriptableObject
    {
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume = 1;
        public float startTime = 0;
        public bool isBackground = false;
    }
}
