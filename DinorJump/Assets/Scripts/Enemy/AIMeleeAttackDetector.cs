using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIMeleeAttackDetector : MonoBehaviour
{
    public bool PlayerDetected { get; private set; }

    public LayerMask playerMask;
    public UnityEvent<GameObject> OnPlayerDetected;

    [Header("Gizmoz parameters")]
    [Range(0, 1)]
    public float radius;
    public Color gizmozColor = Color.green;
    public bool showGizmoz;
    void Update()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius,playerMask);
        PlayerDetected = hit!=null;
        if (PlayerDetected)
        {
            OnPlayerDetected?.Invoke(hit.gameObject);
            
        }

    }
    private void OnDrawGizmos()
    {
        if (showGizmoz)
        {
            Gizmos.color = gizmozColor;
            Gizmos.DrawWireSphere(transform.position,radius);
        }
    }
}
