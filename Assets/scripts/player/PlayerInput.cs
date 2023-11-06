using System.Collections.Generic;
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
            if (_inputBlocks.Count > 0)
                return;

            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            _movementAxis = new Vector2(attack.IsAttacking ? 0 : x, attack.IsAttacking ? 0 : y).normalized;
            _attackButton = Input.GetButtonDown("Attack");
            _interactButton = Input.GetButtonDown("Interact");

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

        private readonly List<string> _inputBlocks = new();

        public void AddInputBlock(string name)
        {
            Debug.Log($"Adding input block: {name} ({_inputBlocks.Count})");
            if (!_inputBlocks.Contains(name))
                _inputBlocks.Add(name);

            _movementAxis = Vector2.zero;
            _attackButton = false;
            _interactButton = false;
        }

        public void RemoveInputBlock(string name)
        {
            Debug.Log($"Removing input block: {name} ({_inputBlocks.Count})");
            _inputBlocks.Remove(name);
        }

        public Vector2 MovementAxis => _movementAxis;
        public bool AttackButton => _attackButton;
        public bool InteractButton => _interactButton;
    }
}
