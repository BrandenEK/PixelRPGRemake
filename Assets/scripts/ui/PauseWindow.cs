using PixelRPG.Framework;
using UnityEngine;

namespace PixelRPG.UI
{
    public class PauseWindow : BaseWindow
    {
        public void ResumeGame()
        {
            Core.StateChanger.UnpauseGame();
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
    }
}
