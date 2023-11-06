using UnityEngine;

namespace PixelRPG.Enemy
{
    public class EnemyGraphics : MonoBehaviour
    {
        private Animator[] animators;
        private Rigidbody2D rb;
        private RotateToOrientation orientation;

        void Start()
        {
            animators = GetComponentsInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();
            orientation = GetComponentInChildren<RotateToOrientation>();
        }

        void Update()
        {
            Vector2 movement = rb.velocity.normalized;
            bool isMoving = rb.velocity.sqrMagnitude > 0;

            UpdateAnimators(movement, isMoving);
            orientation.RotateMovement(movement);
        }

        private void UpdateAnimators(Vector2 movement, bool isMoving)
        {
            foreach (var anim in animators)
            {
                if (isMoving)
                {
                    anim.SetFloat("x", movement.x);
                    anim.SetFloat("y", movement.y);
                }

                anim.SetBool("isMoving", isMoving);
            }
        }

        public void Attack()
        {
            foreach (var anim in animators)
            {
                anim.SetTrigger("attack");
            }
        }

        public void Die()
        {
            foreach (var anim in animators)
            {
                anim.SetTrigger("death");
            }
        }
    }
}
