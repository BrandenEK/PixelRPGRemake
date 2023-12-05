using PixelRPG.Framework;
using UnityEngine;

namespace PixelRPG
{
    public class ArmorSwitcher : MonoBehaviour
    {
        private void Start()
        {
            UpdateArmor(Core.InventoryStorer.CurrentArmor);
        }

        public void UpdateArmor(int level)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(i == level);
            }
        }

        private void OnEnable() => InventoryStorer.OnArmorUpgraded += UpdateArmor;
        private void OnDisable() => InventoryStorer.OnArmorUpgraded -= UpdateArmor;
    }
}
