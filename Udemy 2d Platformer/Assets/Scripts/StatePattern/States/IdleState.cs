using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns.State;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;

public class IdleState : State
{
    public IdleState(Player player) : base(player)
    {
        this.player = player;
    }

    protected override void EnterState()
    {
       player.ID.playerEvents.OnSwitchAnimation?.Invoke(AnimationType.idle);
       if(player.groundedDetector.IsGrounded)
        {
            rb2d.velocity = Vector2.zero;
        }
    }
    public override void StateUpdate()
    {
        if (rb2d.velocity.y < 0)
        {
            player.playerStateMachine.TransitionTo(player.playerStateMachine.fallState);
        }
        if (Mathf.Abs(player.MovementData.movementVector.x) > 0)
        {
            player.playerStateMachine.TransitionTo(player.playerStateMachine.walkState);
        }
        else if (player.MovementData.movementVector.y > 0 && player.climbingDetector.CanClimb)
        {
            player.playerStateMachine.TransitionTo(player.playerStateMachine.climbState);
        }
    }
    protected override void HandleMove(Vector2 vector)
    {

        player.MovementData.movementVector = vector;
       
    }
    protected override void HandleJumpPressed()
    {
        player.playerStateMachine.TransitionTo(player.playerStateMachine.jumpState);
    }
}
