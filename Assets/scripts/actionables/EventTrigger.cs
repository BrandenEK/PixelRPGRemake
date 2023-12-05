using PixelRPG.Persistence;
using UnityEngine;

namespace PixelRPG.Actionables
{
    public class EventTrigger : EventBase
    {
        //public bool CurrentStatus
        //{
        //    get => _triggered;
        //    set
        //    {
        //        if (value)
        //            DeactivateTrigger();
        //    }
        //}

        //public int SceneIndex => _sceneIndex;

        private void Awake()
        {
            collide = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Event: Entered trigger");
                DeactivateTrigger();
                TriggerEvent();
            }
        }

        private void DeactivateTrigger()
        {
            //if (!_singleUse)
                //return;

            collide.enabled = false;
            //_triggered = true;
        }

        private Collider2D collide;

        //[SerializeField] int _sceneIndex;
        //[SerializeField] bool _singleUse;

        //private bool _triggered;
    }
}
