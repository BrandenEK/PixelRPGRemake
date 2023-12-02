using PixelRPG.Audio;
using UnityEngine;

namespace PixelRPG.Framework
{
    public class MusicPlayer : GameSystem
    {
        public void PlayBackgroundMusic(MusicTrack music)
        {
            if (_lastBackgroundMusic == music.name)
                return;

            StartAudioSource(_backgroundPlayer, music);
            _lastBackgroundMusic = music.name;
        }

        public void PlaySoundEffect(MusicTrack music)
        {
            AudioSource player = null;
            float mostTime = 0;

            foreach (AudioSource source in _sfxPlayers)
            {
                // If this sfx player is not playing, immediately use it
                if (source.time == 0)
                {
                    player = source;
                    break;
                }

                // Otherwise, if this sfx player is further along, possibly use it
                if (source.time > mostTime)
                {
                    player = source;
                    mostTime = source.time;
                }
            }

            Debug.Log($"Playing sfx through {player.name}");
            StartAudioSource(player, music);
        }

        private void StartAudioSource(AudioSource source, MusicTrack music)
        {
            source.Stop();
            source.volume = (music.isBackground ? BACKGROUND_VOLUME : SFX_VOLUME) * music.volume;
            source.time = music.startTime;
            source.clip = music.clip;
            source.Play();
        }

        private string _lastBackgroundMusic = string.Empty;

        [SerializeField] AudioSource _backgroundPlayer;
        [SerializeField] AudioSource[] _sfxPlayers;

        private const float BACKGROUND_VOLUME = 0.3f;
        private const float SFX_VOLUME = 1f;
    }
}
