using RPG.Utility;
using System;
using UnityEngine;

namespace RPG.Character
{
    public class EnemyController : MonoBehaviour
    {

        public float chaseRange = 2.5f;
        public float attackRange = 0.75f;

        [NonSerialized] public GameObject player;
        [NonSerialized] public Movement movementCMP;
        [NonSerialized] public float distanceFromPlayer;
        [NonSerialized] public Vector3 originalPosition;

        private AI_BaseState currentState;
        public AI_ReturnState returnState = new AI_ReturnState();
        public AI_ChaseState chaseState = new AI_ChaseState();
        public AI_AttackState attackState = new AI_AttackState();

        private void Awake()
        {
            currentState = returnState;

            player = GameObject.FindWithTag(Constants.PLAYER_TAG);
            movementCMP = GetComponent<Movement>();

            originalPosition = transform.position;
        }

        private void Start()
        {
            currentState.EnterState(this);
        }

        private void Update()
        {
            CalculateDistance();

            currentState.UpdateState(this);
        }

        public void SwitchState(AI_BaseState newState)
        {
            currentState = newState;
            currentState.EnterState(this);
        }

        private void CalculateDistance()
        {
            if (player == null) return;

            Vector3 playerPosition = player.transform.position;
            Vector3 enemyPosition = transform.position;

            distanceFromPlayer = Vector3.Distance(enemyPosition, playerPosition);
        }

        private void OnDrawGizmosSelected()
        {
            Vector3 enemyPositon = transform.position;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(enemyPositon, chaseRange);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(enemyPositon, attackRange);

            Gizmos.color = Color.white;
            Gizmos.DrawLine(enemyPositon, originalPosition);
        }
    }
}