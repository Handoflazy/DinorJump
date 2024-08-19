using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS.Pickable
{
    public class HealthPickable : Pickable
    {
        // Start is called before the first frame update
        [SerializeField]
        private int healtBoost = 1;

        public override void PickUp(Agent agent)
        {
            if(agent.TryGetComponent<Damageable>(out  var damageable))
            {
                damageable.GetHeal(healtBoost);
            }
        }
    }
}