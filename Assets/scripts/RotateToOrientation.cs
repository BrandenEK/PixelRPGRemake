using UnityEngine;

namespace PixelRPG
{
    public class RotateToOrientation : MonoBehaviour
    {
        public void Rotate(Orientation orientation)
        {
            transform.up = orientation switch
            {
                Orientation.Up => Vector3.up,
                Orientation.Left => Vector3.left,
                Orientation.Right => Vector3.right,
                Orientation.Down => Vector3.down,
                _ => throw new System.NotImplementedException()
            };
        }

        public void RotateInput(Vector2 vector)
        {
            if (vector.y < 0)
                Rotate(Orientation.Down);
            else if (vector.x > 0)
                Rotate(Orientation.Right);
            else if (vector.x < 0)
                Rotate(Orientation.Left);
            else if (vector.y > 0)
                Rotate(Orientation.Up);
        }

        public void RotateMovement(Vector2 vector)
        {
            if (Mathf.Abs(vector.y) > Mathf.Abs(vector.x))
            {
                if (vector.y <= -0.5f)
                    Rotate(Orientation.Down);
                else if (vector.y >= 0.5f)
                    Rotate(Orientation.Up);
            }
            else if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
            {
                if (vector.x <= -0.5f)
                    Rotate(Orientation.Left);
                else if (vector.x >= 0.5f)
                    Rotate(Orientation.Right);
            }
        }

        public Vector2 OffsetPosition(float offset)
        {
            return transform.position + transform.up * offset;
        }

        public static Vector2 OrientationToVector(Orientation orientation) => orientation switch
        {
            Orientation.Up => Vector2.up,
            Orientation.Left => Vector2.left,
            Orientation.Right => Vector2.right,
            Orientation.Down => Vector2.down,
            _ => throw new System.NotImplementedException(),
        };

        public static Orientation VectorToOrientation(Vector2 vector, Orientation current)
        {
            if (vector.y < 0)
                return Orientation.Down;
            else if (vector.x > 0)
                return Orientation.Right;
            else if (vector.x < 0)
                return Orientation.Left;
            else if (vector.y > 0)
                return Orientation.Up;

            return current;
        }
    }
}
