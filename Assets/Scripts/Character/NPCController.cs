using UnityEngine;

namespace RPG.Character
{
    public class NPCController : MonoBehaviour
    {
        private Canvas canvasCmp;

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
    }
}
