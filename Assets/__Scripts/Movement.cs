using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace RPG.Character
{
    public class Movement : MonoBehaviour
    {
        private NavMeshAgent player;

        private Vector3 movementVector;

        private void Awake()
        {
            player = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            MovePlayer();
            Rotate();
        }

        private void MovePlayer()
        {
            Vector3 offset = (movementVector * player.speed) * Time.deltaTime;
            player.Move(offset);
        }

        public void HandleMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            movementVector = new Vector3(input.x, 0, input.y);

            Debug.Log(movementVector);
        }

        private void Rotate()
        {
            if (movementVector == Vector3.zero) return;

            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.LookRotation(movementVector);

            transform.rotation = Quaternion.Lerp(startRotation, endRotation, 0.1f);
        }
    }
}
