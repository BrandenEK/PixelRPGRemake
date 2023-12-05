using PixelRPG.Persistence;
using UnityEngine;

namespace PixelRPG.Actionables
{
    public class ActionSpawnObjects : MonoBehaviour, IActionable, IPersistentObject
    {
        public void Activate()
        {
            Debug.Log("Action: Spawning objects");
            SetObjectStatus(true);
        }

        public bool CurrentStatus
        {
            get => _spawned;
            set
            {
                if (value)
                    SetObjectStatus(true);
            }
        }

        public int SceneIndex => _sceneIndex;

        private void SetObjectStatus(bool spawn)
        {
            foreach (var obj in _enableObjects)
            {
                obj.SetActive(spawn);
            }
            foreach (var obj in _disableObjects)
            {
                obj.SetActive(!spawn);
            }

            _spawned = spawn;
        }

        [SerializeField] int _sceneIndex;
        [SerializeField] GameObject[] _enableObjects;
        [SerializeField] GameObject[] _disableObjects;

        private bool _spawned;
    }
}
