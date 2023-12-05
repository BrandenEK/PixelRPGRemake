using PixelRPG.Framework;
using PixelRPG.Input;
using UnityEngine;
using UnityEngine.UI;

namespace PixelRPG.UI
{
    public class PauseWindow : BaseWindow
    {
        [SerializeField] GameObject[] _sections;
        [SerializeField] Image[] _tabs;

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

            Core.DataSaver.SaveGame();
            ReturnToMenu();
        }

        public void ReturnToMenu()
        {
            Debug.Log("Returning to menu");
            Core.LevelChanger.ChangeLevel("MainMenu", false);
        }

        public void ResetCurrentSection()
        {
            _currentSection = 0;
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
            // Handle pause and unpause
            if (Core.InputHandler.GetButtonDown(InputType.Pause))
            {
                if (IsOpen)
                    UnpauseGame();
                else
                    PauseGame();
            }

            if (!IsOpen)
                return;

            // Handle tab controls
            if (Core.InputHandler.GetButtonDown(InputType.UITabLeft))
                OpenSection(_currentSection - 1);
            else if (Core.InputHandler.GetButtonDown(InputType.UITabRight))
                OpenSection(_currentSection + 1);
        }

        private void OpenSection(int section)
        {
            if (section < 0 || section >= _sections.Length)
                return;

            foreach (var obj in _sections)
            {
                obj.SetActive(false);
            }
            foreach (var tab in _tabs)
            {
                tab.color = TAB_INACTIVE;
            }

            _currentSection = section;
            _sections[section].SetActive(true);
            _tabs[section].color = TAB_ACTIVE;
        }

        private readonly InputBlock pauseBlock = new InputBlock(new InputType[]
        {
            InputType.Attack, InputType.Interact, InputType.MoveHorizontal, InputType.MoveVertical,
        });

        private static readonly Color TAB_ACTIVE = new Color((float)106 / 255, (float)106 / 255, (float)106 / 255);
        private static readonly Color TAB_INACTIVE = new Color((float)164 / 255, (float)164 / 255, (float)164 / 255);
    }
}
