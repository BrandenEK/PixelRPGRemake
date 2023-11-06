using UnityEngine;

namespace PixelRPG.Framework
{
    public class StateChanger : GameSystem
    {
        private bool _paused;

        public bool IsPaused => _paused;

        public override void OnUpdate()
        {
            if (Input.GetButtonDown("Pause"))
            {
                if (_paused)
                    UnpauseGame();
                else
                    PauseGame();
            }
        }

        public void PauseGame()
        {
            _paused = true;
            Time.timeScale = 0;
            Core.PlayerSpawner.PlayerInput.AddInputBlock("pause");
            Core.UIDisplayer.ShowWindow(UI.WindowType.Pause);
        }

        public void UnpauseGame()
        {
            _paused = false;
            Time.timeScale = 1;
            Core.PlayerSpawner.PlayerInput.RemoveInputBlock("pause");
            Core.UIDisplayer.HideWindow(UI.WindowType.Pause);
        }
    }
}
