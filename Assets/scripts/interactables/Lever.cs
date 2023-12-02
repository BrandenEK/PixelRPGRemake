using PixelRPG.Persistence;
using UnityEngine;

namespace PixelRPG.Interactables
{
    public class Lever : MonoBehaviour, IInteractable, IPersistentObject
    {
        public void Interact()
        {
            Debug.Log("Toggling lever");
            anim.SetTrigger("triggering");
            _active = !_active;
        }

        public Vector3 PopupPosition => transform.position + Vector3.up * 0.8f;

        public bool IsInteractable => !_active || !_singleUse;

        public bool CurrentStatus
        {
            get => _active;
            set
            {
                if (!_persistState)
                    return;

                _active = value;
                anim.SetTrigger(value ? "right" : "left");
            }
        }

        public int SceneIndex => _sceneIndex;

        [SerializeField] int _sceneIndex;
        [SerializeField] bool _persistState;
        [SerializeField] bool _singleUse;
        private bool _active;

        private Animator anim;

        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
        }
    }
}
