using UnityEngine;
using UnityEngine.Events;

namespace RPG.Utility
{
    public class BubbleEvent : MonoBehaviour
    {
        // ATTACKING
        public event UnityAction onBubbleStartAttack = () => { };
        public event UnityAction onBubbleCompleteAttack = () => { };
        public event UnityAction onBubbleHit = () => { };

        // DEATH
        public event UnityAction OnBubbleCompleteDefeat = () => { };

        private void OnStartAttack()
        {
            onBubbleStartAttack.Invoke();
        }

        private void OnCompleteAttack()
        {
            onBubbleCompleteAttack.Invoke();
        }

        private void OnHit()
        {
            onBubbleHit.Invoke();
        }

        private void OnCompleteDefeat()
        {
            OnBubbleCompleteDefeat.Invoke();
        }
    }
}
