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

            Vector3 oldPosition = enemy.patrolCMP.GetNextPosition();

            enemy.patrolCMP.CalculateNextPositon();

            Vector3 currentPositon = enemy.transform.position;
            Vector3 newPostion = enemy.patrolCMP.GetNextPosition();
            Vector3 offset = newPostion - currentPositon;

            enemy.movementCMP.MoveAgentByOffset(offset);

            Vector3 fartherOutPostion = enemy.patrolCMP.GetFartherOutPosition();
            Vector3 newForwardVector = fartherOutPostion - currentPositon;
            newForwardVector.y = 0;

            enemy.movementCMP.Rotate(newForwardVector);

            if (oldPosition == newPostion)
            {
                enemy.movementCMP.isMoving = false;
            }
        }
    }
}
