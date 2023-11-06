using UnityEngine;

namespace PixelRPG.Player
{
    public class PlayerGraphics : MonoBehaviour
    {
        private PlayerInput input;
        private Animator[] animators;

        private void Awake()
        {
            input = GetComponent<PlayerInput>();
            animators = GetComponentsInChildren<Animator>();
        }

        private void Update()
        {
            Vector2 movement = input.MovementAxis;
            bool isMoving = movement.x != 0 || movement.y != 0;

            UpdateAnimators(movement, isMoving);
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

        public void SetOrientation(Orientation orientation)
        {
            Vector2 direction = RotateToOrientation.OrientationToVector(orientation);
            UpdateAnimators(direction, true);
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
