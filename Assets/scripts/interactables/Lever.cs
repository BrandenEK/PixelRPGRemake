using UnityEngine;

namespace PixelRPG.Interactables
{
    public class Lever : MonoBehaviour, IInteractable
    {
        private Animator anim;

        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
        }

        public void Interact()
        {
            Debug.Log("Toggling lever");
            anim.SetTrigger("triggering");
        }

        public Vector3 PopupPosition => transform.position + Vector3.up * 0.8f;

        public bool IsInteractable => true;
    }
}
