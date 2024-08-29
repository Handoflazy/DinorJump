using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS.AI
{
    public class AIBehaviorBossIdle : AIBehavior
    {
       
        public override bool PerformAction(AIEnemy enemyAI)
        {
            enemyAI.MovementVector = Vector3.zero;
            enemyAI.CallOnMovement(Vector2.zero);
            return true;
        }
    }
}