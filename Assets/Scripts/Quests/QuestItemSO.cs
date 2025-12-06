using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quest
{
    [CreateAssetMenu(fileName = "Quest Item", menuName = "Udemy RPG/Quest Item SO", order = 1)]
    public class QuestItemSO : ScriptableObject
    {
        public string itemName;
    }
}