
using UnityEngine;

namespace PixelRPG.Framework
{
    public abstract class GameSystem : MonoBehaviour
    {
        public virtual void OnInitialize() { }

        public virtual void OnUpdate() { }

        public virtual void OnSceneLoaded(string sceneName) { }

        public virtual void OnSceneUnloaded(string sceneName) { }
    }
}
