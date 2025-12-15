using RPG.Core;
using RPG.Quest;
using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Character
{
    public class NPCController : MonoBehaviour
    {
        public TextAsset inkJSON;
        public QuestItemSO desiredQuestItem;
        private Canvas canvasCmp;
        public bool hasQuestItem;

        private void Awake()
        {
            canvasCmp = GetComponentInChildren<Canvas>();
        }

        private void OnTriggerEnter()
        {
            canvasCmp.enabled = true;
        }

        private void OnTriggerExit()
        {
            canvasCmp.enabled = false;
        }

        public void HandleInteract(InputAction.CallbackContext context)
        {
            if (!context.performed || !canvasCmp.enabled)
                return;

            if (inkJSON == null)
            {
                Debug.LogWarning("Please add an ink file to the NPC");
                return;
            }

            EventManager.RaiseInitiateDialogue(inkJSON, gameObject);
        }

        public bool CheckPlayerForQuestItem()
        {
            if (hasQuestItem) return true;

            var inventoryCmp = GameObject.FindWithTag(Constants.PLAYER_TAG)
                .GetComponent<Inventory>();

            hasQuestItem = inventoryCmp.HasItem(desiredQuestItem);

            return hasQuestItem;
        }
    }
}
