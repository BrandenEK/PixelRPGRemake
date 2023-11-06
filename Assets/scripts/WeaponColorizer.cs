using PixelRPG.Framework;
using UnityEngine;

namespace PixelRPG
{
    public class WeaponColorizer : MonoBehaviour
    {
        private SpriteRenderer sr;

        private void OnEnable() => InventoryStorer.OnWeaponUpgraded += Colorize;
        private void OnDisable() => InventoryStorer.OnWeaponUpgraded -= Colorize;

        private void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            Colorize(Core.InventoryStorer.CurrentWeapon);
        }

        private void Colorize(int level)
        {
            if (level < 0 || level >= _weaponColors.Length)
            {
                Debug.LogError("Invalid weapon level");
                return;
            }

            Debug.Log("Setting weapon color for level " +  level);
            sr.color = _weaponColors[level];
        }

        private readonly Color[] _weaponColors = new Color[]
        {
            Color.white, Color.magenta, Color.cyan, Color.red
        };
    }
}
