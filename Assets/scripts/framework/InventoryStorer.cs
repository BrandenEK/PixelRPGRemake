using PixelRPG.Persistence;
using System;

namespace PixelRPG.Framework
{
    public class InventoryStorer : GameSystem, IPersistentSystem
    {
        private int _weaponLevel;
        private int _armorLevel;
        private int _currentKeys;

        public int CurrentWeapon => _weaponLevel;
        public int CurrentArmor => _armorLevel;

        // Weapon and armor

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

        // Keys

        public void ObtainKey() => _currentKeys++;

        public void UseKey() => _currentKeys = Math.Max(_currentKeys - 1, 0);

        public int NumberOfKeys => _currentKeys;

        // Persistence

        public SaveData SaveData()
        {
            return new InventorySaveData()
            {
                weaponLevel = _weaponLevel,
                armorLevel = _armorLevel,
                currentKeys = _currentKeys,
            };
        }

        public void LoadData(SaveData data)
        {
            var inventoryData = data as InventorySaveData;
            _weaponLevel = inventoryData.weaponLevel;
            _armorLevel = inventoryData.armorLevel;
            _currentKeys = inventoryData.currentKeys;
        }

        public void ResetData()
        {
            _weaponLevel = 0;
            _armorLevel = 0;
            _currentKeys = 0;
        }

        public delegate void WeaponDelegate(int level);
        public static event WeaponDelegate OnWeaponUpgraded;

        public delegate void ArmorDelegate(int level);
        public static event ArmorDelegate OnArmorUpgraded;
    }
}
