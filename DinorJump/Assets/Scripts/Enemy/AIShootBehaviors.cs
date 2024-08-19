using SVS.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShootBehaviors : AIBehavior
{
    public AIPlayerDetector RangeDetector;

    [SerializeField]
    private bool AttackCooldown = false;
    [SerializeField]
    private float delay = 1;

    private Agent agent;
    private void Awake()
    {
        agent = transform.root.GetComponent<Agent>();
        if (RangeDetector == null)
        {
           RangeDetector = transform.root.GetComponentInChildren<AIPlayerDetector>();
        }
    }

    private bool CheckFaceDirection()
    {
        if(agent.IsFacingRight)
        {
            return RangeDetector.DirectionToTarget.x > 0;
        }
        else
        {
            return RangeDetector.DirectionToTarget.x < 0;
        }

    }
    public override bool PerformAction(AIEnemy enemyAI)
    {
        if (AttackCooldown)
            return true;
        if (RangeDetector.PlayerDetected == false)
        {
            return false;
        }
        if(!CheckFaceDirection())
        {
            enemyAI.CallOnMovement(RangeDetector.DirectionToTarget);
            return false;
        }
        enemyAI.CallOnAttack();
        AttackCooldown = true;
        StartCoroutine(AttackDelayCooldown());
        return true;
    }

    private IEnumerator AttackDelayCooldown()
    {
        yield return new WaitForSeconds(delay);
        AttackCooldown = false;
    }
}
