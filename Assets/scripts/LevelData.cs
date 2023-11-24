using PixelRPG.Framework;
using UnityEngine;

namespace PixelRPG
{
    public class LevelData : MonoBehaviour
    {
        [SerializeField] string _name;

        private AudioSource music;

        private void Start()
        {
            music = gameObject.GetComponent<AudioSource>();

            float time = Core.MusicPlayer.StartBackgroundMusic(_name);
            music.time = time;
            music.Play();
        }

        public void FakeLevelUnloaded()
        {
            Core.MusicPlayer.StopBackgroundMusic(music.time);
        }
    }
}
