using PixelRPG.Framework;
using PixelRPG.Persistence;
using UnityEngine;

namespace PixelRPG.Interactables
{
    public class LockedDoor : MonoBehaviour, IInteractable, IPersistentObject
    {
        [SerializeField] int _sceneIndex;

        private SpriteRenderer sr;
        private BoxCollider2D col;
        private AudioSource music;

        private bool _unlocked;

        public bool IsInteractable => Core.InventoryStorer.NumberOfKeys > 0;

        public Vector3 PopupPosition => transform.position;

        public bool CurrentStatus
        {
            get => _unlocked;
            set
            {
                if (value)
                {
                    Unlock();
                }
            }
        }

        public int SceneIndex => _sceneIndex;

        public void Interact()
        {
            Core.InventoryStorer.UseKey();
            music.Play();
            Unlock();
        }

        private void Unlock()
        {
            _unlocked = true;
            sr.enabled = false;
            col.enabled = false;
        }

        void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
            col = GetComponent<BoxCollider2D>();
            music = GetComponentInChildren<AudioSource>();
        }
    }
}
