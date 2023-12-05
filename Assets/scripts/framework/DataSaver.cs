using Newtonsoft.Json;
using PixelRPG.Persistence;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace PixelRPG.Framework
{
    public class DataSaver : GameSystem
    {
        private string SaveFilePath => Application.persistentDataPath + "/progress.bin";

        public bool SaveFileExists => File.Exists(SaveFilePath);

        public void SaveGame()
        {
            // Store all pers. objects in the current scene
            Core.LevelChanger.StoreLevelObjects();

            // Add save data from each persistent manager to list
            var data = new Dictionary<string, SaveData>();
            foreach (var system in Core.AllSystems)
            {
                if (system is IPersistentSystem persistentSystem)
                {
                    data.Add(system.GetType().Name, persistentSystem.SaveData());
                }
            }

            // Serialize and write data to file
            string json = JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            File.WriteAllText(SaveFilePath, json);

            Debug.Log("Saved game data!");
        }

        public void LoadGame()
        {
            // Read from file and deserialize
            string json = File.ReadAllText(SaveFilePath);
            var data = JsonConvert.DeserializeObject<Dictionary<string, SaveData>>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            // Process save data for each persistent manager
            foreach (var system in Core.AllSystems)
            {
                if (system is IPersistentSystem persistentSystem && data.TryGetValue(system.GetType().Name, out SaveData save))
                {
                    persistentSystem.LoadData(save);
                }
            }

            Debug.Log("Loaded game data!");
        }

        public void ResetGame()
        {
            foreach (var system in Core.AllSystems)
            {
                if (system is IPersistentSystem persistentSystem)
                {
                    persistentSystem.ResetData();
                }
            }

            Debug.Log("Reset game data!");
        }

        public void DeleteSaveFile()
        {
            File.Delete(SaveFilePath);
            Debug.Log("Deleted save file!");
        }
    }
}
