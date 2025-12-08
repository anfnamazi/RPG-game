using UnityEngine;

namespace RPG.Character
{
    public class AIAttackState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.movementCmp.StopMoveAgent();
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.player == null)
            {
                enemy.combatCmp.CancelAttack();
                return;
            }

            if (enemy.distanceFromPlayer > enemy.attackRange)
            {
                enemy.combatCmp.CancelAttack();
                enemy.SwitchState(enemy.chaseState);
                return;
            }

            if (enemy.hasOpenUI) return;

            enemy.combatCmp.StartAttack();
            Vector3 targetPosition = enemy.player.transform.position;
            targetPosition.y = enemy.transform.position.y;
            enemy.transform.LookAt(targetPosition);
        }
    }
}
