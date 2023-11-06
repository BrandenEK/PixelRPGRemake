using UnityEngine;

namespace PixelRPG.Interactables
{
    public interface IInteractable
    {
        public void Interact();

        public bool IsInteractable { get; }

        public Vector3 PopupPosition { get; }
    }
}
