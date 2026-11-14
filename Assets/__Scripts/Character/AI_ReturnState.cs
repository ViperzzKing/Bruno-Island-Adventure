using UnityEngine;

namespace RPG.Character
{
    public class AI_ReturnState : AI_BaseState
    {
        Vector3 targetPostion;

        public override void EnterState(EnemyController enemy)
        {
            enemy.movementCMP.UpdateAgentSpeed(enemy.stats.walkSpeed);

            if (enemy.patrolCMP != null)
            {
                targetPostion = enemy.patrolCMP.GetNextPosition();

                enemy.movementCMP.MoveAgentByDestination(targetPostion);
            }
            else
            {
                enemy.movementCMP.MoveAgentByDestination
                    (enemy.originalPosition);
            }
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.distanceFromPlayer <= enemy.chaseRange)
            {
                enemy.SwitchState(enemy.chaseState);
                return;
            }

            if (enemy.movementCMP.ReachedDestination())
            {
                if (enemy.patrolCMP != null)
                {
                    enemy.SwitchState(enemy.patrolState);
                    return;
                }

            }
        }
    }
}