using PixelRPG.Framework;
using UnityEngine;

namespace PixelRPG.UI
{
    public class PauseWindow : BaseWindow
    {
        public void PauseGame()
        {
            Core.UIDisplayer.ShowWindow(this);
        }

        public void UnpauseGame()
        {
            Core.UIDisplayer.HideWindow(this);
        }

        public void SaveAndQuit()
        {
            Debug.Log("Save and quit to menu");

            Core.LevelChanger.StoreLevelObjects();
            Core.DataSaver.SaveGame();
            ReturnToMenu();
        }

        public void ReturnToMenu()
        {
            Debug.Log("Returning to menu");
            Core.LevelChanger.ChangeLevel("MainMenu", false);
        }

        protected override void OnShow()
        {
            Time.timeScale = 0;
            Core.PlayerSpawner.PlayerInput.AddInputBlock("pause");
        }

        protected override void OnHide()
        {
            Time.timeScale = 1;
            Core.PlayerSpawner.PlayerInput.RemoveInputBlock("pause");
        }

        protected override void OnUpdate()
        {
            if (Input.GetButtonDown("Pause"))
            {
                if (IsOpen)
                    UnpauseGame();
                else
                    PauseGame();
            }
        }
    }
}
