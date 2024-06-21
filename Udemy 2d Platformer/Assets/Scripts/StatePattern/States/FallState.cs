using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FallState : MoveState
{

    public FallState(Player player) : base(player)
    {
        this.player = player;
    }
    
    protected override void EnterState()
    {
        base.EnterState();
        SetGravityScale(Data.gravityScale * Data.fallGravityMult);
        player.ID.playerEvents.OnSwitchAnimation(AnimationType.fall);
    }
    public override void StateUpdate()
    {

        MoveAgent(newMovementInput);
        rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Max(rb2d.velocity.y, -Data.maxFallSpeed));
        if (player.groundedDetector.IsGrounded&&rb2d.velocity.y <0.01f)
        {
            if (Mathf.Abs(rb2d.velocity.x) > 0.1f)
            {
                player.playerStateMachine.TransitionTo(player.playerStateMachine.walkState);

            }
            else
                player.playerStateMachine.TransitionTo(player.playerStateMachine.idleState);
           
        }
    }
    protected override void HandleMove(Vector2 vector)
    {
        base.HandleMove(vector);
        if (vector.y == -1)
        {
            PerformFastFall();

        }
    }

    private void PerformFastFall()
    {
        SetGravityScale(Data.fastFallGravityMult);
        rb2d.velocity = Vector2.down;
    }
    public void SetGravityScale(float scale)
    {
        rb2d.gravityScale = scale;
    }
    protected override void ExitState()
    {
        SetGravityScale(Data.gravityScale);
    }
    protected override void HandleJumpPressed()
    {
        //if (!player.MovementData.doubleJump)
        //{

        //    DoubleJump();
        //   player.MovementData.doubleJump = true;
        //}
    }
    protected void DoubleJump()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x,0);
        SetGravityScale(player.MovementData.gravityScale);
        float force = player.MovementData.jumpForce;
        if (rb2d.velocity.y < 0)
            force -= rb2d.velocity.y;

        rb2d.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        player.groundedDetector.IsGrounded = false;
    }
}
