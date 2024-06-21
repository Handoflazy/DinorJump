using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace DesignPatterns.State
{
    public class StateMachine 
    {
        public IState CurrentState { get; private set; }

        public IdleState idleState;
        public MoveState walkState;
        public JumpState jumpState;
        public FallState fallState;

        public event Action<IState> stateChanged;
        public StateMachine(Player player)
        {
            // create an instance for each state and pass in PlayerController
            this.idleState = new IdleState(player);
            this.walkState = new MoveState(player);
            this.jumpState = new JumpState(player);
            this.fallState = new FallState(player);
        }
        public void Initialize(IState state)
        {
            CurrentState = state;
            state.Enter();

            // notify other objects that state has changed
            stateChanged?.Invoke(state);
        }

        // exit this state and enter another
        public void TransitionTo(IState nextState)
        {
            CurrentState.Exit();
            CurrentState = nextState;
            nextState.Enter();

            // notify other objects that state has changed
            stateChanged?.Invoke(nextState);
        }

        // allow the StateMachine to update this state
        public void Update()
        {
            if (CurrentState != null)
            {
                CurrentState.StateUpdate();
            }
        }
        public void FixedUpdate()
        {
            if(CurrentState != null)
            {
                CurrentState.StateFixedUpdate();
            }
        }
    }
}