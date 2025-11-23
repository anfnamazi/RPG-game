using UnityEngine;
using UnityEngine.Events;

namespace RPG.Utility
{
    public class BubbleEvent : MonoBehaviour
    {
        public event UnityAction OnBubbleStartAttack = () => { };
        public event UnityAction OnBubbleCompleteAttack = () => { };
        public event UnityAction OnBubbleHitAttack = () => { };
        public event UnityAction OnBubbleCompleteDefeat = () => { };

        public void OnStartAttack()
        {
            OnBubbleStartAttack.Invoke();
        }

        public void OnCompleteAttack()
        {
            OnBubbleCompleteAttack.Invoke();
        }

        public void OnHitAttack()
        {
            OnBubbleHitAttack.Invoke();
        }

        public void OnCompleteDefeat()
        {
            OnBubbleCompleteDefeat.Invoke();
        }
    }
}
