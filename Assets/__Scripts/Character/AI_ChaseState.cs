namespace RPG.Character
{
    public class AI_ChaseState : AI_BaseState
    {
        public override void EnterState(EnemyController enemy)
        {

        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.distanceFromPlayer > enemy.chaseRange)
            {
                enemy.SwitchState(enemy.returnState);
                return;
            }

            if (enemy.distanceFromPlayer <= enemy.attackRange)
            {
                enemy.SwitchState(enemy.attackState);
                return;
            }

            enemy.movementCMP.MoveAgentByDestination
                (enemy.player.transform.position);
        }
    }
}