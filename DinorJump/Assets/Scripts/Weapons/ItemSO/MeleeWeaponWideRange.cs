using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "Defalt Melee Weapon", menuName = "InventorySystem/Item/Weapon/MeleeWideRange")]
    public class MeleeWeaponWideRange : WeaponData
    {
        public float attackRange = 2;
        public override bool CanBeUsed(bool isGrounded)
        {
            if (isGrounded)
            {
                return true;
            }
            return false;
        }

        public override void PerformAttack(Agent player, LayerMask hitableMask, Vector3 direction)
        {
            Collider2D hit = Physics2D.OverlapCircle(player.agentWeapon.transform.position,attackRange, hitableMask);
            if (hit)
            {

                foreach (var hittable in hit.GetComponents<IHittable>())
                {
                    hittable.GetHit(player.gameObject, weaponDamage);
                }
            }
        }
        public override void DrawWeaponGizmo(Vector3 origin, Vector3 direction)
        {
            Gizmos.DrawWireSphere(origin, attackRange);
        }

    }
}