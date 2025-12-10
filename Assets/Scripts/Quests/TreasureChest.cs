using RPG.Core;
using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;


namespace RPG.Quest
{
    public class TreasureChest : MonoBehaviour
    {
        [SerializeField] private QuestItemSO questItemSO;
        public Animator animatorCmp;
        private bool isIntractable = false;
        private bool hasBeenOpened = false;
        private void OnTriggerEnter()
        {
            isIntractable = true;
        }

        private void OnTriggerExit(Collider other)
        {
            isIntractable = false;
        }

        public void handleInteract(InputAction.CallbackContext context)
        {
            if (!isIntractable || hasBeenOpened || !context.performed) return;

            EventManager.RaiseTreasureChestUnlock(questItemSO);
            animatorCmp.SetBool(Constants.IS_SHAKING_ANIMATOR_PARAM, false);
            hasBeenOpened = true;
        }
    }

}
