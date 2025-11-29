using Ink.Runtime;
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

        public UIDialogueState(UIController ui)
            : base(ui) { }

        public override void EnterState()
        {
            dialogueContainer = controller.root.Q("dialogue-container");
            dialogueText = dialogueContainer.Q<Label>("dialogue-text");
            nextButton = dialogueContainer.Q<Label>("dialogue-next-button");
            choicesGroup = dialogueContainer.Q<Label>("choices-group");

            dialogueContainer.style.display = DisplayStyle.Flex;

            playerInputCmp = GameObject
                .FindGameObjectWithTag(Constants.GAME_MANAGER_TAG)
                .GetComponent<PlayerInput>();
            playerInputCmp.SwitchCurrentActionMap(Constants.UI_ACTION_MAP);
        }

        public override void SelectButton()
        {
            throw new System.NotImplementedException();
        }

        public void SetStory(TextAsset inkJSON)
        {
            currentStory = new Story(inkJSON.text);
            UpdateStory();
        }

        public void UpdateStory()
        {
            dialogueText.text = currentStory.Continue();
        }
    }
}
