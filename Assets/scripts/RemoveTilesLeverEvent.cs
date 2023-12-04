using PixelRPG.Persistence;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PixelRPG
{
    public class RemoveTilesLeverEvent : BaseLeverEvent, IPersistentObject
    {
        public bool CurrentStatus
        {
            get => _removed;
            set
            {
                if (value)
                    RemoveTiles();
            }
        }

        public int SceneIndex => _sceneIndex;

        protected override void OnLeverToggled()
        {
            Debug.Log("Removing tiles from lever event");
            RemoveTiles();
        }

        private void RemoveTiles()
        {
            _removed = true;

            int minX = Mathf.FloorToInt(tileOverlap.position.x);
            int minY = Mathf.FloorToInt(tileOverlap.position.y);
            int maxX = minX + Mathf.CeilToInt(tileOverlap.localScale.x);
            int maxY = minY + Mathf.CeilToInt(tileOverlap.localScale.y);

            for (int x = minX; x < maxX; x++)
            {
                for (int y = minY; y < maxY; y++)
                {
                    tilemap.SetTile(new Vector3Int(x, y), null);
                }
            }
        }

        [SerializeField] int _sceneIndex;
        [SerializeField] Tilemap tilemap;
        [SerializeField] Transform tileOverlap;

        private bool _removed;
    }
}
