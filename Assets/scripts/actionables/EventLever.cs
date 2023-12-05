using PixelRPG.Interactables;
using UnityEngine;

namespace PixelRPG.Actionables
{
    public class EventLever : EventBase
    {
        private void OnLeverToggled(Lever lever, bool active)
        {
            if (lever == _lever && (active || !_requireActive))
                TriggerEvent();
        }

        [SerializeField] Lever _lever;
        [SerializeField] bool _requireActive;

        private void OnEnable() => Lever.OnLeverToggled += OnLeverToggled;
        private void OnDisable() => Lever.OnLeverToggled -= OnLeverToggled;
    }
}
