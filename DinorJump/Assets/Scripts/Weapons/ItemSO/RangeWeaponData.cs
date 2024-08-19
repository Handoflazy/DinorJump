using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "Defalt Range Weapon", menuName = "InventorySystem/Item/Weapon/Range")]
    public class RangeWeaponData : WeaponData
    {
        public GameObject rangeWeaponPrefab;
        public float weaponThrowSpeed = 1;
        public float attackRange = 2;
         

        public override bool CanBeUsed(bool isGrounded)
        {
            return true;
        }

        public override void PerformAttack(Agent player, LayerMask hitableMask, Vector3 direction)
        {
            player.agentWeapon.ToggleWeaponVisiblity(false);
            GameObject gameObject = Instantiate(rangeWeaponPrefab,player.agentWeapon.transform.position,Quaternion.identity);
            gameObject.GetComponent<ThrowableWeapon>().Initialized(this, direction, hitableMask);
        }
    }
}