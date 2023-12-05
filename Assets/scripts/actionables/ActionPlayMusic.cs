using PixelRPG.Audio;
using PixelRPG.Framework;
using UnityEngine;

namespace PixelRPG.Actionables
{
    public class ActionPlayMusic : MonoBehaviour, IActionable
    {
        public void Activate()
        {
            if (_music.isBackground)
                Core.MusicPlayer.PlayBackgroundMusic(_music);
            else
                Core.MusicPlayer.PlaySoundEffect(_music);
        }

        [SerializeField] MusicTrack _music;
    }
}
