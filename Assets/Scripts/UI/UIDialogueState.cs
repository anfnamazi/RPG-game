using System.Collections.Generic;
using Ink.Runtime;
using RPG.Character;
using RPG.Core;
using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace RPG.UI
{
    public class UIDialogueState : UIBaseState
    {
        private VisualElement dialogueContainer;
        private Label dialogueText;
        private VisualElement nextButton;
        private VisualElement choicesGroup;
        private Story currentStory;
        private PlayerInput playerInputCmp;
        private NPCController npcController;
        private bool hasChoices = false;

        public UIDialogueState(UIController ui)
            : base(ui) { }

        public override void EnterState()
        {
            dialogueContainer = controller.root.Q("dialogue-container");
            dialogueText = dialogueContainer.Q<Label>("dialogue-text");
            nextButton = dialogueContainer.Q<VisualElement>("dialogue-next-button");
            choicesGroup = dialogueContainer.Q<VisualElement>("choices-group");

            dialogueContainer.style.display = DisplayStyle.Flex;

            playerInputCmp = GameObject
                .FindGameObjectWithTag(Constants.GAME_MANAGER_TAG)
                .GetComponent<PlayerInput>();
            playerInputCmp.SwitchCurrentActionMap(Constants.UI_ACTION_MAP);
        }

        public override void SelectButton()
        {
            UpdateStory();
        }

        public void SetStory(TextAsset inkJSON, GameObject npc)
        {
            currentStory = new Story(inkJSON.text);
            currentStory.BindExternalFunction("VerifyQuest", VerifyQuest);

            npcController = npc.GetComponent<NPCController>();

            UpdateStory();
        }

        public void UpdateStory()
        {
            if (hasChoices)
            {
                currentStory.ChooseChoiceIndex(controller.currentSelection);
            }

            if (!currentStory.canContinue)
            {
                ExitDialogue();
                return;
            }

            dialogueText.text = currentStory.Continue();

            hasChoices = currentStory.currentChoices.Count > 0;
            if (hasChoices)
            {
                HandleNewChoices(currentStory.currentChoices);
            }
            else
            {
                choicesGroup.style.display = DisplayStyle.None;
                nextButton.style.display = DisplayStyle.Flex;
            }
        }

        public void HandleNewChoices(List<Choice> choices)
        {
            nextButton.style.display = DisplayStyle.None;
            choicesGroup.style.display = DisplayStyle.Flex;

            choicesGroup.Clear();
            controller.buttons?.Clear();

            choices.ForEach(CreateNewChoiceButton);

            controller.buttons = choicesGroup.Query<Button>().ToList();
            controller.buttons[0].AddToClassList("active");
            controller.currentSelection = 0;
        }

        private void CreateNewChoiceButton(Choice choice)
        {
            var button = new Button();
            button.AddToClassList("menu-button");
            button.text = choice.text;
            button.style.marginRight = 20;

            choicesGroup.Add(button);
        }

        private void ExitDialogue()
        {
            dialogueContainer.style.display = DisplayStyle.None;
            playerInputCmp.SwitchCurrentActionMap(Constants.GAMEPLAY_ACTION_MAP);
        }

        public void VerifyQuest()
        {
            npcController.CheckPlayerForQuestItem();
        }
    }
}
