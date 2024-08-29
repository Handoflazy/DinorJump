using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS.AI
{
    public class AIBehaviorBossWait : AIBehavior
    {
        [SerializeField]
        private AIDataBoard aiBoard;
        [SerializeField]
        private float waitTime = 1;
        [SerializeField]
        private Damageable damageable;
        public override bool PerformAction(AIEnemy enemyAI)
        {
            enemyAI.MovementVector = Vector2.zero;
            StartCoroutine(WaitCoroutine());
            return true;
        }
        private IEnumerator WaitCoroutine()
        {
            yield return new WaitForSeconds(waitTime*damageable.GetRatio());
            aiBoard.SetBoard(AIDataTypes.Waiting, false);
        }
    }
}