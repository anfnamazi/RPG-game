using System.Collections.Generic;
using RPG.Core;
using RPG.Quest;
using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace RPG.UI
{
    public class UIController : MonoBehaviour
    {
        UIBaseState currentState;
        public UIMainMenuState mainMenuState;
        public UIDialogueState dialogueState;
        public UIQuestItemState questItemState;
        public UIDocument uIDocumentCmp;
        public VisualElement root;
        public VisualElement mainMenuContainer;
        public VisualElement playerInfoContainer;
        public Label playerHealthLabel;
        public Label playerPotionsLabel;
        public VisualElement questItemIcon;
        public List<Button> buttons = new();
        public int currentSelection;

        private void Awake()
        {
            uIDocumentCmp = GetComponent<UIDocument>();
            root = uIDocumentCmp.rootVisualElement;

            mainMenuContainer = root.Q<VisualElement>("main-menu-container");
            playerInfoContainer = root.Q<VisualElement>("player-info-container");
            playerHealthLabel = playerInfoContainer.Q<Label>("health-label");
            playerPotionsLabel = playerInfoContainer.Q<Label>("potions-label");
            questItemIcon = playerInfoContainer.Q<VisualElement>("quest-item-icon");

            mainMenuState = new UIMainMenuState(this);
            dialogueState = new UIDialogueState(this);
            questItemState = new UIQuestItemState(this);
        }

        private void OnEnable()
        {
            EventManager.OnChangeHealth += HandleChangePlayerHealth;
            EventManager.OnChangePotions += HandleChangePlayerPotions;
            EventManager.OnInitiateDialogue += HandleInitiateDialogue;
            EventManager.OnTreasureChestUnlock += HandleTreasureChestUnlock;
        }

        private void OnDisable()
        {
            EventManager.OnChangeHealth -= HandleChangePlayerHealth;
            EventManager.OnChangePotions -= HandleChangePlayerPotions;
            EventManager.OnInitiateDialogue -= HandleInitiateDialogue;
            EventManager.OnTreasureChestUnlock -= HandleTreasureChestUnlock;
        }

        // Start is called before the first frame update
        void Start()
        {
            var sceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (sceneIndex == Constants.MAIN_MENU_INDEX_SCENE)
            {
                currentState = mainMenuState;
                currentState.EnterState();
            }
            else
            {
                playerInfoContainer.style.display = DisplayStyle.Flex;
            }
        }

        public void HandleInteract(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;
            currentState.SelectButton();
        }

        public void HandleNavigation(InputAction.CallbackContext context)
        {
            if (!context.performed || buttons.Count == 0)
                return;

            buttons[currentSelection].RemoveFromClassList("active");

            var input = context.ReadValue<Vector2>();
            currentSelection = input.x > 0 ? 1 : -1;
            currentSelection = Mathf.Clamp(currentSelection, 0, buttons.Count - 1);

            buttons[currentSelection].AddToClassList("active");
        }

        private void HandleChangePlayerHealth(float newHealthPoint)
        {
            playerHealthLabel.text = newHealthPoint.ToString();
        }

        private void HandleChangePlayerPotions(int newPotions)
        {
            playerPotionsLabel.text = newPotions.ToString();
        }

        private void HandleInitiateDialogue(TextAsset inkJSON, GameObject npc)
        {
            currentState = dialogueState;
            currentState.EnterState();

            (currentState as UIDialogueState).SetStory(inkJSON, npc);
        }

        private void HandleTreasureChestUnlock(QuestItemSO questItemSO)
        {
            currentState = questItemState;
            currentState.EnterState();

            (currentState as UIQuestItemState).UpdateQuestItemText(questItemSO.itemName);

            questItemIcon.style.display = DisplayStyle.Flex;
        }
    }
}
