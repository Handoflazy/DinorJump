using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerEvents
{
    public Action OnAttackPressed;
    public Action<Vector2> OnMove;
    public Action OnJumpPressed;
    public Action OnJumpReleased;

    //Animation Events;
    public Action<AnimationType> OnSwitchAnimation;
    public Action OnStopAnimation;
    public Action OnStartAnimation;
    public Action OnAnimationAction;
    public Action OnAnimationEnd;

    //Spawn Events

    public Action<GameObject> OnRespawnRequired;
    public Action OnPlayerdied;

    //Weapon Events

    public Action OnUseWeapon;

    //PlayerUI Events
    public Action<int> OnInitializeMaxHealth;
    public Action<int> OnHealthValueChange;

   

    public void ResetAnimationEvents()
    {
        OnAnimationAction = null;
        OnAnimationEnd = null;
    }
}
