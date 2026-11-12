namespace RPG.Character
{
    public class AI_AttackState : AI_BaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.movementCMP.StopMovingAgent();
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.distanceFromPlayer > enemy.attackRange)
            {
                enemy.SwitchState(enemy.chaseState);
                return;
            }
        }
    }
}