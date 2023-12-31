using PixelRPG.Input;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PixelRPG.Framework
{
    public class InputHandler : GameSystem
    {
        private readonly List<InputBlock> _inputBlocks = new();

        public void AddInputBlock(InputBlock block)
        {
            if (!_inputBlocks.Contains(block))
                _inputBlocks.Add(block);
            Debug.Log($"Adding input block: {_inputBlocks.Count}");
        }

        public void RemoveInputBlock(InputBlock block)
        {
            _inputBlocks.Remove(block);
            Debug.Log($"Removing input block: {_inputBlocks.Count}");
        }

        public override void OnMenuLoaded()
        {
            // Clear all input blocks on menu
            _inputBlocks.Clear();
        }

        public bool GetButton(InputType input)
        {
            return !IsInputBlocked(input) && UnityEngine.Input.GetButton(input.ToString());
        }

        public bool GetButtonDown(InputType input)
        {
            return !IsInputBlocked(input) && UnityEngine.Input.GetButtonDown(input.ToString());
        }

        public bool GetButtonUp(InputType input)
        {
            return !IsInputBlocked(input) && UnityEngine.Input.GetButtonUp(input.ToString());
        }

        public float GetAxis(InputType input)
        {
            return !IsInputBlocked(input) ? UnityEngine.Input.GetAxisRaw(input.ToString()) : 0f;
        }

        public bool IsInputBlocked(InputType input)
        {
            return _inputBlocks.Where(x => x.BlockedInputs.Contains(InputType.Any) || x.BlockedInputs.Contains(input)).Count() > 0;
        }
    }
}
