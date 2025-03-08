using UnityEngine;
using UnityEngine.Serialization;

public class AgentAnimation : AgentSystem
{

    [SerializeField] Animator animator;
    public bool isFacingRight;
    [SerializeField] private float kCrossFadeTime = 0.15f;

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
        if (Agent.IsAttacking||Agent.IsDeath)
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

    AnimationType currentAnimationType;

    private void PlayAnimation(AnimationType animationType)
    {
        if (animator == null|| currentAnimationType == animationType)
            return;
        currentAnimationType = animationType;
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

    private void SwitchAnimationState(string stateName)
    {
        Debug.Log(stateName);
        animator.CrossFade(stateName,kCrossFadeTime);
    }


    public void InvokeAnimationAction()
    {
        Agent.ID.PlayerEvents.OnAnimationAction?.Invoke();
    }

    public void InvokeAnimationEnd()
    {
        Agent.ID.PlayerEvents.OnAnimationEnd?.Invoke();
    }
}


public enum AnimationType
{
    die,
    hit,
    idle,
    attack,
    run,
    jump,
    fall,
    climb,
    respawn
}

