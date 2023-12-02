using PixelRPG.Persistence;
using UnityEngine;

namespace PixelRPG
{
    public class SpawnObjectLeverEvent : BaseLeverEvent, IPersistentObject
    {
        public bool CurrentStatus
        {
            get => _spawned;
            set => SetObjectStatus(value);
        }

        public int SceneIndex => _sceneIndex;

        protected override void OnLeverToggled()
        {
            Debug.Log("Spawning object from lever");
            SetObjectStatus(true);
        }

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
