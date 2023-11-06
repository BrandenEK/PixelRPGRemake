using PixelRPG.Framework;
using PixelRPG.Interactables;
using UnityEngine;

namespace PixelRPG.Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] LayerMask _interactLayer;
        [SerializeField] float _interactOffset;
        [SerializeField] float _interactRadius;

        private PlayerInput input;
        private RotateToOrientation orientation;

        private readonly Collider2D[] _colliders = new Collider2D[1];
        
        void Start()
        {
            input = GetComponent<PlayerInput>();
            orientation = GetComponentInChildren<RotateToOrientation>();
        }

        void Update()
        {
            // No interactables in range
            if (Physics2D.OverlapCircleNonAlloc(orientation.OffsetPosition(_interactOffset), _interactRadius, _colliders, _interactLayer) < 1)
            {
                Core.UIDisplayer.HideInteractPopup();
                return;
            }

            // Display popup at interactable position
            IInteractable interactable = _colliders[0].GetComponent<IInteractable>();
            Core.UIDisplayer.ShowInteractPopup(interactable.PopupPosition, interactable.IsInteractable);

            // Interact button pressed
            if (input.InteractButton && interactable.IsInteractable)
                interactable.Interact();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(orientation.OffsetPosition(_interactOffset), _interactRadius);
        }
    }
}
