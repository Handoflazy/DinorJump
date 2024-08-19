using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS.Feedback
{
    public class KnockbackFeedback : AgentSystem, IHittable
    {
        [SerializeField]
        private float knockForce = 10;

        public void GetHit(GameObject opponent, int weaponDamage)
        {
            Vector2 dir = new Vector2(gameObject.transform.position.x - opponent.transform.position.x, 0).normalized;
            gameObject.GetComponent<Rigidbody2D>().AddForce(dir * knockForce, ForceMode2D.Impulse);

        }


    }
}