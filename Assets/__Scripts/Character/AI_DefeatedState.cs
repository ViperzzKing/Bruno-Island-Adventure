using UnityEngine;

namespace RPG.Character
{
    public class AI_DefeatedState : AI_BaseState
    {

        public override void EnterState(EnemyController enemy)
        {
            Debug.Log("Defeated State Entered");
        }

        public override void UpdateState(EnemyController enemy) { }
    }
}