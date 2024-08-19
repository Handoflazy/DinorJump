using SVS.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIBehavior : MonoBehaviour
{
    public abstract bool PerformAction(AIEnemy enemyAI);
   
}
