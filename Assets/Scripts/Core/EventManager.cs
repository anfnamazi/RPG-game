using RPG.Quest;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Core
{
    public static class EventManager
    {
        public static event UnityAction<float> OnChangeHealth;
        public static event UnityAction<int> OnChangePotions;
        public static event UnityAction<TextAsset, GameObject> OnInitiateDialogue;
        public static event UnityAction<QuestItemSO> OnTreasureChestUnlock;
        public static event UnityAction<bool> OnToggleUI;
        public static event UnityAction<RewardSO> OnReward;
        public static event UnityAction<Collider, int> OnPortalEnter;

        public static void RaiseChangeHealth(float newHealthPoint) =>
            OnChangeHealth?.Invoke(newHealthPoint);

        public static void RaiseChangePotions(int newPotion) =>
            OnChangePotions?.Invoke(newPotion);

        public static void RaiseInitiateDialogue(TextAsset inkJSON, GameObject gameObject) =>
            OnInitiateDialogue?.Invoke(inkJSON, gameObject);

        public static void RaiseTreasureChestUnlock(QuestItemSO questItemSO) =>
            OnTreasureChestUnlock?.Invoke(questItemSO);

        public static void RaiseToggleUI(bool isOpened) =>
            OnToggleUI?.Invoke(isOpened);

        public static void RaiseReward(RewardSO reward) =>
            OnReward?.Invoke(reward);

        public static void RaisePortalEnter(Collider player, int nextSceneIndex) =>
            OnPortalEnter?.Invoke(player, nextSceneIndex);
    }
}
