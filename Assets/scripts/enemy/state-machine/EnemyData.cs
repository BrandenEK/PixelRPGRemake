using UnityEngine;

namespace PixelRPG.Enemy.StateMachine
{
    [System.Serializable]
    public class EnemyData
    {
        public float patrolToChaseDistance = 4f;
        public float chaseToPatrolDistance = 5f;

        public float attackCooldown = 0.7f;

        public LayerMask damageLayer;
        public float damageOffset = 0.8f;
        public float damageRadius = 0.3f;
        public int damageAmount = 20;
    }
}
