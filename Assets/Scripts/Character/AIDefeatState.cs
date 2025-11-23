using RPG.Character;
using UnityEngine;

public class AIDefeatState : AIBaseState
{
    public override void EnterState(EnemyController enemy)
    {
        Debug.Log("defeat state entering");
    }

    public override void UpdateState(EnemyController enemy) { }
}
