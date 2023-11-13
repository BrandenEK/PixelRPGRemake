using PixelRPG.Framework;
using UnityEngine;

namespace PixelRPG
{
    public class Door : MonoBehaviour
    {
        [Header("Load info")]
        public string id;
        public string otherScene;
        public string otherId;

        [Header("Spawn info")]
        public Vector2 position;
        public Orientation orientation;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player"))
                return;

            Core.PlayerSpawner.DoorIdToSpawnFrom = otherId;
            Core.PlayerSpawner.HealthToSpawnWith = Core.PlayerSpawner.PlayerHealth.CurrentHealth;
            Core.LevelChanger.ChangeLevel(otherScene, true);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(position, 0.3f);
        }
    }
}
