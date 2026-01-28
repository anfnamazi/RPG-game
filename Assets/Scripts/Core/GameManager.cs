using RPG.Character;
using UnityEngine;

namespace RPG.Core
{
    public class GameManager : MonoBehaviour
    {
        void OnEnable()
        {
            EventManager.OnPortalEnter += HandlePortalEnter;
        }

        void OnDisable()
        {
            EventManager.OnPortalEnter -= HandlePortalEnter;
        }

        void HandlePortalEnter(Collider player, int nextSceneIndex)
        {
            var playerController = player.GetComponent<PlayerController>();

            PlayerPrefs.SetFloat("Health", playerController.healthCmp.healthPoints);
            PlayerPrefs.SetInt("Potions", playerController.healthCmp.potionCount);
            PlayerPrefs.SetFloat("Damage", playerController.combatCmp.damage);
            PlayerPrefs.SetInt("Weapon", (int)playerController.weapon);
            PlayerPrefs.SetInt("SceneIndex", nextSceneIndex);
        }
    }
}
