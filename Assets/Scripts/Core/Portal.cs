using RPG.Utility;
using UnityEngine;

namespace RPG.Core
{
    public class Portal : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Constants.PLAYER_TAG)) return;

            print("Player detected!");
        }
    }
}
