using SpriteAnimations;
using UnityEngine;

public class AgentSpriteAnimation : AgentSystem
{
    [SerializeField]
    SpriteAnimator animator;

    public bool isFacingRight;

    private void OnEnable()
    {
        Agent.ID.PlayerEvents.OnMoveInput += OnUpdateDirection;
        Agent.ID.PlayerEvents.OnSwitchAnimation += PlayAnimation;
        Agent.ID.PlayerEvents.OnStopAnimation += StopAnimation;
        Agent.ID.PlayerEvents.OnStartAnimation += StartAnimation;
    }
    private void OnDisable()
    {
        Agent.ID.PlayerEvents.OnMoveInput -= OnUpdateDirection;
        Agent.ID.PlayerEvents.OnSwitchAnimation -= PlayAnimation;
        Agent.ID.PlayerEvents.OnStopAnimation -= StopAnimation;
        Agent.ID.PlayerEvents.OnStartAnimation -= StartAnimation;
    }

    private void StopAnimation()
    {
        animator.enabled = false;
    }
    private void StartAnimation()
    {
        animator.enabled = true;
    }
    private void OnUpdateDirection(Vector2 direction)
    {
        if (Agent.IsAttacking || Agent.IsDeath)
        {
            return;
        }
        if (direction.x > 0)
        {
            transform.parent.rotation = Quaternion.Euler(0, 0, 0);

        }
        else if (direction.x < 0)
        {
            transform.parent.rotation = Quaternion.Euler(0, 180, 0);

        }


    }
    public void PlayAnimation(AnimationType animationType)
    {
        if (animator == null)
            return;
        switch (animationType)
        {

            case AnimationType.die:
                SwitchAnimationState(AnimConsts.PLAYER_DEATH_STATE);
                break;
            case AnimationType.hit:
                SwitchAnimationState(AnimConsts.PLAYER_HIT_STATE);
                break;
            case AnimationType.idle:
                SwitchAnimationState(AnimConsts.PLAYER_IDLE_STATE);
                break;
            case AnimationType.attack:
                SwitchAnimationState(AnimConsts.PLAYER_ATTACK_STATE);
                break;
            case AnimationType.run:
                SwitchAnimationState(AnimConsts.PLAYER_RUN_STATE);
                break;
            case AnimationType.jump:
                SwitchAnimationState(AnimConsts.PLAYER_JUMP_STATE);
                break;
            case AnimationType.fall:
                SwitchAnimationState(AnimConsts.PLAYER_FAll_STATE);
                break;
            case AnimationType.climb:
                SwitchAnimationState(AnimConsts.PLAYER_CLIMB_STATE);
                break;
            case AnimationType.respawn:
                SwitchAnimationState(AnimConsts.PLAYER_RESPAWN_STATE);
                break;
            default:
                break;
        }

    }

    public void SwitchAnimationState(string stateName)
    {
        animator.Play(stateName).SetOnFrame("OnAction", InvokeAnimationAction).SetOnEnd(InvokeAnimationEnd);
    }

    public void InvokeAnimationAction(Frame frame)
    {
        Agent.ID.PlayerEvents.OnAnimationAction?.Invoke();
    }

    public void InvokeAnimationEnd()
    {
        Agent.ID.PlayerEvents.OnAnimationEnd?.Invoke();
    }
}
