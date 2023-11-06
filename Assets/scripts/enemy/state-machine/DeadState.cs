using UnityEngine;

namespace PixelRPG.Enemy.StateMachine
{
    public class DeadState : BaseState
    {
        public DeadState(EnemyStateMachine stateMachine) : base(stateMachine) { }

        public override void OnEnter()
        {
            Debug.Log("Entering dead state");

            _stateMachine.Graphics.Die();

            foreach (var collision in _stateMachine.GetComponentsInChildren<BoxCollider2D>())
                collision.enabled = false;
            foreach (var sorter in _stateMachine.GetComponentsInChildren<SortingOrderAdjuster>())
                sorter.PermanentlyMoveBehind();
        }

        public override void OnUpdate()
        {
            _stateMachine.Physics.Move(Vector2.zero);
        }
    }
}
