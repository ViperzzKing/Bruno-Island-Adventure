namespace RPG.Character
{
    public abstract class AI_BaseState
    {
        public abstract void EnterState(EnemyController enemy);

        public abstract void UpdateState(EnemyController enemy);
    }
}

