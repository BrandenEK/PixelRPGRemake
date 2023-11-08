using PixelRPG.Framework;
using PixelRPG.Persistence;
using UnityEngine;

namespace PixelRPG.Interactables
{
    public class Chest : MonoBehaviour, IInteractable, IPersistentObject
    {
        [SerializeField] int _sceneIndex;
        [SerializeField] ChestRewardType _reward;

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
            switch (_reward)
            {
                case ChestRewardType.Weapon:
                    Core.InventoryStorer.UpgradeWeapon();
                    break;
                case ChestRewardType.Armor:
                    Core.InventoryStorer.UpgradeArmor();
                    break;
                case ChestRewardType.Key:
                    Core.InventoryStorer.ObtainKey();
                    break;
            }
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

        public enum ChestRewardType
        {
            Weapon,
            Armor,
            Key,
        }
    }
}
