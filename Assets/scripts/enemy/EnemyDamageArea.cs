using PixelRPG.Damage;
using PixelRPG.Enemy.StateMachine;
using UnityEngine;

namespace PixelRPG.Enemy
{
    public class EnemyDamageArea : MonoBehaviour, IDamageable
    {
        private EnemyStateMachine stateMachine;

        private void Start()
        {
            stateMachine = GetComponentInParent<EnemyStateMachine>();
        }

        public void TakeDamage(int amount, DamageType type)
        {
            if (type == DamageType.Enemy)
                return;

            Debug.Log("Enemy taking damage");
            stateMachine.ChangeState(3);
        }
    }
}
