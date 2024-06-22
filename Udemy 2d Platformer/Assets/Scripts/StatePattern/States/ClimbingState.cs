using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingState : MoveState
{
    public ClimbingState(Player player) : base(player)
    {
        this.player = player;
    }
    protected override void EnterState()
    {
        base.EnterState();
        rb2d.velocity = Vector3.zero;
        player.ID.playerEvents.OnSwitchAnimation(AnimationType.climb);
    }

    public override void StateUpdate()
    {
        MoveAgent(newMovementInput);
        if (player.climbingDetector.CanClimb == false)
        {
            if(Mathf.Abs(rb2d.velocity.x)>0.1)
                player.playerStateMachine.TransitionTo(player.playerStateMachine.walkState);
            else
            {
                player.playerStateMachine.TransitionTo(player.playerStateMachine.idleState);
            }
        }
    }
    private void Climp()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, Data.climbSpeed);
    }
    protected override void HandleMove(Vector2 vector)
    {
        newMovementInput = vector.normalized;
        if (vector.y <= 0)
        {
            player.playerStateMachine.TransitionTo(player.playerStateMachine.idleState);
        }
        else if(vector.y>0)
        {
            Climp();
        }
    }
}
