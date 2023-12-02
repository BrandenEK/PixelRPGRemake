using PixelRPG.Framework;
using UnityEngine;

namespace PixelRPG.Audio
{
    public class BackgroundMusicPlayer : MonoBehaviour
    {
        void Start()
        {
            Core.MusicPlayer.PlayBackgroundMusic(music);
        }

        [SerializeField] MusicTrack music;
    }
}
