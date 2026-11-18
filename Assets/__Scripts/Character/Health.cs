using RPG.Utility;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Character
{
    public class Health : MonoBehaviour
    {
        public event UnityAction OnStartDefeated = () => { };

        [NonSerialized] public float healthPoints;
        private bool isDefeated;

        private Animator animatorCMP;
        private BubbleEvent bubbleEventCMP;

        private void Awake()
        {
            animatorCMP = GetComponentInChildren<Animator>();
            bubbleEventCMP = GetComponentInChildren<BubbleEvent>();
        }

        private void OnEnable()
        {
            bubbleEventCMP.OnBubbleCompleteDefeat += HandleBubbleCompleteDefeat;
        }

        private void OnDisable()
        {
            bubbleEventCMP.OnBubbleCompleteDefeat -= HandleBubbleCompleteDefeat;
        }

        public void TakeDamage(float damageAmount)
        {
            healthPoints = Mathf.Max(healthPoints - damageAmount, 0);

            if (healthPoints == 0)
            {
                Defeated();
            }
        }

        private void Defeated()
        {
            if (isDefeated) return;

            if (CompareTag(Constants.ENEMY_TAG))
            {
                OnStartDefeated.Invoke();
            }

            isDefeated = true;
            animatorCMP.SetTrigger(Constants.DEFEATED_ANIM);
        }

        private void HandleBubbleCompleteDefeat()
        {
            Destroy(gameObject);
        }
    }
}
