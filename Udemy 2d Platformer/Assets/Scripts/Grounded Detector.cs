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
    [Space(5)]
    [SerializeField] private Transform _frontWallCheckPoint;
    [SerializeField] private Transform _backWallCheckPoint;
    [SerializeField] private Vector2 _wallCheckSize = new Vector2(0.5f, 1f);
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
        RaycastHit2D hit = Physics2D.BoxCast(AgentCollider.bounds.center +
            new Vector3(boxCastXOffSet, boxCastYOffSet, 0), BoxSize, 0, Vector2.down, 0, groundMask);
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

    public bool CheckLeftWall()
    {
        if (((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, groundMask) && !player.IsFacingRight)
                    || (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, groundMask) && player.IsFacingRight)) && !player.IsWallJumping)
            return true;
        return false;
    }

    public bool CheckRightWall()
    {
        if (((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, groundMask) && player.IsFacingRight)
                    || (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, groundMask) && !player.IsFacingRight)) && !player.IsWallJumping)
            return true;
        return false;
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
