using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS.AI
{
    public class BossAIBrain : AIEnemy
    {
        [SerializeField]
        private AIDataBoard aiBoard;
        [SerializeField]
        private AIPLayerEnterAreaDetector playerDetector;
        [SerializeField]
        private AIMeleeAttackDetector meleeAttackDetector;
        [SerializeField]
        private AIEndPlatformDetector endPlatformDetector;
        [SerializeField]
        private AIBehavior IdleBehaviour, ChargeBehavior, MeleeAttackBehavior, WaitBehavior;

        private void Update()
        {
            aiBoard.SetBoard(AI.AIDataTypes.PlayerDetected, playerDetector.PlayerInArea);
            aiBoard.SetBoard(AI.AIDataTypes.InMeleeRange, meleeAttackDetector.PlayerDetected);
            aiBoard.SetBoard(AI.AIDataTypes.PathBlocked, endPlatformDetector.PathBlocked);

            if(aiBoard.CheckBoard(AIDataTypes.PlayerDetected))
            {
                if(aiBoard.CheckBoard(AIDataTypes.Waiting))
                {
                    WaitBehavior.PerformAction(this);
                }
                else
                {
                    if (aiBoard.CheckBoard(AIDataTypes.InMeleeRange))
                    {
                        MeleeAttackBehavior.PerformAction(this);
                    }
                    else
                    {
                        ChargeBehavior.PerformAction(this);
                    }
                }
            }
            else
            {
                IdleBehaviour.PerformAction(this);
            }
        }


    }
}