using PixelRPG.Framework;
using UnityEngine;

namespace PixelRPG
{
    public class SortingOrderAdjuster : MonoBehaviour
    {
        private SpriteRenderer sr;

        private bool _behindPlayer = true;

        [SerializeField] float _yOffset;

        void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            AdjustSortingOrder(true);
        }

        void Update()
        {
            AdjustSortingOrder(false);
        }

        private void AdjustSortingOrder(bool alwaysUpdate)
        {
            Vector3 playerPosition = Core.PlayerSpawner.PlayerTransform.position;

            bool isBehindPlayer = transform.position.y + _yOffset - playerPosition.y >= 0;

            if (isBehindPlayer && (alwaysUpdate || !_behindPlayer))
            {
                sr.sortingLayerName = "Before Player";
                _behindPlayer = true;
                return;
            }

            if (!isBehindPlayer && (alwaysUpdate || _behindPlayer))
            {
                sr.sortingLayerName = "After Player";
                _behindPlayer = false;
                return;
            }
        }

        public void PermanentlyMoveBehind()
        {
            sr.sortingLayerName = "Before Player";
            enabled = false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position + Vector3.left * 2 + Vector3.up * _yOffset,
                            transform.position + Vector3.right * 2 + Vector3.up * _yOffset);
        }
    }
}
