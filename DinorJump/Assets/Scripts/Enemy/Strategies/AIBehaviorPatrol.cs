using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SVS.AI
{
    public class AIBehaviorPatrol : AIBehavior
    {
        public AIEndPlatformDetector changeDirectionDetector;
        private Vector2 movementVector = Vector2.zero;
        protected void Awake()
        {
            
            if(changeDirectionDetector == null)
            {
                changeDirectionDetector = GetComponent<AIEndPlatformDetector>();
            }
        }
        private void Start()
        {
            changeDirectionDetector.OnPathBlocked += HandlePathBlocked;
            movementVector = new Vector2(Random.value > 0.5f ? -1:1, 0);
        }

        private void HandlePathBlocked()
        {
            movementVector *= new Vector2(-1, 0);
        }

        public override bool PerformAction(AIEnemy enemyAI)
        {
            
            enemyAI.MovementVector = movementVector;
            enemyAI.CallOnMovement(movementVector);
            return true;
        }

        
    }
}