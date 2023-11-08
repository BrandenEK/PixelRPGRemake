using UnityEngine;

namespace PixelRPG.Inventory
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Equipment Item")]
    public class EquipmentItem : ScriptableObject
    {
        public string displayName;
        public Sprite icon;
    }
}
