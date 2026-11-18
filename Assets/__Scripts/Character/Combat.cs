using RPG.Utility;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Character
{
    public class Combat : MonoBehaviour
    {
        [NonSerialized] public float damage;
        [NonSerialized] public bool isAttacking = false;

        private Animator animatorCMP;
        private BubbleEvent bubbleEventCMP;

        private void Awake()
        {
            animatorCMP = GetComponentInChildren<Animator>();
            bubbleEventCMP = GetComponentInChildren<BubbleEvent>();
        }

        private void OnEnable()
        {
            bubbleEventCMP.onBubbleStartAttack += HandleBubbleStartAttack;
            bubbleEventCMP.onBubbleCompleteAttack += HandleBubbleCompleteAttack;
            bubbleEventCMP.onBubbleHit += HandleBubbleHit;
        }


        private void OnDisable()
        {
            bubbleEventCMP.onBubbleHit -= HandleBubbleHit;
            bubbleEventCMP.onBubbleCompleteAttack -= HandleBubbleCompleteAttack;
            bubbleEventCMP.onBubbleStartAttack -= HandleBubbleStartAttack;
        }

        public void HandleAttack(InputAction.CallbackContext context)
        {
            if (!CompareTag(Constants.PLAYER_TAG)) return;
            if (!context.performed) return;

            StartAttack();
        }

        public void StartAttack()
        {
            if (isAttacking) return;

            animatorCMP.SetFloat(Constants.SPEED_ANIM, 0);
            animatorCMP.SetTrigger(Constants.ATTACK_ANIM);
        }

        private void HandleBubbleStartAttack()
        {
            isAttacking = true;
        }

        private void HandleBubbleCompleteAttack()
        {
            isAttacking = false;
        }

        private void HandleBubbleHit()
        {
            RaycastHit[] targets = Physics.BoxCastAll(
                transform.position + transform.forward * 0.5f,
                transform.localScale / 0.5f,
                transform.forward,
                transform.rotation,
                0f
            );

            foreach (RaycastHit target in targets)
            {
                if (CompareTag(target.transform.tag)) continue;

                Health healthCMP = target.transform.
                    gameObject.GetComponent<Health>();

                if (healthCMP == null) continue;

                healthCMP.TakeDamage(damage);
            }
        }

        public void CancelAttack()
        {
            animatorCMP.ResetTrigger(Constants.ATTACK_ANIM);
        }
    }
}
