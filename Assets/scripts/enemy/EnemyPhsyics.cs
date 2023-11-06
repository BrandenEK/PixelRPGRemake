using UnityEngine;

namespace PixelRPG.Enemy
{
    public class EnemyPhsyics : MonoBehaviour
    {
        private Rigidbody2D rb;

        [SerializeField] float _speed;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 direction)
        {
            rb.velocity = direction * _speed;
        }
    }
}
