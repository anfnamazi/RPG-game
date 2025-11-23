using System;
using RPG.Utility;
using UnityEngine;

namespace RPG.Character
{
    public class EnemyController : MonoBehaviour
    {
        [NonSerialized]
        public float distanceFromPlayer;

        [NonSerialized]
        public Vector3 originalPosition;

        [NonSerialized]
        public Movement movementCmp;

        [NonSerialized]
        public GameObject player;

        [NonSerialized]
        public Patrol patrolCmp;

        public CharacterStatsSO stats;

        private Health healthCmp;

        [NonSerialized]
        public Combat combatCmp;
        public float chaseRange = 2.5f;
        public float attackRange = 0.75f;

        private AIBaseState currentState;
        public AIReturnState returnState = new AIReturnState();
        public AIChaseState chaseState = new AIChaseState();
        public AIAttackState attackState = new AIAttackState();
        public AIPatrolState parolState = new AIPatrolState();
        public AIDefeatState defeatState = new AIDefeatState();

        private void Awake()
        {
            if (stats == null)
            {
                Debug.Log($"{name} have not stats!");
            }

            currentState = returnState;

            player = GameObject.FindWithTag(Constants.PLAYER_TAG);
            movementCmp = GetComponent<Movement>();
            patrolCmp = GetComponent<Patrol>();
            healthCmp = GetComponent<Health>();
            combatCmp = GetComponent<Combat>();

            originalPosition = transform.position;
        }

        private void Start()
        {
            currentState.EnterState(this);
            healthCmp.healthPoints = stats.health;
            combatCmp.damage = stats.damage;
            if (healthCmp.sliderCmp != null)
            {
                healthCmp.sliderCmp.maxValue = stats.health;
                healthCmp.sliderCmp.value = stats.health;
            }
        }

        private void Update()
        {
            CalculateDistanceFromPlayer();

            currentState.UpdateState(this);
        }

        void OnEnable()
        {
            healthCmp.OnStartDefeat += HandleStartDefeat;
        }

        void OnDisable()
        {
            healthCmp.OnStartDefeat += HandleStartDefeat;
        }

        public void SwitchState(AIBaseState newState)
        {
            currentState = newState;
            currentState.EnterState(this);
        }

        private void CalculateDistanceFromPlayer()
        {
            if (player == null)
                return;

            Vector3 enemyPosition = transform.position;
            Vector3 playerPosition = player.transform.position;

            distanceFromPlayer = Vector3.Distance(enemyPosition, playerPosition);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }

        private void HandleStartDefeat()
        {
            SwitchState(defeatState);
        }
    }
}
