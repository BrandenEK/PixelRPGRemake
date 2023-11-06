using PixelRPG.Damage;
using UnityEngine;

namespace PixelRPG.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private PlayerInput input;
        private PlayerGraphics graphics;
        private RotateToOrientation orientation;

        [SerializeField] LayerMask _damageLayer;
        [SerializeField] float _damageOffset;
        [SerializeField] float _damageRadius;
        [SerializeField] int _damageAmount;

        private bool _isAttacking;

        private void Awake()
        {
            input = GetComponent<PlayerInput>();
            graphics = GetComponent<PlayerGraphics>();
            orientation = GetComponentInChildren<RotateToOrientation>();
        }

        private void Update()
        {
            bool attack = input.AttackButton && !_isAttacking;

            if (attack)
            {
                StartAttack();
            }
        }

        public void StartAttack()
        {
            Debug.Log("Starting attack");
            _isAttacking = true;
            graphics.Attack();

            ApplyDamage();
        }

        public void FinishAttack()
        {
            Debug.Log("Finishing attack");
            _isAttacking = false;
        }

        private void ApplyDamage()
        {
            foreach (var collider in Physics2D.OverlapCircleAll(orientation.OffsetPosition(_damageOffset), _damageRadius, _damageLayer))
            {
                if (collider.TryGetComponent<IDamageable>(out var damageable))
                    damageable.TakeDamage(_damageAmount, DamageType.Player);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(orientation.OffsetPosition(_damageOffset), _damageRadius);
        }

        public bool IsAttacking => _isAttacking;
    }
}
