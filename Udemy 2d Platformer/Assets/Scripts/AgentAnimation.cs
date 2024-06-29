﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.Events;

public class AgentAnimation : PlayerSystem
{

    [SerializeField]
    Animator animator;


    private Rigidbody2D rb2d;

    protected override void Awake()
    {
        base.Awake();
        rb2d = transform.root.GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        player.ID.playerEvents.OnMove += OnUpdateDirection;
        player.ID.playerEvents.OnSwitchAnimation += PlayAnimation;
        player.ID.playerEvents.OnStopAnimation += StopAnimation;
        player.ID.playerEvents.OnStartAnimation += StartAnimation;
    }
    private void OnDisable()
    {
        player.ID.playerEvents.OnMove -= OnUpdateDirection;
        player.ID.playerEvents.OnSwitchAnimation -= PlayAnimation;
        player.ID.playerEvents.OnStopAnimation -= StopAnimation;
        player.ID.playerEvents.OnStartAnimation -= StartAnimation;
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

        if (direction.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }


    }


    public void PlayAnimation(AnimationType animationType)
    {
        if (animator == null)
            return;
        switch (animationType)
        {
            case AnimationType.die:
                break;
            case AnimationType.hit:
                break;
            case AnimationType.idle:
                SwitchAnimationState(AnimConsts.PLAYER_IDLE_PARAM);
                break;
            case AnimationType.attack:
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
            case AnimationType.land:
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
        player.ID.playerEvents.OnAnimationAction?.Invoke();
    }

    public void InvokeAnimationEnd()
    {
        player.ID.playerEvents.OnAnimationEnd?.Invoke();
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
    land
}

