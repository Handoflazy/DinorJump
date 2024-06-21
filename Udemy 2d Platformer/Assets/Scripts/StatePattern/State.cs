using DesignPatterns.State;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class State : IState
{
    protected Player player;
    protected Rigidbody2D rb2d;
    public UnityEvent OnEnter, OnExit;

    
    public State(Player player)
    {
        this.player = player;
        rb2d = player.gameObject.GetComponent<Rigidbody2D>();
    }

    public void Enter()
    {
       
        player.ID.playerEvents.OnAttack += HandleAttack;
        player.ID.playerEvents.OnMove += HandleMove;
        player.ID.playerEvents.OnJumpPressed += HandleJumpPressed;
        player.ID.playerEvents.OnJumpReleased += HandleJumpReleased;
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
        player.ID.playerEvents.OnAttack = HandleAttack;
        player.ID.playerEvents.OnMove -= HandleMove;
        player.ID.playerEvents.OnJumpPressed -= HandleJumpPressed;
        player.ID.playerEvents.OnJumpReleased -= HandleJumpReleased;
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
}
