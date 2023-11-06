using UnityEngine;

namespace PixelRPG
{
    public class SpawnPoint : MonoBehaviour
    {
        public string id;
        public Vector2 position;
        public Orientation orientation;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(position, 0.3f);
        }
    }
}
