using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackState : State
{

    public UnityEvent<AudioClip> OnWeaponSound;

    public LayerMask hittableLayermask;

    [SerializeField]
    private  bool showGizmoz = false;
    protected override void EnterState()
    {
        player.IsAttacking = true;
        player.agentWeapon.ToggleWeaponVisiblity(true);
        rb2d.velocity = new Vector2(0,rb2d.velocity.y);
        player.ID.playerEvents.OnSwitchAnimation(AnimationType.attack);
        player.ID.playerEvents.OnAnimationAction += PerformAttack;
        player.ID.playerEvents.OnAnimationEnd += CompleteAttack;
    }
    private void CompleteAttack()
    {
       player.playerStateMachine.TransitionTo(player.playerStateMachine.GetState(StateType.Idle));
    }

    protected override void ExitState()
    {
        player.ID.playerEvents.ResetAnimationEvents();
        player.agentWeapon.ToggleWeaponVisiblity(false);
        player.IsAttacking = false;
    }
    private void PerformAttack()
    {
        OnWeaponSound?.Invoke(player.agentWeapon.GetCurrentWeapon().weaponSwingSound);
        player.agentWeapon.GetCurrentWeapon().PerformAttack(player, hittableLayermask, Vector3.right);
        player.ID.playerEvents.OnAnimationAction -= PerformAttack;
    }

    private void OnDrawGizmos()
    {
        if(Application.isPlaying== false ) {
            return;
        }
        if (showGizmoz == false)
        {
            return;
        }
        Gizmos.color = Color.red;
        var pos = player.agentWeapon.transform.position;
        player.agentWeapon.GetCurrentWeapon().DrawWeaponGizmo(pos, Vector3.right);
    }
    public override void GetHit()
    {
       
    }
}
