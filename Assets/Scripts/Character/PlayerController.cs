using RPG.Core;
using RPG.Quest;
using UnityEngine;

namespace RPG.Character
{
    public class PlayerController : MonoBehaviour
    {
        public CharacterStatsSO stats;
        private Health healthCmp;
        private Combat combatCmp;

        private void Awake()
        {
            if (stats == null)
            {
                Debug.Log($"{name} have not stats!");
            }

            healthCmp = GetComponent<Health>();
            combatCmp = GetComponent<Combat>();
        }

        private void Start()
        {
            healthCmp.healthPoints = stats.health;
            combatCmp.damage = stats.damage;

            EventManager.RaiseOnChangeHealth(healthCmp.healthPoints);
            EventManager.RaiseOnChangePotions(healthCmp.potionCount);
        }

        void OnEnable()
        {
            EventManager.OnReward += HandleReward;
        }
        void OnDisable()
        {
            EventManager.OnReward -= HandleReward;

        }

        private void HandleReward(RewardSO reward)
        {
            healthCmp.healthPoints += reward.bonusHealth;
            healthCmp.potionCount += reward.bonusPotions;
            combatCmp.damage += reward.bonusDamage;

            EventManager.RaiseOnChangeHealth(healthCmp.healthPoints);
            EventManager.RaiseOnChangePotions(healthCmp.potionCount);
        }
    }
}
