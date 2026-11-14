using UnityEngine;

namespace RPG.Character
{
    public class PlayerController : MonoBehaviour
    {
        public CharacterStats_SO stats;
        private Health healthCMP;
        private Combat combatCMP;

        private void Awake()
        {
            if (stats == null) Debug.LogWarning($"{name} does not have stats");

            healthCMP = GetComponent<Health>();
            combatCMP = GetComponent<Combat>();
        }

        private void Start()
        {
            healthCMP.healthPoints = stats.health;
            combatCMP.damage = stats.damage;
        }
    }
}
