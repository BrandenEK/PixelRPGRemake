using PixelRPG.Framework;
using UnityEngine;

namespace PixelRPG.Audio
{
    public class SFXPlayer : MonoBehaviour
    {
        public void Play()
        {
            Core.MusicPlayer.PlaySoundEffect(music);
        }

        [SerializeField] MusicTrack music;
    }
}
