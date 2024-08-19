using SVS.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBehaviorMeleeAttack : AIBehavior
{
    public AIMeleeAttackDetector MeleeRangeDetector;

    [SerializeField]
    private bool AttaclCooldown = false;

    [SerializeField]
    private float delay = 1;

    private void Awake()
    {
        if(MeleeRangeDetector == null)
        {
            MeleeRangeDetector = transform.root.GetComponentInChildren<AIMeleeAttackDetector>();
        }
    }
    public override bool PerformAction(AIEnemy enemyAI)
    {
        if (AttaclCooldown)
            return true;
        if(MeleeRangeDetector.PlayerDetected == false )
        {
            return false;
        }
        enemyAI.CallOnAttack();
        AttaclCooldown = true;
        StartCoroutine(AttackDelayCooldown());
        return true;
    }

    private IEnumerator AttackDelayCooldown()
    {
        yield return new WaitForSeconds(delay);
        AttaclCooldown = false;
    }
}
