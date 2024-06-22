using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GroundedDetector : PlayerSystem
{

    public Collider2D AgentCollider;
    [Header("Gizmoz parameters: ")]
    [Range(-2f, 2f)]
    public float boxCastYOffSet = 0.1f;
    [Range(-2f, 2f)]
    public float boxCastXOffSet = 0.1f;
    [SerializeField]
    private Vector2 BoxSize;
    [SerializeField]
    private LayerMask groundMask;
    public Color gizmozColorNotGrounded = Color.red, gizmozColorIsGrounded = Color.green;
    [SerializeField]
    private bool isGrounded = false;

    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }

    protected override void Awake()
    {
        base.Awake();
        if(AgentCollider == null)
        {
            AgentCollider = GetComponent<Collider2D>();
        }
    }

    public void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(AgentCollider.bounds.center + new Vector3(boxCastXOffSet, boxCastYOffSet, 0), BoxSize, 0, Vector2.down, 0, groundMask);
        if (hit)
        {
           if(hit.collider.IsTouching(AgentCollider)==true)// có interaction được hay không?? tương tu oncollision Enter 
             isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void OnDrawGizmos()
    {

            if (AgentCollider == null)
                return;
            Gizmos.color = gizmozColorNotGrounded;
            if (isGrounded == true)
            {
                Gizmos.color = gizmozColorIsGrounded;
            }
            Gizmos.DrawWireCube(AgentCollider.bounds.center + new Vector3(boxCastXOffSet, boxCastYOffSet, 0), BoxSize);
        
    }

}
