using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quest
{
    [CreateAssetMenu(fileName = "Quest Item", menuName = "RPG/Quest Item SO", order = 1)]
    public class QuestItemSO : ScriptableObject
    {
        [Tooltip("Item name must be uniq to prevent conflicts with other quest items.")]
        public string itemName;
    }
}