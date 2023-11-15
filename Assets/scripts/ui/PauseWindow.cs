using PixelRPG.Framework;
using PixelRPG.Input;
using UnityEngine;

namespace PixelRPG.UI
{
    public class PauseWindow : BaseWindow
    {
        [SerializeField] GameObject[] _sections;

        private int _currentSection = 0;

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
            Core.InputHandler.AddInputBlock(pauseBlock);
            OpenSection(_currentSection);
        }

        protected override void OnHide()
        {
            Time.timeScale = 1;
            Core.InputHandler.RemoveInputBlock(pauseBlock);
        }

        protected override void OnUpdate()
        {
            if (Core.InputHandler.GetButtonDown(InputType.Pause))
            {
                if (IsOpen)
                    UnpauseGame();
                else
                    PauseGame();
            }
        }

        private void OpenSection(int section)
        {
            foreach (var obj in _sections)
            {
                obj.SetActive(false);
            }

            _currentSection = section;
            _sections[section].SetActive(true);
        }

        private readonly InputBlock pauseBlock = new InputBlock(new InputType[]
        {
            InputType.Attack, InputType.Interact, InputType.MoveHorizontal, InputType.MoveVertical,
        });
    }
}
