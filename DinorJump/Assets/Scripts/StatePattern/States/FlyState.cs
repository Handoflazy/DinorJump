using DesignPatterns;
using DesignPatterns.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyState : MoveState
{
    protected override void EnterState()
    {
        base.EnterState();
    }

    public override void StateUpdate()
    {
        MoveAgent(Data.MoveVector);
        if (Mathf.Abs(rb2d.velocity.x) < 0.01f)
        {

            player.playerStateMachine.TransitionTo(player.playerStateMachine.GetState(StateType.Idle));
        }




    }

    protected override void MoveAgent(Vector2 Input)
    {
       
        if (Input.magnitude>0)
        {
            oldMovementInput = Input;
            Data.currentSpeed += Data.runAcceleration * Data.runMaxSpeed * Time.deltaTime;

        }
        else
        {
            Data.currentSpeed -= Data.runDeccelAmount * Data.runMaxSpeed * Time.deltaTime;

        }

        Data.currentSpeed = Mathf.Clamp(Data.currentSpeed, 0, Data.runMaxSpeed);

        rb2d.velocity = oldMovementInput * Data.currentSpeed;

    }
    protected override void HandleMove(Vector2 vector)
    {
        Data.MoveVector = vector;
    }
    protected override void ExitState()
    {
       //
    }
}
