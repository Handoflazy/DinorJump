using SVS;
using SVS.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrollingEnemyBrain : AIEnemy
{
  
    public bool CanMove = false;
    public AIBehavior AttackBehavior, PatrolBehavior;
    public GroundedDetector groundedDetector;


    // Update is called once per frame
    void Update()
    {
        if (groundedDetector.IsGrounded)
        {
            if (AttackBehavior && AttackBehavior.PerformAction(this))
                return;
            if(PatrolBehavior)
                PatrolBehavior.PerformAction(this);
        }
    }
}
