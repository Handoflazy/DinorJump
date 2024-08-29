using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.Events;

public class AgentAnimation : AgentSystem
{

    [SerializeField]
    Animator animator;
    public bool isFacingRight;

    private void OnEnable()
    {
        agent.ID.playerEvents.OnMoveInput += OnUpdateDirection;
        agent.ID.playerEvents.OnSwitchAnimation += PlayAnimation;
        agent.ID.playerEvents.OnStopAnimation += StopAnimation;
        agent.ID.playerEvents.OnStartAnimation += StartAnimation;
    }
    private void OnDisable()
    {
        agent.ID.playerEvents.OnMoveInput -= OnUpdateDirection;
        agent.ID.playerEvents.OnSwitchAnimation -= PlayAnimation;
        agent.ID.playerEvents.OnStopAnimation -= StopAnimation;
        agent.ID.playerEvents.OnStartAnimation -= StartAnimation;
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
        if (agent.IsAttacking||agent.IsDeath)
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
                SwitchAnimationState(AnimConsts.PLAYER_DEATH_PARAM);
                break;
            case AnimationType.hit:
                SwitchAnimationState(AnimConsts.PLAYER_HIT_PARAM);
                break;
            case AnimationType.idle:
                SwitchAnimationState(AnimConsts.PLAYER_IDLE_PARAM);
                break;
            case AnimationType.attack:
                SwitchAnimationState(AnimConsts.PLAYER_ATTACK_PARAM);
                break;
            case AnimationType.run:
                SwitchAnimationState(AnimConsts.PLAYER_RUN_PARAM);
                break;
            case AnimationType.jump:
                SwitchAnimationState(AnimConsts.PLAYER_JUMP_PARAM);
                break;
            case AnimationType.fall:
                SwitchAnimationState(AnimConsts.PLAYER_FAll_PARAM);
                break;
            case AnimationType.climb:
                SwitchAnimationState(AnimConsts.PLAYER_CLIMB_PARAM);
                break;
            case AnimationType.respawn:
                SwitchAnimationState(AnimConsts.PLAYER_RESPAWN_PARAM);
                break;
            default:
                break;
        }

    }
    public void SwitchAnimationState(string stateName)
    {
        animator.Play(stateName, -1, 0f);
    }


    public void InvokeAnimationAction()
    {
        agent.ID.playerEvents.OnAnimationAction?.Invoke();
    }

    public void InvokeAnimationEnd()
    {
        agent.ID.playerEvents.OnAnimationEnd?.Invoke();
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

