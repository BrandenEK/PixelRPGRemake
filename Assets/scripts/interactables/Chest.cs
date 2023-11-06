using PixelRPG.Framework;
using PixelRPG.Persistence;
using UnityEngine;

namespace PixelRPG.Interactables
{
    public class Chest : MonoBehaviour, IInteractable, IPersistentObject
    {
        [SerializeField] private int _sceneIndex;

        private bool _opened;

        private Animator anim;
        private BoxCollider2D boxCollider;

        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            boxCollider = GetComponent<BoxCollider2D>();
        }

        public void Interact()
        {
            Debug.Log("Opening chest");
            anim.SetTrigger("triggering");
            boxCollider.enabled = false;
            _opened = true;

            GiveReward();
        }

        private void GiveReward()
        {
            Core.InventoryStorer.UpgradeWeapon();
        }

        public bool CurrentStatus
        {
            get => _opened;
            set
            {
                if (value)
                {
                    anim.SetTrigger("triggered");
                    boxCollider.enabled = false;
                    _opened = true;
                }
            }
        }

        public Vector3 PopupPosition => transform.position + Vector3.up * 0.8f;

        public bool IsInteractable => true;

        public int SceneIndex => _sceneIndex;
    }
}
