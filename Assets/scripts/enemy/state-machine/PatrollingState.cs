using PixelRPG.Framework;
using UnityEngine;

namespace PixelRPG.Enemy.StateMachine
{
    public class PatrollingState : BaseState
    {
        public const float TARGET_CUTOFF = 0.3f;

        private readonly Vector2[] _waypoints;

        private int _currentWayPoint;
        private bool _increaseWaypoint;

        public PatrollingState(EnemyStateMachine stateMachine, Vector2[] waypoints) : base(stateMachine)
        {
            _waypoints = waypoints;
        }

        public override void OnEnter()
        {
            _currentWayPoint = FindClosestWaypoint();
            _increaseWaypoint = true;
        }

        public override void OnUpdate()
        {
            Vector2 currentPosition = _stateMachine.Transform.position;
            Vector2 targetPosition = _waypoints[_currentWayPoint];
            Vector2 playerPosition = Core.PlayerSpawner.PlayerTransform.position;

            Vector2 direction = (targetPosition - currentPosition).normalized;
            _stateMachine.Physics.Move(direction);

            if (Vector2.Distance(currentPosition, targetPosition) < TARGET_CUTOFF)
            {
                ChangeToNextWaypoint();
            }

            if (Vector2.Distance(currentPosition, playerPosition) < _stateMachine.data.patrolToChaseDistance)
            {
                ChangeState(1);
            }
        }

        private int FindClosestWaypoint()
        {
            int minIdx = 0;
            float minDistance = 1000f;
            
            for (int i = 1; i < _waypoints.Length; i++)
            {
                float distance = Vector2.Distance(_stateMachine.Transform.position, _waypoints[i]);
                if (distance < minDistance)
                {
                    minIdx = i;
                    minDistance = distance;
                }
            }

            return minIdx;
        }

        private void ChangeToNextWaypoint()
        {
            _currentWayPoint += _increaseWaypoint ? 1 : -1;
            if (_currentWayPoint < 0 || _currentWayPoint >= _waypoints.Length)
            {
                _increaseWaypoint = !_increaseWaypoint;
                _currentWayPoint += _increaseWaypoint ? 2 : -2;
            }
        }
    }
}
