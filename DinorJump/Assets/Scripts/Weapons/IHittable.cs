using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    void GetHit(GameObject gameObject, int weaponDamage);
}
