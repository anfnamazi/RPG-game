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
    }
}
