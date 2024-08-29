using SVS.AI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class AIBehaviorPatrolPath : AIBehavior
{
    [SerializeField]
    private PatrolPath patrolPath;
    [SerializeField]
    [Range(0.1f, 1)]
    private float arriveDistace = 1f;

    [SerializeField]
    private float awaitTime = .5f;
    [SerializeField]
    private bool isWaiting = false;
    [SerializeField]
    private Vector2 currentPatrolTarget = Vector2.zero;
    private bool isInitailized = false;
    [SerializeField]
    private Transform agent;

    [SerializeField]
    private int currentIndex = -1;
    public override bool PerformAction(AIEnemy enemyAI)
    {
        if(!isWaiting)
        {
            if (patrolPath.Length < 2)
                return false;
            if(!isInitailized)
            {
                var currentPathPoint = patrolPath.GetClosestPathPoint(agent.position);
                this.currentIndex = currentPathPoint.Index;
                this.currentPatrolTarget = currentPathPoint.position;
                isInitailized = true;
            }
            if (Vector2.Distance(agent.position, currentPatrolTarget) < arriveDistace)
            {
                isWaiting = true;
                enemyAI.MovementVector = Vector2.zero;
                enemyAI.CallOnMovement(Vector2.zero);
                StartCoroutine(WaitCoroutine());
                return true;
            }
            var directionToGo = currentPatrolTarget - (Vector2)agent.position;
            enemyAI.CallOnMovement(directionToGo.normalized);
            enemyAI.MovementVector = directionToGo.normalized;
       
        }
        return true;
    }
    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(awaitTime);
        var nextPathPoint = patrolPath.GetNextPathPoint(currentIndex);
        currentPatrolTarget = nextPathPoint.position;
        currentIndex = nextPathPoint.Index;
        isWaiting = false;
    }
}



