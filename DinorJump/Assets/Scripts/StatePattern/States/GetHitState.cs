using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.States
{
    public class GetHitState : State
    {
        protected override void EnterState()
        {
            player.ID.playerEvents.OnSwitchAnimation?.Invoke(AnimationType.hit);
            player.ID.playerEvents.OnAnimationEnd += CompleteAnimation;
        }
        private void CompleteAnimation()
        {
            player.playerStateMachine.TransitionTo(player.playerStateMachine.GetState(StateType.Idle));
            player.ID.playerEvents.ResetAnimationEvents();
        }
        #region HANDLE EVENTS
        protected override void HandleAttack()
        {

        }
        protected override void HandleJumpPressed()
        {

        }
        public override void StateUpdate()
        {

        }
        protected override void HandleMove(Vector2 vector)
        {

        }

        public override void GetHit()
        {

        }
        #endregion
    }
}