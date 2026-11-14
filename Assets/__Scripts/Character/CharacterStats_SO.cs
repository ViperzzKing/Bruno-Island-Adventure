using UnityEngine;

namespace RPG.Character
{
    [CreateAssetMenu(
        fileName = "Character Stats",
        menuName = "RPG SO/Character Stats",
        order = 0
    )]
    public class CharacterStats_SO : ScriptableObject
    {
        public float health = 100;
        public float damage = 10;

        public float walkSpeed = 1;
        public float runSpeed = 1.5f;
    }
}

