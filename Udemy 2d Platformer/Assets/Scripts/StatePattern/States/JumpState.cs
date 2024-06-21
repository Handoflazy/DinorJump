using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;

public class JumpState : MoveState
{
    public JumpState(Player player) : base(player)
    {
        this.player = player;
    }

    protected override void EnterState()
    {
        base.EnterState();
        player.ID.playerEvents.OnSwitchAnimation(AnimationType.jump);
        player.MovementData.doubleJump = false;
        Jump();
    }
    protected override void HandleJumpReleased()
    {
        if(rb2d.velocity.y> Data.jumpHangTimeThreshold)
        {
            player.playerStateMachine.TransitionTo(player.playerStateMachine.fallState);
        }
    }
    protected void Jump()
    {
        float force = player.MovementData.jumpForce;
        if (rb2d.velocity.y < 0)
            force -= rb2d.velocity.y;

        rb2d.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        player.groundedDetector.IsGrounded = false;
    }
    protected void DoubleJump()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        float force = player.MovementData.jumpForce;
        if (rb2d.velocity.y < 0)
            force -= rb2d.velocity.y;

        rb2d.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        player.groundedDetector.IsGrounded = false;
    }
    public override void StateUpdate()
    {
        MoveAgent(newMovementInput);
        if (Mathf.Abs(rb2d.velocity.y) < Data.jumpHangTimeThreshold)
        {
            SetGravityScale(Data.gravityScale * Data.jumpHangGravityMult);
        }
        else if (rb2d.velocity.y < 0)
        {
            player.playerStateMachine.TransitionTo(player.playerStateMachine.fallState);
        }


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
        if(Mathf.Abs(rb2d.velocity.y) < Data.jumpHangTimeThreshold)
        {

            Data.currentSpeed *= Data.jumpHangAccelerationMult;
            targetSpeed *= Data.jumpHangMaxSpeedMult;
        }
        Data.currentSpeed = Mathf.Clamp(Data.currentSpeed, 0, targetSpeed);

        rb2d.velocity = new Vector2(oldMovementInput.x * Data.currentSpeed, rb2d.velocity.y);

      
    }

}
