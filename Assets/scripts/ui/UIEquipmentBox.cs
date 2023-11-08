using PixelRPG.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PixelRPG.UI
{
    public class UIEquipmentBox : MonoBehaviour
    {
        private Image icon;
        private TMP_Text text;

        [SerializeField] int _equipmentType;

        void Start()
        {
            icon = transform.GetChild(0).GetComponent<Image>();
            text = transform.GetChild(1).GetComponent<TMP_Text>();
        }

        private void Update()
        {
            switch (_equipmentType)
            {
                case 0:
                    var weapon = Core.InventoryStorer.CurrentWeaponItem;
                    icon.sprite = weapon.icon;
                    text.text = weapon.displayName;
                    break;
                case 1:
                    var armor = Core.InventoryStorer.CurrentArmorItem;
                    icon.sprite = armor.icon;
                    text.text = armor.displayName;
                    break;
                case 2:
                    var key = Core.InventoryStorer.GetEquipmentItem("KEY");
                    icon.sprite = key.icon;
                    text.text = "x " + Core.InventoryStorer.NumberOfKeys;
                    break;
                default:
                    throw new System.Exception("Invalid equipment type!");
            }
        }
    }
}
