
using UnityEngine;

public class LandState : MoveState
{
    private bool isRolling = false;
    protected override void EnterState()
    {
        
        player.ID.playerEvents.OnSwitchAnimation(AnimationType.land);
        player.ID.playerEvents.OnAnimationEnd +=CompleteState;
        player.ID.playerEvents.OnAnimationAction +=CompleteRoll;
        float DirectionFactor = player.IsFacingRight ? 1 : -1;
        newMovementInput = new Vector2(DirectionFactor, 0);
        isRolling = true;
       
    }
    private void CompleteRoll()
    {
        rb2d.velocity = Vector2.zero;
        isRolling = false;
    }
    public override void StateUpdate()  
    {
        if(isRolling)
             MoveAgent(newMovementInput);
    }
    public void CompleteState()
    {
          player.playerStateMachine.TransitionTo(player.playerStateMachine.GetState(StateType.Idle));        
    }
    protected override void HandleMove(Vector2 vector)
    {
     
    }
    protected override void ExitState()
    {
        player.ID.playerEvents.ResetAnimationEvents();
    }
}
