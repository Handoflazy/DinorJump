using DesignPatterns;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.States
{
    public class RespawnState : State
    {
        public float waitTime = 3;

        protected override void EnterState()
        {
            player.ID.PlayerEvents.OnSwitchAnimation?.Invoke(AnimationType.respawn);
            WaitForTransition();
            rb2d.velocity = Vector3.zero;

        }
        private void WaitForTransition()
        {
            StartCoroutine(WaitTime());
        }
        private IEnumerator WaitTime()
        {
            player.GetComponent<Collider2D>().enabled = false;
            yield return new WaitForSeconds(waitTime);
            player.GetComponent<Collider2D>().enabled = true;
            player.playerStateMachine.TransitionTo(player.playerStateMachine.GetState(StateType.Idle));
        }
        public override void GetHit()
        {

        }
        public override void Die()
        {

        }
        public override void Respawn()
        {

        }
        protected override void ExitState()
        {
            StopAllCoroutines();
        }
    }
}