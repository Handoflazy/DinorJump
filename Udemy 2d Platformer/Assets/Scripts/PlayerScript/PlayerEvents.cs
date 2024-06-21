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


    public Action<AnimationType> OnSwitchAnimation;
}
