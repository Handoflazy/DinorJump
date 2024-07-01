using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFallingObjects : MonoBehaviour
{
    public LayerMask objectToDestroyLayerMask;
    public Vector2 size;

    [Header("Gizmoz Parameter")]

    public Color gizmozColor = Color.red;

    public bool showGizmoz = true;

    private void FixedUpdate()
    {
        Collider2D collider = Physics2D.OverlapBox(transform.position, size, 0, objectToDestroyLayerMask);
        if (collider)
        {
            Player player = collider.GetComponent<Player>();
            if(player == null) {
                Destroy(collider.gameObject);
                return;
            }
            var damagable = player.GetComponent<Damageable>();
            if(damagable != null)
            {
                damagable.GetHit(1);
            }
            player.AgentDied();
        }    
    }
    private void OnDrawGizmos()
    {
        if(showGizmoz)
        {
            Gizmos.color = gizmozColor;
            Gizmos.DrawCube(transform.position, size);
        }
    }
}
