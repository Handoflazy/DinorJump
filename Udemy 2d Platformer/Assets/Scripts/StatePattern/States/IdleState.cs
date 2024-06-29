using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns.State;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;

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
        if(player.CanJump()&& player.LastPressedJumpTime > 0)
        {
            player.playerStateMachine.TransitionTo(player.playerStateMachine.jumpState);
        }
        else if (rb2d.velocity.y < 0 && !player.groundedDetector.IsGrounded)
        {
            player.playerStateMachine.TransitionTo(player.playerStateMachine.fallState);
        }
        else if(Mathf.Abs(rb2d.velocity.x) > 0.1f)
        {
            player.playerStateMachine.TransitionTo(player.playerStateMachine.walkState);
        }
    }
    protected override void HandleMove(Vector2 vector)
    {

        if (Mathf.Abs(vector.x) > 0 || Mathf.Abs(rb2d.velocity.x) > 0.1f)
        {
            player.playerStateMachine.TransitionTo(player.playerStateMachine.walkState);
        }
        else if (vector.y > 0 && player.climbingDetector.CanClimb)
        {
            player.playerStateMachine.TransitionTo(player.playerStateMachine.climbState);
        }

    }
    protected override void HandleJumpPressed()
    {
        player.LastPressedJumpTime = player.Data.jumpInputBufferTime;
    }
}
