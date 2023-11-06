using PixelRPG.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace PixelRPG.UI
{
    public class UIHealthBar : MonoBehaviour
    {
        private Image _redLine;
        private Image _yellowLine;

        [SerializeField] float _percentPerSecond;
        [SerializeField] float _timeBeforeDecrease;

        private float _currentYellow;
        private float _previousRed;
        private float _timeSinceDamage;

        private void Start()
        {
            _yellowLine = transform.GetChild(0).GetComponent<Image>();
            _redLine = transform.GetChild(1).GetComponent<Image>();
        }

        void Update()
        {
            float healthPercent = Core.PlayerSpawner.PlayerHealth.HealthPercentage;

            // If red has gone up since last frame, also increase yellow
            if (healthPercent > _previousRed)
            {
                _currentYellow = healthPercent;
            }
            // If red has gone down since last frame, reset timer
            else if (healthPercent < _previousRed)
            {
                _timeSinceDamage = 0;
            }

            // If it hasn't been much time since damage, just increase timer
            if (_timeSinceDamage < _timeBeforeDecrease)
            {
                _timeSinceDamage += Time.deltaTime;
            }
            // If timer is up and yellow is more then red, decrease it until otherwise
            else if (_currentYellow > healthPercent)
            {
                _currentYellow -= Time.deltaTime * _percentPerSecond;
            }

            // Set fill amounts
            _redLine.fillAmount = healthPercent;
            _yellowLine.fillAmount = _currentYellow;

            _previousRed = healthPercent;
        }
    }
}
