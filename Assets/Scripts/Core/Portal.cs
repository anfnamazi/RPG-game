using RPG.Utility;
using UnityEngine;

namespace RPG.Core
{
    public class Portal : MonoBehaviour
    {
        private Collider colliderCmp;
        [SerializeField] private int nextSceneIndex;

        private void Awake()
        {
            colliderCmp = GetComponent<Collider>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Constants.PLAYER_TAG)) return;

            colliderCmp.enabled = false;
            SceneTransition.Initiate(nextSceneIndex);
        }
    }
}
