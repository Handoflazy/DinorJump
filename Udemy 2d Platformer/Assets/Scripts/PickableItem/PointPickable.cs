using SVS.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS.Pickable
{
    public class PointPickable : Pickable
    {
        [SerializeField]
        private int points =1;
        public override void PickUp(Agent agent)
        {
            if (agent.TryGetComponent(out PlayerPoints PP))
            {
                PP.Add(points);
            }
        }
    }
}