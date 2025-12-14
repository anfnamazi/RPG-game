using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

namespace RPG.Quest
{
    public class Inventory : MonoBehaviour
    {
        public List<QuestItemSO> items = new();

        void OnEnable()
        {
            EventManager.OnTreasureChestUnlock += HandleTreasureChestUnlock;
        }

        void OnDisable()
        {
            EventManager.OnTreasureChestUnlock -= HandleTreasureChestUnlock;
        }

        public void HandleTreasureChestUnlock(QuestItemSO questItem)
        {
            items.Add(questItem);
        }

        public bool HasItem(QuestItemSO desiredItem)
        {
            bool hasItem = false;

            items.ForEach(item =>
            {
                if (desiredItem.name == item.name) hasItem = true;
            });

            return hasItem;
        }
    }
}
