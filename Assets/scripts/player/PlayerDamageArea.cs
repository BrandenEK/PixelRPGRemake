using PixelRPG.Damage;
using UnityEngine;

namespace PixelRPG.Player
{
    public class PlayerDamageArea : MonoBehaviour, IDamageable
    {
        private PlayerHealth health;

        void Start()
        {
            health = GetComponentInParent<PlayerHealth>();
        }

        public void TakeDamage(int amount, DamageType type)
        {
            if (type == DamageType.Player)
                return;

            Debug.Log("Player taking damage: " + amount);
            health.TakeDamage(amount);
        }
    }
}
