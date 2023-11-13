using PixelRPG.Framework;
using PixelRPG.Input;
using System.Collections;
using UnityEngine;

namespace PixelRPG.UI
{
    public class DeathWindow : BaseWindow
    {
        private const float WAIT_TIME = 1f;
        private const float FADE_TIME = 2.5f;

        private CanvasGroup screen;

        private bool _acceptInput;

        private void Awake()
        {
            screen = GetComponent<CanvasGroup>();
        }

        protected override void OnShow()
        {
            _acceptInput = false;
            Core.InputHandler.AddInputBlock(deathBlock);
            StartCoroutine(FadeIn());
        }

        protected override void OnHide()
        {
            Core.InputHandler.RemoveInputBlock(deathBlock);
        }

        private IEnumerator FadeIn()
        {
            screen.alpha = 0;

            yield return new WaitForSeconds(WAIT_TIME);

            float startTime = Time.time;
            while (screen.alpha < 0.9f)
            {
                screen.alpha = (Time.time - startTime) / FADE_TIME;
                yield return new WaitForEndOfFrame();
            }

            _acceptInput = true;
        }

        private void Update()
        {
            if (_acceptInput && UnityEngine.Input.anyKeyDown)
            {
                Debug.Log("Respawning...");
                Core.PlayerSpawner.SpawnFromLastCampfire();
            }
        }

        private readonly InputBlock deathBlock = new InputBlock(new InputType[]
        {
            InputType.Any
        });
    }
}
