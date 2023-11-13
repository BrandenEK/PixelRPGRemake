using PixelRPG.Interactables;
using PixelRPG.Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelRPG.Framework
{
    public class EnemySpawner : GameSystem
    {
        public override void OnInitialize()
        {
            Campfire.OnRestAtCampfire += RespawnEnemies;
            PlayerHealth.OnPlayerDeath += ResetKilledEnemies;
        }

        public override void OnSceneLoaded(string sceneName)
        {
            SpawnEnemies();
        }

        public override void OnSceneUnloaded(string sceneName)
        {
            _aliveEnemies.Clear();
        }

        public override void OnMenuLoaded()
        {
            ResetKilledEnemies();
        }

        public void AddKilledEnemy(EnemySpawnPoint spawnPoint)
        {
            string spawnId = GetSpawnPointId(spawnPoint);
            if (!_killedEnemies.Contains(spawnId))
            {
                Debug.Log("Adding killed enemy: " +  spawnId);
                _killedEnemies.Add(spawnId);
            }
        }

        private void SpawnEnemies()
        {
            _aliveEnemies.Clear();
            foreach (var enemySpawn in FindObjectsOfType<EnemySpawnPoint>())
            {
                string spawnId = GetSpawnPointId(enemySpawn);
                if (enemySpawn.spawnOnLoad && !_killedEnemies.Contains(spawnId))
                {
                    Debug.Log("Spawning enemy: " + spawnId);
                    _aliveEnemies.Add(enemySpawn.SpawnEnemy());
                }
            }
        }

        private void ResetKilledEnemies()
        {
            _killedEnemies.Clear();
        }

        private void RespawnEnemies()
        {
            ResetKilledEnemies();

            foreach (var enemy in _aliveEnemies)
            {
                if (enemy != null)
                    Destroy(enemy);
            }

            SpawnEnemies();
        }

        private string GetSpawnPointId(EnemySpawnPoint spawn)
        {
            string scene = SceneManager.GetActiveScene().name;
            string position = spawn.transform.position.ToString();

            return scene + "@" + position;
        }

        private readonly List<GameObject> _aliveEnemies = new();
        private readonly List<string> _killedEnemies = new();
    }
}
