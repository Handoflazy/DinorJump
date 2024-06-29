using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingState : MoveState
{
    protected override void EnterState()
    {
        rb2d.velocity = Vector2.zero;
        player.ID.playerEvents.OnSwitchAnimation(AnimationType.climb);
        player.ID.playerEvents.OnAnimationAction += () => OnAction?.Invoke();
    }

    public override void StateUpdate()
    {
        MoveAgent(newMovementInput);
        if(newMovementInput.y > 0)
        {
            Climp();
        }
        if (player.climbingDetector.CanClimb == false)
        {
            if(Mathf.Abs(rb2d.velocity.x)>0.1f)
                player.playerStateMachine.TransitionTo(player.playerStateMachine.walkState);
            else
            {
                player.playerStateMachine.TransitionTo(player.playerStateMachine.idleState);
            }
        }
    }
    protected override void HandleJumpPressed()
    {
        player.playerStateMachine.TransitionTo(player.playerStateMachine.jumpState);
    }
    private void Climp()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, Data.climbSpeed);
    }
    protected override void HandleMove(Vector2 vector)
    {

        newMovementInput = vector.normalized;
        if (vector.magnitude == 0)
        {
            rb2d.gravityScale = 0;
            rb2d.velocity = Vector3.zero;
            player.ID.playerEvents.OnStopAnimation?.Invoke();

        }
        else
        {
            player.ID.playerEvents.OnStartAnimation?.Invoke();
        }
        if(vector.y<0)
        {
            player.playerStateMachine.TransitionTo(player.playerStateMachine.idleState);
        }
    }
    protected override void ExitState()
    {
        base.ExitState();
        rb2d.velocity = new Vector2(rb2d.velocity.x, Data.climbSpeed*2.5f);
        player.ID.playerEvents.OnStartAnimation?.Invoke();

    }
}
