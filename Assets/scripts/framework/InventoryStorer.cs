using PixelRPG.Inventory;
using PixelRPG.Persistence;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PixelRPG.Framework
{
    public class InventoryStorer : GameSystem, IPersistentSystem
    {
        private int _weaponLevel;
        private int _armorLevel;
        private int _currentKeys;

        public int CurrentWeapon => _weaponLevel;
        public EquipmentItem CurrentWeaponItem => _items["WE0" + (_weaponLevel + 1)];

        public int CurrentArmor => _armorLevel;
        public EquipmentItem CurrentArmorItem => _items["AM0" + (_armorLevel + 1)];

        private readonly Dictionary<string, EquipmentItem> _items = new();

        public override void OnInitialize()
        {
            LoadAllItems();
        }

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

        // Asset loading

        private void LoadAllItems()
        {
            foreach (var item in Resources.LoadAll<EquipmentItem>("Inventory"))
            {
                _items.Add(item.name, item);
            }
            Debug.Log($"Loaded {_items.Count} equipment items!");
        }

        public EquipmentItem GetEquipmentItem(string id) => _items[id];

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

        public delegate void InventoryDelegate(int level);
        public static event InventoryDelegate OnWeaponUpgraded;
        public static event InventoryDelegate OnArmorUpgraded;
    }
}
