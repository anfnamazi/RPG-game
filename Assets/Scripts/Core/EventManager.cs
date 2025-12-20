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

        public static void RaiseOnChangeHealth(float newHealthPoint) =>
            OnChangeHealth?.Invoke(newHealthPoint);

        public static void RaiseOnChangePotions(int newPotion) =>
            OnChangePotions?.Invoke(newPotion);

        public static void RaiseInitiateDialogue(TextAsset inkJSON, GameObject gameObject) =>
            OnInitiateDialogue?.Invoke(inkJSON, gameObject);

        public static void RaiseTreasureChestUnlock(QuestItemSO questItemSO) =>
            OnTreasureChestUnlock?.Invoke(questItemSO);

        public static void RaiseToggleUI(bool isOpened) =>
            OnToggleUI?.Invoke(isOpened);

        public static void RaiseReward(RewardSO reward) =>
            OnReward?.Invoke(reward);
    }
}
