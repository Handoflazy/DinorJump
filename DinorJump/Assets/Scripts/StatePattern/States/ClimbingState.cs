using DesignPatterns.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingState : MoveState
{
    protected override void EnterState()
    {
        rb2d.velocity = Vector2.zero;
        player.ID.PlayerEvents.OnSwitchAnimation?.Invoke(AnimationType.climb);
        rb2d.gravityScale = 0;
    }

    public override void StateUpdate()
    {
        MoveAgent(Data.MoveVector);
       
        if (Data.MoveVector.y > 0)
        {
            Climp();
        }
        if (player.climbingDetector.CanClimb == false)
        {
            if (Mathf.Abs(rb2d.velocity.x) > 0.1f)
                player.playerStateMachine.TransitionTo(player.playerStateMachine.GetState(StateType.Move));
            else
            {
                player.playerStateMachine.TransitionTo(player.playerStateMachine.GetState(StateType.Idle));
            }
        }
    }
    protected override void HandleJumpPressed()
    {
        player.playerStateMachine.TransitionTo(player.playerStateMachine.GetState(StateType.Jump));
    }
    private void Climp()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, Data.climbSpeed);
    }
    protected override void HandleMove(Vector2 vector)
    {
        Data.MoveVector = vector;
        if (vector.magnitude == 0)
        {
            player.ID.PlayerEvents.OnStopAnimation?.Invoke();

        }
        else
        {
            player.ID.PlayerEvents.OnStartAnimation?.Invoke();
        }
        if(vector.y<0)
        {
            player.playerStateMachine.TransitionTo(player.playerStateMachine.GetState(StateType.Idle));
        }
    }
    protected override void ExitState()
    {
        base.ExitState();
        rb2d.velocity = new Vector2(rb2d.velocity.x, Data.climbSpeed*2.5f);
        player.ID.PlayerEvents.OnStartAnimation?.Invoke();

    }
}
