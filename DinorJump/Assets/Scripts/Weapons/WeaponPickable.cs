using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;
using static UnityEditor.Progress;

namespace SVS.Pickable
{
    public class WeaponPickable: Pickable
    {
        public WeaponData WeaponData;
        private void Start()
        {
            GetComponentInChildren<SpriteRenderer>().sprite = WeaponData.weaponSprite;
        }
        public override void PickUp(Agent agent)
        {
            agent.PickUp(WeaponData);
        }

        
    }
}