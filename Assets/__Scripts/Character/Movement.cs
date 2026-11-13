using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace RPG.Character
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movement : MonoBehaviour
    {
        private NavMeshAgent agent;

        private Vector3 movementVector;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            MovePlayer();
            Rotate();
        }

        private void MovePlayer()
        {
            Vector3 offset = (movementVector * agent.speed) * Time.deltaTime;
            agent.Move(offset);
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

        public void MoveAgentByDestination(Vector3 destination)
        {
            agent.SetDestination(destination);
        }

        public void StopMovingAgent()
        {
            agent.ResetPath();
        }

        public bool ReachedDestination()
        {
            if (agent.pathPending) return false;

            if (agent.remainingDistance > agent.stoppingDistance) return false;

            if (agent.hasPath || agent.velocity.sqrMagnitude != 0f) return false;

            return true;
        }

        public void MoveAgentByOffset(Vector3 offset)
        {
            agent.Move(offset);
        }
    }
}
