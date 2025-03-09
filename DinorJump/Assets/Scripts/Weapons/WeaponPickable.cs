using UnityEngine;
using WeaponSystem;

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