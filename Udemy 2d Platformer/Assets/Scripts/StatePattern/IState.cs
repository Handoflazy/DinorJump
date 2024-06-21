using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.State
{
    public interface IState
    {
        void Enter();
        void StateUpdate();
        void StateFixedUpdate();
        void Exit();
    }
}