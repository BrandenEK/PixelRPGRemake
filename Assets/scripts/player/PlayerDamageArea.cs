using PixelRPG.Audio;
using PixelRPG.Damage;
using UnityEngine;

namespace PixelRPG.Player
{
    public class PlayerDamageArea : MonoBehaviour, IDamageable
    {
        private PlayerHealth health;
        private SFXPlayer sfx;

        void Start()
        {
            health = GetComponentInParent<PlayerHealth>();
            sfx = GetComponent<SFXPlayer>();
        }

        public void TakeDamage(int amount, DamageType type)
        {
            if (type == DamageType.Player)
                return;

            Debug.Log("Player taking damage: " + amount);
            health.TakeDamage(amount);
            sfx.Play();
        }
    }
}
