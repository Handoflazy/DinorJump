using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingDetector : MonoBehaviour
{
    public LayerMask climbingLayerMask;

    [SerializeField,ReadOnlyInspector]
    private bool canClimb;

    public bool CanClimb { get => canClimb; private set => canClimb = value; }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((climbingLayerMask & (1 << collision.gameObject.layer)) != 0){
            canClimb = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((climbingLayerMask & (1 << collision.gameObject.layer)) != 0)
        {
            canClimb = false;
        }
    }
}
