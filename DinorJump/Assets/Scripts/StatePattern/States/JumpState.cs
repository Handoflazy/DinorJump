using DesignPatterns.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;

public class JumpState : MoveState
{

    protected override void EnterState()
    {
        player.LastPressedJumpTime = 0;
        rb2d.velocity = Vector2.zero;
        player.ID.PlayerEvents.OnSwitchAnimation?.Invoke(AnimationType.jump);
        player.IsJumping = true;
        player.IsWallJumping = false;
        player._isJumpCut = false;
        player._isJumpFalling = false;
        if(rb2d)
         Jump();
    }
    protected override void HandleJumpReleased()
    {
        if(rb2d.velocity.y> Data.jumpHangTimeThreshold)
        {
            player.playerStateMachine.TransitionTo(player.playerStateMachine.GetState(StateType.Fall));
        }
    }
    protected void Jump()
    {
        float force = player.Data.jumpForce;
        if (rb2d.velocity.y < 0)
            force -= rb2d.velocity.y;

        rb2d.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        player.groundedDetector.IsGrounded = false;
    }
    protected void DoubleJump()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        float force = player.Data.jumpForce;
        if (rb2d.velocity.y < 0)
            force -= rb2d.velocity.y;

        rb2d.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        player.groundedDetector.IsGrounded = false;
    }
    public override void StateUpdate()
    {
        MoveAgent(newMovementInput);
        if (rb2d.velocity.y < 0)
        {
            player.playerStateMachine.TransitionTo(player.playerStateMachine.GetState(StateType.Fall));
        }


    }
    protected override void HandleMove(Vector2 vector)
    {
        newMovementInput = vector.normalized;
        //if (vector.y > 0 && player.climbingDetector.CanClimb)
        //{
        //    player.playerStateMachine.TransitionTo(player.playerStateMachine.climbState);
        //}
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
        Data.currentSpeed = Mathf.Clamp(Data.currentSpeed, 0, targetSpeed);

        rb2d.velocity = new Vector2(oldMovementInput.x * Data.currentSpeed, rb2d.velocity.y);
    }

}
