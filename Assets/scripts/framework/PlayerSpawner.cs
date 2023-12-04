using PixelRPG.Interactables;
using PixelRPG.Persistence;
using PixelRPG.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelRPG.Framework
{
    public class PlayerSpawner : GameSystem, IPersistentSystem
    {
        public override void OnInitialize()
        {
            Campfire.OnRestAtCampfire += UpdateSpawnCampfire;
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
            Vector3 position;
            Orientation orientation;

            if (DoorIdToSpawnFrom == "CAMPFIRE")
            {
                var fire = FindObjectOfType<Campfire>();
                position = fire?.transform.position + Vector3.down * 2f ?? Vector3.zero;
                orientation = Orientation.Up;
                PlayerHealth.FillHealth();
            }
            else
            {
                var door = FindDoorToSpawnFrom();
                position = door?.position ?? Vector3.zero;
                orientation = door?.orientation ?? Orientation.Down;
                PlayerHealth.SetHealthOnStart(HealthToSpawnWith);
            }

            PlayerTransform.position = position;
            PlayerInput.SetOrientation(orientation);
            PlayerGraphics.SetOrientation(orientation);
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

        public void SpawnFromLastCampfire()
        {
            Debug.Log("Resting at campfire");
            DoorIdToSpawnFrom = "CAMPFIRE";
            Core.LevelChanger.ChangeLevel(_savedSpawnRoom, false);
        }

        public void UpdateSpawnCampfire()
        {
            _savedSpawnRoom = SceneManager.GetActiveScene().name;
        }

        public SaveData SaveData()
        {
            return new SpawnSaveData()
            {
                spawnRoom = _savedSpawnRoom
            };
        }

        public void LoadData(SaveData data)
        {
            var spawnData = data as SpawnSaveData;
            _savedSpawnRoom = spawnData.spawnRoom;
        }

        public void ResetData()
        {
            _savedSpawnRoom = "Z0401";
        }

        [SerializeField] GameObject _playerPrefab;
        [SerializeField] string _initialDoorId;

        public Transform PlayerTransform { get; private set; }
        public PlayerInput PlayerInput { get; private set; }
        public PlayerGraphics PlayerGraphics { get; private set; }
        public PlayerPhysics PlayerPhysics { get; private set; }
        public PlayerHealth PlayerHealth { get; private set; }

        public string DoorIdToSpawnFrom { get; set; } = string.Empty;
        public int HealthToSpawnWith { get; set; } = 1000;
        
        private string _savedSpawnRoom;
    }
}
