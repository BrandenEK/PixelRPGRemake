using PixelRPG.UI;
using UnityEngine;

namespace PixelRPG.Framework
{
    public class UIDisplayer : GameSystem
    {
        [SerializeField] GameObject _interactPopup;
        [SerializeField] GameObject _hud;

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

            HideWindow(WindowType.Death);
            HideWindow(WindowType.Pause);
        }

        public void ShowWindow(WindowType type)
        {
            BaseWindow window = GetWindowByType(type);
            window.gameObject.SetActive(true);
            window.ShowWindow();
        }

        public void HideWindow(WindowType type)
        {
            BaseWindow window = GetWindowByType(type);
            window.HideWindow();
            window.gameObject.SetActive(false);
        }

        private BaseWindow GetWindowByType(WindowType type)
        {
            return type switch
            {
                WindowType.Death => _deathWindow,
                WindowType.Pause => _pauseWindow,
                _ => throw new System.Exception("Invalid window type!")
            };
        }
    }
}
