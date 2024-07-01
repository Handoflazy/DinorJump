using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.UI.Image;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "Defalt Melee Weapon", menuName = "InventorySystem/Item/Weapon/Melee")]
    public class MeleeWeaponData : WeaponData
    {
        public float attackRange = 2;
        public override bool CanBeUsed(bool isGrounded)
        {
            if(isGrounded)
            {
                return true;
            }
            return false;
        }

        public override void PerformAttack(Player player, LayerMask hitableMask, Vector3 direction)
        {
            
            RaycastHit2D hit = Physics2D.Raycast(player.agentWeapon.transform.position,direction,attackRange,hitableMask);
            if (hit.collider)
            {
                foreach (var hittable in hit.collider.GetComponents<IHittable>())
                {
                    hittable.GetHit(player.gameObject, weaponDamage);
                }
            }
        }
        public override void DrawWeaponGizmo(Vector3 origin, Vector3 direction)
        {
            Gizmos.DrawLine(origin, origin + direction * attackRange);
        }
    } 
}