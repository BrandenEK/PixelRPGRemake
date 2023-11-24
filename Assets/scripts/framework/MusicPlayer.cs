
namespace PixelRPG.Framework
{
    public class MusicPlayer : GameSystem
    {
        public override void OnMenuLoaded()
        {
            _lastMusicName = string.Empty;
            _lastMusicTime = 0;
        }

        public float StartBackgroundMusic(string name)
        {
            if (_lastMusicName != name)
            {
                // If this is a new music track, restart time
                _lastMusicName = name;
                _lastMusicTime = 0;
            }

            return _lastMusicTime;
        }

        public void StopBackgroundMusic(float time)
        {
            _lastMusicTime = time;
        }

        public void FakeStopMusic()
        {
            // Do this better !!
            FindObjectOfType<LevelData>()?.FakeLevelUnloaded();
        }

        private string _lastMusicName = string.Empty;
        private float _lastMusicTime = 0;
    }
}
