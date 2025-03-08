using DesignPatterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class DieState : State
{
    public float TimeToWaitBeforeRespawn = 2;
    protected override void EnterState()
    {
        SetGravityScale(2);
        rb2d.GetComponent<Collider2D>().enabled = false;
        player.ID.PlayerEvents.OnSwitchAnimation?.Invoke(AnimationType.die);
        player.ID.PlayerEvents.OnAnimationEnd += WaitBeforeDieAction;
        

    }
    private void WaitBeforeDieAction()
    {
        player.ID.PlayerEvents.OnAnimationEnd -= WaitBeforeDieAction;
        StartCoroutine(WaiTCoroutine());
    }
    public override void StateUpdate()
    {
         rb2d.velocity = new Vector2(0,rb2d.velocity.y);
    }
    IEnumerator WaiTCoroutine()
    {
        yield return new WaitForSeconds(TimeToWaitBeforeRespawn);
        player.ID.PlayerEvents.OnPlayerDied?.Invoke();
    }
    protected override void ExitState()
    {
        StopAllCoroutines();
    }
    public override void GetHit()
    {
       
    }
    public void SetGravityScale(float scale)
    {
        if (rb2d == null)
            return;
        if (scale == float.NaN)
            rb2d.gravityScale = 1;
        rb2d.gravityScale = scale;

    }
}
