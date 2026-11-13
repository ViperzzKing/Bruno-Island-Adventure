using UnityEngine;

namespace RPG.Character
{
    public class AI_PatrolState : AI_BaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.patrolCMP.ResetTimers();
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.distanceFromPlayer <= enemy.chaseRange)
            {
                enemy.SwitchState(enemy.chaseState);
            }

            enemy.patrolCMP.CalculateNextPositon();

            Vector3 currentPositon = enemy.transform.position;
            Vector3 newPostion = enemy.patrolCMP.GetNextPosition();
            Vector3 offset = newPostion - currentPositon;

            enemy.movementCMP.MoveAgentByOffset(offset);
        }
    }
}
