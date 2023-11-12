using PixelRPG.Framework;
using UnityEngine;

namespace PixelRPG.UI
{
    public class MainMenuWindow : BaseWindow
    {
        public void NewGame()
        {
            Core.DataSaver.ResetGame();
            Core.PlayerSpawner.SpawnFromLastSave();
        }

        public void ContinueGame()
        {
            if (!Core.DataSaver.SaveFileExists)
            {
                Debug.LogWarning("No save file exists!");
                return;
            }

            Core.DataSaver.LoadGame();
            Core.PlayerSpawner.SpawnFromLastSave();
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
