using UnityEngine;

namespace PixelRPG.UI.Selectables
{
    public abstract class SelectableOption : MonoBehaviour
    {
        public abstract void OnSelect();

        public abstract void OnDeselect();

        public abstract void OnClick();
    }
}
