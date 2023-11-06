using PixelRPG.Framework;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PixelRPG.UI
{
    public class UIMainMenu : MonoBehaviour
    {
        [SerializeField] GameObject _newGameButton;

        private void Start()
        {
            Core.UIDisplayer.DisableHud();
        }

        public void NewGame()
        {
            Core.DataSaver.ResetGame();
            Core.PlayerSpawner.SpawnFromLastSave();
        }

        public void ContinueGame()
        {
            Core.DataSaver.LoadGame();
            Core.PlayerSpawner.SpawnFromLastSave();
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
