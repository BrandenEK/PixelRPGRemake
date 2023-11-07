using PixelRPG.Framework;
using PixelRPG.Input;
using UnityEngine;

namespace PixelRPG.UI
{
    public class SelectableGroup : MonoBehaviour
    {
        [SerializeField] SelectableText[] _selectables;
        [SerializeField] bool _vertical;

        private int _selectedIndex;
        private SelectableText SelectedElement => _selectables[_selectedIndex];

        private void OnEnable()
        {
            foreach (var selectable in _selectables)
                selectable.OnUnselect();

            _selectables[_selectedIndex = 0].OnSelect();
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(_vertical ? KeyCode.W : KeyCode.A))
                ChangeSelection(-1);
            if (UnityEngine.Input.GetKeyDown(_vertical ? KeyCode.S : KeyCode.D))
                ChangeSelection(1);

            if (Core.InputHandler.GetButtonDown(InputType.UIConfirm))
                SelectedElement.ClickEvent();
        }

        private void ChangeSelection(int diff)
        {
            SelectedElement.OnUnselect();

            _selectedIndex += diff;

            if (_selectedIndex < 0)
                _selectedIndex = _selectables.Length - 1;
            else if (_selectedIndex >= _selectables.Length)
                _selectedIndex = 0;

            SelectedElement.OnSelect();
        }
    }
}
