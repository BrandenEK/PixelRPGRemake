using UnityEngine;

namespace PixelRPG.Actionables
{
    public class ActionSpawnObjectsTemp : MonoBehaviour, IActionable
    {
        public void Activate()
        {
            Debug.Log("Action: Spawning objects temporarily");

            foreach (var obj in _enableObjects)
            {
                obj.SetActive(true);
            }
            foreach (var obj in _disableObjects)
            {
                obj.SetActive(!false);
            }
        }

        [SerializeField] GameObject[] _enableObjects;
        [SerializeField] GameObject[] _disableObjects;
    }
}
