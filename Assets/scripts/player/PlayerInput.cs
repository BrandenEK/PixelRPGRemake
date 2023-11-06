using PixelRPG.Framework;
using UnityEngine;

namespace PixelRPG.Player
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerAttack attack;
        private RotateToOrientation _orientationRotater;

        [SerializeField] private Vector2 _movementAxis;
        [SerializeField] private bool _attackButton;
        [SerializeField] private bool _interactButton;

        private void Awake()
        {
            attack = GetComponent<PlayerAttack>();
            _orientationRotater = GetComponentInChildren<RotateToOrientation>();
        }

        // Gather input
        private void Update()
        {
            float x = Core.InputHandler.GetAxis(Input.InputType.MoveHorizontal);
            float y = Core.InputHandler.GetAxis(Input.InputType.MoveVertical);

            _movementAxis = new Vector2(attack.IsAttacking ? 0 : x, attack.IsAttacking ? 0 : y).normalized;
            _attackButton = Core.InputHandler.GetButtonDown(Input.InputType.Attack);
            _interactButton = Core.InputHandler.GetButtonDown(Input.InputType.Interact);

            _orientationRotater.RotateInput(_movementAxis);
        }

        // Reset input
        private void LateUpdate()
        {
            _attackButton = false;
            _interactButton = false;
        }

        // Set stored orientation on scene start (Should be combined with graphics one)
        public void SetOrientation(Orientation orientation)
        {
            _orientationRotater.Rotate(orientation);
        }

        public Vector2 MovementAxis => _movementAxis;
        public bool AttackButton => _attackButton;
        public bool InteractButton => _interactButton;
    }
}
