using RPG.Core;
using RPG.Utility;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.UI
{
    public class UIMainMenuState : UIBaseState
    {
        public UIMainMenuState(UIController ui)
            : base(ui) { }

        public override void EnterState()
        {
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
                SceneTransition.Initiate(Constants.ISLAND_INDEX_SCENE);
            }
        }
    }
}
