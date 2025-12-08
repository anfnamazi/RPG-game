using RPG.Quest;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Core
{
    public static class EventManager
    {
        public static event UnityAction<float> OnChangeHealth;
        public static event UnityAction<int> OnChangePotions;
        public static event UnityAction<TextAsset> OnInitiateDialogue;
        public static event UnityAction<QuestItemSO> OnTreasureChestUnlock;
        public static event UnityAction<bool> OnToggleUI;

        public static void RaiseOnChangeHealth(float newHealthPoint) =>
            OnChangeHealth?.Invoke(newHealthPoint);

        public static void RaiseOnChangePotions(int newPotion) =>
            OnChangePotions?.Invoke(newPotion);

        public static void RaiseInitiateDialogue(TextAsset inkJSON) =>
            OnInitiateDialogue?.Invoke(inkJSON);

        public static void RaiseTreasureChestUnlock(QuestItemSO questItemSO) =>
            OnTreasureChestUnlock?.Invoke(questItemSO);

        public static void RaiseToggleUI(bool isOpened) =>
            OnToggleUI?.Invoke(isOpened);
    }
}
