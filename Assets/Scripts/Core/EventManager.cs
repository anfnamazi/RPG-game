using UnityEngine;
using UnityEngine.Events;

namespace RPG.Core
{
    public static class EventManager
    {
        public static event UnityAction<float> OnChangeHealth;
        public static event UnityAction<int> OnChangePotions;
        public static event UnityAction<TextAsset> OnInitiateDialogue;
        public static event UnityAction OnTreasureChestUnlock;

        public static void RaiseOnChangeHealth(float newHealthPoint) =>
            OnChangeHealth?.Invoke(newHealthPoint);

        public static void RaiseOnChangePotions(int newPotion) =>
            OnChangePotions?.Invoke(newPotion);

        public static void RaiseInitiateDialogue(TextAsset inkJSON) =>
            OnInitiateDialogue?.Invoke(inkJSON);

        public static void RaiseTreasureChestUnlock() =>
            OnTreasureChestUnlock?.Invoke();
    }
}
