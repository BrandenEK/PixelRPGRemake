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

                    text.text = "Lvl: " + (Core.InventoryStorer.CurrentWeapon + 1);
                    break;
                case 1:

                    text.text = "Lvl: " + (Core.InventoryStorer.CurrentArmor + 1);
                    break;
                case 2:

                    text.text = "x" + Core.InventoryStorer.NumberOfKeys;
                    break;
                default:
                    throw new System.Exception("Invalid equipment type!");
            }
        }
    }
}
