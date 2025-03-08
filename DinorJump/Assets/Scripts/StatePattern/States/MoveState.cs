using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

namespace DesignPatterns.States
{
    public class MoveState : State
    {
        protected MovementDataSO Data;
        protected Vector2 oldMovementInput = Vector2.zero;
        protected Vector2 newMovementInput = Vector2.zero;

        private void Start()
        {
            Data = player.Data;
        }
        protected override void EnterState()
        {
            player.ID.PlayerEvents.OnSwitchAnimation?.Invoke(AnimationType.run);
        }
        public override void StateUpdate()
        {
            MoveAgent(Data.MoveVector);
            if (player.CanJump() && player.LastPressedJumpTime > 0)
            {
                player.playerStateMachine.TransitionTo(player.playerStateMachine.GetState(StateType.Jump));
            }
            else if (Mathf.Abs(rb2d.velocity.x) < 0.01f)
            {

                player.playerStateMachine.TransitionTo(player.playerStateMachine.GetState(StateType.Idle));
            }
            else if (rb2d.velocity.y < 0 && !player.groundedDetector.IsGrounded)
            {
                player.playerStateMachine.TransitionTo(player.playerStateMachine.GetState(StateType.Fall));
            }



        }

        protected override void HandleJumpPressed()
        {
            player.LastPressedJumpTime = Data.jumpInputBufferTime;
        }
        protected override void HandleMove(Vector2 vector)
        {
            Data.MoveVector = vector;
            if (vector.y > 0 && player.climbingDetector.CanClimb)
            {
                player.playerStateMachine.TransitionTo(player.playerStateMachine.GetState(StateType.Climb));
            }
        }
        protected override void HandleAttack()
        {
            if (player.agentWeapon.CanIUseWeapon(player.groundedDetector.IsGrounded))
            {
                player.playerStateMachine.TransitionTo(player.playerStateMachine.GetState(StateType.Attack));
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
            if (rb2d == null)
                return;
            if (scale == float.NaN)
                rb2d.gravityScale = 1;
            rb2d.gravityScale = scale;

        }
        protected override void ExitState()
        {
            SetGravityScale(Data.gravityScale);
        }
        public void MoveEnemy(Vector2 input)
        {
            newMovementInput = input.normalized;
        }
    }
}