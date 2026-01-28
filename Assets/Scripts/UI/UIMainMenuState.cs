using RPG.Core;
using RPG.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace RPG.UI
{
    public class UIMainMenuState : UIBaseState
    {
        private int sceneIndex;
        public UIMainMenuState(UIController ui)
            : base(ui) { }

        public override void EnterState()
        {
            if (PlayerPrefs.HasKey("SceneIndex"))
            {
                sceneIndex = PlayerPrefs.GetInt("SceneIndex");
                AddButton();
            }

            controller.mainMenuContainer.style.display = DisplayStyle.Flex;
            controller.buttons = controller
                .mainMenuContainer.Query<Button>(null, "menu-button")
                .ToList();
            controller.buttons[0].AddToClassList("active");
        }

        public override void SelectButton()
        {
            var button = controller.buttons[controller.currentSelection];
            if (button.name == "start-button")
            {
                PlayerPrefs.DeleteAll();
                // SceneManager.LoadScene(1);
                SceneTransition.Initiate(Constants.ISLAND_INDEX_SCENE);
            }
            else
            {
                SceneTransition.Initiate(sceneIndex);
            }
        }

        private void AddButton()
        {
            var continueButton = new Button();
            continueButton.AddToClassList("menu-button");
            continueButton.text = "Continue";

            var mainMenuButtons = controller.mainMenuContainer.Q<VisualElement>("buttons");
            mainMenuButtons.Add(continueButton);
        }
    }
}
