using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS.AI
{
    public class AIBehaviorBossMeleeAttack : AIBehavior
    {
        [SerializeField]
        private AIDataBoard aiBoard;
        public override bool PerformAction(AIEnemy enemyAI)
        {
            enemyAI.MovementVector = Vector2.zero;
            enemyAI.CallOnAttack();

            aiBoard.SetBoard(AIDataTypes.Arrived,true);
            aiBoard.SetBoard(AIDataTypes.Waiting,true);
            return true;
           
        }
    }
}