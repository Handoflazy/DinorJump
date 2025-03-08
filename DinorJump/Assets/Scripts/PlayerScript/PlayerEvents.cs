using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct PlayerEvents
{
    public Action OnAttackPressed;
    public Action<Vector2> OnMoveInput;
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
    public Action OnPlayerDied;

    //Weapon Events

    public Action OnUseWeapon;
    public Action<bool> OnMulipleWeapons;

    //PlayerUI Events
    public Action<int> OnInitializeMaxHealth;
    public Action<Sprite> OnWeaponSwap;
    public Action<int> OnHealthValueChange;

    public Action<bool> OnToggleMenu;

    public Action OnWeaponChange;

    //Player Events
    public Action<int> OnPointsValueChange;
    public Action OnPickUpPoints;

    public Action OnResetInputAction;


    public void ResetAnimationEvents()
    {
        OnAnimationAction = null;
        OnAnimationEnd = null;
    }
}
