using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerEvents
{
    public Action OnAttack;
    public Action<Vector2> OnMove;
    public Action OnJumpPressed;
    public Action OnJumpReleased;

    //Animation Events;
    public Action<AnimationType> OnSwitchAnimation;
    public Action OnStopAnimation;
    public Action OnStartAnimation;

}
