using UnityEngine;

namespace RPG.Character
{
    public class AIReturnState : AIBaseState
    {
        private Vector3 targetPosition;
        public override void EnterState(EnemyController enemy)
        {

            if (enemy.patrolCmp == null)
            {
                enemy.movementCmp.MoveAgentByDestination(enemy.originalPosition);
                return;
            }

            enemy.movementCmp.UpdateAgentSpeed(enemy.stats.walkSpeed, true);
            targetPosition = enemy.patrolCmp.GetNextPosition();
            enemy.movementCmp.MoveAgentByDestination(targetPosition);
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.distanceFromPlayer < enemy.chaseRange)
            {
                enemy.SwitchState(enemy.chaseState);
                return;
            }

            if (enemy.movementCmp.ReachDestination())
            {
                if (enemy.patrolCmp != null)
                {
                    enemy.SwitchState(enemy.parolState);
                    return;
                }
                enemy.movementCmp.isMoving = false;
                enemy.movementCmp.Rotate(enemy.movementCmp.originalForward);
            }
            else
            {
                if (enemy.patrolCmp != null)
                {
                    Vector3 patrolDirection = targetPosition - enemy.transform.position;
                    patrolDirection.y = 0;
                    enemy.movementCmp.Rotate(patrolDirection);
                }
                else
                {
                    Vector3 originalDirection = enemy.originalPosition - enemy.transform.position;
                    originalDirection.y = 0;
                    enemy.movementCmp.Rotate(originalDirection);
                }
            }


        }
    }
}