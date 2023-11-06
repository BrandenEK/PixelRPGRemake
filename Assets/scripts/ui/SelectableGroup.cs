using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PixelRPG.UI
{
    public class SelectableGroup : MonoBehaviour
    {
        [SerializeField] Selectable[] _selectables;
        [SerializeField] bool _vertical;

        private int _selectedIndex;

        private void OnEnable()
        {
            _selectedIndex = 0;
        }

        private void Update()
        {
            if (!IsElementSelected())
            {
                EventSystem.current.SetSelectedGameObject(_selectables[_selectedIndex].gameObject);
                return;
            }

            bool decrease = UnityEngine.Input.GetKeyDown(_vertical ? KeyCode.W : KeyCode.A);
            bool increase = UnityEngine.Input.GetKeyDown(_vertical ? KeyCode.S : KeyCode.D);

            if (decrease)
            {
                _selectedIndex--;
                if (_selectedIndex < 0)
                    _selectedIndex = _selectables.Length - 1;
            }
            else if (increase)
            {
                _selectedIndex++;
                if (_selectedIndex >= _selectables.Length)
                    _selectedIndex = 0;
            }

            EventSystem.current.SetSelectedGameObject(_selectables[_selectedIndex].gameObject);
        }

        private bool IsElementSelected()
        {
            GameObject obj = EventSystem.current.currentSelectedGameObject;

            foreach (var selectable in _selectables)
            {
                if (selectable.gameObject == obj)
                    return true;
            }

            return false;
        }
    }
}
