using RPG.Utility;
using System;
using UnityEngine;

namespace RPG.Character
{
    public class EnemyController : MonoBehaviour
    {
        private GameObject player;
        private Movement movementCMP;

        public float chaseRange = 2.5f;
        public float attackRange = 0.75f;
        [NonSerialized] public float distanceToPlayer;

        private void Awake()
        {
            player = GameObject.FindWithTag(Constants.PLAYER_TAG);
            movementCMP = GetComponent<Movement>();
        }

        private void Update()
        {
            CalculateDistance();
            ChasePlayer();
        }

        private void ChasePlayer()
        {
            if (player == null) return;
            if (distanceToPlayer <= chaseRange)
            {
                movementCMP.MoveAgentByDestination(player.transform.position);
            }
        }

        private void CalculateDistance()
        {
            if (player == null) return;

            Vector3 playerPosition = player.transform.position;
            Vector3 enemyPosition = transform.position;

            distanceToPlayer = Vector3.Distance(enemyPosition, playerPosition);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}