using PixelRPG.Framework;
using UnityEngine;

namespace PixelRPG.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;

        private int _currentHealth;

        private PlayerGraphics graphics;

        private void Start()
        {
            graphics = GetComponent<PlayerGraphics>();
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
            Core.UIDisplayer.ShowWindow(UI.WindowType.Death);
        }

        public void FillHealth()
        {
            _currentHealth = _maxHealth;
        }

        public void SetHealthOnStart(int health)
        {
            _currentHealth = Mathf.Max(Mathf.Min(health, _maxHealth), 0);
        }

        public int CurrentHealth => _currentHealth;
        public int MaxHealth => _maxHealth;

        public float HealthPercentage => (float)_currentHealth / _maxHealth;
    }
}
