using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MoveState : State
{

    public MovementDataSO Data;
    protected Vector2 oldMovementInput = Vector2.zero;
    protected Vector2 newMovementInput = Vector2.zero;

    public MoveState(Player player) : base(player)
    {
        this.player = player;
        Data = player.MovementData;
    }

    protected override void EnterState()
    {
        newMovementInput = Data.movementVector;
        player.ID.playerEvents.OnSwitchAnimation?.Invoke(AnimationType.run);

    }
    public override void StateUpdate()
    {
        MoveAgent(newMovementInput);
        if (Mathf.Abs(rb2d.velocity.x) < 0.01f)
        {
            player.playerStateMachine.TransitionTo(player.playerStateMachine.idleState);
        }
        else if (rb2d.velocity.y < 0 && !player.groundedDetector.IsGrounded)
        {
            player.playerStateMachine.TransitionTo(player.playerStateMachine.fallState);
        }



    }

    protected override void HandleJumpPressed()
    {
        if (player.groundedDetector.IsGrounded)
            player.playerStateMachine.TransitionTo(player.playerStateMachine.jumpState);
    }
    protected override void HandleMove(Vector2 vector)
    {
        newMovementInput = vector.normalized;
        if (vector.y > 0 && player.climbingDetector.CanClimb)
        {
            player.playerStateMachine.TransitionTo(player.playerStateMachine.climbState);
        }
    }

    protected virtual void MoveAgent(Vector2 Input)
    {

        if (Mathf.Abs(Input.x) > 0 && Data.currentSpeed >= 0)
        {
            oldMovementInput = Input;
            Data.currentSpeed += Data.runAcceleration * Data.runMaxSpeed * Time.deltaTime;

        }
        else
        {
            Data.currentSpeed -= Data.runDeccelAmount * Data.runMaxSpeed * Time.deltaTime;

        }

        Data.currentSpeed = Mathf.Clamp(Data.currentSpeed, 0, Data.runMaxSpeed);

        rb2d.velocity = new Vector2(oldMovementInput.x * Data.currentSpeed, rb2d.velocity.y);

    }
    public void SetGravityScale(float scale)
    {
        rb2d.gravityScale = scale;
 
    }
    protected override void ExitState()
    {
        SetGravityScale(Data.gravityScale);
        Data.movementVector = newMovementInput;
    }
}


