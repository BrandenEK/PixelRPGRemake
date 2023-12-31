using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelRPG.Framework
{
    public class Core : MonoBehaviour
    {
        private static Core _instance;
        private static GameSystem[] _systems;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Debug.Log("Creating instance of Core");
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
            Cursor.visible = false;
            _instance = this;

            _systems = new GameSystem[9];
            _systems[0] = PlayerSpawner = GetComponent<PlayerSpawner>();
            _systems[1] = LevelChanger = GetComponent<LevelChanger>();
            _systems[2] = EnemySpawner = GetComponent<EnemySpawner>();
            _systems[3] = UIDisplayer = GetComponent<UIDisplayer>();
            _systems[4] = InventoryStorer = GetComponent<InventoryStorer>();
            _systems[5] = DataSaver = GetComponent<DataSaver>();
            _systems[6] = StateChanger = GetComponent<StateChanger>();
            _systems[7] = InputHandler = GetComponent<InputHandler>();
            _systems[8] = MusicPlayer = GetComponent<MusicPlayer>();
            Initialize();
        }

        private void Initialize()
        {
            foreach (var system in _systems)
            {
                try
                {
                    system.OnInitialize();
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"[{system.GetType().Name}] Encountered error: {e.Message}\n{e.StackTrace}");
                }
            }
        }

        private void Update()
        {
            if (SceneManager.GetActiveScene().name == "MainMenu")
                return;

            foreach (var system in _systems)
            {
                try
                {
                    system.OnUpdate();
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"[{system.GetType().Name}] Encountered error: {e.Message}\n{e.StackTrace}");
                }
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            foreach (var system in _systems)
            {
                try
                {
                    if (scene.name == "MainMenu")
                        system.OnMenuLoaded();
                    else
                        system.OnSceneLoaded(scene.name);
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"[{system.GetType().Name}] Encountered error: {e.Message}\n{e.StackTrace}");
                }
            }
        }

        private void OnSceneUnloaded(Scene scene)
        {
            if (scene.name == "MainMenu")
                return;

            foreach (var system in _systems)
            {
                try
                {
                    system.OnSceneUnloaded(scene.name);
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"[{system.GetType().Name}] Encountered error: {e.Message}\n{e.StackTrace}");
                }
            }
        }

        public static DataSaver DataSaver { get; private set; }
        public static EnemySpawner EnemySpawner { get; private set; }
        public static InputHandler InputHandler { get; private set; }
        public static InventoryStorer InventoryStorer { get; private set; }
        public static LevelChanger LevelChanger { get; private set; }
        public static PlayerSpawner PlayerSpawner { get; private set; }
        public static StateChanger StateChanger { get; private set; }
        public static UIDisplayer UIDisplayer { get; private set; }
        public static MusicPlayer MusicPlayer { get; private set; }

        public static IEnumerable<GameSystem> AllSystems => _systems;
    }
}
