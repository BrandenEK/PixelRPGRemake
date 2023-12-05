using PixelRPG.Framework;
using UnityEngine;

namespace PixelRPG.Actionables
{
    public class EventEnemy : EventBase
    {
        private void OnEnemyKilled(EnemySpawnPoint spawnPoint)
        {
            if (spawnPoint == _spawnPoint)
            {
                Debug.Log("Event: Enemy killed");
                TriggerEvent();
            }
        }

        [SerializeField] EnemySpawnPoint _spawnPoint;

        private void OnEnable() => EnemySpawner.OnEnemyKilled += OnEnemyKilled;
        private void OnDisable() => EnemySpawner.OnEnemyKilled -= OnEnemyKilled;
    }
}
