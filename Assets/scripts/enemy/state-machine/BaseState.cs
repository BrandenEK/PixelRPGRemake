using UnityEngine;

namespace PixelRPG.Enemy.StateMachine
{
    public abstract class BaseState
    {
        protected readonly EnemyStateMachine _stateMachine;

        public BaseState(EnemyStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        protected void ChangeState(int idx) => _stateMachine.ChangeState(idx);

        public virtual void OnEnter() { }

        public virtual void OnUpdate() { }

        public virtual void OnAnimationEvent(string e) { }

        public virtual void OnExit() { }

        //public Vector3 CurrentPosition => _stateMachine.CurrentPosition;
        //public Vector3 PlayerPosition => _stateMachine.PlayerPosition;
        //public Rigidbody2D Physics => _stateMachine.Physics;
        //public Animator Animator => _stateMachine.Animator;
    }
}
