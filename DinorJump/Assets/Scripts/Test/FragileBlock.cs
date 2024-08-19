using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileBlock : MonoBehaviour,IHittable
{
    public int hitPoint = 100;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void GetHit(GameObject gameObject, int weaponDamage)
    {
        hitPoint -= weaponDamage;
        if(hitPoint < 0)
        {
            hitPoint = 0;
            Destroy();
        }
    }

    private void Destroy()
    {
        animator.Play(AnimConsts.PLAYER_DEATH_PARAM);
    }
    public void Destructible()
    {
        Destroy(this.gameObject);
    }
  
}
