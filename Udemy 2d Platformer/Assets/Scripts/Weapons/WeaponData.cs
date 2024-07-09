using System;
using UnityEngine;

namespace WeaponSystem
{
    public abstract class WeaponData : ScriptableObject, IEquatable<WeaponData>
    {
        public int ID;
        public string WeaponName;
        public int weaponDamage = 1;
        public Sprite weaponSprite;
        public AudioClip weaponSwingSound;

        public bool Equals(WeaponData other)
        {
            if (other.WeaponName != WeaponName) return false;
            return true;
        }

        public abstract bool CanBeUsed(bool isGrounded);
        public abstract void PerformAttack(Agent player, LayerMask hitableMask, Vector3 direction);

        public virtual void DrawWeaponGizmo(Vector3 origin, Vector3 direction)
        {

        }

    }
}