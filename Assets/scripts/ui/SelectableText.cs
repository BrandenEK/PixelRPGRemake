using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace PixelRPG.UI
{
    public class SelectableText : MonoBehaviour
    {
        private TMP_Text text;
        private TMP_Text TextElement
        {
            get
            {
                if (text == null)
                    text = GetComponent<TMP_Text>();
                return text;
            }
        }

        [SerializeField] UnityEvent onClick;

        public void OnSelect()
        {
            TextElement.color = SELECTED_COLOR;
        }

        public void OnUnselect()
        {
            TextElement.color = NORMAL_COLOR;
        }

        public void ClickEvent()
        {
            onClick?.Invoke();
        }

        private static readonly Color NORMAL_COLOR = Color.white;
        private static readonly Color SELECTED_COLOR = Color.yellow;
    }
}
