using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns.States;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;

namespace DesignPatterns.States
{
    public class IdleState : State
    {

        protected override void EnterState()
        {
            player.ID.playerEvents.OnSwitchAnimation?.Invoke(AnimationType.idle);
            if (player.groundedDetector.IsGrounded)
            {
                rb2d.velocity = Vector2.zero;
            }
        }
        public override void StateUpdate()
        {
            if (player.CanJump() && player.LastPressedJumpTime > 0)
            {
                player.playerStateMachine.TransitionTo(player.playerStateMachine.GetState(StateType.Jump));
            }
            else if (rb2d.velocity.y < 0 && !player.groundedDetector.IsGrounded)
            {
                player.playerStateMachine.TransitionTo(player.playerStateMachine.GetState(StateType.Fall));
            }
        }
        protected override void HandleMove(Vector2 vector)
        {

            player.Data.MoveVector = vector;
            if (!rb2d)
                return;
            if (Mathf.Abs(vector.x) > 0 || Mathf.Abs(rb2d.velocity.x) > 0.1f)
            {
                player.playerStateMachine.TransitionTo(player.playerStateMachine.GetState(StateType.Move));
            }
            else if (vector.y > 0 && player.climbingDetector.CanClimb)
            {
                player.playerStateMachine.TransitionTo(player.playerStateMachine.GetState(StateType.Climb));
            }

        }
        protected override void HandleAttack()
        {
            if (player.agentWeapon.CanIUseWeapon(player.groundedDetector.IsGrounded))
            {
                player.playerStateMachine.TransitionTo(player.playerStateMachine.GetState(StateType.Attack));
            }
        }
        protected override void HandleJumpPressed()
        {
            player.LastPressedJumpTime = player.Data.jumpInputBufferTime;
        }
    }
}