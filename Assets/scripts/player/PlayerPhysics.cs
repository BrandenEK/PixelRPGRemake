using UnityEngine;

namespace PixelRPG.Player
{
    public class PlayerPhysics : MonoBehaviour
    {
        private Rigidbody2D rb;
        private PlayerInput input;

        [SerializeField] float _movementSpeed;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            input = rb.GetComponent<PlayerInput>();
        }

        void FixedUpdate()
        {
            Vector2 movementDirection = input.MovementAxis * _movementSpeed;
            rb.velocity = movementDirection;
        }
    }
}
