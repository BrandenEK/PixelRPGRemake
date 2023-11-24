using PixelRPG.Input;
using PixelRPG.Persistence;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PixelRPG.Framework
{
    public class LevelChanger : GameSystem, IPersistentSystem
    {
        private Dictionary<string, byte> _levelData = new();

        public void ChangeLevel(string levelName, bool useFade)
        {
            Debug.Log("Changing level to " + levelName);

            Core.InputHandler.AddInputBlock(fadeBlock);
            Core.MusicPlayer.FakeStopMusic(); // Fix!
            StoreLevelObjects();
            StartCoroutine(ChangeLevelCorroutine(levelName, _totalFadeTime, useFade));
        }

        public override void OnSceneLoaded(string sceneName)
        {
            StartCoroutine(FadeIn(Color.black, _totalFadeTime));
            RetrieveLevelObjects();
            Core.InputHandler.RemoveInputBlock(fadeBlock);
        }

        private IEnumerator ChangeLevelCorroutine(string levelName, float fadeTime, bool useFade)
        {
            if (useFade)
                yield return FadeOut(Color.black, fadeTime);
            else
                yield return new WaitForEndOfFrame();

            SceneManager.LoadScene(levelName);
        }

        private IEnumerator FadeOut(Color color, float fadeTime)
        {
            float startTime = Time.time;
            _fadeImage.color = new Color(color.r, color.g, color.b, 0);

            while (_fadeImage.color.a < 1)
            {
                float alpha = (Time.time - startTime) / fadeTime;
                _fadeImage.color = new Color(color.r, color.g, color.b, alpha);
                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator FadeIn(Color color, float fadeTime)
        {
            float startTime = Time.time;
            _fadeImage.color = new Color(color.r, color.g, color.b, 1);

            while (_fadeImage.color.a > 0)
            {
                float alpha = 1 - ((Time.time - startTime) / fadeTime);
                _fadeImage.color = new Color(color.r, color.g, color.b, alpha);
                yield return new WaitForEndOfFrame();
            }
        }

        public void StoreLevelObjects()
        {
            string level = SceneManager.GetActiveScene().name;
            byte levelData = 0;

            foreach (var obj in FindObjectsOfType<MonoBehaviour>())
            {
                if (obj is IPersistentObject pobj && pobj.CurrentStatus)
                {
                    levelData |= (byte)(1 << pobj.SceneIndex);
                }
            }

            Debug.Log("Storing objects for level " + level);
            _levelData[level] = levelData;
        }

        private void RetrieveLevelObjects()
        {
            string level = SceneManager.GetActiveScene().name;
            if (!_levelData.TryGetValue(level, out byte levelData))
                return;

            foreach (var obj in FindObjectsOfType<MonoBehaviour>())
            {
                if (obj is IPersistentObject pobj)
                {
                    pobj.CurrentStatus = (levelData & 1 << pobj.SceneIndex) != 0;
                }
            }

            Debug.Log("Retrieving objects for level " + level);
        }

        public SaveData SaveData()
        {
            return new LevelSaveData()
            {
                levelData = _levelData
            };
        }

        public void LoadData(SaveData data)
        {
            var levelData = data as LevelSaveData;
            _levelData = levelData.levelData ?? new Dictionary<string, byte>();
        }

        public void ResetData()
        {
            _levelData = new Dictionary<string, byte>();
        }

        [SerializeField] float _totalFadeTime;
        [SerializeField] Image _fadeImage;

        private readonly InputBlock fadeBlock = new InputBlock(new InputType[]
        {
            InputType.Any
        });
    }
}
