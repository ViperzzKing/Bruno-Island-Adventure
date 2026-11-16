using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Quest
{
    public class TreasureChest : MonoBehaviour
    {
        private Animator animatorCMP;

        private bool isInteractable = false;
        private bool hasBeenOpened = false;

        private void Awake()
        {
            animatorCMP = GetComponentInChildren<Animator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Constants.PLAYER_TAG)) return;

            isInteractable = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(Constants.PLAYER_TAG)) return;

            isInteractable = false;
        }

        public void HandleInteract(InputAction.CallbackContext context)
        {
            if (!isInteractable || hasBeenOpened) return;

            animatorCMP.SetBool(Constants.IS_SHAKING_ANIM, false);
            hasBeenOpened = true;
        }
    }
}
