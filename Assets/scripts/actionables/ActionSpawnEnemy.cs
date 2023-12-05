using UnityEngine;

namespace PixelRPG.Actionables
{
    public class ActionSpawnEnemy : MonoBehaviour, IActionable
    {
        public void Activate()
        {
            Debug.Log("Action: Spawning enemy");
            _spawnPoint.SpawnEnemy();
        }

        [SerializeField] EnemySpawnPoint _spawnPoint;
    }
}
