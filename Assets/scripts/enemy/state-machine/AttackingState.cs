using PixelRPG.Damage;
using UnityEngine;

namespace PixelRPG.Enemy.StateMachine
{
    public class AttackingState : BaseState
    {
        public AttackingState(EnemyStateMachine stateMachine) : base(stateMachine) { }

        public override void OnEnter()
        {
            _stateMachine.Graphics.Attack();
            ApplyDamage();
        }

        public override void OnUpdate()
        {
            _stateMachine.Physics.Move(Vector2.zero);
        }

        public override void OnAnimationEvent(string e)
        {
            if (e == "attack-finish")
            {
                ChangeState(1);
            }
        }

        private void ApplyDamage()
        {
            foreach (var collider in Physics2D.OverlapCircleAll(
                _stateMachine.Orientation.OffsetPosition(_stateMachine.data.damageOffset),
                _stateMachine.data.damageRadius,
                _stateMachine.data.damageLayer))
            {
                if (collider.TryGetComponent<IDamageable>(out var damageable))
                    damageable.TakeDamage(_stateMachine.data.damageAmount, DamageType.Enemy);
            }
        }
    }
}
