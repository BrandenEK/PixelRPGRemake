
namespace PixelRPG.Framework
{
    public class EnemySpawner : GameSystem
    {
        public override void OnSceneLoaded(string sceneName)
        {
            foreach (var enemySpawn in FindObjectsOfType<EnemySpawnPoint>())
            {
                // Store the reference to the enemy to tell how many are alive in the scene
                if (enemySpawn.spawnOnLoad)
                    enemySpawn.SpawnEnemy();
            }
        }
    }
}
