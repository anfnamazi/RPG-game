using System;
using RPG.Core;
using RPG.Quest;
using RPG.Utility;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Character
{
    public class PlayerController : MonoBehaviour
    {
        public CharacterStatsSO stats;
        [NonSerialized] public Health healthCmp;
        [NonSerialized] public Combat combatCmp;
        private GameObject axeWeapon;
        private GameObject swordWeapon;

        public Weapons weapon = Weapons.Axe;

        private void Awake()
        {
            if (stats == null)
            {
                Debug.Log($"{name} have not stats!");
            }

            healthCmp = GetComponent<Health>();
            combatCmp = GetComponent<Combat>();

            axeWeapon = GameObject.FindGameObjectWithTag(Constants.AXE_TAG);
            swordWeapon = GameObject.FindGameObjectWithTag(Constants.SWORD_TAG);
        }

        private void Start()
        {
            if (PlayerPrefs.HasKey("Health"))
            {
                healthCmp.healthPoints = PlayerPrefs.GetFloat("Health");
                healthCmp.potionCount = PlayerPrefs.GetInt("Potions");
                combatCmp.damage = PlayerPrefs.GetFloat("Damage");
                weapon = (Weapons)PlayerPrefs.GetInt("Weapon");

                var agentCmp = GetComponent<NavMeshAgent>();
                var portalCmp = FindObjectOfType<Portal>();

                agentCmp.Warp(portalCmp.spawnPoint.position);
                transform.rotation = portalCmp.spawnPoint.rotation;
            }
            else
            {
                healthCmp.healthPoints = stats.health;
                combatCmp.damage = stats.damage;
            }

            EventManager.RaiseChangeHealth(healthCmp.healthPoints);
            EventManager.RaiseChangePotions(healthCmp.potionCount);

            SetWeapon();
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

            EventManager.RaiseChangeHealth(healthCmp.healthPoints);
            EventManager.RaiseChangePotions(healthCmp.potionCount);

            if (reward.forceWeaponSwitch)
            {
                weapon = reward.weapons;
                SetWeapon();
            }
        }

        private void SetWeapon()
        {
            if (weapon == Weapons.Axe)
            {
                axeWeapon.SetActive(true);
                swordWeapon.SetActive(false);
            }
            else
            {
                axeWeapon.SetActive(false);
                swordWeapon.SetActive(true);
            }
        }
    }
}
