using PixelRPG.Enemy.StateMachine;
using UnityEngine;

namespace PixelRPG.Enemy
{
    public class EnemyEventReceiver : MonoBehaviour
    {
        private EnemyStateMachine _stateMachine;

        private void Start()
        {
            _stateMachine = GetComponentInParent<EnemyStateMachine>();
        }

        public void ReceiveEvent(string e)
        {
            _stateMachine.SendAnimationEvent(e);
        }
    }
}
