using PixelRPG.Audio;
using PixelRPG.Damage;
using PixelRPG.Enemy.StateMachine;
using PixelRPG.Framework;
using UnityEngine;

namespace PixelRPG.Enemy
{
    public class EnemyDamageArea : MonoBehaviour, IDamageable
    {
        private EnemyStateMachine stateMachine;
        private SFXPlayer sfx;

        // Should have a separate script for health but Im just trowing it here
        [SerializeField] int _health;

        private void Start()
        {
            stateMachine = GetComponentInParent<EnemyStateMachine>();
            sfx = GetComponent<SFXPlayer>();
        }

        public void TakeDamage(int amount, DamageType type)
        {
            if (type == DamageType.Enemy)
                return;

            Debug.Log("Enemy taking damage");
            sfx.Play();

            _health -= amount;
            if (_health <= 0)
                Kill();
        }

        private void Kill()
        {
            Core.EnemySpawner.AddKilledEnemy(stateMachine.SpawnPoint);
            stateMachine.ChangeState(3);
        }
    }
}
