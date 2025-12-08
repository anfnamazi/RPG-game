using System;
using RPG.Core;
using RPG.Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace RPG.Character
{
    public class Health : MonoBehaviour
    {
        public event UnityAction OnStartDefeat = () => { };

        [NonSerialized] public float healthPoints = 0f;
        private Animator animatorCmp;
        private bool isDefeated = false;
        private BubbleEvent bubbleEventCmp;

        [NonSerialized] public Slider sliderCmp;

        public int potionCount = 1;

        [SerializeField]
        private float healAmount = 15f;

        void Awake()
        {
            animatorCmp = GetComponentInChildren<Animator>();
            bubbleEventCmp = GetComponentInChildren<BubbleEvent>();
            sliderCmp = GetComponentInChildren<Slider>();
        }

        void OnEnable()
        {
            bubbleEventCmp.OnBubbleCompleteDefeat += HandleCompleteDefeat;
        }

        void OnDisable()
        {
            bubbleEventCmp.OnBubbleCompleteDefeat -= HandleCompleteDefeat;
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            if (CompareTag(Constants.PLAYER_TAG))
            {
                EventManager.RaiseOnChangeHealth(healthPoints);
            }

            if (sliderCmp != null)
            {
                sliderCmp.value = healthPoints;
            }

            if (healthPoints == 0)
            {
                Defeated();
            }
        }

        public void Defeated()
        {
            if (isDefeated)
                return;

            if (CompareTag(Constants.ENEMY_TAG))
                OnStartDefeat.Invoke();

            isDefeated = true;
            animatorCmp.SetTrigger(Constants.DEFEATED_ANIMATOR_PARAM);
        }

        private void HandleCompleteDefeat()
        {
            Destroy(gameObject);
        }

        public void HandleHeal(InputAction.CallbackContext context)
        {
            if (!context.performed || potionCount == 0)
                return;

            potionCount--;
            healthPoints += healAmount;
            EventManager.RaiseOnChangeHealth(healthPoints);
            EventManager.RaiseOnChangePotions(potionCount);
        }
    }
}
