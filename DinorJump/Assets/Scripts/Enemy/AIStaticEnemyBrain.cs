using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS.AI
{
    public class AIStaticEnemyBrain : AIEnemy
    {
        public AIBehavior AttackBehaviour;
        // Update is called once per frame
        void Update()
        {

            if (AttackBehaviour)
            {
                AttackBehaviour.PerformAction(this);
            }



        }
    }
}