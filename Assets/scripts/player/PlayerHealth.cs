using PixelRPG.Framework;
using UnityEngine;

namespace PixelRPG.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;

        private int _currentHealth;

        private PlayerGraphics graphics;
        private PlayerInput input;

        private void Start()
        {
            graphics = GetComponent<PlayerGraphics>();
            input = GetComponent<PlayerInput>();
            FillHealth();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                TakeDamage(50);
            }
        }

        public void TakeDamage(int damage)
        {
            _currentHealth = Mathf.Max(_currentHealth - damage, 0);

            if (_currentHealth == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            foreach (var collider in GetComponentsInChildren<BoxCollider2D>())
            {
                collider.enabled = false;
            }

            graphics.Die();
            input.AddInputBlock("death");
            Core.UIDisplayer.ShowWindow(UI.WindowType.Death);
        }

        public void FillHealth()
        {
            _currentHealth = _maxHealth;
        }

        public float HealthPercentage => (float)_currentHealth / _maxHealth;
    }
}
