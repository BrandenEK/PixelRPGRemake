using PixelRPG.Persistence;
using UnityEngine;

namespace PixelRPG.Interactables
{
    public class Campfire : MonoBehaviour, IInteractable, IPersistentObject
    {
        [SerializeField] int _sceneIndex;

        private bool _isLit;

        public bool IsInteractable => true;

        public Vector3 PopupPosition => transform.position + Vector3.up * 0.7f;

        public bool CurrentStatus
        {
            get => _isLit;
            set
            {
                if (value)
                {
                    _isLit = true;
                    ShowFire(false);
                }
            }
        }

        public int SceneIndex => _sceneIndex;

        public void Interact()
        {
            _isLit = true;
            ShowFire(true);
            // Refill player health
            // Set save spawn
        }

        private void ShowFire(bool start)
        {
            fireSystem.Play();
            smokeSystem.Play();
            fireSystem.time = start ? 0 : 3;
            smokeSystem.time = start ? 0 : 3;
        }

        private void Awake()
        {
            fireSystem = transform.GetChild(1).GetComponent<ParticleSystem>();
            smokeSystem = transform.GetChild(2).GetComponent<ParticleSystem>();
        }

        private ParticleSystem fireSystem;
        private ParticleSystem smokeSystem;
    }
}