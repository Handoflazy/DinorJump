using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.IO.LowLevel.Unsafe;
using UnityEditorInternal;
using UnityEngine;
using static UnityEngine.Random;

namespace DesignPatterns.States
{
    public class StateMachine : AgentSystem
    {
        [field: SerializeField]
        public IState CurrentState { get; private set; }

        [Space(20)]
        [SerializeField]
        private State idleState, walkState, jumpState, fallState, climbState, attackState
        ,getHitState, dieState, respawnState, flyState;

        private event Action<IState> stateChanged;
        protected override void Awake()
        {
            base.Awake();
        }
        private void Start()
        {
            CurrentState = idleState;
            CurrentState.Enter();

            // notify other objects that state has changed
            stateChanged?.Invoke(CurrentState);
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
            if(nextState == null) return;
            CurrentState.Exit();
            CurrentState = nextState;
            nextState.Enter();
            // notify other objects that state has changed
            stateChanged?.Invoke(nextState);
        }
        public IState GetState(StateType stateType)
            => stateType switch
            {
                StateType.Idle => idleState,
                StateType.Fall => fallState,
                StateType.Attack => attackState,
                StateType.Climb => climbState,
                StateType.Jump => jumpState,
                StateType.Move => walkState,
                StateType.GetHit =>getHitState,
                StateType.Die => dieState,
                StateType.Respawn => respawnState,
                _ => throw new System.Exception("State not define" + stateType.ToString())
            };

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
            if (CurrentState != null)
            {
                CurrentState.StateFixedUpdate();
            }
        }

        public void GetHit()
        {
            CurrentState.GetHit();
        }

    }
}
public enum StateType
{
    Idle, Move, Jump, Fall, Climb, Attack, GetHit, Die,Land,Respawn

}