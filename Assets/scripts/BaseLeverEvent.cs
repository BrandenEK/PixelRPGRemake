using PixelRPG.Interactables;
using UnityEngine;

namespace PixelRPG
{
    public abstract class BaseLeverEvent : MonoBehaviour
    {
        private void OnLeverToggled(Lever lever, bool active)
        {
            if (lever == _lever && (active || !_requireActive))
                OnLeverToggled();
        }

        protected abstract void OnLeverToggled();

        [SerializeField] Lever _lever;
        [SerializeField] bool _requireActive;

        private void OnEnable() => Lever.OnLeverToggled += OnLeverToggled;
        private void OnDisable() => Lever.OnLeverToggled -= OnLeverToggled;
    }
}
