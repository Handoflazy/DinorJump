using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SVS.AI
{
    public class AIEnemy : AgentSystem
    {
        private Vector2 movementVector;
        public Vector2 MovementVector
        {
            get { return movementVector; }
            set
            {
                movementVector = value;
                CallOnMovement(value); 
            }
        }
        internal void PerformAction(AIPatrollingEnemyBrain aIPatrollingEnemyBrain)
        {
            throw new NotImplementedException();
        }
        public void CallOnAttack()
        {
            Agent.ID.PlayerEvents.OnAttackPressed?.Invoke();
        }
        public void CallJumpPressed()
        {
            Agent.ID.PlayerEvents.OnJumpPressed?.Invoke();
        }
        public void CallJumpReleased()
        {
            Agent.ID.PlayerEvents.OnJumpReleased?.Invoke();
        }
        public void CallOnMovement(Vector2 input)
        {
            Agent.ID.PlayerEvents.OnMoveInput?.Invoke(input);
        }
        public void CallOnWeaponChange()
        {
            Agent.ID.PlayerEvents.OnWeaponChange?.Invoke();
        }
    }
}