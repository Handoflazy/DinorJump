using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

namespace SVS.AI
{
    public class AIBehaviorBossCharge : AIBehavior
    {
        [SerializeField]
        private AIDataBoard aiBoard;

        [SerializeField]
        private AIPLayerEnterAreaDetector playerDetector;

        [SerializeField]
        private Agent agent;

        private Vector3 tempPosition;
        private Vector2 direction;

        private bool initialized = false;
        [SerializeField]
        private Damageable damageable;
       
        public override bool PerformAction(AIEnemy enemyAI)
        {
            ChangeAcce();
            if (aiBoard.CheckBoard(AIDataTypes.Arrived))
                initialized = false;
            SetChargeDestination();

            ChargeAtThePlayer(enemyAI);
            if (aiBoard.CheckBoard(AIDataTypes.PathBlocked))
            {
                enemyAI.MovementVector = Vector2.zero;
                aiBoard.SetBoard(AIDataTypes.Waiting,true);
                aiBoard.SetBoard(AIDataTypes.Arrived,true);
            }
            return true;
        }

        private void ChargeAtThePlayer(AIEnemy enemyAI)
        {
            enemyAI.CallOnMovement(direction.normalized);
            //enemyAI.MovementVector()
        }

        private void SetChargeDestination()
        {
            if(initialized)
            {
                return;
            }
            tempPosition = new Vector2(playerDetector.Player.position.x,agent.transform.position.y);
            direction = tempPosition - agent.transform.position;
            aiBoard.SetBoard(AIDataTypes.Arrived,false);
            initialized = true;
        }

        private void ChangeAcce()
        {
            agent.Data.runAcceleration = agent.Data.runAcceleration/damageable.GetRatio();
        }
    }
}