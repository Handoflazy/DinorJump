using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SVS.AI
{
    public  class AIEnemy : AgentSystem
    {
        public Vector2 MovementVector;
        internal void PerformAction(AIPatrollingEnemyBrain aIPatrollingEnemyBrain)
        {
            throw new NotImplementedException();
        }
        public void CallOnAttack()
        {
            agent.ID.playerEvents.OnAttackPressed?.Invoke();
        }
        public void CallJumpPressed()
        {
            agent.ID.playerEvents.OnJumpPressed?.Invoke();
        } 
        public void CallJumpReleased()
        {
            agent.ID.playerEvents.OnJumpReleased?.Invoke();
        }
        public void CallOnMovement(Vector2 input)
        {
            agent.ID.playerEvents.OnMoveInput?.Invoke(input);
        }
        public void CallOnWeaponChange()
        {
            agent.ID.playerEvents.OnWeaponChange?.Invoke();
        }
    }
}