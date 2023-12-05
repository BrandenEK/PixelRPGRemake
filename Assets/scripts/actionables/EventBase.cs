using UnityEngine;

namespace PixelRPG.Actionables
{
    public abstract class EventBase : MonoBehaviour
    {
        protected void TriggerEvent()
        {
            foreach (var component in GetComponents<Component>())
            {
                if (component is IActionable action)
                    action.Activate();
            }
        }
    }
}
