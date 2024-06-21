using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MoveState : State
{

    public MovementDataSO Data;


    protected float currentSpeed = 0;
    protected Vector2 oldMovementInput = Vector2.zero;
    protected Vector2 newMovementInput = Vector2.zero;

    public MoveState(Player player) : base(player)
    {
        this.player = player;
        Data = player.MovementData;
    }

    protected override void EnterState()
    {
        player.ID.playerEvents.OnSwitchAnimation?.Invoke(AnimationType.run);
    }
    public override void StateUpdate()
    {
        MoveAgent(newMovementInput);
        if (Mathf.Abs(rb2d.velocity.x) < 0.01f)
        {
            player.playerStateMachine.TransitionTo(player.playerStateMachine.idleState);
        }
        if (rb2d.velocity.y < 0 && !player.groundedDetector.IsGrounded)
        {
            player.playerStateMachine.TransitionTo(player.playerStateMachine.fallState);
        }

    }
    protected override void HandleJumpPressed()
    {

        player.playerStateMachine.TransitionTo(player.playerStateMachine.jumpState);
    }
    protected override void HandleMove(Vector2 vector)
    {
         newMovementInput = vector.normalized;
    }

    protected virtual void MoveAgent(Vector2 Input)
    {

        if (Mathf.Abs(Input.x) > 0 && currentSpeed >= 0)
        {
            oldMovementInput = Input;
            currentSpeed += Data.runAcceleration * Data.runMaxSpeed * Time.deltaTime;

        }
        else
        {
            currentSpeed -= Data.runDeccelAmount * Data.runMaxSpeed * Time.deltaTime;

        }

        currentSpeed = Mathf.Clamp(currentSpeed, 0, Data.runMaxSpeed);

        rb2d.velocity = new Vector2(oldMovementInput.x * currentSpeed, rb2d.velocity.y);

    }

}


