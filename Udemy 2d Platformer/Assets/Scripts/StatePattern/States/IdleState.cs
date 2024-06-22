using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns.State;

public class IdleState : State
{
    public IdleState(Player player) : base(player)
    {
        this.player = player;
    }

    protected override void EnterState()
    {
       player.ID.playerEvents.OnSwitchAnimation?.Invoke(AnimationType.idle);
    }
    public override void StateUpdate()
    {
        if (rb2d.velocity.y < 0)
        {
            player.playerStateMachine.TransitionTo(player.playerStateMachine.fallState);
        }

    }
    protected override void HandleMove(Vector2 vector)
    {
        if (Mathf.Abs(vector.x) > 0)
        {
            player.playerStateMachine.TransitionTo(player.playerStateMachine.walkState);
        }
        if (vector.y > 0 && player.climbingDetector.CanClimb)
        {
            player.playerStateMachine.TransitionTo(player.playerStateMachine.climbState);
        }
       
    }
    protected override void HandleJumpPressed()
    {
        player.playerStateMachine.TransitionTo(player.playerStateMachine.jumpState);
    }
}
