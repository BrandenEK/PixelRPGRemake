using PixelRPG.Framework;
using UnityEngine;

namespace PixelRPG.Enemy.StateMachine
{
    public class ChasingState : BaseState
    {
        private float _currentCooldown;
        private readonly bool _canPatrol;

        public ChasingState(EnemyStateMachine stateMachine, bool canPatrol) : base(stateMachine)
        {
            _canPatrol = canPatrol;
        }

        public override void OnEnter()
        {
            _currentCooldown = 0f;
        }

        public override void OnUpdate()
        {
            Vector2 currentPosition = _stateMachine.Transform.position;
            Vector2 playerPosition = Core.PlayerSpawner.PlayerTransform.position;

            Vector2 direction = (playerPosition - currentPosition).normalized;
            _stateMachine.Physics.Move(direction);

            float distanceToPlayer = Vector2.Distance(currentPosition, playerPosition);
            _currentCooldown += Time.deltaTime;

            if (_canPatrol && distanceToPlayer > _stateMachine.data.chaseToPatrolDistance)
            {
                ChangeState(0);
                return;
            }

            if (distanceToPlayer < 1f)
            {
                _stateMachine.Physics.Move(Vector2.zero);

                if (_currentCooldown >= _stateMachine.data.attackCooldown)
                    ChangeState(2);

                return;
            }
        }
    }
}
