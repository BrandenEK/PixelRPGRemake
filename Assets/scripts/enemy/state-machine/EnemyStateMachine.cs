using UnityEngine;

namespace PixelRPG.Enemy.StateMachine
{
    public class EnemyStateMachine : MonoBehaviour
    {
        private BaseState[] _states;
        private BaseState _currentState;


        public Transform Transform { get; private set; }
        public EnemyGraphics Graphics { get; private set; }
        public EnemyPhsyics Physics { get; private set; }
        public RotateToOrientation Orientation { get; private set; }
        public EnemySpawnPoint SpawnPoint { get; private set; }

        public EnemyData data;

        private void Start()
        {
            Transform = transform;
            Graphics = GetComponent<EnemyGraphics>();
            Physics = GetComponent<EnemyPhsyics>();
            Orientation = GetComponentInChildren<RotateToOrientation>();
            SpawnPoint = GetComponentInParent<EnemySpawnPoint>();

            _states = new BaseState[]
            {
                new PatrollingState(this, SpawnPoint.wayPoints),
                new ChasingState(this),
                new AttackingState(this),
                new DeadState(this),
            };

            ChangeState(0);
        }

        private void Update() => _currentState.OnUpdate();

        public void SendAnimationEvent(string e) => _currentState.OnAnimationEvent(e);

        public void ChangeState(int idx)
        {
            _currentState?.OnExit();
            _currentState = _states[idx];
            _currentState.OnEnter();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Orientation.OffsetPosition(data.damageOffset), data.damageRadius);
        }
    }
}
