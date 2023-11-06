using UnityEngine;

namespace PixelRPG.UI
{
    public abstract class BaseWindow : MonoBehaviour
    {
        private bool _isOpen;

        public void ShowWindow()
        {
            if (!_isOpen)
            {
                Debug.Log("Opening window: " + GetType().Name);
                OnShow();
                _isOpen = true;
            }
        }

        public void HideWindow()
        {
            if (_isOpen)
            {
                Debug.Log("Closing window: " + GetType().Name);
                OnHide();
                _isOpen = false;
            }
        }

        protected virtual void OnShow() { }

        protected virtual void OnHide() { }
    }
}
