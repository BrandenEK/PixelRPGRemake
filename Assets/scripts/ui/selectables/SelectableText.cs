using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace PixelRPG.UI.Selectables
{
    public class SelectableText : SelectableOption
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

        public override void OnSelect()
        {
            TextElement.color = SELECTED_COLOR;
        }

        public override void OnDeselect()
        {
            TextElement.color = NORMAL_COLOR;
        }

        public override void OnClick()
        {
            onClick?.Invoke();
        }

        private static readonly Color NORMAL_COLOR = Color.white;
        private static readonly Color SELECTED_COLOR = Color.yellow;
    }
}
