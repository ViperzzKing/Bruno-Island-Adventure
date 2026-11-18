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
            if (enemy.player == null)
            {
                enemy.combatCMP.CancelAttack();
                return;
            }

            if (enemy.distanceFromPlayer > enemy.attackRange)
            {
                enemy.combatCMP.CancelAttack();

                enemy.SwitchState(enemy.chaseState);
                return;
            }

            enemy.combatCMP.StartAttack();
            enemy.transform.LookAt(enemy.player.transform);
        }
    }
}