using DesignPatterns.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FallState : MoveState
{
    protected override void EnterState()
    {
        SetGravityScale(Data.gravityScale * Data.fallGravityMult);
        player.ID.PlayerEvents.OnSwitchAnimation?.Invoke(AnimationType.fall);
    } 
    public override void StateUpdate()
    {

        MoveAgent(Data.MoveVector);
        rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Max(rb2d.velocity.y, -Data.maxFallSpeed));
        if (Mathf.Abs(rb2d.velocity.y) < Data.jumpHangTimeThreshold)
        {
            SetGravityScale(Data.gravityScale * Data.jumpHangGravityMult);
        }
        else
        {
            SetGravityScale(Data.gravityScale * Data.fallGravityMult);
        }
        if (player.CanJump() && player.LastPressedJumpTime > 0)
        {
            player.playerStateMachine.TransitionTo(player.playerStateMachine.GetState(StateType.Jump));
        }
        else if (player.groundedDetector.IsGrounded&&rb2d.velocity.y <0.01f)
        {
            if(Mathf.Abs(newMovementInput.x)>0)
                player.playerStateMachine.TransitionTo(player.playerStateMachine.GetState(StateType.Move));
            else
            {
                player.playerStateMachine.TransitionTo(player.playerStateMachine.GetState(StateType.Idle));
            }
        }
    }

    protected override void HandleMove(Vector2 vector)
    {
        Data.MoveVector = vector;
    }

    protected override void MoveAgent(Vector2 Input)
    {
        float targetSpeed = Data.runMaxSpeed;
        if (Mathf.Abs(Input.x) > 0 && Data.currentSpeed >= 0)
        {
            oldMovementInput = Input;
            Data.currentSpeed += Data.runAcceleration * Data.runMaxSpeed * Time.deltaTime;

        }
        else
        {
            Data.currentSpeed -= Data.runDeccelAmount * Data.runMaxSpeed * Time.deltaTime;

        }
        if (Mathf.Abs(rb2d.velocity.y) < Data.jumpHangTimeThreshold)
        {

            Data.currentSpeed *= Data.jumpHangAccelerationMult;
            targetSpeed *= Data.jumpHangMaxSpeedMult;
        }
        Data.currentSpeed = Mathf.Clamp(Data.currentSpeed, 0, targetSpeed);

        rb2d.velocity = new Vector2(oldMovementInput.x * Data.currentSpeed, rb2d.velocity.y);


    }
   
}
