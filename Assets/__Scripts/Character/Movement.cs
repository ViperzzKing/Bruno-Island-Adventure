using RPG.Utility;
using System;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace RPG.Character
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movement : MonoBehaviour
    {
        private Animator animatorCMP;
        private NavMeshAgent agent;

        [NonSerialized] public Vector3 originalForwardVector;
        [NonSerialized] public bool isMoving;

        private bool clampAnimationSpeedAgain = true;
        private Vector3 movementVector;


        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animatorCMP = GetComponentInChildren<Animator>();

            originalForwardVector = transform.forward;
        }

        private void Start()
        {
            agent.updateRotation = false;
        }

        private void Update()
        {
            MovePlayer();
            MovementAnimator();

            if (CompareTag(Constants.PLAYER_TAG)) Rotate(movementVector);
        }


        private void MovePlayer()
        {
            Vector3 offset = (movementVector * agent.speed) * Time.deltaTime;
            agent.Move(offset);
        }

        public void HandleMove(InputAction.CallbackContext context)
        {
            if (context.performed) isMoving = true;
            if (context.canceled) isMoving = false;

            Vector2 input = context.ReadValue<Vector2>();
            movementVector = new Vector3(input.x, 0, input.y);

            //Debug.Log(movementVector);
        }

        public void Rotate(Vector3 newForwardVector)
        {
            if (newForwardVector == Vector3.zero) return;

            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.LookRotation(newForwardVector);

            transform.rotation = Quaternion.Lerp(startRotation, endRotation, 0.1f);
        }

        public void MoveAgentByDestination(Vector3 destination)
        {
            agent.SetDestination(destination);
            isMoving = true;

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
            isMoving = true;
        }

        public void UpdateAgentSpeed(float newSpeed, bool shouldClampSpeed)
        {
            agent.speed = newSpeed;
            clampAnimationSpeedAgain = shouldClampSpeed;
        }

        private void MovementAnimator()
        {
            float speed = animatorCMP.GetFloat(Constants.SPEED_ANIM);
            float smoothening = Time.deltaTime * agent.acceleration;

            if (isMoving)
            {
                speed += smoothening;
            }
            else
            {
                speed -= smoothening;
            }

            speed = Mathf.Clamp01(speed);

            if (CompareTag(Constants.ENEMY_TAG) && clampAnimationSpeedAgain)
            {
                speed = Mathf.Clamp(speed, 0f, 0.5f);
            }

            animatorCMP.SetFloat(Constants.SPEED_ANIM, speed);
        }
    }
}
