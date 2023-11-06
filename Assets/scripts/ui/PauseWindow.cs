using PixelRPG.Framework;
using PixelRPG.Input;
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
            Core.InputHandler.AddInputBlock(pauseBlock);
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

        private readonly InputBlock pauseBlock = new InputBlock(new InputType[]
        {
            InputType.Attack, InputType.Interact, InputType.MoveHorizontal, InputType.MoveVertical,
        });
    }
}
