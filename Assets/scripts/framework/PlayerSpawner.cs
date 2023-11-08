using PixelRPG.Persistence;
using PixelRPG.Player;
using UnityEngine;

namespace PixelRPG.Framework
{
    public class PlayerSpawner : GameSystem, IPersistentSystem
    {
        public override void OnInitialize()
        {
            DoorIdToSpawnFrom = _initialDoorId;
        }

        public override void OnSceneLoaded(string sceneName)
        {
            // Spawn player and get references
            Debug.Log("Spawning player");
            PlayerTransform = Instantiate(_playerPrefab).transform;
            PlayerInput = PlayerTransform.GetComponent<PlayerInput>();
            PlayerGraphics = PlayerTransform.GetComponent<PlayerGraphics>();
            PlayerPhysics = PlayerTransform.GetComponent<PlayerPhysics>();
            PlayerHealth = PlayerTransform.GetComponent<PlayerHealth>();

            // Set spawn properties
            var door = FindDoorToSpawnFrom();
            if (door == null)
                return;

            PlayerTransform.position = door.position;
            PlayerInput.SetOrientation(door.orientation);
            PlayerGraphics.SetOrientation(door.orientation);

            // Update save data
            _lastScene = sceneName;
            _lastDoor = door.id;

            OnPlayerSpawn?.Invoke();
        }

        private Door FindDoorToSpawnFrom()
        {
            Door[] doors = FindObjectsOfType<Door>();

            if (doors.Length == 0)
            {
                Debug.LogError("Failed to find a door in this scene!");
                return null;
            }

            foreach (var door in doors)
            {
                if (door.id == DoorIdToSpawnFrom)
                    return door;
            }

            Debug.LogError($"Failed to find door with id '{DoorIdToSpawnFrom}'");
            return doors[0];
        }

        public void SpawnFromLastSave()
        {
            DoorIdToSpawnFrom = _lastDoor;
            Core.LevelChanger.ChangeLevel(_lastScene, false);
        }

        public SaveData SaveData()
        {
            return new SpawnSaveData()
            {
                spawnLevel = _lastScene,
                spawnDoor = _lastDoor
            };
        }

        public void LoadData(SaveData data)
        {
            var spawnData = data as SpawnSaveData;
            _lastScene = spawnData.spawnLevel;
            _lastDoor = spawnData.spawnDoor;
        }

        public void ResetData()
        {
            _lastScene = "Z0101";
            _lastDoor = "Start";
        }

        [SerializeField] GameObject _playerPrefab;
        [SerializeField] string _initialDoorId;

        public Transform PlayerTransform { get; private set; }
        public PlayerInput PlayerInput { get; private set; }
        public PlayerGraphics PlayerGraphics { get; private set; }
        public PlayerPhysics PlayerPhysics { get; private set; }
        public PlayerHealth PlayerHealth { get; private set; }

        public string DoorIdToSpawnFrom { get; set; } = string.Empty;

        private string _lastScene;
        private string _lastDoor;

        public delegate void SpawnDelegate();
        public static event SpawnDelegate OnPlayerSpawn;
    }
}
