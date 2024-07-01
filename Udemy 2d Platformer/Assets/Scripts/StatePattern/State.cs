using DesignPatterns.State;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class State : MonoBehaviour, IState
{
    protected Player player;
    protected Rigidbody2D rb2d;

    [Header("State Event")]
    public UnityEvent OnEnter;
    public UnityEvent OnExit;


    [Space(10)]
    [Header("Animation Event")]
    public UnityEvent OnAction;
    public UnityEvent OnEnd;
    private void Awake()
    {
        player = transform.root.GetComponent<Player>();
        rb2d = player.gameObject.GetComponent<Rigidbody2D>();
    }

    public void Enter()
    {

        player.ID.playerEvents.OnAttackPressed += HandleAttack;
        player.ID.playerEvents.OnMove += HandleMove;
        player.ID.playerEvents.OnJumpPressed += HandleJumpPressed;
        player.ID.playerEvents.OnJumpReleased += HandleJumpReleased;
        player.ID.playerEvents.OnAnimationAction += () => OnAction?.Invoke();
        player.ID.playerEvents.OnAnimationEnd += () => OnEnd?.Invoke();
        OnEnter?.Invoke();
        EnterState();
    }

    protected virtual void EnterState()
    {

    }

    protected virtual void HandleAttack()
    {

    }

    protected virtual void HandleJumpPressed()
    {

    }
    protected virtual void HandleJumpReleased()
    {

    }


    protected virtual void HandleMove(Vector2 vector)
    {

    }

    public void Exit()
    {
        player.ID.playerEvents.OnAttackPressed -= HandleAttack;
        player.ID.playerEvents.OnMove -= HandleMove;
        player.ID.playerEvents.OnJumpPressed -= HandleJumpPressed;
        player.ID.playerEvents.OnJumpReleased -= HandleJumpReleased;
        player.ID.playerEvents.ResetAnimationEvents();
        OnExit?.Invoke();
        ExitState();
    }

    protected virtual void ExitState()
    {

    }

    public virtual void StateUpdate()
    {

    }

    public virtual void StateFixedUpdate()
    {

    }

    public virtual void GetHit()
    {
        player.playerStateMachine.TransitionTo(player.playerStateMachine.GetState(StateType.GetHit));
    }

    public virtual void Die()
    {
        player.playerStateMachine.TransitionTo(player.playerStateMachine.GetState(StateType.Die));
    }

    public virtual void Respawn()
    {
        player.playerStateMachine.TransitionTo(player.playerStateMachine.GetState(StateType.Respawn));
    }
}