using PixelRPG.Persistence;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PixelRPG.Actionables
{
    public class ActionRemoveTiles : MonoBehaviour, IActionable, IPersistentObject
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

        public void Activate()
        {
            Debug.Log("Action: Removing tiles");
            RemoveTiles();
        }

        private void RemoveTiles()
        {
            _removed = true;

            foreach (var tileOverlap in tiles)
            {
                ProcessTileOverlap(tileOverlap);
            }
        }

        private void ProcessTileOverlap(Transform tileOverlap)
        {
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
        [SerializeField] Transform[] tiles;

        private bool _removed;
    }
}
