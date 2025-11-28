using Ink.Runtime;
using RPG.Core;
using UnityEngine;
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

        public UIDialogueState(UIController ui)
            : base(ui) { }

        public override void EnterState()
        {
            dialogueContainer = controller.root.Q("dialogue-container");
            dialogueText = dialogueContainer.Q<Label>("dialogue-text");
            nextButton = dialogueContainer.Q<Label>("dialogue-next-button");
            choicesGroup = dialogueContainer.Q<Label>("choices-group");

            dialogueContainer.style.display = DisplayStyle.Flex;
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
