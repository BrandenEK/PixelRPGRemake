using PixelRPG.Player;
using UnityEngine;

namespace PixelRPG
{
    public class AnimationReceiver : MonoBehaviour
    {
        private PlayerAttack attack;

        private void Awake()
        {
            attack = GetComponentInParent<PlayerAttack>();
        }

        public void AttackFinished()
        {
            attack.FinishAttack();
        }
    }
}
