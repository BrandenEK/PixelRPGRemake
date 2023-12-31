using PixelRPG.Framework;
using UnityEngine;

namespace PixelRPG.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] float _damping;

        private UnityEngine.Camera cam;
        private Transform _target;

        private Vector2 _xBounds;
        private Vector2 _yBounds;

        public void UpdateBounds(Vector2 x, Vector2 y)
        {
            _xBounds = x;
            _yBounds = y;
        }

        void Start()
        {
            cam = GetComponent<UnityEngine.Camera>();
            _target = Core.PlayerSpawner.PlayerTransform;
            transform.position = CalculateTargetPosition();
        }

        void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, CalculateTargetPosition(), _damping * Time.deltaTime);
        }

        private Vector3 CalculateTargetPosition()
        {
            Vector3 targetPosition = new(_target.position.x, _target.position.y, -10);

            // x bounds
            if (_xBounds.x == _xBounds.y)
            {
                targetPosition.x = _xBounds.x;
            }
            else
            {
                if (targetPosition.x + Xsize > _xBounds.y)
                    targetPosition.x = _xBounds.y - Xsize;
                else if (targetPosition.x - Xsize < _xBounds.x)
                    targetPosition.x = _xBounds.x + Xsize;
            }

            // y bounds
            if (_yBounds.x == _yBounds.y)
            {
                targetPosition.y = _yBounds.x;
            }
            else
            {
                if (targetPosition.y + Ysize > _yBounds.y)
                    targetPosition.y = _yBounds.y - Ysize;
                else if (targetPosition.y - Ysize < _yBounds.x)
                    targetPosition.y = _yBounds.x + Ysize;
            }

            return targetPosition;
        }

        private float Xsize => cam.orthographicSize * cam.aspect;
        private float Ysize => cam.orthographicSize;
    }
}
