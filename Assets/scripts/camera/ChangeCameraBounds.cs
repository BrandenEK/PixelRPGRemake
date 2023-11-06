using UnityEngine;

namespace PixelRPG.Camera
{
    public class ChangeCameraBounds : MonoBehaviour
    {
        [SerializeField] Vector2 _xBounds;
        [SerializeField] Vector2 _yBounds;
        [SerializeField] bool _startingBounds;

        private CameraMovement cam;

        void Awake()
        {
            cam = FindObjectOfType<CameraMovement>();

            if (_startingBounds)
                cam.UpdateBounds(_xBounds, _yBounds);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player"))
                return;

            Debug.Log("Updating camera bounds");
            cam.UpdateBounds(_xBounds, _yBounds);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(new Vector2(_xBounds.x, _yBounds.x), new Vector2(_xBounds.x, _yBounds.y));
            Gizmos.DrawLine(new Vector2(_xBounds.x, _yBounds.y), new Vector2(_xBounds.y, _yBounds.y));
            Gizmos.DrawLine(new Vector2(_xBounds.y, _yBounds.y), new Vector2(_xBounds.y, _yBounds.x));
            Gizmos.DrawLine(new Vector2(_xBounds.y, _yBounds.x), new Vector2(_xBounds.x, _yBounds.x));
        }
    }
}
