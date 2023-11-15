using PixelRPG.UI;
using UnityEngine;

namespace PixelRPG.Framework
{
    public class UIDisplayer : GameSystem
    {
        [SerializeField] GameObject _interactPopup;
        [SerializeField] GameObject _hud;

        [SerializeField] MainMenuWindow _mainMenuWindow;
        [SerializeField] DeathWindow _deathWindow;
        [SerializeField] PauseWindow _pauseWindow;

        public void ShowInteractPopup(Vector3 position, bool canInteract)
        {
            _interactPopup.SetActive(true);
            _interactPopup.transform.position = UnityEngine.Camera.main.WorldToScreenPoint(position);
            _interactPopup.transform.GetChild(0).gameObject.SetActive(!canInteract);
        }

        public void HideInteractPopup()
        {
            _interactPopup.SetActive(false);
        }

        public void EnableHud() => _hud.SetActive(true);

        public void DisableHud() => _hud.SetActive(false);

        public override void OnSceneLoaded(string sceneName)
        {
            EnableHud();
            HideWindow(_mainMenuWindow);
        }

        public override void OnSceneUnloaded(string sceneName)
        {
            HideWindow(_mainMenuWindow);
            HideWindow(_deathWindow);
            HideWindow(_pauseWindow);
        }

        public override void OnMenuLoaded()
        {
            DisableHud();
            ShowWindow(WindowType.MainMenu);
            _pauseWindow.ResetCurrentSection();
        }

        public override void OnUpdate()
        {
            _mainMenuWindow.UpdateWindow();
            _deathWindow.UpdateWindow();
            _pauseWindow.UpdateWindow();
        }

        public void ShowWindow(WindowType type) => ShowWindow(GetWindowByType(type));

        public void ShowWindow(BaseWindow window)
        {
            window.gameObject.SetActive(true);
            window.ShowWindow();
        }

        public void HideWindow(WindowType type) => HideWindow(GetWindowByType(type));

        public void HideWindow(BaseWindow window)
        {
            window.HideWindow();
            window.gameObject.SetActive(false);
        }

        private BaseWindow GetWindowByType(WindowType type)
        {
            return type switch
            {
                WindowType.MainMenu => _mainMenuWindow,
                WindowType.Death => _deathWindow,
                WindowType.Pause => _pauseWindow,
                _ => throw new System.Exception("Invalid window type!")
            };
        }
    }
}
