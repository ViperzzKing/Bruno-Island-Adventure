namespace RPG.Character
{
    public class AI_ReturnState : AI_BaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.movementCMP.MoveAgentByDestination
                (enemy.originalPosition);
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.distanceFromPlayer <= enemy.chaseRange)
            {
                enemy.SwitchState(enemy.chaseState);
                return;
            }
        }
    }
}