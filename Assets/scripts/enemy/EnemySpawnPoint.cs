using UnityEngine;

namespace PixelRPG
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        public bool spawnOnLoad;
        public GameObject enemyToSpawn;
        public Vector2[] wayPoints = new Vector2[2];

        public GameObject SpawnEnemy()
        {
            return Instantiate(enemyToSpawn, wayPoints[0], Quaternion.identity, transform);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Vector2? lastPoint = null;
            foreach (var point in wayPoints)
            {
                Gizmos.DrawSphere(point, 0.2f);
                if (lastPoint != null)
                {
                    Gizmos.DrawLine(lastPoint.Value, point);
                }
                lastPoint = point;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(wayPoints[0], 0.3f);
        }
    }
}
