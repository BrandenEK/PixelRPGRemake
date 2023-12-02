using PixelRPG.Audio;
using PixelRPG.Framework;
using PixelRPG.Persistence;
using System.Collections;
using UnityEngine;

namespace PixelRPG.Interactables
{
    public class Chest : MonoBehaviour, IInteractable, IPersistentObject
    {
        [SerializeField] int _sceneIndex;
        [SerializeField] ChestRewardType _reward;
        [SerializeField] float _displayTime;

        private bool _opened;

        private Animator anim;
        private BoxCollider2D boxCollider;
        private Transform itemDisplay;
        private SFXPlayer music;

        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            boxCollider = GetComponent<BoxCollider2D>();
            itemDisplay = transform.GetChild(1);
            music = GetComponentInChildren<SFXPlayer>();
        }

        public void Interact()
        {
            Debug.Log("Opening chest");
            anim.SetTrigger("triggering");
            music.Play();
            boxCollider.enabled = false;
            _opened = true;

            GiveReward();
        }

        private void GiveReward()
        {
            Sprite icon;

            switch (_reward)
            {
                case ChestRewardType.Weapon:
                    Core.InventoryStorer.UpgradeWeapon();
                    icon = Core.InventoryStorer.CurrentWeaponItem.icon;
                    break;
                case ChestRewardType.Armor:
                    Core.InventoryStorer.UpgradeArmor();
                    icon = Core.InventoryStorer.CurrentArmorItem.icon;
                    break;
                case ChestRewardType.Key:
                    Core.InventoryStorer.ObtainKey();
                    icon = Core.InventoryStorer.GetEquipmentItem("KEY").icon;
                    break;
                default:
                    throw new System.Exception("Invalid chest reward type");
            }

            StartCoroutine(DisplayReward(icon));
        }

        private IEnumerator DisplayReward(Sprite icon)
        {
            itemDisplay.GetComponent<SpriteRenderer>().sprite = icon;
            float startPos = itemDisplay.position.y;
            float endPos = itemDisplay.position.y + 1f;

            float startTime = Time.time;
            while (itemDisplay.position.y < endPos)
            {
                yield return new WaitForEndOfFrame();
                float percent = (Time.time - startTime) / _displayTime;
                itemDisplay.position = new Vector3(itemDisplay.position.x, startPos + percent);
            }

            yield return new WaitForSeconds(1f);
            itemDisplay.gameObject.SetActive(false);
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
