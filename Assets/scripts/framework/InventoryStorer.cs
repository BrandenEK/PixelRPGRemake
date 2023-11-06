using PixelRPG.Persistence;

namespace PixelRPG.Framework
{
    public class InventoryStorer : GameSystem, IPersistentSystem
    {
        private int _weaponLevel;
        private int _armorLevel;

        public int CurrentWeapon => _weaponLevel;
        public int CurrentArmor => _armorLevel;

        public void UpgradeWeapon()
        {
            _weaponLevel++;
            OnWeaponUpgraded?.Invoke(_weaponLevel);
        }

        public void UpgradeArmor()
        {
            _armorLevel++;
            OnArmorUpgraded?.Invoke(_armorLevel);
        }

        public SaveData SaveData()
        {
            return new InventorySaveData()
            {
                weaponLevel = _weaponLevel,
                armorLevel = _armorLevel
            };
        }

        public void LoadData(SaveData data)
        {
            var inventoryData = data as InventorySaveData;
            _weaponLevel = inventoryData.weaponLevel;
            _armorLevel = inventoryData.armorLevel;
        }

        public void ResetData()
        {
            _weaponLevel = 0;
            _armorLevel = 0;
        }

        public delegate void WeaponDelegate(int level);
        public static event WeaponDelegate OnWeaponUpgraded;

        public delegate void ArmorDelegate(int level);
        public static event ArmorDelegate OnArmorUpgraded;
    }
}
