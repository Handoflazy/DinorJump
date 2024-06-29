using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditorInternal;
using UnityEngine;
using static UnityEngine.Random;

namespace DesignPatterns.State
{
    public class StateMachine: PlayerSystem
    {
        [field:SerializeField]
        public IState CurrentState { get; private set; }

        [Space(20)]

        public IdleState idleState;
        public MoveState walkState;
        public JumpState jumpState;
        public FallState fallState;
        public ClimbingState climbState;

        public event Action<IState> stateChanged;
        protected override void Awake()
        {
            base.Awake();
                     
            this.idleState = GetComponentInChildren<IdleState>();
            this.walkState = GetComponentInChildren<MoveState>();
            this.jumpState = GetComponentInChildren<JumpState>();
            this.fallState = GetComponentInChildren < FallState>();
            this.climbState = GetComponentInChildren<ClimbingState>();
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